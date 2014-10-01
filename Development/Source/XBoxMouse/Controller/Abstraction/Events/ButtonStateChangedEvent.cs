using System;
using Controller.Abstraction.Hardware;
using Microsoft.Xna.Framework.Input;

namespace Controller.Abstraction.Events
{
    public class ButtonStateChangedEvent : EventArgs

    {
        public ButtonStateChangedEvent(ButtonState state, Button button)
        {
            State = state;
            Button = button;
        }

        public ButtonState State { get; private set; }

        public Button Button { get; private set; }
    }
}
