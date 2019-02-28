using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Cobntrola todo lo que tiene que ver con el panel de confimacion de ubicacion del personaje
/// </summary>
public class ControlLocationConfirmationPanel : MonoBehaviour
{

    private GameObject _player; // player que creo el cuadro de confirmacion
    private GameObject _square; // casilla donde el player se ubico
    /// <summary>
    /// Cuando es creado el panel nesecita ser ubicado como hijo del canvas para tener el tamaño correcto
    /// </summary>
    private void Start()
    {
        transform.SetParent(FindObjectOfType<Canvas>().transform,false);
        _player.GetComponent<locateCharacter>().enabled = false;

    }//Cierre Start

    /// <summary>
    /// Este metodo es llamada cuando se preciona el boton de si en el panel de confirmacion y destruye el codigo que hace que siga 
    /// al cursor, da paso al siguiente turno y destruye el panel
    /// </summary>
    public void confirmationButton()
    {
        _player.GetComponent<locateCharacter>().enabled = false;

        _player.GetComponent<PlayerMove>().Square = Square;

        Destroy(_player.GetComponent<locateCharacter>());

        Square.GetComponent<Square>().Player = Player;

        Square.GetComponent<Square>().photonView.RPC("selectSquare",PhotonTargets.All);

        Square.GetComponent<Square>().photonView.RPC("saveCurrentPlayer", PhotonTargets.Others, FindObjectOfType<ControlTurn>().IndexTurn);

        _player.GetComponent<ConfigurationPlayer>().photonView.RPC("endSelectionBox", PhotonTargets.All);



        FindObjectOfType<ControlTurn>().nextTurn();

        Destroy(gameObject);
    }//Cierre ConfirmationButton


    /// <summary>
    /// Es llamado cuando se le al boton de no al panel de confirmacion y ocaciona que el personaje regrese a la posicion donde fue creado 
    /// y destruya el panel
    /// </summary>
    public void denyButton()
    {
        _player.transform.position = new Vector3(-6, 2, 0);
        _player.GetComponent<locateCharacter>().enabled = true;
        Destroy(gameObject);
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
