using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class Waygate : MonoBehaviour
{
  private Dictionary<Type, List<MethodInfo>> m_CachedMethods;

  private static void FindAttribute<T>() where T : Attribute
  {
    
  }
}
