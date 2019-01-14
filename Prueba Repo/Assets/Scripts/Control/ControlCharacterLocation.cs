using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Controlara el posicionamiento de nuestro personaje en el tablero, es decir
/// cuando se posiciona por primera vez o cuando queda soble un muro
/// </summary>
public class ControlCharacterLocation : Photon.PunBehaviour {

    public bool _firtsTurn = true;
    
    private void OnEnable()
    {
        if (_firtsTurn)
        {
            locateCharacterOnTheEdge();
            _firtsTurn = false;
        }
        else
        {
            // este else sera usado para cuando se tenga que reubicar la pieza por que esta sobre un muro
        }

        gameObject.GetComponent<ControlCharacterLocation>().enabled = false;   
    }

    public void locateCharacterOnTheEdge()
    {
        PhotonNetwork.Instantiate("Personaje Basico",new Vector3(-6,2,0),Quaternion.identity,0);
    }
    

}
