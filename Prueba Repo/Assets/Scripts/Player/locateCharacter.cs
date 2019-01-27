using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controla el movimiento del personaje cuando sigue el cursor
/// </summary>
public class locateCharacter : Photon.MonoBehaviour
{

    private bool _isMoving = false;
    [SerializeField] private GameObject _panelConfimatio;
    private GameObject aux;
    private bool _panelConfimation = false;
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

                if (aux == null && FindObjectOfType<ControlTurn>().FirstTurn)
                {
                    aux = Instantiate(_panelConfimatio, new Vector3(0, 0, 0), Quaternion.identity);
                    aux.GetComponent<ControlLocationConfirmationPanel>().Player = gameObject;
                    aux.GetComponent<ControlLocationConfirmationPanel>().Square = collision.gameObject;
                }
                
            }
        }
    }


}
