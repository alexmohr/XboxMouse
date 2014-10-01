#region

using Actions;
using Controller.Abstraction.Hardware;

#endregion

namespace Controller.Bindings
{
    public class TriggerBinding
    {
        private readonly TriggerActions _actions = new TriggerActions();
        private ActionProvider.Action? _moveAction;
        private ActionProvider.Action? _moveActionRounded;
        private Trigger _trigger;

        public Trigger Trigger
        {
            get { return _trigger; }
            set
            {
                _trigger = value;
                MoveAction = _moveAction;
                MoveActionRounded = _moveActionRounded;
            }
        }

        public ActionProvider.Action? MoveAction
        {
            get { return _moveAction; }
            set
            {
                if (value == null) return;

                _moveAction = value;

                if (Trigger == null) return;
                if (_moveAction == ActionProvider.Action.None) return;


                Trigger.TriggerChanged +=
                    (sender, @event) =>
                        ActionProvider.GetTriggerAction((ActionProvider.Action) value).Invoke(_actions, null);
            }
        }

        public ActionProvider.Action? MoveActionRounded
        {
            get { return _moveActionRounded; }
            set
            {
                if (value == null) return;
                _moveActionRounded = value;

                if (Trigger == null) return;
                if (_moveActionRounded == ActionProvider.Action.None) return;


                Trigger.TriggerChangedRounded +=
                    (sender, @event) =>
                        ActionProvider.GetStickAction((ActionProvider.Action) value)
                            .Invoke(_actions, null);
                _moveActionRounded = value;
            }
        }
    }
}