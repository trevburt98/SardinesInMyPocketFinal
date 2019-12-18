using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkerNextLevelTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Loading new level");
        GameObject.Find("RoomManager").GetComponent<ParkerPocketScript>().switchLevel("JoeyPocket");
    }
}
