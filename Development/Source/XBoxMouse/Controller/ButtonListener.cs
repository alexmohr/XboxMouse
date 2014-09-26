

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace Controller
{
  public  class ButtonListener : IStoppableListener
    {
        public HardwareAbstraction Hardware { get; set; }

        /// <summary>
        /// Tells the thread to stop.
        /// </summary>
        private bool _stop;

        /// <summary>
        /// The mouse emulation
        /// </summary>
        private Thread _keyListening;

      public List<KeyBinding> KeyBindings { get; set; }

        /// <summary>
        /// Starts the listener.
        /// </summary>
        public void Start()
        {
            _keyListening = new Thread(KeyListening);
            _keyListening.Start();
            _stop = false;
        }

        /// <summary>
        /// Stops the listener.
        /// </summary>
        public void Stop()
        {
            _stop = true;
            _keyListening.Join(500);
        }

      private void KeyListening()
      {
          while (!_stop)
          {
              List<HardwareAbstraction.Buttons> allPressedKey = GetKeyStates(ButtonState.Pressed);
              List<HardwareAbstraction.Buttons> allReleasedKeys = GetKeyStates(ButtonState.Pressed);

              foreach (KeyBinding binding in KeyBindings)
              {
                  if (binding.NeedsReset && !binding.Reseted)
                  {
                      KeyBinding localBinding = binding;
                      foreach (var needed in binding.NeededButtons.Where(needed => allReleasedKeys.Contains(localBinding.Key)))
                      {
                          binding.Reseted = true;
                      }
                  }

                  bool run = true;
                  
                  foreach (HardwareAbstraction.Buttons b in binding.NeededButtons.Where(b => !allPressedKey.Contains(b)))
                  {
                      run = false;
                  }

                  if (run)
                  {
                      binding.Command();
                  }
                  
                  
              }

          }
      }



      /// <summary>
      /// Gets all pressed key.
      /// </summary>
      /// <returns>Collection with all buttons</returns>
      /// <remarks>
      /// Can this be done better?
      /// </remarks>
      private List<HardwareAbstraction.Buttons> GetKeyStates( ButtonState state)
      {
          List<HardwareAbstraction.Buttons> buttons = new List<HardwareAbstraction.Buttons>();

          // BUTTON A
          if (Hardware.UsedGamePad.Buttons.A == state)
          {
              buttons.Add(HardwareAbstraction.Buttons.A);
          }

          // BUTTON B
          if (Hardware.UsedGamePad.Buttons.B == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.B);
          } 
          
          // BUTTON BACK
          if (Hardware.UsedGamePad.Buttons.Back == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.Back);
          }
          

          // BUTTON BIG
          if (Hardware.UsedGamePad.Buttons.BigButton == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.BigButton);
          }

          // LeftShoulder
          if (Hardware.UsedGamePad.Buttons.LeftShoulder == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.LeftShoulder);
          } 
          
          if (Hardware.UsedGamePad.Buttons.LeftStick == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.LeftStick);
          }
          
          if (Hardware.UsedGamePad.Buttons.RightShoulder == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.RightShoulder);
          } 
          
          if (Hardware.UsedGamePad.Buttons.RightStick == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.RightStick);
          } 
          
          if (Hardware.UsedGamePad.Buttons.Start == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.Start);
          } 
          
          if (Hardware.UsedGamePad.Buttons.X == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.X);
          }
          
          if (Hardware.UsedGamePad.Buttons.Y == ButtonState.Pressed)
          {
              buttons.Add(HardwareAbstraction.Buttons.A);
          }
          
          return buttons; 
      }

    }
}
