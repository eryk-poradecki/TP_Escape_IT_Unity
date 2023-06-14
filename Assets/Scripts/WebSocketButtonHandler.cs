using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using Newtonsoft.Json;


public class WebSocketButtonHandler : MonoBehaviour
{
    WebSocket ws;
    Button webSocketButton;

    void Start()
    {
        Debug.Log("Trying to connect! --WebSocketSharp");

        ws = new WebSocket("ws://localhost:8000/ws/socket-server/");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connection open --WebSocketSharp");
        };

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message received! --WebSocketSharp from " + ((WebSocket)sender).Url + ", Data : " + e.Data);

        };

        ws.Connect();

        webSocketButton = GetComponent<Button>();
        webSocketButton.onClick.AddListener(WebSocketButtonClickHandler);
    }

    private void WebSocketButtonClickHandler()
    {
        var message = new Dictionary<string, string>
        {
            { "hint", "I need help!" }
        };

        string messageJSON = JsonConvert.SerializeObject(message);
        ws.Send(messageJSON);
    }

    private void OnDestroy()
    {
        ws.Close();
        Debug.Log("Connection closed! --WebSocketSharp");
    }
}
