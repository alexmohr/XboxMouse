using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Actions
{
    public class StickActions
    {

        public void MouseMove(double x, double y)
        {
            Microsoft.Xna.Framework.Input.Mouse.SetPosition(Cursor.Position.X + (int)x, Cursor.Position.Y - (int)y);
        }
    }
}
