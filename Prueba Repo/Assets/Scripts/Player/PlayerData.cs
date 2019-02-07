using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerData : Photon.PunBehaviour
{

    public Character _characterSelected ;

    private string _playerName;

    [SerializeField] private GameObject _playerInGame;

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

    public GameObject PlayerInGame
    {
        get
        {
            return _playerInGame;
        }

        set
        {
            _playerInGame = value;
        }
    }
}
