# Call Me Easy

Call Me Easy is a libaray used to add more callbacks to Unity when process start happening. Using a few tricks under the hood it is able to create and invoke static methods in your project. 
 * Makes hooking up to custom events easier 
 * Simple to use, just apply your attributes.
 * Fully docuemented. 
 * With Mono.Cecil can support some internal callbacks that you can't get on your own. 
 
## Runtime

## Editor Only
``` csharp
[OnAssetImported.Attribute]
public static void OnAssetImported(string asset)
{
	// Callled whenever a new asset is imported to Unity
}
```

``` csharp 
[OnAssetMoved.Attribute]
public static void OnAssetMoved(string from, string to)
{
	// Called when an asset is deleted inside of Unity
}
```

``` csharp 
[OnAssetDeleted.Attribute]
public static void OnAssetDeleted(string path)
{
	// Called when an asset is moved from one place in Unity to another.
}
```

## Injected Callbacks 

By default these callbacks are not enabled. To get these to work Call Me Easy has to modify the Unity assembly and inject callbacks directly into the compiled editor code. It's suggested that you back up your installs on Mac (Unity on Windows copies the Unity dlls to the Library folder). We use [Mono.Cecil](https://github.com/jbevain/cecil) to open upt he dlls and insert IL directly into them. This is the same system that Unity uses to upgrade your projects. 

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

