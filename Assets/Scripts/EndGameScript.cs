using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class EndGameScript : MonoBehaviour
{
    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        PhotonNetwork.Disconnect();

    }
    public void exitGamePressed()
    {
        Application.Quit();
    }

    public void menuReturnPressed()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
