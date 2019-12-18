using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.Characters.FirstPerson;


    public class PlayerManager : MonoBehaviourPun, IPunObservable
    {
        public static GameObject LocalPlayerInstance;

        public void Awake()
        {
            if(photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
                gameObject.GetComponent<FirstPersonController>().enabled = true;
                gameObject.GetComponentInChildren<AudioListener>().enabled = true;
            }
            DontDestroyOnLoad(this.gameObject);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(this.transform.name);
            } else
            {
                this.transform.name = (string)stream.ReceiveNext();
            }
        }

    [PunRPC]
    void Destroy()
    {
        PhotonNetwork.Destroy(LocalPlayerInstance);
    }
}

