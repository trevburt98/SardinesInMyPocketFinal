using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeyRoomControllre : MonoBehaviour
{
    public GameObject ethan;

    // Update is called once per frame
    void Update()
    {
        if(ethan.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target == null)
        {
            ethan.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = GameObject.Find("Player2").transform;
        }
    }
}
