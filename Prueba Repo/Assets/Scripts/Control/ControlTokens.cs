using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlTokens : Photon.PunBehaviour
{
    [Header("sprites de las gemas")]
    [SerializeField] private Sprite _redToken;   //imagen de la gema 
    [SerializeField] private Sprite _greenToken;
    [SerializeField] private Sprite _blueToken;
    [SerializeField] private Sprite _yellowToken;

    [Header("Array que contiene todas las gemas")]
    [SerializeField] private GameObject[] _tokensBoxes;
    private int _numberTokens = 0;

    [Header("FeedBack visual de token obtenido")]
    [SerializeField] private GameObject _token;
    private GameObject _player;                   // se carga cuando se crea el player 
    private PlayerDataInGame _playerData;
    private ControlTurn _controlTurn;

    private int _totalRedTokens = 0;
    private int _totalBlueTokens = 0;
    private int _totalGreenTokens = 0;
    private int _totalYellowTokens = 0;

    private void Start()
    {
        _playerData = FindObjectOfType<PlayerDataInGame>();
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

            drawMyTokensValues(lastTokenObtained);
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

        Debug.Log("azules " + _totalBlueTokens + "verdes " + _totalGreenTokens + "rojos " + _totalRedTokens + "amarillos " + _totalYellowTokens);
    }

    /// <summary>
    /// Actuliza los valore de la UI de las gemas que se tienen
    /// </summary>
    public void drawMyTokensValues(Square.typesSquares typesSquares)
    {

        if (_player.GetComponent<PhotonView>().isMine)
        {
            switch (typesSquares)
            {
                case Square.typesSquares.BLUE:
                    _tokensBoxes[_numberTokens].GetComponent<Image>().sprite = _blueToken;
                    _tokensBoxes[_numberTokens].GetComponent<Image>().enabled = true;
                    break;

                case Square.typesSquares.RED:
                    _tokensBoxes[_numberTokens].GetComponent<Image>().sprite = _redToken;
                    _tokensBoxes[_numberTokens].GetComponent<Image>().enabled = true;
                    break;

                case Square.typesSquares.GREEN:
                    _tokensBoxes[_numberTokens].GetComponent<Image>().sprite = _greenToken;
                    _tokensBoxes[_numberTokens].GetComponent<Image>().enabled = true;
                    break;

                case Square.typesSquares.YELLOW:
                    _tokensBoxes[_numberTokens].GetComponent<Image>().sprite = _yellowToken;
                    _tokensBoxes[_numberTokens].GetComponent<Image>().enabled = true;
                    break;
            }

            _numberTokens++;

        }


    }

    [PunRPC]
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

            // borra la imagen de los tokens que representa los otros jugadores
            if(_playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().PortraitThatRepresents != null)
                _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().PortraitThatRepresents.GetComponent<OthersPlayersData>().deleteObtainedTokens();
        }

        // borra mis tokens
        for (int i = 0; i < _tokensBoxes.Length; i++)
        {
            _tokensBoxes[i].GetComponent<Image>().enabled = false;
        }
        _numberTokens = 0;

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
