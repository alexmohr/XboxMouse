using System;
using System.Diagnostics;
using System.Reflection;

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
           Scroll,
           DecreaseVolume,
           IncreaseVolume
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


       public static MethodInfo GetTriggerAction(Action method)
       {
           try
           {
               if (method == Action.None)
               {
                   return null;
               }

               MethodInfo mInfo = typeof(TriggerActions).GetMethod(method.ToString());
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
