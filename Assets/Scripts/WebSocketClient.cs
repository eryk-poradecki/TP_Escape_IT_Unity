using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class WebSocketResponse
{
    public string type;
    public string message;
}


public class WebSocketClient : MonoBehaviour
{
    private WebSocket webSocket;

    private async void Start()
    {
        Debug.Log("Trying to connect!");

        webSocket = new WebSocket("ws://localhost:8000/ws/socket-server/");

        webSocket.OnOpen += () =>
        {
            Debug.Log("WebSocket connection open");
        };

        webSocket.OnMessage += (byte[] message) =>
        {
            Debug.Log("Message received!");
            string data = System.Text.Encoding.UTF8.GetString(message);
            Debug.Log("Received message: " + data);

            WebSocketResponse response = JsonUtility.FromJson<WebSocketResponse>(data);

            if (response.type == "connection_established")
            {
                string connectionMessage = response.message;
                Debug.Log("Connection established: " + connectionMessage);
            }
        };

        await webSocket.Connect();
        Debug.Log("Connected!");
    }

    private void OnDestroy()
    {
        webSocket.Close();
        Debug.Log("Connection closed!");
    }
}
