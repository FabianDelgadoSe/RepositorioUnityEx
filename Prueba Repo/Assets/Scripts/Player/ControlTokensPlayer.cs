using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controla el numero de tokens obtenidos por un jugador en una ronda
/// </summary>
public class ControlTokensPlayer : Photon.PunBehaviour {

    public int _redToken = 0;
    public int _blueToken = 0;
    public int _greenToken = 0;
    public int _yellowToken = 0;
    public int _indexSquare = 0;
    private GameObject _portraitThatRepresents;  //marco que muestra la informacion de los otros players

    /// <summary>
    /// decide a que contodor aumentarle uan gema de pendiendo del enum enviado
    /// </summary>
    /// <param name="typesSquares"></param>
    [PunRPC]
    public void newToken(Square.typesSquares typesSquares)
    {

        switch (typesSquares)
        {
            case Square.typesSquares.BLUE:
                _blueToken++;
    
                break;

            case Square.typesSquares.GREEN:
                _greenToken++;
   
                break;

            case Square.typesSquares.RED:
                _redToken++;

                break;

            case Square.typesSquares.YELLOW:
                _yellowToken++;

                break;

            default:
                Debug.Log("salio del rango " + _indexSquare);
                break;
        }

        if(!FindObjectOfType<ControlTurn>().MyTurn)
            PortraitThatRepresents.GetComponent<OthersPlayersData>().getToken(typesSquares);           

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
}
