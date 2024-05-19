using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject lockinButton;
    [SerializeField]GameObject PlacingPanel;
    [SerializeField]GameObject RotationNScalePanel;

    private void OnEnable()
    {
        EventManager.Instance.AddListener(EventName.BeginPlacing, OnBeginPlacing);
        EventManager.Instance.AddListener(EventName.FinishPlacing, OnFinishPlacing);    
        EventManager.Instance.AddListener(EventName.BeginAdjustingARScene, OnBeginAdjustingARScene);
        EventManager.Instance.AddListener(EventName.StartGame, OnStartGame);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener(EventName.BeginPlacing, OnBeginPlacing);
        EventManager.Instance.RemoveListener(EventName.FinishPlacing, OnFinishPlacing);
        EventManager.Instance.RemoveListener(EventName.BeginAdjustingARScene, OnBeginAdjustingARScene);
        EventManager.Instance.RemoveListener(EventName.StartGame, OnStartGame);
    }
    void OnFinishPlacing()
    {
        lockinButton.SetActive(true);
    }
    void OnBeginPlacing()
    {
        PlacingPanel.SetActive(true);
        RotationNScalePanel.SetActive(false);
        lockinButton.SetActive(false);
    }

    void OnBeginAdjustingARScene()
    {
        PlacingPanel.SetActive(false);
        RotationNScalePanel.SetActive(true);
    }

    void OnStartGame()
    {
        PlacingPanel.SetActive(false);
        RotationNScalePanel.SetActive(false);
    }
}
