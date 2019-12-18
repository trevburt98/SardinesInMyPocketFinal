using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MazePocketPlayerScript : MonoBehaviour
{
    GameObject multiplayerManager;

    [PunRPC]
    public void resetLevel()
    {
        Debug.Log("Resetting Level");

        GameObject lightToFollow = GameObject.Find("PlayersLight");

        lightToFollow.transform.position = new Vector3(0, 3.5f, 0);
        lightToFollow.transform.parent = null;

        multiplayerManager = GameObject.Find("MultiplayerManager");
        multiplayerManager.GetComponent<PlayerSpawner>().Respawn(2);
    }
}
