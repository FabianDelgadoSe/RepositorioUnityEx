using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTokensPlayer : Photon.PunBehaviour {

    private int _redToken = 0;
    private int _blueToken = 0;
    private int _greenToken = 0;
    private int _yellowToken = 0;
    private int _indexSquare = 0;
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

        _player = _playerData.GetComponent<PlayerData>().CharactersInGame[_controlTurn.GetComponent<ControlTurn>().IndexTurn-1];

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

        
        if (_player.GetComponent<PhotonView>().isMine)
            FindObjectOfType<ControlTokens>().drawMyTokensValues();
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
