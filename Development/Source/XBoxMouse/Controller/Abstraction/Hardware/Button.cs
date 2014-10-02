using System;
using Controller.Abstraction.Events;
using Microsoft.Xna.Framework.Input;

namespace Controller.Abstraction.Hardware
{
    public class Button
    {
        public EventHandler<ButtonStateChangedEvent> ButtonReleased;
        public EventHandler<ButtonStateChangedEvent> ButtonPressed;
        
        public HardwareAbstraction.ButtonNames Name
        {
            get; set;
        }

        private ButtonState _state;
        public ButtonState State
        {
            set
            {
                if (_state == value) return;
                _state = value; 
                RaiseChangedEvent();
            }
            get { return _state; }
        }

        private void RaiseChangedEvent()
        {
            EventHandler<ButtonStateChangedEvent> changedEvent = _state == ButtonState.Pressed ? ButtonPressed : ButtonReleased;

            if (changedEvent != null)
            {
                changedEvent(this, new ButtonStateChangedEvent(_state, this));
            }
        }
    }
}
