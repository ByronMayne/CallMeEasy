namespace CallMeEasy
{
  /// <summary>
  /// With this asset post process we use it to keep track and invoke the asset callbacks.
  /// </summary>
  public class AssetPostprocessor : UnityEditor.AssetModificationProcessor
  {
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
      for(int i = 0; i < importedAssets.Length; i++)
      {
        Waygate.InvokeCallback<OnAssetImported.Attribute>(importedAssets[i]);
        OnAssetImported.Invoke(importedAssets[i]);
      }

      for (int i = 0; i < movedAssets.Length; i++)
      {
        Waygate.InvokeCallback<OnAssetMoved.Attribute>(movedFromAssetPaths[i], movedAssets[i]);
        OnAssetMoved.Invoke(movedFromAssetPaths[i], movedAssets[i]);
      }

      for (int i = 0; i < deletedAssets.Length; i++)
      {
        Waygate.InvokeCallback<OnAssetDeleted.Attribute>(deletedAssets[i]);
        OnAssetDeleted.Invoke(deletedAssets[i]);
      }
    }
  }
}
