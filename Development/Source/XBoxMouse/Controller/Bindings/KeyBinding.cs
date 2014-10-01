using System.Linq;
using Actions;
using Microsoft.Xna.Framework.Input;
using Buttons = Controller.Abstraction.Hardware.Buttons;

namespace Controller.Bindings
{
  public class KeyBinding
    {
      private readonly ButtonActions _commandActions = new ButtonActions();

      public Buttons Buttons
      {
          get { return _buttons; }
          set
          {
              _buttons = value;
              PressAction = _pressAction;
              ReleaseAction = _releaseAction;
          }
      }

      private ActionProvider.Action? _pressAction;
      private ActionProvider.Action? _releaseAction;
      private Buttons _buttons;


      public ActionProvider.Action? PressAction
      {
          get
          {
                return _pressAction;
          }
          set
          {
              if (value == null) return;
              
              _pressAction = value;
              
              if (Buttons == null) return;
             
              
              Buttons[0].ButtonPressed += (sender, @event) =>
              {
                  if (Buttons.Any(b => b.State != ButtonState.Pressed))
                      return;

                  ActionProvider.GetButtonAction((ActionProvider.Action)value).Invoke(_commandActions, null);
              };
              
          }
      }
      public ActionProvider.Action? ReleaseAction
      {
          get { return _releaseAction; }
          set
          {
              if (value == null) return;

              _releaseAction = value;

              if (Buttons == null) return;

              Buttons[0].ButtonReleased += (sender, @event) =>
              {
                  if (Buttons.Any(b => b.State == ButtonState.Released))
                  {
                      ActionProvider.GetButtonAction((ActionProvider.Action)value).Invoke(_commandActions, null);    
                  }
              };
             
          }
      }

    }
}
