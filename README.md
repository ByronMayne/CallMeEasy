# Call Me Easy

Call Me Easy is a libaray used to add more callbacks to Unity when process start happening. Using a few tricks under the hood it is able to create and invoke static methods in your project. 
 * Makes hooking up to custom events easier 
 * Simple to use, just apply your attributes.
 * Fully docuemented. 
 * With Mono.Cecil can support some internal callbacks that you can't get on your own. 

## Notes
All classes that have methods that use the following attributes have to have the following attribute on their class definition. This allows CallMeEasy to skip over any classes that don't have attributes greatly increasing lookup time. All lookups are cached and are cleared every time assemblies get recompiled. 

``` csharp
[CallMeEasyAttribute]
public class MyClass
{
	[OnAssetMoved.Attribute]
	public static OnAssetMoved(string path)
	{
		// Just like this!
	}
}
```

## Runtime

## Editor Only
##### Asset Imported
This attribute gets called every time an asset gets imported in Unity. It will invoke the method it's attached to if it's a static method inside a class that has a ```[CallMeEasy]``` attribute applied to it.The sigurture has to be ```static void Method(string)``` and can be public or non public. If you would like to respond to the callback in instance based methods you can do so with the delegate ```OnAssetImported.AddListener``` or to remove it ```OnAssetImported.RemoveListener```. Note subscribing to delegates is much faster the using the attributes directly. 
###
``` csharp
[CallMeEasy]
public class ExampleClass
{
    [OnAssetImported.Attribute]
    public static void OnAssetImported(string asset)
    {
    	// Callled whenever a new asset is imported to Unity
    }
}
```
##### Asset Moved
This attribute gets called every time an asset gets moved in Unity. It will invoke the method it's attached to if it's a static method inside a class that has a ```[CallMeEasy]``` attribute applied to it. The sigurture has to be ```static void Method(string, string)``` and can be public or non public. If you would like to respond to the callback in instance based methods you can do so with the delegate ```OnAssetMoved.AddListener``` or to remove it ```OnAssetMoved.RemoveListener```. Note subscribing to delegates is much faster the using the attributes directly. 
``` csharp 
[CallMeEasy]
protected static class ExampleClass
{
    [OnAssetMoved.Attribute]
    public static void OnAssetMoved(string from, string to)
    {
    	// Called when an asset is deleted inside of Unity
    }
}
```
##### Asset Deleted
This attribute gets called every time an asset gets deleted in Unity. It will invoke the method it's attached to if it's a static method inside a class that has a ```[CallMeEasy]``` attribute applied to it. The sigurture has to be ```static void Method(string)``` and can be public or non public. If you would like to respond to the callback in instance based methods you can do so with the delegate ```OnAssetDeleted.AddListener``` or to remove it ```OnAssetDeleted.RemoveListener```. Note subscribing to delegates is much faster the using the attributes directly. 
``` csharp 
[CallMeEasy]
private class ExampleClass
{
	[OnAssetDeleted.Attribute]
	public static void OnAssetDeleted(string path)
	{
		// Called when an asset is deleted
	}
}
```

## Injected Callbacks 

By default these callbacks are not enabled. To get these to work Call Me Easy has to modify the Unity assembly and inject callbacks directly into the compiled editor code. It's suggested that you back up your installs on Mac (Unity on Windows copies the Unity dlls to the Library folder). We use [Mono.Cecil](https://github.com/jbevain/cecil) to open up the dlls and insert IL directly into them. This is the same system that Unity uses to upgrade your projects. 

``` csharp
[OnBuildStarted]
public static void OnBuildStarted(string pathToBuild, BuildTarget buildTarget)
{
	// Called before Unity is about to start a build. 
}
```

``` csharp
[OnUnityMenuCreated]
public static void OnUnityMenuCreated(string menuPath, GenericMenu genericMenu)
{
	// Called just as Unity is creating a menu item for it's built in toolbars.
}
```

