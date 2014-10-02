using System;
using System.Collections.Generic;
using System.Drawing;
using Controller.Abstraction.Events;
using Microsoft.Xna.Framework.Input;

namespace Controller.Abstraction.Hardware
{
    public class ThumbStick
    {
        public HardwareAbstraction.ThumbStickName Name { get; set; }

        private KeyValuePair<int, int> _value;
        private KeyValuePair<double, double> _realValue;

        public EventHandler<StickStateChangedEvent> StickChangedRounded;
        public EventHandler<StickStateChangedEvent> StickChanged;



        internal KeyValuePair<int, int> Value 
        {
            get { return _value; }
            set
            {
                _value = value; 
                RaiseChangedEvent(StickChangedRounded, _value.Key, _value.Value);
            }
        }

        internal KeyValuePair<double, double> RealValue
        {
            get { return _realValue; }
            set
            {
                _realValue = value;
                RaiseChangedEvent(StickChanged, _realValue.Key, _realValue.Value);
            }
        }

        private void RaiseChangedEvent(EventHandler<StickStateChangedEvent> changedEvent, double x, double y)
        {
          

            if (changedEvent != null)
            {
                changedEvent(this, new StickStateChangedEvent(this, x, y));
            }
        }
    }
}
