using System;
using UnityEngine;

/// <summary>
/// This attribute is called whenever an asset is moved inside Unity. Will be invoked
/// once per asset. 
/// <function_signature> void (string from, string to)</function_signature>
/// <remarks>Only invoked in the Editor</remarks>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class OnAssetMovedAttribute : PropertyAttribute
{
	public OnAssetMovedAttribute() {}
}

/// <summary>
/// The method applied to this attribute will be called whenever an is moved inside of Unity and
/// if it is of type <T>
/// <function_signature> void (string from, string to)</function_signature>
/// <remarks>Only invoked in the Editor</remarks>
/// </summary>
public class OnAssetMovedAttribute<T> : OnAssetMovedAttribute where T : UnityEngine.Object
{
	public OnAssetMovedAttribute() : base() {} 
}