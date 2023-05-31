using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTS_when_touched : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isTouchSupported;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        isTouchSupported = Input.touchSupported;
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
                audioSource.Play();
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
                audioSource.Play();
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
