using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTokensPlayer : Photon.PunBehaviour {

    public int _redToken = 0;
    public int _blueToken = 0;
    public int _greenToken = 0;
    public int _yellowToken = 0;
    public int _indexSquare = 0;
    private GameObject _playerData;
    private GameObject _controlTurn;
    private GameObject _player;

    private void Start()
    {
        _playerData = FindObjectOfType<PlayerData>().gameObject;
        _controlTurn = FindObjectOfType<ControlTurn>().gameObject;
    }

    public void newToken()
    {

        switch (GetComponent<PlayerMove>().Square.GetComponent<Square>().Index)
        {
            case 1:
                _blueToken++;
    
                break;

            case 2:
                _greenToken++;
   
                break;

            case 3:
                _redToken++;

                break;

            case 4:
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
