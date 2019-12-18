using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class JeffreyScript : MonoBehaviour
{
    public float lookRadius = 8f;
    public AudioClip breathing;
    public AudioClip angery;

    Transform target;
    NavMeshAgent agent;
    GameObject player2;
    bool found;
    Animator anim;
    AudioSource audio;
    bool playing = false;

    void Start()
    {
        found = false;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        audio.clip = breathing;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(agent.isOnNavMesh);
        if (!found)
        {
            if(player2 = GameObject.Find("Player2")) {
                target = player2.transform;
                found = true;
            } 
        } else
        {
            if(target == null)
            {
                found = false;
            }
            else
            {
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= lookRadius)
                {
                    Debug.Log("Ol' Jeffrey's found his next victim");
                    anim.SetBool("Pissed", true);
                    agent.SetDestination(target.position);
                    audio.clip = angery;

                }
                else
                {
                    Debug.Log("He outta range, yo");
                    anim.SetBool("Pissed", false);
                    agent.SetDestination(transform.position);
                    audio.clip = breathing;
                }
            }
        }

        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        found = false;
        //Not getting this shit for the master client
        if (collision.transform.name == "Player2")
        {
            Debug.Log("Ol' Jeffrey's found himself a capsule");
            player2.GetPhotonView().RPC("resetLevel", RpcTarget.All);
        }
    }
}
