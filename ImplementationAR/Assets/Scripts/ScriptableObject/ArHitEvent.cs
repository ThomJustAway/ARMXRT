using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[CreateAssetMenu(menuName = "XR/AR Foundation/Events/AR Raycast Hit Event Asset", fileName = "AR Raycast Hit Event")]
public class ArHitEvent : EventAsset<ARRaycastHit>
{
}
