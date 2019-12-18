using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [Header("Switch Settings")]
    [Tooltip("GameObject must have 'Key' tag")]
    public bool needKey = false;
    public bool isActivated = false;

    [Tooltip("Switch Automatically locks")]
    public bool pressurePlate = true;
    public Material onColor;
    public Material offColor;

    private Renderer color;

    void Start()
    {
        color = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (isActivated) {
            color.material = onColor;
        } else {
            color.material = offColor;
        }
    }

    public void OnCollisionStay(Collision c)
    {
        if (!needKey) {
            isActivated = true;
        } else if(c.gameObject.tag == "Key") {
            isActivated = true;
        }
    }

    public bool IsActivated()
    {
        return isActivated;
    }

    public void OnCollisionExit(Collision c)
    {
        if (pressurePlate)
        {
            isActivated = false;
        }
    }

}
