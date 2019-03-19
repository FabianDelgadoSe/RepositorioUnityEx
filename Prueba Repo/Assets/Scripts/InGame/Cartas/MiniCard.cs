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

                _player.GetComponent<PlayerMove>().photonView.RPC("receiveNumberOfSteps", PhotonTargets.All, _numberSteps);
                _player.GetComponent<PlayerMove>().createMovementDirections();

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
            _foundPlayer = false;
            _player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Input.GetMouseButtonUp(0))
        {
            if (collision.CompareTag("Player"))
            {
                _foundPlayer = true;
                _player = null;

            }
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
