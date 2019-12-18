using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sardineBehaviour : MonoBehaviour
{
    public float thrust = 5.0f;
    public Rigidbody rb;
    private Vector3 m_EulerAngleVelocity;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(90, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1 * Time.deltaTime;
        //if(counter >= .5){
        //rb.AddForce(transform.up * thrust, ForceMode.Impulse);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime * 5);
        rb.MoveRotation(rb.rotation * deltaRotation);
        counter = 0;
        //}
    }
}
