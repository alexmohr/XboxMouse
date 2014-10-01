using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controller.Abstraction.Events;
using Microsoft.Xna.Framework.Input;

namespace Controller.Abstraction.Hardware
{
   public class Trigger
    {
        public EventHandler<TriggerChangedEvent> TriggerChanged;
        public EventHandler<TriggerChangedEvent> TriggerChangedRounded;
       private int _value;
       private double _realValue;
       public HardwareAbstraction.TriggerName Name
        {
            get;
            set;
        }

       public double RealValue
       {
           get { return _realValue; }
           set
           {
               if (Math.Abs(value) < 0.01) return;

               _realValue = value; 
               RaiseChangedEvent(TriggerChanged, value);
           }
       }

       public int Value
       {
           get { return _value; }
           set
           {
               if (_value == 0) return;
               
               _value = value;
               RaiseChangedEvent(TriggerChangedRounded, value);
           }
       }

       private void RaiseChangedEvent(EventHandler<TriggerChangedEvent> changedEvent, double value)
       {


           if (changedEvent != null)
           {
               changedEvent(this, new TriggerChangedEvent(this, value));
           }
       }
    }
}
