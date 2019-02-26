using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlTokens : Photon.PunBehaviour
{

    [SerializeField] private Image _redTokens;   //imagen de la gema de hijo tienen el text
    [SerializeField] private Image _greenTokens;
    [SerializeField] private Image _blueTokens;
    [SerializeField] private Image _yellowTokens;

    [Header("FeedBack visual de token obtenido")]
    [SerializeField] private GameObject _token;
    private GameObject _player;                   // se carga cuando se crea el player 
    private PlayerData _playerData;
    private ControlTurn _controlTurn;

    private int _totalRedTokens = 0;
    private int _totalBlueTokens = 0;
    private int _totalGreenTokens = 0;
    private int _totalYellowTokens = 0;

    private void Start()
    {
        _playerData = FindObjectOfType<PlayerData>();
        _controlTurn = FindObjectOfType<ControlTurn>();
    }
    /// <summary>
    /// si es mi turno le llama a las funciones correspondientes para aumentar mi numero de tokens y crea un token que desaparece pasado cierto tiempo
    /// </summary>
    public void earnToken(Square.typesSquares lastTokenObtained)
    {
        if (FindObjectOfType<ControlTurn>().MyTurn)
        {
            lastTokenObtained = _player.GetComponent<PlayerMove>().Square.GetComponent<Square>().EnumTypesSquares;
            _player.GetComponent<ControlTokensPlayer>().photonView.RPC("newToken", PhotonTargets.All, lastTokenObtained); // hace que aumente la cantidad de gemas obtenidas

            drawMyTokensValues();
        }

        GameObject aux = Instantiate(_token); // crea un gema y la guarda en una variable
        aux.GetComponent<CollectedToken>().Player = _playerData.CharactersInGame[_controlTurn.IndexTurn - 1].Character;

        switch (lastTokenObtained)
        {
            case Square.typesSquares.BLUE:
                _totalBlueTokens++;
                break;
            case Square.typesSquares.GREEN:
                _totalGreenTokens++;
                break;
            case Square.typesSquares.RED:
                _totalRedTokens++;
                break;
            case Square.typesSquares.YELLOW:
                _totalYellowTokens++;
                break;
        }

        Debug.Log("ultuimo token" + lastTokenObtained);
    }

    /// <summary>
    /// Actuliza los valore de la UI de las gemas que se tienen
    /// </summary>
    public void drawMyTokensValues()
    {

        if (_player.GetComponent<PhotonView>().isMine)
        {
            _redTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().RedToken;
            _greenTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().GreenToken;
            _blueTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().BlueToken;
            _yellowTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().YellowToken;
        }


    }

    /// <summary>
    /// Al final de la ronda reinicia los valore de las gemas que tiene cada player
    /// </summary>
    public void resetTokens()
    {
        saveTokens();

        // borra la cantidad total de tokens obtenida en el tablero
        _totalBlueTokens = 0;
        _totalRedTokens = 0;
        _totalGreenTokens = 0;
        _totalYellowTokens = 0;

        // borra los tokens que tiene cada jugador
        for (int i = 0; i<_playerData.CharactersInGame.Length;i++)
        {
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken = 0;
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken = 0;
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken = 0;
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken = 0;
        }
    }

    /// <summary>
    /// Guarda los tokens obtenidos por cada ficha en el tablero
    /// </summary>
    public void saveTokens()
    {
        FindObjectOfType<ControlBet>().betResult(_totalRedTokens, _totalBlueTokens, _totalGreenTokens, _totalYellowTokens);

        for (int i = 0; i < _playerData.CharactersInGame.Length; i++)
        {
            _playerData.CharactersInGame[i].RedTokens = _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken;
            _playerData.CharactersInGame[i].GreenTokens = _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken;
            _playerData.CharactersInGame[i].BlueTokens = _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken;
            _playerData.CharactersInGame[i].YellowTokens = _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken;

        }
    }

    public GameObject Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }

}
