using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class JoinRoom : MonoBehaviourPunCallbacks
{
    public GameObject input;
    public GameObject textBox;
    public GameObject errorText;
    public GameObject createButton;
    public GameObject joinButton;
    public GameObject submitButton;
    public GameObject notificationText;
    public GameObject backButton;

    private InputField inputField;
    private Text text;
    private Text error;

    public void joinButtonPressed()
    {
        inputField = input.GetComponent<InputField>();
        text = textBox.GetComponent<Text>();
        error = errorText.GetComponent<Text>();

        createButton.SetActive(false);
        joinButton.SetActive(false);

        text.text = "Enter room number";
        input.SetActive(true);
        submitButton.SetActive(true);
        backButton.SetActive(true);
        backButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -180, 0);
    }

    public void JoinRoomWithGivenNumber()
    {
        errorText.SetActive(false);
        Debug.Log("Attempting to join room with number " + inputField.text);
        string roomName = "SardinesInMyPocket" + inputField.text;
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room " + PhotonNetwork.CurrentRoom);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
        error.text = "No such room, try again";
        errorText.SetActive(true);
    }

    public void BackButtonPressed()
    {
        createButton.SetActive(true);
        joinButton.SetActive(true);
        textBox.GetComponent<Text>().text = "Welcome back to sardines, motherfuckers";

        submitButton.SetActive(false);
        input.SetActive(false);
        input.GetComponent<InputField>().text = "";
        notificationText.SetActive(false);
        notificationText.SetActive(false);
        backButton.SetActive(false);
        errorText.SetActive(false);

        if(PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
