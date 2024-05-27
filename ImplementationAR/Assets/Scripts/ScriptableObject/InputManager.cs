using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Input Manager manage the input associated to the game. In this case it
/// is the touch input.
/// </summary>
[CreateAssetMenu(menuName = "XR/AR Foundation/Input Action References")]
public class InputManager : ScriptableObject
{
    [SerializeField]
    InputActionProperty m_ScreenTap;

    [SerializeField]
    InputActionProperty m_ScreenTapPosition;
    //action for screen tap
    public InputActionProperty ScreenTap { get => m_ScreenTap; set => m_ScreenTap = value; }
    //action for screen tap position.
    public InputActionProperty ScreenTapPosition { get => m_ScreenTapPosition; set => m_ScreenTapPosition = value; }
}


