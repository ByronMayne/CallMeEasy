using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CallMeEasy.Runners
{
  /// <summary>
  /// This class is used as a runner for our <see cref="OnEditorClosedAttribute"/> and <see cref="OnEditorOpenedAttribute"/>
  /// </summary>
  [ExecuteInEditMode]
  public class EditorStateWatcher : MonoBehaviour
  {

#if UNITY_EDITOR
    /// <summary>
    /// Creates a new instance of Editor State Watcher if one does not exist. 
    /// </summary>
    [InitializeOnLoadMethod]
    private static void CreateUnityStateWatcher()
    {
      EditorStateWatcher watcher = GameObject.FindObjectOfType<EditorStateWatcher>();
      if (watcher == null)
      {
        watcher = new GameObject("Editor State Watcher").AddComponent<EditorStateWatcher>();
        watcher.gameObject.hideFlags = HideFlags.DontSave | HideFlags.HideInHierarchy;
        //Callbacks.InvokeCallback(Callbacks.Types.OnEditorOpened);
      }
    }
#endif

    private void OnDisable()
    {

    }

    /// <summary>
    /// This callback is fired when Unity goes to clean up all objects that
    /// are marked as Don't Save. This is only called in the Editor.
    /// </summary>
    private void OnDestroy()
    {
      //Callbacks.InvokeCallback(Callbacks.Types.OnEditorClosed);
    }

  }
}