using System;
using System.Diagnostics;
using Controller.Abstraction.Hardware;
using Controller.Abstraction.Listener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Controller.Abstraction
{
    /// <summary>
    /// Provides abstraction for the control. 
    /// Used to provide easy configuration over the key mappings.
    /// </summary>
    public class HardwareAbstraction
    {
        private readonly Button[] _buttons;
        private readonly ThumbStick[] _thumbSticks;
        private readonly Trigger[] _triggers;


        private readonly StickListener _stickListener = new StickListener();
        private readonly ButtonListener _buttonListener = new ButtonListener();
        private readonly TriggerListener _triggerListener = new TriggerListener();

        public HardwareAbstraction ()
        {
            try
            {
                string[] buttonNames = Enum.GetNames(typeof (ButtonNames));
                _buttons = new Button[buttonNames.Length];

                for (int i = 0; i < buttonNames.Length; i++)
                {
                    _buttons[i] = new Button
                    {
                        Name = (ButtonNames) Enum.Parse(typeof (ButtonNames), buttonNames[i])
                    };
                }
                _thumbSticks = new ThumbStick[2];
                _thumbSticks[0] = new ThumbStick {Name = ThumbStickName.Left};
                _thumbSticks[1] = new ThumbStick {Name = ThumbStickName.Right};


                _triggers = new Trigger[2];
                _triggers[0] = new Trigger { Name = TriggerName.Left };
                _triggers[1] = new Trigger { Name = TriggerName.Right };

                _buttonListener.Hardware = this;
                _stickListener.Hardware = this;
                _triggerListener.Hardware = this; 

                _buttonListener.Start();
                _stickListener.Start();
                _triggerListener.Start();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
        
        /// <summary>
        /// Enum for thumb sticks.
        /// </summary>
        public enum ThumbStickName
        {
            Left,
            Right
        }

        /// <summary>
        /// Enum for thumb sticks.
        /// </summary>
        public enum TriggerName
        {
            Left,
            Right
        }

        public Trigger[] Triggers
        {
            get { return _triggers; }
        }



        public ThumbStick[] ThumbSticks
        {
            get { return _thumbSticks; }
        }

        public Button[] Buttons
        {
            get { return _buttons; }
        }

        /// <summary>
        /// The gamepad buttons.
        /// </summary>
        public enum ButtonNames
        {
            A, 
            B, 
            X, 
            Y,
            Back, 
            Start,
            BigButton,
            LeftShoulder,
            LeftStick,
            RightShoulder,
            RightStick
        }


        /// <summary>
        /// Gets or sets the game pad identifier.
        /// </summary>
        /// <value>
        /// The game pad identifier.
        /// </value>
        public PlayerIndex GamePadId { get; set; }


        /// <summary>
        /// Gets the used game pad.
        /// </summary>
        /// <value>
        /// The used game pad.
        /// </value>
        public GamePadState UsedGamePad
        {
            get { return GamePad.GetState(GamePadId); }
        }

        //public 
    }
}
