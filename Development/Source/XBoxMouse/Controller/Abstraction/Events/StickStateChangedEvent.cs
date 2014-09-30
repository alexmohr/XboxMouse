using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controller.Abstraction.Hardware;
using Controller.Abstraction.Listener;
using Microsoft.Xna.Framework.Input;

namespace Controller.Abstraction.Events
{
    public class StickStateChangedEvent : EventArgs
    {
        public StickStateChangedEvent(ThumbStick stick, double x, double y)
        {
            X = x;
            Y = y;
            Stick = stick;
        }

        public double X { get; private set; }
        public double Y { get; private set; }

        public ThumbStick Stick { get; private set; }

    }
}
