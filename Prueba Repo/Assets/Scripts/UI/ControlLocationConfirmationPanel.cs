using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Cobntrola todo lo que tiene que ver con el panel de confimacion de ubicacion del personaje
/// </summary>
public class ControlLocationConfirmationPanel : MonoBehaviour
{

    private GameObject _player; // player que creo el cuadro de confirmacion
    private GameObject _square; // casilla donde el player se ubico
    [SerializeField] private Color _color; // color original del las casillas es decir blanco
    /// <summary>
    /// Cuando es creado el panel nesecita ser ubicado como hijo del canvas para tener el tamaño correcto
    /// </summary>
    private void Start()
    {
        transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        _player.GetComponent<locateCharacter>().enabled = false;
        _player.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }//Cierre Start

    /// <summary>
    /// Este metodo es llamada cuando se preciona el boton de si en el panel de confirmacion y destruye el codigo que hace que siga 
    /// al cursor, da paso al siguiente turno y destruye el panel
    /// </summary>
    public void confirmationButton()
    {
        restoreColorInSquares();

        _player.GetComponent<PlayerMove>().Square = Square;
        _player.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 1.8f);
        Square.GetComponent<Square>().Player = Player;

        Square.GetComponent<Square>().photonView.RPC("selectSquare", PhotonTargets.All);

        Square.GetComponent<Square>().photonView.RPC("saveCurrentPlayer", PhotonTargets.Others, FindObjectOfType<ControlTurn>().IndexTurn);

        _player.GetComponent<ConfigurationPlayer>().photonView.RPC("endSelectionBox", PhotonTargets.Others, _player.transform.position);

        FindObjectOfType<ControlTurn>().Myturn.GetComponent<Button>().enabled = true;
        FindObjectOfType<ControlTurn>().nextTurn();

        _player.GetComponent<Animator>().runtimeAnimatorController = FindObjectOfType<PlayerDataInGame>().CharacterSelected._animator;

        Destroy(gameObject);
    }//Cierre ConfirmationButton


    /// <summary>
    /// coloca las casillas de su color original
    /// </summary>
    private void restoreColorInSquares()
    {
        Square[] squares = FindObjectsOfType<Square>();

        for (int i = 0; i < squares.Length; i++)
        {
            if (!squares[i].ItsOnEdge)
            {
                squares[i].gameObject.GetComponent<SpriteRenderer>().color = _color;
            }
        }
    }


    /// <summary>
    /// Es llamado cuando se le al boton de no al panel de confirmacion y ocaciona que el personaje regrese a la posicion donde fue creado 
    /// y destruya el panel
    /// </summary>
    public void denyButton()
    {
        _player.transform.position = new Vector3(0,0, 0);
        _player.AddComponent<locateCharacter>();
        _player.GetComponent<locateCharacter>().PanelConfirmationInGame = gameObject;
        _player.GetComponent<locateCharacter>().MarkSquare = false;
        _player.GetComponent<SpriteRenderer>().sortingOrder = 25;
        gameObject.SetActive(false);
    }//Cierre DenyButton

    /// <summary>
    /// get y set del player para poder cargarlo cuando se crea el objeto
    /// </summary>
    public GameObject Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }//Cierre get y ser de player


    /// <summary>
    /// get y set de la casilla donde el player se ubico
    /// </summary>
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
}//Cierre Clase
