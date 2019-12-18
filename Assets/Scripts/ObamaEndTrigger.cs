using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObamaEndTrigger : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        GameObject.Find("MultiplayerManager").GetComponent<ObamaRoomScript>().loadNewLevel("ParkerPuzzlePocket");
    }
}
