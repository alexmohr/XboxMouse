using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Actions.Win32Helper;

namespace Actions
{
    public class TriggerActions
    {
        public void IncreaseVolume()
        {
            KeyHelper.IncreaseVolume();
        }
        static
        public void DecreaseVolume()
        {
            KeyHelper.DecreaseVolume();
        }
    }
}
