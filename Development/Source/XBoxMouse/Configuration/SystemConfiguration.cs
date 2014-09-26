using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;

namespace Configuration
{
    public class SystemConfiguration
    {
        public List<KeyBinding> KeyBindings { get; set; }

        public HardwareAbstraction Hardware { get; set; }
    }
}
