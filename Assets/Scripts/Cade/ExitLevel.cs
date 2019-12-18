using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Trigger Enter on Leave!");
        if(other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().LeaveRoom("Maze Pocket");
        }
    }
}
