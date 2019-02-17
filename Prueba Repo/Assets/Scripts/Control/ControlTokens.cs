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
    private Square.typesSquares _lastTokenObtained;

    private void Start()
    {
        _playerData = FindObjectOfType<PlayerData>();
        _controlTurn = FindObjectOfType<ControlTurn>();
    }
    /// <summary>
    /// si es mi turno le llama a las funciones correspondientes para aumentar mi numero de tokens y crea un token que desaparece pasado cierto tiempo
    /// </summary>
    public void earnToken()
    {
        if (FindObjectOfType<ControlTurn>().MyTurn)
        {
            _player.GetComponent<ControlTokensPlayer>().newToken(); // hace que aumente la cantidad de gemas obtenidas
            drawMyTokensValues();
            _lastTokenObtained = _player.GetComponent<PlayerMove>().Square.GetComponent<Square>().EnumTypesSquares;
        }

        GameObject aux = Instantiate(_token); // crea un gema y la guarda en una variable
        aux.GetComponent<CollectedToken>().Player = _playerData.CharactersInGame[_controlTurn.IndexTurn - 1];
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
            Player.GetComponent<ControlTokensPlayer>().RedToken = 0;
            Player.GetComponent<ControlTokensPlayer>().GreenToken = 0;
            Player.GetComponent<ControlTokensPlayer>().BlueToken = 0;
            Player.GetComponent<ControlTokensPlayer>().YellowToken = 0;
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
