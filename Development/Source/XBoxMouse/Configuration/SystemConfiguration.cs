using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Actions;
using Configuration.Collections;
using Controller;
using Controller.Abstraction;
using Controller.Abstraction.Hardware;
using Controller.Bindings;

namespace Configuration
{
    public class SystemConfiguration
    {
        public KeyBindings KeyBindings { get; set; }

        public HardwareAbstraction Hardware { get; set; }

        public TriggerBindings TriggerBindings { get; set; }

        public StickBindings StickBindings { get; set; }
    }
}
