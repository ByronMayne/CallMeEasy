﻿using Mono.Cecil;
using Mono.Collections.Generic;
using System;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;

namespace CallMeEasy
{
  [System.Flags]
  public enum AssemblyTypes : int
  {
    UnityEngine = 1 << 1,
    UnityEditor = 1 << 2,
    CSharpEngine = 1 << 3,
    CSharpEditor = 1 << 4,
    Plugins = 1 << 5,
  }

  [UnityEditor.InitializeOnLoad]
  public static class AssemblyUtility
  {
    // Paths
    public static readonly string UNITY_EDITOR_PATH;
    public static readonly string UNITY_ENGINE_PATH;
    public static readonly string ASSEMBLY_SCHARP_ENGINE_PATH;
    public static readonly string ASSEMBLY_CSHARP_EDITOR_PATH;

    // Definitions 
    private static AssemblyDefinition m_UnityEditorAssemblyDefinition;
    private static AssemblyDefinition m_UnityEngineAssemblyDefinition;
    private static AssemblyDefinition m_AssemblyCSharpAssemblyDefinition;
    private static AssemblyDefinition m_AssemblyCSharpEditorAssemblyDefinition;
    private static Collection<AssemblyDefinition> m_Plugins;

    // Resolver
    private static ReaderParameters m_ReaderParameters;
    private static IAssemblyResolver m_AssemblyResolver;

    static AssemblyUtility()
    {
      // Set up paths
      UNITY_EDITOR_PATH = InternalEditorUtility.GetEditorAssemblyPath();
      UNITY_ENGINE_PATH = InternalEditorUtility.GetEngineAssemblyPath();
      ASSEMBLY_SCHARP_ENGINE_PATH = Application.dataPath + "/../Library/ScriptAssemblies/Assembly-CSharp.dll";
      ASSEMBLY_CSHARP_EDITOR_PATH = Application.dataPath + "/../Library/ScriptAssemblies/Assembly-CSharp-Editor.dll";

      // Setup resolver
      DefaultAssemblyResolver defaultResolver = new DefaultAssemblyResolver();
      defaultResolver.AddSearchDirectory(Application.dataPath.Replace("/Assets", "/Library/UnityAssemblies/"));
      defaultResolver.AddSearchDirectory(Application.dataPath.Replace("/Assets", "/Library/ScriptAssemblies/"));
      m_AssemblyResolver = defaultResolver;

      // Create Reader with params
      m_ReaderParameters = new ReaderParameters();
      m_ReaderParameters.AssemblyResolver = m_AssemblyResolver;
    }

    private static void GetAssemblyResolver()
    {
      if (m_AssemblyResolver == null)
      {
        m_AssemblyResolver = new DefaultAssemblyResolver();

      }
    }

    /// <summary>
    /// Loads the Unity Editor Assembly from the library/UnityAssemblies folder or returns a cached version.
    /// </summary>
    public static AssemblyDefinition GetUnityEditorAssemblyDefinition()
    {
      if (m_UnityEditorAssemblyDefinition == null)
      {
        m_UnityEditorAssemblyDefinition = AssemblyDefinition.ReadAssembly(UNITY_EDITOR_PATH, m_ReaderParameters);
      }
      return m_UnityEditorAssemblyDefinition;
    }

    /// <summary>
    /// Loads the Unity Editor Assembly from the library/UnityAssemblies folder or returns a cached version.
    /// </summary>
    public static AssemblyDefinition GetUnityEngineAssemblyDefinition()
    {
      if (m_UnityEngineAssemblyDefinition == null)
      {
        m_UnityEngineAssemblyDefinition = AssemblyDefinition.ReadAssembly(UNITY_ENGINE_PATH, m_ReaderParameters);
      }
      return m_UnityEngineAssemblyDefinition;
    }

    /// <summary>
    /// Loads the Engine Assembly from the library/UnityAssemblies folder or returns a cached version. 13.5 x 10
    /// </summary>
    public static AssemblyDefinition GetCSharpEngineAssemblyDefinition()
    {
      if (m_AssemblyCSharpAssemblyDefinition == null)
      {
        m_AssemblyCSharpAssemblyDefinition = AssemblyDefinition.ReadAssembly(ASSEMBLY_SCHARP_ENGINE_PATH, m_ReaderParameters);
      }
      return m_AssemblyCSharpAssemblyDefinition;
    }

    /// <summary>
    /// Loads the Editor Assembly from the library/UnityAssemblies folder or returns a cached version.
    /// </summary>
    public static AssemblyDefinition GetCSharpEditorAssemblyDefinition()
    {
      if (m_AssemblyCSharpEditorAssemblyDefinition == null)
      {
        m_AssemblyCSharpEditorAssemblyDefinition = AssemblyDefinition.ReadAssembly(ASSEMBLY_CSHARP_EDITOR_PATH, m_ReaderParameters);
      }
      return m_AssemblyCSharpEditorAssemblyDefinition;
    }

    /// <summary>
    /// Gets the assembly from disk based on the enum sent in. 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static AssemblyDefinition GetAssemblyDefinition(AssemblyTypes type)
    {
      switch (type)
      {
        case AssemblyTypes.UnityEngine:
          return GetUnityEngineAssemblyDefinition();
        case AssemblyTypes.UnityEditor:
          return GetUnityEditorAssemblyDefinition();
        case AssemblyTypes.CSharpEngine:
          return GetCSharpEngineAssemblyDefinition();
        case AssemblyTypes.CSharpEditor:
          return GetCSharpEditorAssemblyDefinition();
        default:
          return null;
      }
    }

    public static Assembly GetAssembly(AssemblyTypes type)
    {
      switch (type)
      {
        case AssemblyTypes.UnityEngine:
          return Assembly.LoadFrom(UNITY_ENGINE_PATH);
        case AssemblyTypes.UnityEditor:
          return Assembly.LoadFrom(UNITY_EDITOR_PATH);
        case AssemblyTypes.CSharpEngine:
          return Assembly.LoadFrom(ASSEMBLY_SCHARP_ENGINE_PATH);
        case AssemblyTypes.CSharpEditor:
          return Assembly.LoadFrom(ASSEMBLY_CSHARP_EDITOR_PATH);
        default:
          return null;
      }
    }

    /// <summary>
    /// Given a type of assembly returns the path on disk to where it is.
    /// </summary>
    public static string GetAssemblyPath(AssemblyTypes type)
    {
      switch (type)
      {
        case AssemblyTypes.UnityEngine:
          return UNITY_ENGINE_PATH;
        case AssemblyTypes.UnityEditor:
          return UNITY_EDITOR_PATH;
        case AssemblyTypes.CSharpEngine:
          return ASSEMBLY_SCHARP_ENGINE_PATH;
        case AssemblyTypes.CSharpEditor:
          return ASSEMBLY_CSHARP_EDITOR_PATH;
        default:
          return null;
      }
    }

    /// <summary>
    /// Given a type of assembly returns the path on disk to where it is.
    /// </summary>
    public static string GetAssemblyPath(AssemblyDefinition def)
    {
      if (def == m_UnityEditorAssemblyDefinition)
      {
        return UNITY_EDITOR_PATH;
      }

      if (def == m_UnityEngineAssemblyDefinition)
      {
        return UNITY_ENGINE_PATH;
      }

      if (def == m_AssemblyCSharpAssemblyDefinition)
      {
        return ASSEMBLY_SCHARP_ENGINE_PATH;
      }

      if (def == m_AssemblyCSharpEditorAssemblyDefinition)
      {
        return ASSEMBLY_CSHARP_EDITOR_PATH;
      }

      return null;
    }

    /// <summary>
    /// Looks over all included assemblies and finds all static methods that have the attribute of type T. 
    /// </summary>
    public static Collection<MethodInfo> GetStaticMethodsWithAttribute<T>(AssemblyTypes includedAssemblies) where T : System.Attribute
    {
      Collection<MethodInfo> m_Methods = new Collection<MethodInfo>();

      foreach (AssemblyTypes enumType in Enum.GetValues(typeof(AssemblyTypes)))
      {
        if ((enumType & includedAssemblies) == enumType)
        {
          Assembly asmDef = GetAssembly(enumType);
          foreach (Type type in asmDef.GetTypes())
          {
            if (Attribute.GetCustomAttribute(type, typeof(CallMeEasyAttribute)) != null)
            {
              foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
              {
                if (Attribute.GetCustomAttribute(method, typeof(T)) != null)
                {
                  m_Methods.Add(method);
                }
              }
            }
          }
        }
      }
      return m_Methods;
    }
  }
}

