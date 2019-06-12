using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controla el numero de tokens obtenidos por un jugador en una ronda
/// </summary>
public class ControlTokensPlayer : Photon.PunBehaviour
{

    [Header("FeedBack visual de token obtenido")]
    [SerializeField] private GameObject _token;
    private List<Square.typesSquares> _obtainedTokens = new List<Square.typesSquares>();


    private int _redToken = 0;
    private int _blueToken = 0;
    private int _greenToken = 0;
    private int _yellowToken = 0;
    private int _indexSquare = 0;
    private int _numberTokens = 0;
    private GameObject _portraitThatRepresents;  //marco que muestra la informacion de los otros players

    private PlayerDataInGame _playerDataInGame;
    private ControlTurn _controlTurn;
    private ControlTokens _controlTokens;

    private void Start()
    {
        _playerDataInGame = FindObjectOfType<PlayerDataInGame>();
        _controlTurn = FindObjectOfType<ControlTurn>();
        _controlTokens = FindObjectOfType<ControlTokens>();
    }

    /// <summary>
    /// decide a que contodor aumentarle uan gema de pendiendo del enum enviado
    /// </summary>
    /// <param name="typesSquares"></param>
    [PunRPC]
    public void newToken(Square.typesSquares typesSquares)
    {
        ObtainedTokens.Add(typesSquares);
        switch (typesSquares)
        {
            case Square.typesSquares.BLUE:
                _blueToken++;
                _controlTokens.TotalBlueTokens++;
                break;

            case Square.typesSquares.GREEN:
                _greenToken++;
                _controlTokens.TotalGreenTokens++;
                break;

            case Square.typesSquares.RED:
                _redToken++;
                _controlTokens.TotalRedTokens++;
                break;

            case Square.typesSquares.YELLOW:
                _yellowToken++;
                _controlTokens.TotalYellowTokens++;
                break;

            default:
                Debug.Log("salio del rango " + _indexSquare);
                break;
        }

        NumberTokens++;

        GameObject aux = Instantiate(_token); // crea un gema y la guarda en una variable
        aux.GetComponent<CollectedToken>().Player = _playerDataInGame.CharactersInGame[_controlTurn.IndexTurn - 1].Character;

        if (!FindObjectOfType<ControlTurn>().MyTurn)
            PortraitThatRepresents.GetComponent<OthersPlayersData>().getToken(typesSquares);

    }

    [PunRPC]
    public void loseToken(int index)
    {
        switch (ObtainedTokens[index])
        {
            case Square.typesSquares.BLUE:
                BlueToken--;
                break;

            case Square.typesSquares.GREEN:
                GreenToken--;
                break;

            case Square.typesSquares.RED:
                RedToken--;
                break;

            case Square.typesSquares.YELLOW:
                YellowToken--;
                break;
        }

        if (photonView.isMine)
        {
            FindObjectOfType<ControlTokens>().lostTokenForMy(index);
        }
        else
        {
            OthersPlayersData[] aux = FindObjectsOfType<OthersPlayersData>();
            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].IdOfThePlayerThatRepresents == GetComponent<PlayerMove>().IdOwner.ID)
                {
                    aux[i].loseToken(index);
                }
            }
        }

       
    }

    public int RedToken
    {
        get
        {
            return _redToken;
        }

        set
        {
            _redToken = value;
        }
    }

    public int BlueToken
    {
        get
        {
            return _blueToken;
        }

        set
        {
            _blueToken = value;
        }
    }

    public int YellowToken
    {
        get
        {
            return _yellowToken;
        }

        set
        {
            _yellowToken = value;
        }
    }

    public int GreenToken
    {
        get
        {
            return _greenToken;
        }

        set
        {
            _greenToken = value;
        }
    }

    public GameObject PortraitThatRepresents
    {
        get
        {
            return _portraitThatRepresents;
        }

        set
        {
            _portraitThatRepresents = value;
        }
    }

    public List<Square.typesSquares> ObtainedTokens
    {
        get
        {
            return _obtainedTokens;
        }

        set
        {
            _obtainedTokens = value;
        }
    }

    public int NumberTokens
    {
        get
        {
            return _numberTokens;
        }

        set
        {
            _numberTokens = value;
        }
    }
}
