﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBait : MonoBehaviour
{
    private GameObject _bait;
    private bool _foundSquare = true;
    private GameObject _square;
    public Bait.typeBait _typeBait;
    private bool _createdBait = false;


    private void Start()
    {
        switch (_typeBait)
        {
            case global::Bait.typeBait.COIN:
                GetComponent<ParticleSystem>().startColor = Color.green;
                break;

            case global::Bait.typeBait.POOP:
                GetComponent<ParticleSystem>().startColor = Color.red;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        if (Input.GetMouseButtonUp(0))
        {
            if (!_square)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            if (Input.GetMouseButtonUp(0))
            {
                _square = collision.gameObject;
                if (_foundSquare && !_square.GetComponent<Square>().IsOccupied && !_square.GetComponent<Square>().IsWall && !_square.GetComponent<Square>().HaveBait)
                {//SE PUEDE
                    if (FindObjectOfType<ControlTurn>().AllowToPlaceBait)
                    {
                        _createdBait = true;
                        FindObjectOfType<ControlTurn>().AllowToPlaceBait = false;
                        _square.GetComponent<Square>().photonView.RPC("generateBait", PhotonTargets.All, _typeBait);
                    }
                }
                Destroy(gameObject);

            }

        }
    }

    private void OnDestroy()
    {
        if (!_createdBait)
        {
            switch (_typeBait)
            {
                case global::Bait.typeBait.COIN:
                    FindObjectOfType<ControlBait>().NumberBaitCoin++;
                    break;

                case global::Bait.typeBait.POOP:
                    FindObjectOfType<ControlBait>().NumberBaitPoop++;
                    break;
            }
            FindObjectOfType<ControlBait>().changeUINumberBaits(_typeBait);
        }
    }


    //GET SETS

    public GameObject Bait
    {
        get
        {
            return _bait;
        }

        set
        {
            _bait = value;
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
}