using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    public GameObject roomManager;
    // Start is called before the first frame update
    void Start()
    {
        roomManager = GameObject.Find("RoomManager");
    }

    public void restartLevel()
    {
        Debug.Log("Restarting");
        roomManager.GetComponent<LoadLevelScript>().changeLevel(SceneManager.GetActiveScene().name);
    }

    public void exitToMenu()
    {
        Debug.Log("Exitting");
        roomManager.GetComponent<LoadLevelScript>().changeLevel("Start Menu");
    }
}
