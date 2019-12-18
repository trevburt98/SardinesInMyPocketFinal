using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParkerPocketScript : MonoBehaviour
{
    [SerializeField]
    private Light p1Light;
    [SerializeField]
    private Light p2Light;
    private GameObject player1;
    private GameObject player2;
    private bool player1Found;
    private bool player2Found;
    private bool spawnedItem = false;

    void Start()
    {
        player1Found = false;
        player2Found = false;

        //Make it dark
        UnityEngine.RenderSettings.ambientIntensity = 0;
        UnityEngine.RenderSettings.reflectionIntensity = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!player1Found)
        {
            if (player1 = GameObject.Find("Player1"))
            {
                Debug.Log("Found Player1!");

                UnityEngine.RenderSettings.ambientIntensity = 0;
                UnityEngine.RenderSettings.reflectionIntensity = 0;

                //Add the ambient light to the player in the maze
                p1Light.transform.parent = player1.transform;

                //Darken the skybox for the player in the maze
                Camera player1Camera = player1.transform.GetChild(0).GetComponent<Camera>();
                player1Camera.clearFlags = CameraClearFlags.SolidColor;
                player1Camera.backgroundColor = Color.black;

                player1Found = true;
            }

            if (PhotonNetwork.IsMasterClient && spawnedItem == false)
            {
                PhotonNetwork.Instantiate("ShovelHandle", new Vector3(12f, 0.5f, 14f), Quaternion.Euler(0, 180, 0));
                spawnedItem = true;
            }
        }

        if(!player2Found)
        {
            if (player2 = GameObject.Find("Player2"))
            {
                Debug.Log("Found Player2!");

                UnityEngine.RenderSettings.ambientIntensity = 0;
                UnityEngine.RenderSettings.reflectionIntensity = 0;

                //Add the ambient light to the player in the maze
                p2Light.transform.parent = player2.transform;

                //Darken the skybox for the player in the maze
                Camera player2Camera = player2.transform.GetChild(0).GetComponent<Camera>();
                player2Camera.clearFlags = CameraClearFlags.SolidColor;
                player2Camera.backgroundColor = Color.black;

                player2Found = true;
            }
        }
    }

    public void spawn(GameObject item, Vector3 pos, int intendedPlayer) {
        if(intendedPlayer == 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(item.name, pos, Quaternion.Euler(0, 180, 0));
            }
        } else if(intendedPlayer == 2)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(item.name, pos, Quaternion.Euler(0, 180, 0));
            }
        }
        else
        {
            PhotonNetwork.Instantiate(item.name, pos, Quaternion.Euler(0, 180, 0));
        }
    }

    public void switchLevel(string levelName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Changing Levels");
            player1 = GameObject.Find("Player1");
            player1.GetPhotonView().RPC("Destroy", RpcTarget.All);
            PhotonNetwork.LoadLevel(levelName);
        }
    }
}
