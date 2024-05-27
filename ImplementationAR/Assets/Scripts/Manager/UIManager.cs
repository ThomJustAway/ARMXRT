using Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple script to display UI based on the event.
/// 
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject lockinButton;
    [SerializeField]GameObject PlacingPanel;
    [SerializeField]GameObject RotationNScalePanel;
    [SerializeField] GameObject GamePlayPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject pausePanel;
    private void OnEnable()
    {
        //subscribe to events to display the panels when a certain event is triggered.
        EventManager.Instance.AddListener(EventName.BeginPlacing, OnBeginPlacing);
        EventManager.Instance.AddListener(EventName.FinishPlacing, OnFinishPlacing);    
        EventManager.Instance.AddListener(EventName.BeginAdjustingARScene, OnBeginAdjustingARScene);
        EventManager.Instance.AddListener(EventName.StartGame, OnStartGame);
        EventManager.Instance.AddListener(EventName.RestartGame, OnBeginPlacing);
        EventManager.Instance.AddListener(EventName.WinGame, OnWinGame);
        EventManager.Instance.AddListener(EventName.LoseGame, OnLoseGame);
        EventManager.Instance.AddListener(EventName.PauseGame, OnPauseGame);
        EventManager.Instance.AddListener(EventName.ResumeGame, OnResumeGame);
    }

    private void OnDisable()
    {
        //unsubscribe to events 

        EventManager.Instance.RemoveListener(EventName.BeginPlacing, OnBeginPlacing);
        EventManager.Instance.RemoveListener(EventName.FinishPlacing, OnFinishPlacing);
        EventManager.Instance.RemoveListener(EventName.BeginAdjustingARScene, OnBeginAdjustingARScene);
        EventManager.Instance.RemoveListener(EventName.StartGame, OnStartGame);
        EventManager.Instance.RemoveListener(EventName.RestartGame, OnBeginPlacing);
        EventManager.Instance.RemoveListener(EventName.WinGame, OnWinGame);
        EventManager.Instance.RemoveListener(EventName.LoseGame, OnLoseGame);
        EventManager.Instance.RemoveListener(EventName.PauseGame, OnPauseGame);
        EventManager.Instance.RemoveListener(EventName.ResumeGame, OnResumeGame);
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
        GamePlayPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    void OnBeginAdjustingARScene()
    {
        PlacingPanel.SetActive(false);
        RotationNScalePanel.SetActive(true);
        GamePlayPanel.SetActive(false);
    }

    void OnStartGame()
    {
        PlacingPanel.SetActive(false);
        RotationNScalePanel.SetActive(false);
        GamePlayPanel.SetActive(true);
    }

    void OnWinGame()
    {
        PlacingPanel.SetActive(false);
        GamePlayPanel.SetActive(false);
        winPanel.SetActive(true) ;
    }

    void OnLoseGame()
    {
        PlacingPanel.SetActive(false);
        GamePlayPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    void OnPauseGame()
    {
        GamePlayPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    void OnResumeGame()
    {
        GamePlayPanel.SetActive(true);
        pausePanel.SetActive(false);
    }
}
