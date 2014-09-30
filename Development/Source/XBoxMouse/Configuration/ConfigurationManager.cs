using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Actions;
using Controller;
using Controller.Abstraction;
using Controller.Abstraction.Hardware;
using Controller.Abstraction.Listener;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XamlReader = System.Windows.Markup.XamlReader;

namespace Configuration
{
    public class ConfigurationManager
    {
        private SystemConfiguration _config;

        public void ReadConfig(string fileName)
        {
          // WriteDebugConfiguration();
            _config = (SystemConfiguration) XamlServices.Load(fileName);
            InitSystem();
            
        }

        private void InitSystem()
        {
          SetKeyBindinding();
            SetStickBinding();
            
        }

        private void SetKeyBindinding()
        {
           if (_config.KeyBindings == null)
               return;
            
            KeyBinding[] keyBindings = new KeyBinding[_config.KeyBindings.Length];
            for (int i = 0; i < keyBindings.Length; i++)
            {
                Button[] buttons =
                    _config.KeyBindings[i].Buttons.Select(b => _config.Hardware.Buttons.First(h => h.Name == b.Name)).ToArray();

                keyBindings[i] = new KeyBinding
                {
                    Buttons = buttons,
                    PressAction = _config.KeyBindings[i].PressAction,
                    ReleaseAction = _config.KeyBindings[i].ReleaseAction,
                };
            }
        }

        private void SetStickBinding()
        {
            if (_config.StickBindings == null)
                return;
            StickBinding[] stickBindings = new StickBinding[_config.StickBindings.Length];
            for (int i = 0; i < stickBindings.Length; i++)
            {
                stickBindings[i] = new StickBinding
                {
                    MoveAction = _config.StickBindings[i].MoveAction,
                    MoveActionRounded = _config.StickBindings[i].MoveActionRounded,
                    Stick = _config.Hardware.ThumbSticks.First(s => s.Name == _config.StickBindings[i].Stick.Name)
                };
            }
        }

        private void WriteDebugConfiguration()
        {
            HardwareAbstraction hardware = new HardwareAbstraction
            {
                GamePadId = PlayerIndex.One
            };

            StickBinding[] sticksBindings = new StickBinding[1];

            sticksBindings[0] = new StickBinding
           {
               Stick = hardware.ThumbSticks.First(x => x.Name == HardwareAbstraction.ThumbStickName.Left),
               MoveActionRounded = ActionProvider.Action.MouseMove
           };

            StickListener stickListener = new StickListener { Hardware = hardware };
            ButtonListener buttonListener = new ButtonListener { Hardware = hardware };



            KeyBinding aButton = new KeyBinding
            {
                Buttons = new[]
                    {
                        hardware.Buttons.First(x => x.Name == HardwareAbstraction.ButtonNames.A),
                    },
                PressAction = ActionProvider.Action.LeftMouseDown,
                ReleaseAction = ActionProvider.Action.LeftMouseUp,
            };
            KeyBinding[] bindings = { aButton };



            SystemConfiguration cfg = new SystemConfiguration
            {
                Hardware = hardware,
                KeyBindings = bindings,
                StickBindings = sticksBindings
            };

            string t = "default.xaml";
            using (TextWriter writer = File.CreateText(t))
            {
                XamlServices.Save(writer, cfg);
            }


            stickListener.Start();
            buttonListener.Start();
        }

        public SystemConfiguration Configuration
        {
            get { return _config; }
        }

     
    }
}
