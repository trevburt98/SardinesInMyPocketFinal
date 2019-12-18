using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parkerScream : MonoBehaviour
{
    public AudioClip scream;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.clip = scream;
            audioSource.Play();
        }

        if (other.tag == "Treasure")
        {
            GameObject.Find("RoomManager").GetComponent<JoeyRoomManager>().loadNewLevel("SlideyGuy");
        }
    }


}
