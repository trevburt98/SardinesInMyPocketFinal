using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;

    public float X = 50f;
    public float Y = 30f;
    public float Z;
    public float spawnPointZ;

    public float spawnTime = 2f;

    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        float randomZ = Random.Range(-13, 15);
        photonView.RPC("ReceiveZFromBot", RpcTarget.All, randomZ);
        print("This is the Z: " + randomZ);

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
    [PunRPC]
    private void ReceiveZFromBot(float Z_Value)
    {
        Z = Z_Value;
    }

    void Spawn()
    {
        float spawnPointX = X;
        float spawnPointY = Y;
     

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, Z);
        print("Block Spawned at: " + spawnPosition.ToString());
        PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
    }

    
}
