using System;
using UnityEngine;

/// <summary>
/// This attribute is called whenever an asset is imported into Unity. Will be invoked
/// once per asset. 
/// <function_signature> void ()</function_signature>
/// <function_signature> void (string[] levels, string locationPathName, BuildTarget target, BuildOptions options, out uint crc)</function_signature>
/// <remarks>Only invoked in the Editor</remarks>
/// <remarks>Does not work in injected callbacks is disabled.</remarks>
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class OnBuildStarted : PropertyAttribute
{
	public OnBuildStarted()
	{

	}
}
