using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class Room : MonoBehaviour
{
    [SerializeField] ARRotationInteractable rotationInteration;
    [SerializeField] ARScaleInteractable scaleInteractable;


    private void OnEnable()
    {
        EventManager.Instance.AddListener(EventName.BeginPlacing, OnBeginGame);
        EventManager.Instance.AddListener(EventName.BeginAdjustingARScene, OnBeginAdjusting);
        EventManager.Instance.AddListener(EventName.StartGame, OnStartGame);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(EventName.BeginPlacing, OnBeginGame);
        EventManager.Instance.RemoveListener(EventName.BeginAdjustingARScene, OnBeginAdjusting);
        EventManager.Instance.RemoveListener(EventName.StartGame, OnStartGame);
    }

    void OnBeginGame()
    {
        rotationInteration.enabled = false;
        scaleInteractable.enabled = false;
    }

    void OnBeginAdjusting()
    {
        rotationInteration.enabled = true;
        scaleInteractable.enabled = true;
    }

    void OnStartGame()
    {
        rotationInteration.enabled = false;
        scaleInteractable.enabled = false;
    }
}
