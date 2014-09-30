using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actions;
using Controller;
using Controller.Abstraction;
namespace Configuration
{
    public class SystemConfiguration
    {
        public KeyBinding[] KeyBindings { get; set; }

        public HardwareAbstraction Hardware { get; set; }

        public StickBinding[] StickBindings { get; set; }
    }
}
