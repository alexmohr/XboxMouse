using System.Linq;
using System.Threading;

namespace Controller.Abstraction.Listener
{
    public class ButtonListener : IStoppableListener
    {
        

        /// <summary>
        ///     The mouse emulation
        /// </summary>
        private Thread _keyListening;

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
            _keyListening = new Thread(KeyListening);
            _keyListening.Name = "KeyListening";
            _keyListening.IsBackground = true;
            _keyListening.Start();
            _stop = false;
        }

        /// <summary>
        ///     Stops the listener.
        /// </summary>
        public void Stop()
        {
            _stop = true;
            _keyListening.Join(500);
        }

        private void KeyListening()
        {
            while (!_stop)
            {
                SetKeyStates();
                Thread.Sleep(50);
            }
        }


        /// <summary>
        ///     Gets all pressed key.
        /// </summary>
        /// <returns>Collection with all buttons</returns>
        /// <remarks>
        ///     Can this be done better?
        /// </remarks>
        private void SetKeyStates()
        {
            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.A).State =
                Hardware.UsedGamePad.Buttons.A;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.B).State =
                Hardware.UsedGamePad.Buttons.B;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.Back).State =
                Hardware.UsedGamePad.Buttons.Back;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.BigButton).State =
                Hardware.UsedGamePad.Buttons.BigButton;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.LeftShoulder).State =
                Hardware.UsedGamePad.Buttons.LeftShoulder;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.LeftStick).State =
                Hardware.UsedGamePad.Buttons.A;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.RightShoulder).State =
                Hardware.UsedGamePad.Buttons.RightShoulder;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.RightStick).State =
                Hardware.UsedGamePad.Buttons.RightStick;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.Start).State =
                Hardware.UsedGamePad.Buttons.Start;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.X).State =
                Hardware.UsedGamePad.Buttons.X;

            Hardware.Buttons.First(b => b.Name == HardwareAbstraction.ButtonNames.Y).State =
                Hardware.UsedGamePad.Buttons.Y;
        }
    }
}