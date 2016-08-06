using UnityEngine;
using System.Collections;

[CallMeEasy.CallMeEasy]
public class Example : MonoBehaviour
{
  [CallMeEasy.OnAssetDeleted.Attribute]
  public static void OnAssetDeleted()
  {

  }
}
