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
    private float _time = 0;

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
                GetComponent<Animator>().SetBool("Good",true);
                break;

            case Bait.typeBait.POOP:
                GetComponent<SpriteRenderer>().sprite = _poop;
                GetComponent<Animator>().SetBool("Bad", true);
                break;
        }
    }


    public void behaviourCoin()
    {
        if (_typeBait == Bait.typeBait.COIN) {
            _checkPlayerInBait = false;

            if (Square.Player != null) {

                //si el player que quedo sobre la casilla soy yo gano un cebo bueno
                if (_square.Player.GetComponent<PlayerMove>().IdOwner == PhotonNetwork.player)
                {
                    _controlBait.NumberBaitCoin++;
                    _controlBait.changeUINumberBaits(_typeBait);
                    SSTools.ShowMessage("Ganaste un cebo moneda", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }

                if (_controlTurn.MyTurn)
                {
                    // solo lo el feekback visual de que gano punto
                    SSTools.ShowMessage("Ganaste un punto ", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }

                _playerDataInGame.CharactersInGame[_controlTurn.IndexTurn - 1].Score++;

                _square.Player.GetComponent<Animator>().SetBool("Coin", true);
                Invoke("finishTakeCoin", 1);

                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void Update()
    {
        if (_time <= 6)
        {
            _time += Time.deltaTime;
        }
        else
        {
            _time = 0;
        }
        GetComponent<Animator>().SetFloat("Time",_time);
    }

    public void finishTakeCoin()
    {
        if (_typeBait == Bait.typeBait.COIN)
            _square.Player.GetComponent<Animator>().SetBool("Coin", false);
        else
        {
            _square.Player.GetComponent<Animator>().SetBool("Poop", false);
        }
        _square.GetComponent<Square>().HaveBait = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_typeBait == Bait.typeBait.POOP) {
            if (collision.CompareTag("Player") && collision.gameObject == _square.Player)
            {
                _square.Player.GetComponent<Animator>().SetBool("Poop", true);
                Invoke("finishTakeCoin", 1);

                GetComponent<SpriteRenderer>().enabled = false;


                if (_square.Player.GetComponent<PlayerMove>().IdOwner == PhotonNetwork.player)
                {
                    _controlBait.NumberBaitPoop++;
                    _controlBait.changeUINumberBaits(_typeBait);
                    SSTools.ShowMessage("Ganaste un cebo popo", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }

                _playerDataInGame.CharactersInGame[_controlTurn.IndexTurn - 1].Score--;

                if (_controlTurn.MyTurn) {
                    // solo el feekback visual de que perdio puntos
                    FindObjectOfType<MyLostPoint>().NumberLostPoint++;
                    FindObjectOfType<MyLostPoint>().visualLostToken();
                }
                else
                {
                    OthersPlayersData[] aux = FindObjectsOfType<OthersPlayersData>();

                    for (int i = 0; i < aux.Length; i++)
                    {
                        if (aux[i].IdOfThePlayerThatRepresents == FindObjectOfType<ControlTurn>().IndexTurn)
                        {
                            aux[i].NumberLostPoint++;
                            aux[i].visualLostToken();
                        }
                    }
                }


                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        Square.GetComponent<Square>().HaveBait = false;
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
