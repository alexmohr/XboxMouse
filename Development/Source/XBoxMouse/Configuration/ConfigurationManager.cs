using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using Actions;
using Configuration.Collections;
using Controller;
using Controller.Abstraction;
using Controller.Abstraction.Hardware;
using Controller.Abstraction.Listener;
using Controller.Bindings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Buttons = Controller.Abstraction.Hardware.Buttons;
using XamlReader = System.Windows.Markup.XamlReader;

namespace Configuration
{
    public class ConfigurationManager
    {
        private SystemConfiguration _config;

        public void ReadConfig(string fileName)
        {
         WriteDebugConfiguration();
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
            
            KeyBinding[] keyBindings = new KeyBinding[_config.KeyBindings.Count];
            for (int i = 0; i < keyBindings.Length; i++)
            {
               Buttons bt = new Buttons();
                bt.AddRange(_config.KeyBindings[i].Buttons.Select(b => _config.Hardware.Buttons.First(h => h.Name == b.Name)));


                keyBindings[i] = new KeyBinding
                {
                    Buttons = bt,
                    PressAction = _config.KeyBindings[i].PressAction,
                    ReleaseAction = _config.KeyBindings[i].ReleaseAction,
                };
            }
        }

        private void SetStickBinding()
        {
            if (_config.StickBindings == null)
                return;
            StickBindings stickBindings = new StickBindings();
            stickBindings.AddRange(_config.StickBindings.Select(t => new StickBinding
            {
                MoveAction = t.MoveAction, 
                MoveActionRounded = t.MoveActionRounded, 
                Stick = _config.Hardware.ThumbSticks.First(s => s.Name == t.Stick.Name)
            }));
        }

        private void WriteDebugConfiguration()
        {
            HardwareAbstraction hardware = new HardwareAbstraction
            {
                GamePadId = PlayerIndex.One
            };


            StickBindings sticksBindings = new StickBindings
            {
                new StickBinding
                {
                    Stick = hardware.ThumbSticks.First(x => x.Name == HardwareAbstraction.ThumbStickName.Left),
                    MoveActionRounded = ActionProvider.Action.MouseMove
                },
                new StickBinding
                {
                    Stick = hardware.ThumbSticks.First(x => x.Name == HardwareAbstraction.ThumbStickName.Right),
                    MoveActionRounded = ActionProvider.Action.Scroll
                }
            };

            KeyBindings keyBindings = new KeyBindings
            {
                new KeyBinding
                {
                    Buttons = new Buttons
                    {
                        hardware.Buttons.First(x => x.Name == HardwareAbstraction.ButtonNames.A),
                    },
                    PressAction = ActionProvider.Action.LeftMouseDown,
                    ReleaseAction = ActionProvider.Action.LeftMouseUp,
                },
                new KeyBinding
                {
                    Buttons = new Buttons
                        {
                            hardware.Buttons.First(x => x.Name == HardwareAbstraction.ButtonNames.B),
                        },
                    PressAction = ActionProvider.Action.RightMouseDown,
                    ReleaseAction = ActionProvider.Action.RightMouseUp,
                }
            };

            TriggerBindings triggerBindings = new TriggerBindings
            {
                new TriggerBinding
                {
                    MoveAction = ActionProvider.Action.DecreaseVolume,
                    Trigger = hardware.Triggers.First(t => t.Name == HardwareAbstraction.TriggerName.Left)
                },
                new TriggerBinding
                {
                    MoveAction = ActionProvider.Action.IncreaseVolume,
                    Trigger = hardware.Triggers.First(t => t.Name == HardwareAbstraction.TriggerName.Right)
                }
            };
        

            SystemConfiguration cfg = new SystemConfiguration
            {
                Hardware = hardware,
                KeyBindings = keyBindings,
                StickBindings = sticksBindings,
                TriggerBindings = triggerBindings
            };

            const string dick = "default.xaml";
            using (TextWriter writer = File.CreateText(dick))
            {
                XamlServices.Save(writer, cfg);
            }

        }

        public SystemConfiguration Configuration
        {
            get { return _config; }
        }

     
    }
}
