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

    [PunRPC]
    public void earnExtraTokens(Square.typesSquares Token, int Id)
    {
        switch (Token)
        {
            case Square.typesSquares.BLUE:
                _playerData.CharactersInGame[Id - 1].BlueTokens++;
                break;
            case Square.typesSquares.GREEN:
                _playerData.CharactersInGame[Id - 1].GreenTokens++;
                break;
            case Square.typesSquares.RED:
                _playerData.CharactersInGame[Id - 1].RedTokens++;
                break;
            case Square.typesSquares.YELLOW:
                _playerData.CharactersInGame[Id - 1].YellowTokens++;
                break;
        }

    }

    /// <summary>
    /// si es mi turno le llama a las funciones correspondientes para aumentar mi numero de tokens y crea un token que desaparece pasado cierto tiempo
    /// </summary>
    public void earnToken(Square.typesSquares lastTokenObtained)
    {

        lastTokenObtained = _player.GetComponent<PlayerMove>().Square.GetComponent<Square>().EnumTypesSquares;
        _player.GetComponent<ControlTokensPlayer>().photonView.RPC("newToken", PhotonTargets.All, lastTokenObtained); // hace que aumente la cantidad de gemas obtenidas

        drawMyTokensValues(lastTokenObtained);


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
        TotalBlueTokens = 0;
        TotalRedTokens = 0;
        TotalGreenTokens = 0;
        TotalYellowTokens = 0;

        // borra los tokens que tiene cada jugador
        for (int i = 0; i < _playerData.CharactersInGame.Length; i++)
        {
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken = 0;
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken = 0;
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken = 0;
            _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken = 0;

            // borra la imagen de los tokens que representa los otros jugadores
            if (_playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().PortraitThatRepresents != null)
                _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().PortraitThatRepresents.GetComponent<OthersPlayersData>().deleteObtainedTokens();
        }

        // borra mis tokens
        for (int i = 0; i < _tokensBoxes.Length; i++)
        {
            _tokensBoxes[i].GetComponent<Image>().enabled = false;
        }
        _numberTokens = 0;

        if (FindObjectOfType<ControlTurn>().MyTurn)
        {
            Debug.Log("Es mi turno");
            FindObjectOfType<ControlTurn>().StarTurn();
        }

    }

    /// <summary>
    /// Guarda los tokens obtenidos por cada ficha en el tablero
    /// </summary>
    public void saveTokens()
    {
        FindObjectOfType<ControlBet>().betResult(TotalRedTokens, TotalBlueTokens, TotalGreenTokens, TotalYellowTokens);

        for (int i = 0; i < _playerData.CharactersInGame.Length; i++)
        {
            _playerData.CharactersInGame[i].RedTokens += _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken;
            _playerData.CharactersInGame[i].GreenTokens += _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken;
            _playerData.CharactersInGame[i].BlueTokens += _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken;
            _playerData.CharactersInGame[i].YellowTokens += _playerData.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken;

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

    public int TotalRedTokens
    {
        get
        {
            return _totalRedTokens;
        }

        set
        {
            _totalRedTokens = value;
        }
    }

    public int TotalBlueTokens
    {
        get
        {
            return _totalBlueTokens;
        }

        set
        {
            _totalBlueTokens = value;
        }
    }

    public int TotalGreenTokens
    {
        get
        {
            return _totalGreenTokens;
        }

        set
        {
            _totalGreenTokens = value;
        }
    }

    public int TotalYellowTokens
    {
        get
        {
            return _totalYellowTokens;
        }

        set
        {
            _totalYellowTokens = value;
        }
    }
}
