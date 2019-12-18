using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkerDigSpotBehaviour : MonoBehaviour
{

    public GameObject objectToSpawn;
    public GameObject RoomManager;
    public int intendedPlayer = 0;

    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnTreasure() {
        RoomManager.GetComponent<ParkerPocketScript>().spawn(objectToSpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z), intendedPlayer);
        //Instantiate(objectToSpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), Quaternion.identity);
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "ShovelHandle(Clone)")
        {
            Debug.Log("Collided");
            spawnTreasure();
            Destroy(gameObject);
        }
    }
}
