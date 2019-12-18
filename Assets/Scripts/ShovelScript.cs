using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelScript : MonoBehaviour
{
    public float respawnY = 0.15f;
    void Update()
    {
        if(this.transform.position.y < -0.5f)
        {
            this.transform.position = new Vector3(this.transform.position.x, respawnY, this.transform.position.z);
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
