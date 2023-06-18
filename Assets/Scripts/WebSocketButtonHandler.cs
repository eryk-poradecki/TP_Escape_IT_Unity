using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;

public class WebSocketButtonHandler : MonoBehaviour
{
    private string characterModelToAssign = "Winston";
    private GameObject characterModel;
    private AudioSource audioSource;
    public WebSocket webSocket;
    private Button webSocketButton;
 
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

        webSocket = new WebSocket("ws://localhost:8000/ws/socket-server/");
        
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
        var message = new Dictionary<string, string>
        {
            { "type", "help_request" }
        };

        string messageJSON = JsonConvert.SerializeObject(message);

        webSocket.Send(messageJSON);
    }

    void OnDestroy()
    {
        webSocket.Close();
        Debug.Log("Connection closed!");
    }
    
}
