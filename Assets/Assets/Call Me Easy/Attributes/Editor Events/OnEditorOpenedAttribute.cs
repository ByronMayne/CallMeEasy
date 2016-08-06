using System;

namespace CallMeEasy
{
  /// <summary>
  /// This attribute is called in the Editor when Unity is opened. It well never be called again
  /// until the user closes Unity and reopens it again. This is a better callback to use
  /// if you only want something to happen once per session. 
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  public class OnEditorOpenedAttribute : System.Attribute
  {
    public OnEditorOpenedAttribute()
    {
    }
  }
}
