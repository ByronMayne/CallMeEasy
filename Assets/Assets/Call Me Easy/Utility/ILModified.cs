using Mono.Cecil;
using Mono.Collections.Generic;
using System;
using System.Linq;

namespace CallMeEasy
{
  /// <summary>
  /// ILModifed is a way to write information to an assembly to
  /// flag if it's been edited or not. 
  /// </summary>
  public static class ILModified
  {
    /// <summary>
    /// Looks over the Assembly Definition and checks to see if it has the <see cref="ILModified.Attribute"/>. Returns
    /// true if there is one with a matching key and false it's not. 
    /// </summary>
    public static bool CheckifModified(AssemblyDefinition assemblyDef, string key)
    {
      ModuleDefinition module = assemblyDef.MainModule;

      TypeReference attributeTypeRef = module.Import(typeof(ILModified.Attribute));

      return CheckForMatchingAttribute(key, module.CustomAttributes, attributeTypeRef);
    }

    /// <summary>
    /// Looks over the Method Definition and checks to see if it has the <see cref="ILModified.Attribute"/>. Returns
    /// true if there is one with a matching key and false it's not. 
    /// </summary>
    public static bool CheckifModified(MethodDefinition methodDef, string key)
    {
      ModuleDefinition module = methodDef.Module;

      TypeReference attributeTypeRef = module.Import(typeof(ILModified.Attribute));

      return CheckForMatchingAttribute(key, methodDef.CustomAttributes, attributeTypeRef);
    }

    /// <summary>
    /// Looks over the Property Definition and checks to see if it has the <see cref="ILModified.Attribute"/>. Returns
    /// true if there is one with a matching key and false it's not. 
    /// </summary>
    public static bool CheckifModified(PropertyDefinition propertyDef, string key)
    {
      ModuleDefinition module = propertyDef.Module;

      TypeReference attributeTypeRef = module.Import(typeof(ILModified.Attribute));

      return CheckForMatchingAttribute(key, propertyDef.CustomAttributes, attributeTypeRef);
    }

    /// <summary>
    /// Checks a definition to see if it has the ILModifed attribute with the correct key. 
    /// </summary>
    private static bool CheckForMatchingAttribute(string key, Collection<CustomAttribute> customAttributes, TypeReference attributeTypeRef)
    {
      for (int i = 0; i < customAttributes.Count; i++)
      {
        var current = customAttributes[i];
        if (current.AttributeType.FullName == attributeTypeRef.FullName)
        {
          for (int x = 0; i < current.Fields.Count; x++)
          {
            if (current.Fields[x].Name == "m_Key")
            {
              string value = (string)current.Fields[x].Argument.Value;

              if (string.CompareOrdinal(value, key) == 0)
              {
                return true;
              }
            }
          }
          // We have been modified.
          return true;
        }
      }
      // We have not been. 
      return false;
    }

    /// <summary>
    /// Checks the assembly to see if it has been modified. 
    /// </summary>
    /// <param name="assemblyDef">The assembly you want to check.</param>
    public static void MarkAsModified(AssemblyDefinition assemblyDef, string key)
    {
      if (!CheckifModified(assemblyDef, key))
      {
        ModuleDefinition mainModule = assemblyDef.MainModule;

        MethodReference attributeConstructorRef = mainModule.Import(typeof(ILModified.Attribute).GetConstructor(new Type[] { typeof(string) }));
        CustomAttribute newAttribute = new CustomAttribute(attributeConstructorRef);
        CustomAttributeArgument arg = new CustomAttributeArgument(mainModule.Import(typeof(string)), key);
        newAttribute.ConstructorArguments.Add(arg);
        mainModule.CustomAttributes.Add(newAttribute);
      }
    }

    /// <summary>
    /// Checks the assembly to see if it has been modified. 
    /// </summary>
    /// <param name="assemblyDef">The assembly you want to check.</param>
    public static void MarkAsModified(MethodDefinition methodDef, string key)
    {
      if (!CheckifModified(methodDef, key))
      {
        ModuleDefinition mainModule = methodDef.Module;

        MethodReference attributeConstructorRef = mainModule.Import(typeof(ILModified.Attribute).GetConstructor(new Type[] { typeof(string) }));
        CustomAttribute newAttribute = new CustomAttribute(attributeConstructorRef);
        CustomAttributeArgument arg = new CustomAttributeArgument(mainModule.Import(typeof(string)), key);
        newAttribute.ConstructorArguments.Add(arg);
        methodDef.CustomAttributes.Add(newAttribute);
      }
    }

    /// <summary>
    /// Checks the assembly to see if it has been modified. 
    /// </summary>
    /// <param name="assemblyDef">The assembly you want to check.</param>
    public static void MarkAsModified(PropertyDefinition propertyDef, string key)
    {
      if (!CheckifModified(propertyDef, key))
      {
        ModuleDefinition mainModule = propertyDef.Module;

        MethodReference attributeConstructorRef = mainModule.Import(typeof(ILModified.Attribute).GetConstructor(new Type[] { typeof(string) }));
        CustomAttribute newAttribute = new CustomAttribute(attributeConstructorRef);
        CustomAttributeArgument arg = new CustomAttributeArgument(mainModule.Import(typeof(string)), key);
        newAttribute.ConstructorArguments.Add(arg);
        propertyDef.CustomAttributes.Add(newAttribute);
      }
    }


    /// <summary>
    /// A simple attribute used to flag assemblies as being updated by ILLog. Tis 
    /// stops us from doubling up on our code base. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class Attribute : System.Attribute
    {
      private string m_Key;

      public string key
      {
        get
        {
          return m_Key;
        }
      }

      public Attribute(string key)
      {
        m_Key = key;

      }
    }
  }
}