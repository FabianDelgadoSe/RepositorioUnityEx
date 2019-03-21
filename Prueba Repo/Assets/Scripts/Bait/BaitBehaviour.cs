using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitBehaviour : Photon.MonoBehaviour
{
    
    [SerializeField] private Sprite _coin;
    [SerializeField] private Sprite _poop;
    private Bait.typeBait _typeBait;
    private GameObject _firstCharacter;
    private ControlRound _controlRound;
    private ControlTurn _controlTurn;
    private PlayerDataInGame _playerDataInGame;
    private ControlBait _controlBait;
    private bool _checkPlayerInBait = true;
    private Square _square; //lugar donde esta parado

    private void Start()
    {
        _controlBait = FindObjectOfType<ControlBait>();
        _controlRound = FindObjectOfType<ControlRound>();
        _controlTurn = FindObjectOfType<ControlTurn>();
        _playerDataInGame = FindObjectOfType<PlayerDataInGame>();

        switch (_typeBait)
        {
            case Bait.typeBait.COIN:
                GetComponent<SpriteRenderer>().sprite = _coin;
                break;

            case Bait.typeBait.POOP:
                GetComponent<SpriteRenderer>().sprite = _poop;
                break;
        }
    }


    public void behaviourCoin()
    {
        _checkPlayerInBait = false;

        if(Square.Player != null){

            //si el player que quedo sobre la casilla soy yo gano un cebo bueno
            if (_square.Player.GetComponent<PlayerMove>().IdOwner == PhotonNetwork.player)
            {
                _controlBait.NumberBaitCoin++;
                _controlBait.changeUINumberBaits(_typeBait);
                SSTools.ShowMessage("Ganaste un cebo moneda",SSTools.Position.bottom,SSTools.Time.threeSecond);
            }

            if (_controlTurn.MyTurn)
            {
                _playerDataInGame.CharactersInGame[_controlTurn.IndexTurn-1].Score++;
                SSTools.ShowMessage("Ganaste un punto ", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_typeBait == Bait.typeBait.POOP) {
            if (collision.CompareTag("Player") && collision.gameObject == _square.Player)
            {
                if (_square.Player.GetComponent<PlayerMove>().IdOwner == PhotonNetwork.player)
                {
                    _controlBait.NumberBaitPoop++;
                    _controlBait.changeUINumberBaits(_typeBait);
                    SSTools.ShowMessage("Ganaste un cebo popo", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }

                _playerDataInGame.CharactersInGame[_controlTurn.IndexTurn - 1].Score--;

                if (_controlTurn.MyTurn) {
                    SSTools.ShowMessage("Perdiste un punto", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }
                Destroy(gameObject);
            }
        }
    }


    public Bait.typeBait TypeBait
    {
        get
        {
            return _typeBait;
        }

        set
        {
            _typeBait = value;
        }
    }

    public Square Square
    {
        get
        {
            return _square;
        }

        set
        {
            _square = value;
        }
    }
}
