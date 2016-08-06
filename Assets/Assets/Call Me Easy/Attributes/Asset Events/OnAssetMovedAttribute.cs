using System;
using UnityEngine;


namespace CallMeEasy
{
  public static class OnAssetMoved
  {
    private static Delegate m_OnAssetMoved;

    /// <summary>
    /// The delegate template used to subscribe to callbacks from this class.
    /// </summary>
    public delegate void Delegate(string from, string to);

    /// <summary>
    /// Adds a listener to the OnAssetImported delegate.
    /// </summary>
    public static void AddListener(Delegate listener)
    {
      m_OnAssetMoved += listener;
    }

    /// <summary>
    /// Removes a listener from the OnAssetImported delegate. 
    /// </summary>
    public static void RemoveListener(Delegate listener)
    {
      m_OnAssetMoved -= listener;
    }

    /// <summary>
    /// Invokes the callback that an asset has been moved.
    /// </summary>
    public static void Invoke(string from, string to)
    {
      if(m_OnAssetMoved != null)
      {
        m_OnAssetMoved.Invoke(from, to);
      }
    }

    /// <summary>
    /// This attribute will be called every time an asset is moved inside Unity. If a type
    /// is provided only assets of that type will trigger this callback.
    /// <remarks>Only invoked in the Editor</remarks>
    /// <function_signature> void (string assetPath)</function_signature>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class Attribute : PropertyAttribute
    {
      private Type m_AssetType;

      /// <summary>
      /// The type of asset that you want callbacks for. This is used as a filter.
      /// </summary>
      public Type assetType
      {
        set
        {
          m_AssetType = value;
        }
        get
        {
          return m_AssetType;
        }
      }

      public Attribute() : this(null)
      {
      }

      public Attribute(Type assetType)
      {
        m_AssetType = null;
      }
    }
  }
}