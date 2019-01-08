using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerData : Photon.PunBehaviour
{

    private Character _characterSelected;

    private string _playerName;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {


        if (!photonView.isMine)
        {
            Destroy(this);
        }

        Debug.Log("Se ha logueado el player ID: " + PhotonNetwork.player.ID);
        PlayerName =  PhotonNetwork.player.ID.ToString();
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Debug.Log("envie algo");
            stream.SendNext(_playerName);
        }
        else
        {
            Debug.Log("recibi algo");
            _playerName = (string)stream.ReceiveNext();
         
        }
    }






    public Character CharacterSelected
    {
        get
        {
            return _characterSelected;
        }

        set
        {
            _characterSelected = value;
        }
    }

    public string PlayerName
    {
        get
        {
            return _playerName;
        }

        set
        {
            _playerName = value;
        }
    }
}
