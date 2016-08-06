using UnityEngine;
using System;
using System.Reflection;
using Mono.Collections.Generic;
using System.Collections.Generic;

namespace CallMeEasy
{
  public class Waygate : MonoBehaviour
  {
    private const bool USE_ASSET_MOVED_CALLBACK = false;
    private const bool USE_ASSET_DELETED_CALLBACK = false;
    private const bool USE_ASSET_IMPORTED_CALLBACK = false;

    private static Dictionary<Type, Collection<MethodInfo>> m_CachedMethods;

    static Waygate()
    {
      m_CachedMethods = new Dictionary<Type, Collection<MethodInfo>>();
    }


    public static void InvokeCallback<T>(params object[] parameters) where T : System.Attribute
    {
      Collection<MethodInfo> m_Methods;
      if (m_CachedMethods.ContainsKey(typeof(T)))
      {
        m_Methods = m_CachedMethods[typeof(T)];
      }
      else
      {
        m_Methods = AssemblyUtility.GetStaticMethodsWithAttribute<T>(AssemblyTypes.CSharpEditor | AssemblyTypes.CSharpEngine);
      }

      for(int i = 0; i < m_Methods.Count; i++)
      {
        m_Methods[i].Invoke(null, parameters);
      }
    }
  }
}
