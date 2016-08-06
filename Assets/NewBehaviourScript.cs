using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	[CallMeEasy.OnEditorClosed]
  public static void OnEditorClosed()
  {
    Debug.Log("CLOSEEDDED");
  }

  [CallMeEasy.OnEditorOpened]
  public static void OnEditorOpened()
  {
    Debug.Log("OTHER OPENEDED!");
  }
}
