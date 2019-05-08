using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCard : MonoBehaviour
{

    private GameObject _card;
    private int _numberSteps;
    private bool _foundPlayer = true;
    private GameObject _player;
    public GameObject Card
    {
        get
        {
            return _card;
        }

        set
        {
            _card = value;
        }
    }


    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

        if (Input.GetMouseButtonUp(0))
        {
            if (!_foundPlayer && !_player.GetComponent<PlayerMove>().isMyPlayer())
            {
                Card.GetComponent<Card>().deselectCard(false);

                _player.GetComponent<PlayerMove>().createArrows(NumberSteps);
                _player.GetComponent<PlayerMove>().Card = Card;
                _player.transform.localScale = new Vector3(0.5f, 0.5f, 0);
            }
            else
            {
                Card.GetComponent<Card>().deselectCard(true);
            }
            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerMove>().IdOwner != PhotonNetwork.player)
            {

                if (_player)
                {
                    if (_player != collision)
                    {
                        _player.transform.localScale = new Vector3(0.5f, 0.5f, 0);

                        _foundPlayer = false;
                        _player = collision.gameObject;
                        _player.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                    }
                }
                else
                {
                    _foundPlayer = false;
                    _player = collision.gameObject;
                    _player.transform.localScale = new Vector3(0.7f, 0.7f, 0);
                }

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Input.GetMouseButtonUp(0))
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.gameObject == _player)
                {
                    _foundPlayer = true;
                    _player.transform.localScale = new Vector3(0.5f, 0.5f, 0);
                    _player = null;
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (_player)
        {
            _player.transform.localScale = new Vector3(0.5f, 0.5f, 0);
        }
    }

    public int NumberSteps
    {
        get
        {
            return _numberSteps;
        }

        set
        {
            _numberSteps = value;
        }
    }
}
