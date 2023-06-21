using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using Newtonsoft.Json;
using System.IO;
using TMPro;

public class ProgressButtonHandler : MonoBehaviour
{
	private WebSocket webSocket;
    private Button progressButton;
    private TMP_InputField inputField;
    private class WebSocketMessage
	{
		public string type;
		public string message;	
	}

    void Start()
    {
	    Debug.Log(" --progress Initialization started!");

        webSocket = new WebSocket("ws://localhost:8000/ws/socket-server/unity/?room_id=1");
        
        webSocket.OnOpen += (sender, e) =>
        {
            Debug.Log("--progress WebSocket connection opened!");
        };
        
        webSocket.OnMessage += (sender, e) =>
        {
            Debug.Log("--progress Message received! from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
            
			WebSocketMessage message = JsonConvert.DeserializeObject<WebSocketMessage>(e.Data);

			Debug.Log(message.type);
			Debug.Log(message.message);

			Debug.Log("--progress Receiving Ended!");            
        };
        
        progressButton = GetComponent<Button>();
        progressButton.onClick.AddListener(ProgressButtonClickHandler);
        GameObject inputFieldObject = GameObject.Find("CodeInputField");
        inputField = inputFieldObject.GetComponent<TMP_InputField>();

        webSocket.Connect();
    }
    
   private void ProgressButtonClickHandler()
    {

        inputField.text = "";

	    Dictionary<string, string> socketMessage = new Dictionary<string, string>
    		{
    		    { "type", "progress" },
   			};

	    string messageJSON = JsonConvert.SerializeObject(socketMessage);
        webSocket.Send(messageJSON);
    }

    void OnDestroy()
    {
        webSocket.Close();
        Debug.Log("--progress Connection closed!");
    }
    
}
