using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Controller
{
    /// <summary>
    /// Provides abstraction for the control. 
    /// Used to provide easy configuration over the key mappings.
    /// </summary>
    public class HardwareAbstraction
    {
        /// <summary>
        /// Enum for thumb sticks.
        /// </summary>
        public enum ThumbSticks
        {
            Left,
            Right
        }

        /// <summary>
        /// The gamepad buttons.
        /// </summary>
        public enum Buttons
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

        public enum ModifactorButtons
        {
            RightShoulder, 
            LeftShoulder,
            RightAndLeftShoulder,
        }

        /// <summary>
        /// Gets or sets the game pad identifier.
        /// </summary>
        /// <value>
        /// The game pad identifier.
        /// </value>
        public PlayerIndex GamePadId { get; set; }

        /// <summary>
        /// Gets or sets the mouse stick.
        /// </summary>
        /// <value>
        /// The mouse stick.
        /// </value>
        public ThumbSticks MouseStick { get; set; }


  

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
