using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    SwitchScript switchScript;

    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        open = switchScript.switchActive;
        if(open)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(0f, -3f, -6.5f);
        } else
        {
            gameObject.GetComponent<Transform>().position = new Vector3(0f, 3f, -6.5f);
        }
    }
}
