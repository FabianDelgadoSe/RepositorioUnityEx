using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourGhost : Photon.PunBehaviour
{
    [SerializeField] private GameObject _square;
    [SerializeField] private GameObject _panelLseToken;
    private float _time = 0;
    private Arrow.adress _direction;
    private int _steps;
    private bool _move = false;
    private float _rapeVelocity;
    private Vector3 _starPoint;
    private Vector3 _endPoint;
    private const float VELOCITY_MOVE = 5;
    private int _numberPlayerTouched = 0;
    private GameObject _playerDrag;
    private bool _sleep = true;
    private ControlTurn _controlTurn;


    private void Start()
    {
        _controlTurn = FindObjectOfType<ControlTurn>();
    }

    private void Update()
    {
        Debug.Log("numero de players tocados " + _numberPlayerTouched);

        if (_move)
        {
            _time += Time.deltaTime * _rapeVelocity;
            transform.position = Vector3.Lerp(_starPoint, _endPoint, _time);

            if (_time >= 1)
            {

                _time = 0;
                move();

            }
        }
    }

    public void selectDirection()
    {
        int auxNumber = Random.Range(1, 5);
        switch (auxNumber)
        {
            case 1:
                Direction = Arrow.adress.DOWN;
                break;

            case 2:
                Direction = Arrow.adress.LEFT;
                break;

            case 3:
                Direction = Arrow.adress.RIGHT;
                break;

            case 4:
                Direction = Arrow.adress.UP;
                break;
        }

        photonView.RPC("loadBrainGhost", PhotonTargets.AllBuffered, Direction);
    }

    [PunRPC]
    private void loadBrainGhost(Arrow.adress adress)
    {

            Direction = adress;
            Steps = 2;
            _sleep = false;
            move();
        

    }

    public void move()
    {
        if (!_sleep)
        {
            if (Steps >= 1)
            {
                _starPoint = transform.position;
                GetComponent<Animator>().SetBool("Move", true);

                switch (Direction)
                {
                    case Arrow.adress.DOWN:
                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareDown != null)
                        {

                            // le quita un token si no se mueve con normalidad
                            if (Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsOccupied)
                            {
                                if (Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player.GetComponent<ControlTokensPlayer>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }

                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }

                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (Square.GetComponent<Square>()._squareDown.GetComponent<Square>().HaveBait)
                            {
                                Destroy(Square.GetComponent<Square>()._squareDown.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;

                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;
                                Square.GetComponent<Square>().IsOccupied = false;
                            }

                            _endPoint = Square.GetComponent<Square>()._squareDown.transform.position;
                            _move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;


                            Square = Square.GetComponent<Square>()._squareDown;
                            Steps--;

                        }
                        else
                        {
                            // atravezar la pared

                            if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().IsOccupied)
                            {
                                if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<ControlTokensPlayer>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }
                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().HaveBait)
                            {
                                Destroy(Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;

                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;
                                Square.GetComponent<Square>().IsOccupied = false;
                            }
                            _move = true;

                            Square = Square.GetComponent<Square>().OppositeSquareY;
                            transform.position = Square.transform.position;
                            Steps--;
                            Invoke("reactiveGhost", 1);
                        }
                        break;

                    case Arrow.adress.LEFT:
                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareLeft != null)
                        {
                            // le quita un token si no se mueve con normalidad
                            if (Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().IsOccupied)
                            {

                                if (Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player.GetComponent<ControlTokensPlayer>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }
                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().HaveBait)
                            {
                                Destroy(Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;
                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;
                                Square.GetComponent<Square>().IsOccupied = false;
                            }


                            _endPoint = Square.GetComponent<Square>()._squareLeft.transform.position;
                            _move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;

                            Square = Square.GetComponent<Square>()._squareLeft;

                            Steps--;

                        }
                        else
                        {
                            // atravezar la pared
                            if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().IsOccupied)
                            {
                                if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<ControlTokensPlayer>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }
                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().HaveBait)
                            {
                                Destroy(Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;
                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;
                                Square.GetComponent<Square>().IsOccupied = false;

                            }

                            _move = false;
                            Square = Square.GetComponent<Square>().OppositeSquareX;
                            transform.position = Square.transform.position;
                            Steps--;
                            Invoke("reactiveGhost", 1);
                        }
                        break;

                    case Arrow.adress.RIGHT:
                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareRigh != null)
                        {
                            // le quita un token si no se mueve con normalidad
                            if (Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().IsOccupied)
                            {
                                if (Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player.GetComponent<ControlTokensPlayer>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }
                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().HaveBait)
                            {
                                Destroy(Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;

                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;

                                Square.GetComponent<Square>().IsOccupied = false;
                            }

                            _endPoint = Square.GetComponent<Square>()._squareRigh.transform.position;
                            _move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;

                            Square = Square.GetComponent<Square>()._squareRigh;

                            Steps--;

                        }
                        else
                        {
                            // atravezar la pared
                            if (_square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().IsOccupied)
                            {
                                if (_square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (_square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = _square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }
                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (_square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().HaveBait)
                            {
                                Destroy(_square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;
                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;
                                _square.GetComponent<Square>().IsOccupied = false;
                            }

                            _move = false;

                            Square = Square.GetComponent<Square>().OppositeSquareX;
                            transform.position = Square.transform.position;
                            Steps--;
                            Invoke("reactiveGhost", 1);
                        }
                        break;

                    case Arrow.adress.UP:
                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareUp != null)
                        {
                            // le quita un token si no se mueve con normalidad
                            if (Square.GetComponent<Square>()._squareUp.GetComponent<Square>().IsOccupied)
                            {
                                if (Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player.GetComponent<ControlTokensPlayer>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }
                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (Square.GetComponent<Square>()._squareUp.GetComponent<Square>().HaveBait)
                            {
                                Destroy(Square.GetComponent<Square>()._squareUp.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;

                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;
                                Square.GetComponent<Square>().IsOccupied = false;
                            }


                            _endPoint = Square.GetComponent<Square>()._squareUp.transform.position;
                            _move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;

                            Square = Square.GetComponent<Square>()._squareUp;

                            Steps--;

                        }
                        else
                        {
                            // atravezar la pared
                            if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().IsOccupied)
                            {
                                if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<ControlTokensPlayer>().NumberTokens > 0)
                                {
                                    _numberPlayerTouched++;
                                    GetComponent<Animator>().SetBool("Laughter", true);
                                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LOSE_TOKEN);
                                    if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine)
                                    {
                                        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.EMPTY);
                                        _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player;
                                        _panelLseToken.SetActive(true);
                                    }
                                }
                                else
                                {
                                    GetComponent<Animator>().SetBool("Laughter", false);
                                }

                                Steps++;
                            }
                            else
                            {
                                GetComponent<Animator>().SetBool("Laughter", false);
                            }

                            // destruye un cebo si lo toca
                            if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().HaveBait)
                            {
                                Destroy(Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().BaitInGame);
                            }

                            _square.GetComponent<Square>().HasBoss = false;

                            if (_square.GetComponent<Square>().Player == gameObject)
                            {
                                _square.GetComponent<Square>().Player = null;
                                Square.GetComponent<Square>().IsOccupied = false;
                            }

                            _move = false;

                            Square = Square.GetComponent<Square>().OppositeSquareY;
                            transform.position = Square.transform.position;
                            Steps--;
                            Invoke("reactiveGhost", 1);
                        }
                        break;
                }
            }
            else
            {
                _move = false;
                _sleep = true;

                GetComponent<Animator>().SetBool("Move", false);
                GetComponent<Animator>().SetBool("Sleep", true);

                Square.GetComponent<Square>().Player = gameObject;
                Square.GetComponent<Square>().IsOccupied = true;
                Square.GetComponent<Square>().HasBoss = true;

                if (FindObjectOfType<ControlTurn>().MyTurn)
                {
                    if (_numberPlayerTouched == 0)
                    {
                        FindObjectOfType<ControlTurn>().MyTurn = false;
                        FindObjectOfType<ControlTurn>().photonView.RPC("mineTurn", PhotonTargets.AllBuffered, FindObjectOfType<ControlTurn>().IndexTurn);
                    }
                }

            }
        }

        else
        {
            if (Steps >= 1)
            {
                _starPoint = transform.position;

                switch (Direction)
                {
                    case Arrow.adress.DOWN:

                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareDown != null && !Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsWall)
                        {

                            if (!Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsOccupied)
                            {
                                //se mueve normalito
                                _square.GetComponent<Square>().HasBoss = false;
                                _square.GetComponent<Square>().Player = null;
                                _square.GetComponent<Square>().IsOccupied = false;
                                _endPoint = Square.GetComponent<Square>()._squareDown.transform.position;
                                _move = true;
                                _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                                Square = Square.GetComponent<Square>()._squareDown;
                                Steps--;
                            }
                            else
                            {
                                //tendra que empujar alque tiene al frente
                            }
                        }
                        else
                        {
                            //si no hay a donde moverse termiane el movimiento
                            Steps--;
                            _move = false;
                            _sleep = true;

                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;
                            Square.GetComponent<Square>().HasBoss = true;

                            if (PlayerDrag != null)
                            {
                                PlayerDrag.GetComponent<PlayerMove>().lostPoints();
                                FindObjectOfType<ControlRound>().AllowMove = false;
                                if (_controlTurn.MyTurn)
                                {
                                    FindObjectOfType<ControlRound>().useLetter();
                                    FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);
                                    _controlTurn.AllowToPlaceBait = true;
                                }


                                PlayerDrag = null;
                            }
                        }
                        break;

                    case Arrow.adress.LEFT:

                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareLeft != null && !Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().IsWall)
                        {

                            if (!Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().IsOccupied)
                            {
                                //se mueve normalito
                                _square.GetComponent<Square>().HasBoss = false;
                                _square.GetComponent<Square>().Player = null;
                                _square.GetComponent<Square>().IsOccupied = false;
                                _endPoint = Square.GetComponent<Square>()._squareLeft.transform.position;
                                _move = true;
                                _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                                Square = Square.GetComponent<Square>()._squareLeft;
                                Steps--;
                            }
                            else
                            {
                                //tendra que empujar alque tiene al frente
                            }
                        }
                        else
                        {
                            //si no hay a donde moverse termiane el movimiento
                            Steps--;
                            _move = false;
                            _sleep = true;

                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;
                            Square.GetComponent<Square>().HasBoss = true;

                            if (PlayerDrag != null)
                            {
                                PlayerDrag.GetComponent<PlayerMove>().lostPoints();

                                if (_controlTurn.MyTurn)
                                {
                                    FindObjectOfType<ControlRound>().useLetter();
                                    FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);
                                    _controlTurn.AllowToPlaceBait = true;
                                }

                                PlayerDrag = null;
                            }
                        }
                        break;

                    case Arrow.adress.RIGHT:

                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareRigh != null && !Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().IsWall)
                        {

                            if (!Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().IsOccupied)
                            {
                                //se mueve normalito
                                _square.GetComponent<Square>().HasBoss = false;
                                _square.GetComponent<Square>().Player = null;
                                _square.GetComponent<Square>().IsOccupied = false;
                                _endPoint = Square.GetComponent<Square>()._squareRigh.transform.position;
                                _move = true;
                                _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                                Square = Square.GetComponent<Square>()._squareRigh;
                                Steps--;
                            }
                            else
                            {
                                //tendra que empujar alque tiene al frente
                            }
                        }
                        else
                        {
                            //si no hay a donde moverse termiane el movimiento
                            Steps--;
                            _move = false;
                            _sleep = true;

                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;
                            Square.GetComponent<Square>().HasBoss = true;

                            if (PlayerDrag != null)
                            {
                                PlayerDrag.GetComponent<PlayerMove>().lostPoints();
                                FindObjectOfType<ControlRound>().AllowMove = false;

                                if (_controlTurn.MyTurn)
                                {
                                    FindObjectOfType<ControlRound>().useLetter();
                                    FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);
                                    _controlTurn.AllowToPlaceBait = true;
                                }

                                PlayerDrag = null;
                            }
                        }
                        break;

                    case Arrow.adress.UP:

                        // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                        if (Square.GetComponent<Square>()._squareUp != null && !Square.GetComponent<Square>()._squareUp.GetComponent<Square>().IsWall)
                        {

                            if (!Square.GetComponent<Square>()._squareUp.GetComponent<Square>().IsOccupied)
                            {
                                //se mueve normalito
                                _square.GetComponent<Square>().HasBoss = false;
                                _square.GetComponent<Square>().Player = null;
                                _square.GetComponent<Square>().IsOccupied = false;
                                _endPoint = Square.GetComponent<Square>()._squareUp.transform.position;
                                _move = true;
                                _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                                Square = Square.GetComponent<Square>()._squareUp;
                                Steps--;
                            }
                            else
                            {
                                //tendra que empujar alque tiene al frente
                            }
                        }
                        else
                        {
                            //si no hay a donde moverse termiane el movimiento
                            Steps--;
                            _move = false;
                            _sleep = true;

                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;
                            Square.GetComponent<Square>().HasBoss = true;

                            if (PlayerDrag != null)
                            {
                                PlayerDrag.GetComponent<PlayerMove>().lostPoints();
                                FindObjectOfType<ControlRound>().AllowMove = false;

                                if (_controlTurn.MyTurn)
                                {
                                    FindObjectOfType<ControlRound>().useLetter();
                                    FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);
                                    _controlTurn.AllowToPlaceBait = true;
                                }
                                PlayerDrag = null;
                            }
                        }
                        break;
                }
            }
            else
            {

                _move = false;
                _sleep = true;
                Square.GetComponent<Square>().Player = gameObject;
                Square.GetComponent<Square>().IsOccupied = true;
                Square.GetComponent<Square>().HasBoss = true;

                if (PlayerDrag != null)
                {
                    PlayerDrag.GetComponent<PlayerMove>().calculatePointToMove();
                    PlayerDrag = null;
                }

            }
        }

    }

    [PunRPC]
    public void loadnumberplayerTouch()
    {
        _numberPlayerTouched--;

        if (FindObjectOfType<ControlTurn>().MyTurn)
        {
            if (_numberPlayerTouched == 0)
            {
                FindObjectOfType<ControlTurn>().MyTurn = false;
                FindObjectOfType<ControlTurn>().photonView.RPC("mineTurn", PhotonTargets.AllBuffered, FindObjectOfType<ControlTurn>().IndexTurn);
            }
        }
    }

    public void reactiveGhost()
    {
        move();
    }

    public GameObject Square
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

    public GameObject PlayerDrag
    {
        get
        {
            return _playerDrag;
        }

        set
        {
            _playerDrag = value;
        }
    }

    public int Steps
    {
        get
        {
            return _steps;
        }

        set
        {
            _steps = value;
        }
    }

    public Arrow.adress Direction
    {
        get
        {
            return _direction;
        }

        set
        {
            _direction = value;
        }
    }

    public GameObject Square1
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
