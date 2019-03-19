using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBait : MonoBehaviour
{
    private GameObject _bait;
    private bool _foundSquare = true;
    private GameObject _square;

   


    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        if (Input.GetMouseButtonUp(0)) {
            if (!_square) {
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

                Debug.Log("Found Square: " + _foundSquare);
                Debug.Log("IsOcupped: " + !_square.GetComponent<Square>().IsOccupied);
                Debug.Log("IsWall: " + !_square.GetComponent<Square>().IsWall);
                Debug.Log("HaveBait: " + !_square.GetComponent<Square>().HaveBait);

                if (_foundSquare && !_square.GetComponent<Square>().IsOccupied && !_square.GetComponent<Square>().IsWall && !_square.GetComponent<Square>().HaveBait)
                {//SE PUEDE

                    _square.GetComponent<Square>().photonView.RPC("generateBait", PhotonTargets.All, _bait.GetComponent<Bait>().BaitType1);
                }
                else
                {
                    _bait.SetActive(true);
                }
                Destroy(gameObject);

            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On Trigger con: " + collision.name);

        if (collision.CompareTag("Square"))
        {
            _foundSquare = true;
            _square = collision.gameObject;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Input.GetMouseButtonUp(0))
        {
            if (collision.CompareTag("Square"))
            {
                _foundSquare = false;
                _square = null;

            }
        }
    }*/


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
}
