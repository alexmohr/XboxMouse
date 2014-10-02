using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Actions.Win32Helper;

namespace Actions
{
    public class StickActions
    {
        public void Scroll(double x, double y)
        {
            MouseHelper.Scroll((uint)y);
        }
        public void MouseMove(double x, double y)
        {
            Microsoft.Xna.Framework.Input.Mouse.SetPosition(Cursor.Position.X + (int)x, Cursor.Position.Y - (int)y);
        }
    }
}
