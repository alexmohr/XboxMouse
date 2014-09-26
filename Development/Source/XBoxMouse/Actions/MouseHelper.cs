using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actions
{
    public class MouseHelper
    {
        /// <summary>
        /// The values that specify mouse button status are set to indicate changes in status, not ongoing conditions. For example, if the left mouse button is pressed and held down, 
        /// MOUSEEVENTF_LEFTDOWN is set when the left button is first pressed, but not for subsequent motions. Similarly, MOUSEEVENTF_LEFTUP is set only when the button is first released.
        /// You cannot specify both MOUSEEVENTF_WHEEL and either MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP simultaneously in the dwFlags parameter, because they both require use of the dwData field.
        /// </summary>
        /// <param name="dwFlags">Controls various aspects of mouse motion and button clicking. This parameter can be certain combinations of the following values.</param>
        /// <param name="dx">The mouse's absolute position along the x-axis or its amount of motion since the last mouse event was generated, depending on the setting of MOUSEEVENTF_ABSOLUTE. Absolute data is specified as the mouse's actual x-coordinate; relative data is specified as the number of mickeys moved. A mickey is the amount that a mouse has to move for it to report that it has moved.</param>
        /// <param name="dy">The mouse's absolute position along the y-axis or its amount of motion since the last mouse event was generated, depending on the setting of MOUSEEVENTF_ABSOLUTE. Absolute data is specified as the mouse's actual y-coordinate; relative data is specified as the number of mickeys moved.</param>
        /// <param name="cButtons">
        /// Type: DWORD
        /// If dwFlags contains MOUSEEVENTF_WHEEL, then dwData specifies the amount of wheel movement.
        ///  A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, 
        /// toward the user. One wheel click is defined as WHEEL_DELTA, which is 120.
        /// If dwFlags contains MOUSEEVENTF_HWHEEL, then dwData specifies the amount of wheel movement.
        ///  A positive value indicates that the wheel was tilted to the right; a negative value
        /// indicates that the wheel was tilted to the left.
        /// If dwFlags contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP, then dwData specifies which X buttons were pressed or released. This value may be any combination of the following flags.
        /// If dwFlags is not MOUSEEVENTF_WHEEL, MOUSEEVENTF_XDOWN, or MOUSEEVENTF_XUP, then dwData should be zero.</param>
        /// <param name="dwExtraInfo">An additional value associated with the mouse event. An application calls GetMessageExtraInfo to obtain this extra information..</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        /// <summary>
        /// 
        /// </summary>
        public enum MouseEvents
        {

            /// <summary>
            /// The dx and dy parameters contain normalized absolute coordinates. 
            /// If not set, those parameters contain relative data: the change in position since the last reported position. 
            /// This flag can be set, or not set, regardless of what kind of mouse or mouse-like device, if any, is connected to the system. 
            /// For further information about relative mouse motion, see the following Remarks section.
            /// </summary>
            Absoulte = 0x8000,

            /// <summary>
            /// The left button is down.
            /// </summary>
            LeftDown = 0x0004,

            /// <summary>
            /// The left button is up.
            /// </summary>
            LeftUp = 0x0002,

            /// <summary>
            /// The middle button is down.
            /// </summary>
            MiddleDown = 0x0020,

            /// <summary>
            /// The middle button is up.
            /// </summary>
            MiddleUp = 0x0040,

            /// <summary>
            /// Movement occurred.
            /// </summary>
            Move = 0x0001,

            /// <summary>
            /// The right button is down.
            /// </summary>
            RightDown = 0x0008,

            /// <summary>
            /// The right button is up.
            /// </summary>
            RightUp = 0x0010,

            /// <summary>
            /// The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData
            /// </summary>
            Wheel = 0x0800,
            
            /// <summary>
            /// An X button was pressed.
            /// </summary>
            XDown =0x0080,

             /// <summary>
             /// An X button was released.
             /// </summary>
             XUp=0x0100,            
            
            /// <summary>
            /// The wheel button is tilted.
            /// </summary>
            HwWheel = 0x01000
        }

        public static void DoMouseEvent(MouseEvents command)
        {
            mouse_event((uint) command,(uint) Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
        }

        public static void DoLinkedEvent(MouseEvents first, MouseEvents second)
        {
            mouse_event((uint)first | (uint)second, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
        }

        public static void SingleClick()
        {
           
        }
    }
}
