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


    /// <summary>
    /// decide a que contodor aumentarle uan gema
    /// </summary>
    public void newToken()
    {

        switch (GetComponent<PlayerMove>().Square.GetComponent<Square>().EnumTypesSquares)
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

    }



    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            stream.SendNext(_blueToken);
            stream.SendNext(_redToken);
            stream.SendNext(_greenToken);
            stream.SendNext(_yellowToken);
        }
        else
        {
            _blueToken = (int)stream.ReceiveNext();
            _redToken = (int)stream.ReceiveNext();
            _greenToken = (int)stream.ReceiveNext();
            _yellowToken = (int)stream.ReceiveNext();
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
}
