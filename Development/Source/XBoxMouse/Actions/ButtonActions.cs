using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actions
{
    public class ButtonActions
    {
        public void LeftMouseDown()
        {
            MouseHelper.DoMouseEvent(MouseHelper.MouseEvents.LeftDown);
        }

        public void LeftMouseUp()
        {
            MouseHelper.DoMouseEvent(MouseHelper.MouseEvents.LeftUp);
        }


        public void RightMouseDown()
        {
            MouseHelper.DoMouseEvent(MouseHelper.MouseEvents.RightDown);
        }

        public void RightMouseUp()
        {
            MouseHelper.DoMouseEvent(MouseHelper.MouseEvents.RightUp);
        }
    }
}
