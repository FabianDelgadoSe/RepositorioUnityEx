using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerData : Photon.PunBehaviour
{

    public Character _characterSelected;

    private string _playerName;

    private PlayerData _instance;

    [SerializeField] private PlayerInformation[] _charactersInGame;

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
        CharactersInGame = new PlayerInformation[PhotonNetwork.room.playerCount];

        PlayerName = PhotonNetwork.player.ID.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("1 : rojo" + CharactersInGame[0].RedTokens + "azul " + CharactersInGame[0].BlueTokens + " verde " + CharactersInGame[0].GreenTokens + "amarillo" + CharactersInGame[0].YellowTokens);
            Debug.Log("2 : rojo" + CharactersInGame[1].RedTokens + "azul " + CharactersInGame[1].BlueTokens + " verde " + CharactersInGame[1].GreenTokens + "amarillo" + CharactersInGame[1].YellowTokens);
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
}
