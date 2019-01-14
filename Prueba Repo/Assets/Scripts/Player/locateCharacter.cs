using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class locateCharacter : Photon.PunBehaviour
{

    public bool _isMoving = false;

    private void Start()
    {
        
        if (!photonView.isMine)
        {
            Destroy(gameObject.GetComponent<locateCharacter>());
        }
        
    }

    private void OnMouseDrag()
    {

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        _isMoving = true;

    }

    private void OnMouseUp()
    {

        _isMoving = false;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            if (collision.gameObject.GetComponent<Square>().ItsOnEdge && !_isMoving)
            {
                transform.position = collision.gameObject.transform.position;
                Destroy(gameObject.GetComponent<locateCharacter>());
            }
        }
    }

   

   
}
