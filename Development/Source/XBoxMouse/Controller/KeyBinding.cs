
using System;
using System.Linq;
using System.Reflection;
using Actions;
using Controller.Abstraction;
using Controller.Abstraction.Hardware;
using Microsoft.Xna.Framework.Input;

namespace Controller
{
  public class KeyBinding
    {
      private readonly ButtonActions _commandActions = new ButtonActions();

      public Button[] Buttons
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
      private Button[] _buttons;


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
