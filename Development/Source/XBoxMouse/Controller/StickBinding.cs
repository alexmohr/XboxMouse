using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Actions;
using Controller.Abstraction;
using Controller.Abstraction.Hardware;
using Controller.Abstraction.Listener;

namespace Controller
{
    public class StickBinding
    {
        public ThumbStick Stick
        {
            get { return _stick; }
            set
            {
                _stick = value;
                MoveAction = _moveAction;
                MoveActionRounded = _moveActionRounded;
            }
        }


        private readonly StickActions _actions = new StickActions();
        private ActionProvider.Action? _moveAction;
        private ActionProvider.Action? _moveActionRounded;
        private ThumbStick _stick;

        public ActionProvider.Action? MoveAction
        {
            get { return _moveAction; }
            set
            {
                if (value == null) return;
                
                _moveAction = value;
                
                if (Stick == null) return;
                if (_moveAction == ActionProvider.Action.None) return;
                

                Stick.StickChanged += (sender, @event) => ActionProvider.GetStickAction((ActionProvider.Action)value).Invoke(_actions, null);
                
            }
        }

        public ActionProvider.Action? MoveActionRounded
        {
            get { return _moveActionRounded; }
            set
            {
                if (value == null) return;
                _moveActionRounded = value;
                
                if (Stick == null) return;
                if (_moveActionRounded == ActionProvider.Action.None) return;
                
                
                Stick.StickChangedRounded += (sender, @event) => ActionProvider.GetStickAction((ActionProvider.Action)value).Invoke(_actions, new object[] { @event.X, @event.Y });
                _moveActionRounded = value;
            }
        }
    }
}
