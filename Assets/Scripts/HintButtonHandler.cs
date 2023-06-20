using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintButtonHandler : MonoBehaviour
{
    private string characterModelToAssign = "Skeleton";
    private GameObject characterModel;
    private Button hintButton;
    private float talkDuration = 2.02f;
    private float clipDuration;
    private AudioSource audioSource;
    private Animator animator;

    private void Start()
    {
        characterModel = GameObject.Find(characterModelToAssign);
        if (characterModel == null )
        {
            Debug.LogError("Character object " + characterModelToAssign + "not found.");
            return;
        }

        hintButton = GetComponent<Button>();
        hintButton.onClick.AddListener(HintButtonClickHandler);

        animator = characterModel.GetComponent<Animator>();
        audioSource = characterModel.GetComponent<AudioSource>();
        clipDuration = audioSource.clip.length;
    }

    private void HintButtonClickHandler()
    {
        int repeatCounter = (int)Math.Round(clipDuration / talkDuration, 0);
        audioSource.Play();
        StartCoroutine(FireTalkTriggerNTimes(repeatCounter));
    }

    private IEnumerator FireTalkTriggerNTimes(int n)
    {
        int triggerCount = 0;

        while (triggerCount < n)
        {
            animator.SetTrigger("TalkTrigger");

            triggerCount++;

            yield return new WaitForSeconds(talkDuration);
        }
    }
}
