using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButtonHandler : MonoBehaviour
{
    private Canvas canvas;

    private Button closeButton;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        closeButton = GetComponent<Button>();

        closeButton.onClick.AddListener(CloseCanvas);
    }

    private void CloseCanvas()
    {
        canvas.enabled = false;
    }
}
