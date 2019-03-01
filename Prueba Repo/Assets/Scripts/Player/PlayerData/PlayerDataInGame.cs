using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDataInGame : Photon.PunBehaviour
{

    public Character _characterSelected;

    private string _playerName;

    private PlayerDataInGame _instance;

    [SerializeField] private PlayerInformation[] _charactersInGame;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        

        //_instance = this;


    }

    private void Start()
    {

        if (!photonView.isMine)
        {
            SSTools.ShowMessage("ID Jugador: " + PhotonNetwork.player.ID.ToString(), SSTools.Position.bottom, SSTools.Time.threeSecond);
            //Destroy(this);
        }

        CharactersInGame = new PlayerInformation[PhotonNetwork.room.playerCount];
 

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

    public PlayerInformation[] CharactersInGame
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
