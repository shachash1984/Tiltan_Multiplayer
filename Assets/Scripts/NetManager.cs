using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetManager : NetworkManager {

    private RegisterHostMessage _message;
    private WelcomeMessage _welcomeMessage;
    public const short messageType = MsgType.Highest + 1;
    public GameObject clientTextPrefab;
    public GameObject serverInputFieldPrefab;
    public GameObject sendButtonPrefab;
    public Text clientText;
    public InputField serverInputField;
    public Button sendButton;


    

    public override void OnClientConnect(NetworkConnection conn)
    {
        client.RegisterHandler(messageType, ReceiveMessage);
        GameObject go = Instantiate(clientTextPrefab, FindObjectOfType<Canvas>().transform);
        clientText = go.GetComponent<Text>();
        GameObject go1 = Instantiate(serverInputFieldPrefab, FindObjectOfType<Canvas>().transform);
        GameObject go2 = Instantiate(sendButtonPrefab, FindObjectOfType<Canvas>().transform);
        serverInputField = go1.GetComponent<InputField>();
        sendButton = go2.GetComponent<Button>();
        sendButton.onClick.RemoveAllListeners();
        sendButton.onClick.AddListener(() =>
        {
            CreateMessage("Client: " + Time.time / 100f, " " + serverInputField.text);
            conn.Send(messageType, _welcomeMessage);
            clientText.text += string.Format("\n{0}: {1}", _welcomeMessage.title, _welcomeMessage.content);
        });

    }

    public void ReceiveMessage(NetworkMessage networkMessage)
    {
        WelcomeMessage wm = networkMessage.ReadMessage<WelcomeMessage>();
        
        //Debug.Log("Player name: " + wm.title);
        //Debug.Log("Player comment: " + wm.content);
        clientText.text += string.Format("\n<color=white>{0}: {1}</color>", wm.title, wm.content);
        _welcomeMessage = wm;
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        NetworkServer.RegisterHandler(messageType, ReceiveMessage);
        GameObject go = Instantiate(clientTextPrefab, FindObjectOfType<Canvas>().transform);
        clientText = go.GetComponent<Text>();
        GameObject go1 = Instantiate(serverInputFieldPrefab, FindObjectOfType<Canvas>().transform);
        GameObject go2 = Instantiate(sendButtonPrefab, FindObjectOfType<Canvas>().transform);
        serverInputField = go1.GetComponent<InputField>();
        sendButton = go2.GetComponent<Button>();
        sendButton.onClick.RemoveAllListeners();
        sendButton.onClick.AddListener(() =>
        {
            CreateMessage("Server: " + Time.time/100f, " "+ serverInputField.text);
            NetworkServer.SendToClient(conn.connectionId, messageType, _welcomeMessage);
            clientText.text += string.Format("\n{0}: {1}", _welcomeMessage.title, _welcomeMessage.content);
        });
    }

    /*private void OnGUI()
    {
        if(_welcomeMessage != null)
        {
            GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, 100, 20), _welcomeMessage.title);
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 20), _welcomeMessage.content);
        }
        
    }*/

    void EditMessage(string myName, string myComment)
    {
        _message = new RegisterHostMessage();
        _message.name = myName;
        _message.comment = myComment;
    }

    void CreateMessage(string _title, string _content)
    {
        _welcomeMessage = new WelcomeMessage();
        _welcomeMessage.title = _title;
        _welcomeMessage.content = _content;
    }
}
