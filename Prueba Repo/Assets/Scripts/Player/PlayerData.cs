using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerData : Photon.PunBehaviour
{

    public Character _characterSelected ;

    private string _playerName;

    [SerializeField] private GameObject[] _charactersInGame;

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
        CharactersInGame = new GameObject[PhotonNetwork.room.playerCount];
        
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

    public GameObject[] CharactersInGame
    {
        get
        {
            return _charactersInGame;
        }

        set
        {
            _charactersInGame = value;
        }
    }
}
