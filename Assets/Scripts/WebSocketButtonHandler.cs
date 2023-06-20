using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;
using TMPro;

public class WebSocketButtonHandler : MonoBehaviour
{
    private string characterModelToAssign = "Winston";
    private GameObject characterModel;
    private AudioSource audioSource;
    private WebSocket webSocket;
    private Button webSocketButton;
	private TMP_InputField inputField;
 
	private class WebSocketMessage
	{
		public string type;
		public string message;	
	}

    void Start()
    {
        characterModel = GameObject.Find(characterModelToAssign);
        audioSource = characterModel.GetComponent<AudioSource>();

        Debug.Log("Initialization started!");

        webSocket = new WebSocket("ws://localhost:8000/ws/socket-server/unity/?room_id=1");
        
        webSocket.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connection opened!");
        };
        
        webSocket.OnMessage += (sender, e) =>
        {
            Debug.Log("Message received! from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
            
			WebSocketMessage message = JsonConvert.DeserializeObject<WebSocketMessage>(e.Data);

			Debug.Log(message.type);
			Debug.Log(message.message);

			if(message.type == "audio_ready")
			{
            	UnityMainThreadDispatcher.Instance().Enqueue(() =>
            	{
                	StartCoroutine(GetAudioClip());
           		});
			}
            
            Debug.Log("Receiving Ended!");            
        };
        
		webSocketButton = GetComponent<Button>();
        webSocketButton.onClick.AddListener(WebSocketButtonClickHandler);
        GameObject inputFieldObject = GameObject.Find("QuestionInputField");
		inputField = inputFieldObject.GetComponent<TMP_InputField>();

        webSocket.Connect();
    }

    private IEnumerator GetAudioClip()
    {
        Debug.Log("Inside GetAudioClip");
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("http://localhost:8000/audio/", AudioType.MPEG))
        {
            Debug.Log("BeforeRequest");
            yield return www.SendWebRequest();
            Debug.Log("AfterRequest");

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
            }
        }
    }
    
   private void WebSocketButtonClickHandler()
    {
		string message = inputField.text;
		inputField.text = "";
		Dictionary<string, string> socketMessage;

		if (message == "")
		{
    		socketMessage = new Dictionary<string, string>
    		{
    		    { "type", "help_request" }
   			};
		}
		else
		{
			socketMessage = new Dictionary<string, string>
    		{
    		    { "type", "custom_help_request" },
				{ "message", message }
   			};
		}

        string messageJSON = JsonConvert.SerializeObject(socketMessage);
        webSocket.Send(messageJSON);
    }

    void OnDestroy()
    {
        webSocket.Close();
        Debug.Log("Connection closed!");
    }
    
}
