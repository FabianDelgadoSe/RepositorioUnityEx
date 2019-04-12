using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourGhost : Photon.PunBehaviour
{
    [SerializeField] private GameObject _square;
    private float _time = 0;
    private Arrow.adress _direction;
    private int _steps;
    private bool _move = false;
    private float _rapeVelocity;
    private Vector3 _starPoint;
    private Vector3 _endPoint;

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
                    /*
                    if (Square.GetComponent<Square>()._squareDown != null && !Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsWall)
                    {
                        // si hay un player en la siguiente casilla lo mueve sino yo me muevo normalmente
                        if (!Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsOccupied)
                        {

                        }
                    }*/
                    break;
            }
        }
    }
}
