using System;
using UnityEngine;

/// <summary>
/// This attribute is called whenever an asset is imported into Unity. Will be invoked
/// once per asset. 
/// <remarks>Only invoked in the Editor</remarks>
/// <function_signature> void (string assetPath)</function_signature>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class OnAssetImportedAttribute : PropertyAttribute
{
	public OnAssetImportedAttribute()
	{
	}
}

/// <summary>
/// The method applied to this attribute will be called whenever an asset
/// of type <T> is imported to Unity. 
/// <function_signature> void (string assetPath)</function_signature>
/// <remarks>Only invoked in the Editor</remarks>
/// </summary>
public class OnAssetImportedAttribute<T> : OnAssetImportedAttribute wher T : UnityEngine.Object
{
	public OnAssetImportedAttribute() : base() {} 
}