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
    [SerializeField] private GameObject _prefabPanelConfimation;
    private GameObject _panelConfirmationInGame;


    /// <summary>
    /// permite que solo el dispositivo que lo creo pueda moverlo
    /// </summary>
    private void Start()
    {
        
        if (!photonView.isMine)
        {
            Destroy(gameObject.GetComponent<locateCharacter>());
        }
        
    }

    /// <summary>
    /// sigue el cursor cuando se le arrastra este objeto
    /// </summary>
    private void OnMouseDrag()
    {

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.45f));
        _isMoving = true;

    }

    /// <summary>
    /// cuando se levanta el clic deja de seguir el cursor
    /// </summary>
    private void OnMouseUp()
    {

        _isMoving = false;
    }

    /// <summary>
    /// cuando queda sobre una casilla sale el mensaje de confirmacion para saber si esta es la casilla donde quiere iniciar
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            
            if (collision.gameObject.GetComponent<Square>().ItsOnEdge && !_isMoving && !collision.gameObject.GetComponent<Square>().IsOccupied)
            {
                transform.position = collision.gameObject.transform.position;

                if (FindObjectOfType<ControlTurn>().FirstTurn)
                {
                    if (PanelConfirmationInGame == null)
                    {
                        PanelConfirmationInGame = Instantiate(_prefabPanelConfimation, new Vector3(0, 0, 0), Quaternion.identity);
                        PanelConfirmationInGame.GetComponent<ControlLocationConfirmationPanel>().Player = gameObject;
                    }
                    else
                    {
                        PanelConfirmationInGame.SetActive(true);
                    }

                    PanelConfirmationInGame.GetComponent<ControlLocationConfirmationPanel>().Square = collision.gameObject;
                    Destroy(GetComponent<locateCharacter>());
                }
                
            }
        }
    }


    public GameObject PanelConfirmationInGame
    {
        get
        {
            return _panelConfirmationInGame;
        }

        set
        {
            _panelConfirmationInGame = value;
        }
    }


}
