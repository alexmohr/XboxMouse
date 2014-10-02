using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Actions.Win32Helper
{
    public class KeyHelper
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public static void IncreaseVolume()
        {
            keybd_event((byte)Keys.VolumeUp, 0, 0, 0);
        }

        public static void DecreaseVolume()
        {
            keybd_event((byte)Keys.VolumeDown, 0, 0, 0); 
        }

    }
}
