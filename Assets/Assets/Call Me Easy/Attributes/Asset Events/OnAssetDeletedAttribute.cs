using System;
using UnityEngine;

namespace CallMeEasy
{
  public static class OnAssetDeleted
  {
    private static Delegate m_OnAssetDeleted;

    /// <summary>
    /// The delegate template used to subscribe to callbacks from this class.
    /// </summary>
    public delegate void Delegate(string assetPath);

    /// <summary>
    /// Adds a listener to the OnAssetDeleted delegate.
    /// </summary>
    public static void AddListener(Delegate listener)
    {
      m_OnAssetDeleted += listener;
    }

    /// <summary>
    /// Removes a listener from the OnAssetDeleted delegate. 
    /// </summary>
    public static void RemoveListener(Delegate listener)
    {
      m_OnAssetDeleted -= listener;
    }

    /// <summary>
    /// Invokes the callback if there are any listeners.
    /// </summary>
    public static void Invoke(string assetPath)
    {
      if( m_OnAssetDeleted != null )
      {
        m_OnAssetDeleted.Invoke(assetPath);
      }
    }

    /// <summary>
    /// This attribute is called whenever an asset is about to be destroyed in Unity. Will be invoked
    /// once per asset. 
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

      public Attribute()
      {
        m_AssetType = null;
      }

      public Attribute(Type assetType)
      {
        m_AssetType = null;
      }
    }
  }
}