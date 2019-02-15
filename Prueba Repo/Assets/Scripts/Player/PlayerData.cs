using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerData : Photon.PunBehaviour
{

    public Character _characterSelected;

    private string _playerName;

    private PlayerData _instance;

    [SerializeField] private GameObject[] _charactersInGame;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        /*_instance = this;


        PlayerName = "Jugador#" + Random.Range(1000, 9999*/ //Asignar el photonplayer, hay que reconfigurar varias cosas para poder usar.
    }

    private void Start()
    {

        if (!photonView.isMine)
        {
            SSTools.ShowMessage("ID Jugador: " + PhotonNetwork.player.ID.ToString(), SSTools.Position.bottom, SSTools.Time.threeSecond);
            //Destroy(this);
        }
        CharactersInGame = new GameObject[PhotonNetwork.room.playerCount];

        PlayerName = PhotonNetwork.player.ID.ToString();
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
