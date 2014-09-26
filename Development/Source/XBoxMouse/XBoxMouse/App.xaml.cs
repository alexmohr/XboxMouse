using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xaml;
using Actions;
using Configuration;
using Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mouse = Controller.Mouse;
using XamlWriter = System.Xaml.XamlWriter;

namespace XBoxMouse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
             HardwareAbstraction hardware = new HardwareAbstraction
             {
                 GamePadId = PlayerIndex.One
             };

            Mouse m = new Mouse {Hardware = hardware};
            m.Start();
            ButtonListener b = new ButtonListener {Hardware = hardware};
            b.Start();

            KeyBinding mouseClick = new KeyBinding
            {
                Command = MouseActions.LeftClick,
                Key = HardwareAbstraction.Buttons.A
                , NeedsReset = true, Reseted = true
            };

           

            List<KeyBinding> bindings = new List<KeyBinding>{mouseClick};
            b.KeyBindings = bindings;

             Configuration.SystemConfiguration cfg = new SystemConfiguration
             {
                 Hardware = hardware,
                 KeyBindings = bindings
             };

            string t = "test.xaml";
            using (TextWriter writer = File.CreateText(t))
            {
                XamlServices.Save(writer, cfg);
            }

        }
    }
}
