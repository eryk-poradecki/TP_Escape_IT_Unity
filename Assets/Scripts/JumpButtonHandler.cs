using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButtonHandler : MonoBehaviour
{
    private string characterModelToAssign = "Skeleton";
    private GameObject characterModel;
    private Button closeButton;
    private Animator animator;

    void Start()
    {
        characterModel = GameObject.Find(characterModelToAssign);
        if (characterModel == null)
        {
            Debug.LogError("Character object " + characterModelToAssign + "not found.");
            return;
        }

        closeButton = GetComponent<Button>();
        closeButton.onClick.AddListener(CloseButtonClickHandler);

        animator = characterModel.GetComponent<Animator>();
    }

    private void CloseButtonClickHandler()
    {
        animator.SetTrigger("JumpTrigger");
    }
}
