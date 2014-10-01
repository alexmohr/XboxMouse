using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controller.Abstraction.Hardware;

namespace Controller.Abstraction.Events
{
    public class TriggerChangedEvent : EventArgs
    {
        private readonly double _value;
        private readonly Trigger _trigger;

        public TriggerChangedEvent(Trigger trigger, double value)
        {
            _trigger = trigger;
            _value = value;
        }

        public Trigger Trigger
        {
            get { return _trigger; }
        }

        public double Value
        {
            get { return _value; }
        }
    }
}
