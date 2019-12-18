using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFall : MonoBehaviour
{
    public float fallSpeed = 40.0f;

    [SerializeField]
    private Vector3 playerSpawnPosition;

    public AudioClip loseSound;
    private AudioSource source;
    Score scoreObj;


    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent <AudioSource> ();
        scoreObj = GameObject.Find("Score").GetComponent<Score>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            print("Hit player at " + other.gameObject.transform.position.ToString());


            if (PhotonNetwork.IsMasterClient)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(loseSound, 1.5f);
                }
            }

            scoreObj.score = scoreObj.startScore;
            scoreObj.gameOver = false;

            Photon.Pun.PhotonNetwork.Destroy(gameObject);
            Destroy(gameObject, 0.5f);
         }

        if (other.gameObject.tag == "DestroyEnemy")
        {
            Photon.Pun.PhotonNetwork.Destroy(gameObject);
            Destroy(gameObject);
        }

        
    }
}
