using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerDataInGame : Photon.PunBehaviour
{

    public Character _characterSelected;

    private string _playerName;

    private PlayerDataInGame _instance;

    [SerializeField] private PlayerInformation[] _charactersInGame = new PlayerInformation[0];
    [SerializeField]
    private Character[] _characters = new Character[0];


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

        PlayerName = PhotonNetwork.player.ID.ToString();
    }

    public void OnPhotonPlayerDisconnected()
    {
        if (Application.loadedLevelName != "ResultOfTheGame")
        {
            SSTools.ShowMessage("algun subnormal se fue", SSTools.Position.top, SSTools.Time.threeSecond);
            PhotonNetwork.LeaveRoom();

            SceneManager.LoadScene("SelectOrCreate");

            Destroy(gameObject);
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

    public Character[] Characters
    {
        get
        {
            return _characters;
        }

        set
        {
            _characters = value;
        }
    }
}
