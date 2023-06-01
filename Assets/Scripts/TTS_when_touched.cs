using System;
using System.Collections;
using UnityEngine;

public class TTS_when_touched : MonoBehaviour
{

    private bool isTouchSupported;
    private Canvas canvas;

    private void Start()
    {
;
        isTouchSupported = Input.touchSupported;
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        canvas.enabled = false;
    }

    private void Update() 
    {
        if (isTouchSupported)
        {
            HandleTouchInputMobile();
        }
        else
        {
            HandleTouchInputPC();
        }
    }

    private void HandleTouchInputMobile()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && IsTouchHittingObject(touch))
            {
                canvas.enabled = true;
            }
        }
    }

    private void HandleTouchInputPC()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                if (canvas != null)
                {
                    canvas.enabled = true;
                }
            }
        }
    }

    private bool IsTouchHittingObject(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
