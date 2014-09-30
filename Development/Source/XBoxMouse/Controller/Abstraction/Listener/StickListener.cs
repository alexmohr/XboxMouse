using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Controller.Abstraction.Events;
using Controller.Abstraction.Hardware;

namespace Controller.Abstraction.Listener
{
    public class StickListener : IStoppableListener
    {
        /// <summary>
        ///     The mouse emulation
        /// </summary>
        private Thread _rightStick;
        private Thread _leftStick;

    

        /// <summary>
        ///     Tells the thread to stop.
        /// </summary>
        private bool _stop;

        public HardwareAbstraction Hardware { get; set; }

        /// <summary>
        ///     Starts the listener.
        /// </summary>
        public void Start()
        {
            _rightStick = new Thread(StickListening);
            _rightStick.Name = "RightStickListener";
            _leftStick = new Thread(StickListening);
            _leftStick.Name = "LeftStickListener";

            _rightStick.Start(HardwareAbstraction.ThumbStickName.Left);
            _leftStick.Start(HardwareAbstraction.ThumbStickName.Left);
        }

        /// <summary>
        ///     Stops the listener.
        /// </summary>
        public void Stop()
        {
            _stop = true;
            _rightStick.Join(500);
            _leftStick.Join(500);
        }

        private void StickListening(object stickParam)
        {
            HardwareAbstraction.ThumbStickName stick = (HardwareAbstraction.ThumbStickName)stickParam;
            while (!_stop)
            {
                GetValues(stick);
            }
        }

        private void GetValues( HardwareAbstraction.ThumbStickName stick)
        {
            float stickX, stickY;

            // Get the position of the selected stick.
            if (stick == HardwareAbstraction.ThumbStickName.Left)
            {
                stickX = Hardware.UsedGamePad.ThumbSticks.Left.X;
                stickY = Hardware.UsedGamePad.ThumbSticks.Left.Y;
            }
            else
            {
                stickX = Hardware.UsedGamePad.ThumbSticks.Right.X;
                stickY = Hardware.UsedGamePad.ThumbSticks.Right.Y;
            }

            // Gets the stick value.
            int x = 0, y = 0;
            if (Math.Abs(stickY) > 0.01)
            {
                if (stickY > 0)
                {
                    y = 1;
                }
                else
                {
                    y = -1;
                    stickY *= -1;
                }
            }
            if (Math.Abs(stickX) > 0.01)
            {
                if (stickX > 0)
                {
                    x = 1;
                }
                else
                {
                    x = -1;
                    stickX *= -1;
                }
            }

            int sleep = GetSleepTime(stickX, stickY);

            ThumbStick thumbStick = Hardware.ThumbSticks.First(s => s.Name == stick);
            thumbStick.RealValue = new KeyValuePair<double, double>(stickX, stickY);
            thumbStick.Value = new KeyValuePair<int, int>(x, y);

            Thread.Sleep(sleep);

        }

        /// <summary>
        /// Gets the sleep time.
        /// </summary>
        /// <param name="x">The x value of the stick.</param>
        /// <param name="y">The y value of the stick.</param>
        /// <returns>How long the caller should sleep. Used to slow down the mouse movement on less trigger</returns>
        private int GetSleepTime(float x, float y)
        {
            // if one stick is maxed out sleep short.
            if ((int)x == 1 || (int)y == 1)
            {
                return 1;
            }

            double median = (x + y) / 2;
            if (Math.Abs(x) < 0.001)
            {
                median = y;
            }
            if (Math.Abs(y) < 0.001)
            {
                median = x;
            }

            if (median < 0.01)
            {
                return 1;
            }

            // Debugging for this method.
            //Debug.WriteLine("Median: " + median + "; X: " + x + "; Y" + y);

            // We seem to move diagonally.
            // This must be handled extra or diagonal would be slow if user does not want it!
            if (x > 0.45 && y > 0.45)
            {
                return 1;
            }

            int sleepTime = (int)Math.Round(median * 10, 3);
            sleepTime = 10 - sleepTime;

            return sleepTime == 0 ? sleepTime + 1 : sleepTime;
        }
    }
}
