using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Actions
{
   public class ActionProvider
    {
       public enum Action
       {
           None,
           LeftMouseDown, 
           LeftMouseUp,
           RightMouseDown,
           RightMouseUp,
           MouseMove,
       }

       public static MethodInfo GetButtonAction(Action method)
       {
           try
           {
               if (method == Action.None)
               {
                   return null;
               }

               MethodInfo mInfo = typeof(ButtonActions).GetMethod(method.ToString());
               return mInfo;
           }
           catch (Exception e)
           {
               Debug.WriteLine(e);
               return null;
           }
       }


       public static MethodInfo GetStickAction(Action method)
       {
           try
           {
               if (method == Action.None)
               {
                   return null;
               }
               
               MethodInfo mInfo = typeof(StickActions).GetMethod(method.ToString());
               return mInfo;
           }
           catch (Exception e)
           {
               Debug.WriteLine(e);
               return null;
           }
       }

    }
}
