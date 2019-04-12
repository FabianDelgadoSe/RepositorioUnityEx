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

    private void Update()
    {
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
                _direction = Arrow.adress.DOWN;
                break;

            case 2:
                _direction = Arrow.adress.LEFT;
                break;

            case 3:
                _direction = Arrow.adress.RIGHT;
                break;

            case 4:
                _direction = Arrow.adress.UP;
                break;
        }

        photonView.RPC("loadBrainGhost", PhotonTargets.AllBuffered, _direction);
    }

    [PunRPC]
    private void loadBrainGhost(Arrow.adress adress)
    {
        _direction = adress;
        _steps = 2;
        move();
    }

    private void move()
    {
        if (_steps >= 1)
        {
            _starPoint = transform.position;

            switch (_direction)
            {
                case Arrow.adress.DOWN:
                    // si no tiene nada en esa direccion se debe teletransportar al otro lado si no se mueve normal
                    if (Square.GetComponent<Square>()._squareDown != null)
                    {
                        // le quita un token si no se mueve con normalidad
                        if (Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsOccupied)
                        {
                            if (Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>()._squareDown.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareDown.GetComponent<Square>().BaitInGame);
                        }

                        _endPoint = Square.GetComponent<Square>()._squareDown.transform.position;
                        _move = true;
                        _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                        Square = Square.GetComponent<Square>()._squareDown;
                        _steps--;

                    }
                    else
                    {
                        // atravezar la pared

                        if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().IsOccupied)
                        {
                            if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareDown.GetComponent<Square>().BaitInGame);
                        }
                        _move = true;
                        Square = Square.GetComponent<Square>().OppositeSquareY;
                        transform.position = Square.transform.position;
                        _steps--;
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
                            if (Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().BaitInGame);
                        }

                        _endPoint = Square.GetComponent<Square>()._squareLeft.transform.position;
                        _move = true;
                        _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;

                        Square = Square.GetComponent<Square>()._squareLeft;

                        _steps--;

                    }
                    else
                    {
                        // atravezar la pared
                        if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().IsOccupied)
                        {
                            if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareDown.GetComponent<Square>().BaitInGame);
                        }
                        _move = false;
                        Square = Square.GetComponent<Square>().OppositeSquareX;
                        transform.position = Square.transform.position;
                        _steps--;
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
                            if (Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().BaitInGame);
                        }

                        _endPoint = Square.GetComponent<Square>()._squareRigh.transform.position;
                        _move = true;
                        _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;

                        Square = Square.GetComponent<Square>()._squareRigh;

                        _steps--;

                    }
                    else
                    {
                        // atravezar la pared
                        if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().IsOccupied)
                        {
                            if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>().OppositeSquareX.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareDown.GetComponent<Square>().BaitInGame);
                        }

                        _move = false;
                        Square = Square.GetComponent<Square>().OppositeSquareX;
                        transform.position = Square.transform.position;
                        _steps--;
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
                            if (Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens > 0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>()._squareUp.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareUp.GetComponent<Square>().BaitInGame);
                        }

                        _endPoint = Square.GetComponent<Square>()._squareUp.transform.position;
                        _move = true;
                        _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;

                        Square = Square.GetComponent<Square>()._squareUp;

                        _steps--;

                    }
                    else
                    {
                        // atravezar la pared
                        if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().IsOccupied)
                        {
                            if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<PhotonView>().isMine &&
                                Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player.GetComponent<ControlTokens>().NumberTokens>0)
                            {
                                _numberPlayerTouched++;
                                _panelLseToken.SetActive(true);
                                _panelLseToken.GetComponent<LoseTokens>().Player = Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().Player;
                            }
                            _steps++;
                        }

                        // destruye un cebo si lo toca
                        if (Square.GetComponent<Square>().OppositeSquareY.GetComponent<Square>().HaveBait)
                        {
                            Destroy(Square.GetComponent<Square>()._squareDown.GetComponent<Square>().BaitInGame);
                        }
                        _move = false;
                        Square = Square.GetComponent<Square>().OppositeSquareY;
                        transform.position = Square.transform.position;
                        _steps--;
                        Invoke("reactiveGhost", 1);
                    }
                    break;
            }
        }
        else
        {
            _move = false;
            if (FindObjectOfType<ControlTurn>().MyTurn)
            {
                if (_numberPlayerTouched<=0)
                {
                    FindObjectOfType<ControlTurn>().MyTurn = false;
                    FindObjectOfType<ControlTurn>().photonView.RPC("mineTurn", PhotonTargets.AllBuffered, FindObjectOfType<ControlTurn>().IndexTurn);
                }
            }
               
        }
    }

    [PunRPC]
    public void loadnumberplayerTouch()
    {
        _numberPlayerTouched--;
        move();
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
}
