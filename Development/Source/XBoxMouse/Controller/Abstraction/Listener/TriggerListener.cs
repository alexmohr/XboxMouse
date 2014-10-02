using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Controller.Abstraction.Events;
using Controller.Abstraction.Hardware;

namespace Controller.Abstraction.Listener
{
    public class TriggerListener : IStoppableListener
    {
        /// <summary>
        ///     The mouse emulation
        /// </summary>
        private Thread _rightTrigger;
        private Thread _leftTrigger;

    

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
            _rightTrigger = new Thread(TriggerListening) {Name = "RightTriggerListener"};
            _leftTrigger = new Thread(TriggerListening) {Name = "LeftTriggerListener"};

            _rightTrigger.Start(HardwareAbstraction.TriggerName.Right);
            _leftTrigger.Start(HardwareAbstraction.TriggerName.Left);
        }

        /// <summary>
        ///     Stops the listener.
        /// </summary>
        public void Stop()
        {
            _stop = true;
            _rightTrigger.Join(500);
            _leftTrigger.Join(500);
        }

        private void TriggerListening(object TriggerParam)
        {
            HardwareAbstraction.TriggerName trigger = (HardwareAbstraction.TriggerName)TriggerParam;
            while (!_stop)
            {
                GetValues(trigger);
            }
        }

        private void GetValues( HardwareAbstraction.TriggerName trigger)
        {
            // Get the position of the selected Trigger.
            float value = trigger == HardwareAbstraction.TriggerName.Left ? Hardware.UsedGamePad.Triggers.Left : Hardware.UsedGamePad.Triggers.Right;

            // Gets the Trigger value.
            int roundedValue = 0;
            if (Math.Abs(value) > 0.01)
            {
                if (value > 0)
                {
                    roundedValue = 1;
                }
            }


            int sleep = GetSleepTime(value);

            Trigger thumbTrigger = Hardware.Triggers.First(s => s.Name == trigger);
            thumbTrigger.RealValue = value;
            thumbTrigger.Value = roundedValue;

            Thread.Sleep(sleep);

        }

      
        private int GetSleepTime(float value)
        {
            const int minSleep = 70;
            const int maxSleep = 250;
            
            if ((int)value == 1)
            {
                return minSleep;
            }


            int sleepTime = (int)(Math.Round(value * 10000, 3));
            
            if (sleepTime < 0)
                sleepTime *= -1;
            
            if (sleepTime == 0)
                sleepTime = minSleep;

            if (sleepTime > maxSleep)
                sleepTime = maxSleep;


            return sleepTime;
        }
    }
}
