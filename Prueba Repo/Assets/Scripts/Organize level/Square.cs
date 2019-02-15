using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Esta clase tiene el proposito configurar cada cuadrado
/// </summary>
public class Square : Photon.PunBehaviour
{

    private Sprite _boardSquareBlue;
    private Sprite _boardSquareRed;
    private Sprite _boardSquareYellow;
    private Sprite _boardSquareGreen;
    private Sprite _boardSquareWall;

    private int _index;

    private bool _isWall = false;
    private bool _isEmpty = true;

    public GameObject _player = null;
    public GameObject _squareUp;
    public GameObject _squareDown;
    public GameObject _squareLeft;
    public GameObject _squareRigh;
    private const float COLLIDER_RADIO = 0.2f;
    private PhotonPlayer _playerOwner = null;
    private bool _isOccupied = false;

    [SerializeField] private bool _itsOnEdge;

    private void Start()
    {

        if (Physics2D.OverlapCircle(new Vector2(_squareUp.transform.position.x, _squareUp.transform.position.y), COLLIDER_RADIO) != null)
            _squareUp = Physics2D.OverlapCircle(new Vector2(_squareUp.transform.position.x, _squareUp.transform.position.y), COLLIDER_RADIO).gameObject;
        else
            _squareUp = null;

        if (Physics2D.OverlapCircle(new Vector2(_squareDown.transform.position.x, _squareDown.transform.position.y), COLLIDER_RADIO) != null)
            _squareDown = Physics2D.OverlapCircle(new Vector2(_squareDown.transform.position.x, _squareDown.transform.position.y), COLLIDER_RADIO).gameObject;
        else
            _squareDown = null;

        if (Physics2D.OverlapCircle(new Vector2(_squareLeft.transform.position.x, _squareLeft.transform.position.y), COLLIDER_RADIO) != null)
            _squareLeft = Physics2D.OverlapCircle(new Vector2(_squareLeft.transform.position.x, _squareLeft.transform.position.y), COLLIDER_RADIO).gameObject;
        else
            _squareLeft = null;

        if (Physics2D.OverlapCircle(new Vector2(_squareRigh.transform.position.x, _squareRigh.transform.position.y), COLLIDER_RADIO) != null)
            _squareRigh = Physics2D.OverlapCircle(new Vector2(_squareRigh.transform.position.x, _squareRigh.transform.position.y), COLLIDER_RADIO).gameObject;
        else
            _squareRigh = null;

    }



    [PunRPC]
    public void saveCurrentPlayer(int indexTurn)
    {
        Player = FindObjectOfType<PlayerData>().CharactersInGame[indexTurn - 1];
        FindObjectOfType<PlayerData>().CharactersInGame[indexTurn - 1].GetComponent<PlayerMove>().Square = gameObject;
    }

    [PunRPC]
    public void selectSquare()
    {
        IsOccupied = true;
    }

    /// <summary>
    /// Tiene el proposito de configurar el objeto dependiendo de un index
    /// </summary>
    [PunRPC]
    public void changeSprite()
    {
        switch (Index)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = BoardSquareBlue;
                IsWall = false;
                break;

            case 2:
                GetComponent<SpriteRenderer>().sprite = BoardSquareGreen;
                IsWall = false;
                break;

            case 3:
                GetComponent<SpriteRenderer>().sprite = BoardSquareRed;
                IsWall = false;
                break;

            case 4:
                GetComponent<SpriteRenderer>().sprite = BoardSquareYellow;
                IsWall = false;
                break;

            case 5:
                GetComponent<SpriteRenderer>().sprite = BoardSquareWall;
                IsWall = true;
                break;

            default:
                Debug.Log("salio del rango " + Index);
                break;

        }
    }

    /// <summary>
    /// Recibe los datos de el index y si la casilla es un muro
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Debug.Log("envie algo");
            stream.SendNext(Index);
            stream.SendNext(IsWall);
        }
        else
        {
            Debug.Log("recibi algo");
            Index = (int)stream.ReceiveNext();
            IsWall = (bool)stream.ReceiveNext();

            photonView.RPC("changeSprite", PhotonTargets.All);
        }
    }

    public Sprite BoardSquareBlue
    {
        get
        {
            return _boardSquareBlue;
        }

        set
        {
            _boardSquareBlue = value;
        }
    }

    public Sprite BoardSquareRed
    {
        get
        {
            return _boardSquareRed;
        }

        set
        {
            _boardSquareRed = value;
        }
    }

    public Sprite BoardSquareYellow
    {
        get
        {
            return _boardSquareYellow;
        }

        set
        {
            _boardSquareYellow = value;
        }
    }

    public Sprite BoardSquareGreen
    {
        get
        {
            return _boardSquareGreen;
        }

        set
        {
            _boardSquareGreen = value;
        }
    }

    public Sprite BoardSquareWall
    {
        get
        {
            return _boardSquareWall;
        }

        set
        {
            _boardSquareWall = value;
        }
    }

    public int Index
    {
        get
        {
            return _index;
        }

        set
        {
            _index = value;
        }
    }

    public bool IsWall
    {
        get
        {
            return _isWall;
        }

        set
        {
            _isWall = value;
        }
    }

    public bool ItsOnEdge
    {
        get
        {
            return _itsOnEdge;
        }

        set
        {
            _itsOnEdge = value;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return _isEmpty;
        }

        set
        {
            _isEmpty = value;
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

    public PhotonPlayer PlayerOwner
    {
        get
        {
            return _playerOwner;
        }

        set
        {
            _playerOwner = value;
        }
    }

    public bool IsOccupied
    {
        get
        {
            return _isOccupied;
        }

        set
        {
            _isOccupied = value;
        }
    }
}
