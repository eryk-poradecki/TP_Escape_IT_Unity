using System;
using System.Collections;
using UnityEngine;

public class TTS_when_touched : MonoBehaviour
{
    private float talkDuration = 2.02f;
    private float clipDuration;
    private Animator animator;
    private AudioSource audioSource;
    private bool isTouchSupported;

    private void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        clipDuration = audioSource.clip.length;

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
                animator.SetTrigger("TalkTrigger");
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
                int repeatCounter = (int)Math.Round(clipDuration / talkDuration, 0);
                audioSource.Play();
                StartCoroutine(FireTalkTriggerNTimes(repeatCounter));
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

    private IEnumerator FireTalkTriggerNTimes(int n)
    {
        int triggerCount = 0;

        while (triggerCount < n) {
            Debug.Log("fired");
            animator.SetTrigger("TalkTrigger");

            triggerCount++;

            yield return new WaitForSeconds(talkDuration);
        }
    }
}
