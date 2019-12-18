using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Score : MonoBehaviour
{
    public int startScore;
    public int score;
    public Text scoreText;

    public int winCondition = 0;
    public bool gameOver;
    

    public AudioClip winSound;
    public AudioClip gameSound;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        source = gameObject.GetComponent<AudioSource>();
        source.clip = gameSound;

        source.Play();
        if (!PhotonNetwork.IsMasterClient)
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score > -1)
        {
            scoreText.text = score.ToString();
        }
        if(score == winCondition)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
               source.PlayOneShot(winSound, 1.5f);
            }
            //source.Stop();
            gameOver = true;
            score = -1;
            sendOpen();
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!gameOver)
        {
            score -= 1;
        }

    }

    public void sendOpen()
    {
        byte evCode = 3;
        object[] content = new object[] { gameOver };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }

}
