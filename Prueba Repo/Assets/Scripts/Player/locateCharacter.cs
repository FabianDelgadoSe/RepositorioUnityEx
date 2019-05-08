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
    private GameObject _square;
    private Square[] _squares;
    [SerializeField] Color _color;
    private bool _markSquare = true;
    [SerializeField] private GameObject _guide; // flecha que se muentra cuando tienes que ubicar el personaje

    private GameObject[] _selectSquare;

    /// <summary>
    /// permite que solo el dispositivo que lo creo pueda moverlo
    /// </summary>
    private void Start()
    {
        if (!photonView.isMine)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1.8f);
            Destroy(gameObject.GetComponent<locateCharacter>());
            Destroy(Guide);
        }
        else
        {
            _squares = FindObjectsOfType<Square>();
            Guide.SetActive(true);
            if (_markSquare)
                dullSquares();
        }

    }


    /// <summary>
    /// Coloca opacas las casillas que no estan en el borde
    /// </summary>
    private void dullSquares()
    {
        for (int i = 0; i < _squares.Length; i++)
        {
            if (!_squares[i].ItsOnEdge)
            {
                _squares[i].gameObject.GetComponent<SpriteRenderer>().color = _color;
                _squares[i].gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            }
            else
            {
                _squares[i].gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1,1);
            }
        }
    }

    /// <summary>
    /// sigue el cursor cuando se le arrastra este objeto
    /// </summary>
    private void OnMouseDrag()
    {
        Destroy(Guide);
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
    /// revisa si esta en collision con una casilla del borde para proceder a fijarla como la casilla de inicio del player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Square") && !_isMoving)
        {
            if (collision.gameObject.GetComponent<Square>().ItsOnEdge && !collision.gameObject.GetComponent<Square>().IsOccupied)
            {
                if(GameObject.FindWithTag("SelectSquare") != null)
                    _square = GameObject.FindWithTag("SelectSquare").GetComponentInParent<Square>().gameObject;

                transform.position = _square.transform.position;

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

                    PanelConfirmationInGame.GetComponent<ControlLocationConfirmationPanel>().Square = _square;

                    _square.GetComponent<Square>().desactiveVisualFeekbackOfSelectSquare();

                    Destroy(GetComponent<locateCharacter>());
                }
            }

        }

        Debug.Log("estoy tocando " + collision.name);
    }


    /// <summary>
    /// revisa si entra en collision con casillas del borde para resaltarlas
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            if (collision.gameObject.GetComponent<Square>().ItsOnEdge && !collision.gameObject.GetComponent<Square>().IsOccupied)
            {
                collision.GetComponent<Square>().activeVisualFeekbackOfSelectSquare();



                if (_square != null && _square != collision.gameObject)
                {
                    _square.GetComponent<Square>().desactiveVisualFeekbackOfSelectSquare();
                    
                }

                _square = collision.gameObject;
            }
        }
    }


    /// <summary>
    /// Si deja de estar en colision con casillas del borde entonces las deja de resaltar
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Square"))
        {
            collision.GetComponent<Square>().desactiveVisualFeekbackOfSelectSquare();
            

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

    public bool MarkSquare
    {
        get
        {
            return _markSquare;
        }

        set
        {
            _markSquare = value;
        }
    }

    public GameObject Guide
    {
        get
        {
            return _guide;
        }

        set
        {
            _guide = value;
        }
    }
}
