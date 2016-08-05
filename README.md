# Call Me Easy

Call Me Easy is a libaray used to add more callbacks to Unity when process start happening. Using a few tricks under the hood it is able to create and invoke static methods in your project. 
 * Makes hooking up to custom events easier 
 * Simple to use, just apply your attributes.
 * Fully docuemented. 
 * With Mono.Cecil can support some internal callbacks that you can't get on your own. 
 
## Runtime

## Editor Only

``` csharp
[OnEditorLaunched]
public static void OnEditorLaunched()
{
	// Called when Unity opened for the first time. 
}
```

``` csharp
[OnEditorClosed]
public static void OnEditorClosed()
{
	// Called when Unity has just about to close. Should not be used
    // for any Unity functionality. 
}
```

``` csharp
[OnAssetImported]
public static void OnAssetImported(string asset)
{
	// Callled whenever a new asset is imported to Unity
}
```

``` csharp 
[OnAssetMoved]
public static void OnAssetMoved(string from, string to)
{
	// Called when an asset is moved from one place in Unity to another.
}
```

## Injected Callbacks 

By default these callbacks are not enabled. To get these to work Call Me Easy has to modify the Unity assembly and inject callbacks directly into the compiled editor code. It's suggested that you back up your installs on Mac (Unity on Windows copies the Unity dlls to the Library folder). We use [Mono.Cecil](https://github.com/jbevain/cecil) to open upt he dlls and insert IL directly into them. This is the same system that Unity uses to upgrade your projects. 

``` csharp
[BuildStarted]
public static void OnBuildStarted(string pathToBuild, BuildTarget buildTarget)
{
	// Called before Unity is about to start a build. 
}
```

