using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("RoomManager").GetComponent<MazePocketScript>().loadNewLevel("EndScreen");
    }
}
