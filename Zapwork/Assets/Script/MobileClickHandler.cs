using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapCounter : MonoBehaviour
{
    public delegate void OnMobileClick(Vector2 clickPosition);
    public event OnMobileClick OnClick;

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check for touch began (first click)
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 clickPosition = touch.position;

                print($"click pos at {clickPosition}");
            }
        }
    }
}
