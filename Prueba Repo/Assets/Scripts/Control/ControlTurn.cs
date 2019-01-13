using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controla todo lo relacionado con los turnos de los jugadores
/// </summary>
public class ControlTurn : Photon.PunBehaviour {
    [SerializeField] private GameObject _myturn;

    [Header ("Objetos que muestran la informacion de otros jugadores")]
    [SerializeField] private GameObject _otherPlayer;

    private List<GameObject> _otherPlayersList = new List<GameObject>();
    private int _indexTurn;
    private int _mineId;

    private  const float INICIAL_POSITION_X = -4.85f;

    /// <summary>
    /// inicializa variables como mineId crea los objetos que representan los otros jugadores en la pantalla de cada
    /// dispositivo e inicia los turnos de manera aleatoria
    /// </summary>
    void Start () {

        _mineId = PhotonNetwork.player.ID;

        createdOthersPlayers();
        
        /* el player master es el que elegira el que tendra el primer turno
         * el if inicia el primer turno de manera aleatoria
        */
        if (PhotonNetwork.masterClient == PhotonNetwork.player)
        {
            IndexTurn = Random.RandomRange(1, PhotonNetwork.room.playerCount+1);
            photonView.RPC("mineTurn", PhotonTargets.All,IndexTurn);
            FindObjectOfType<ConfigurationBoard>().changeColorBoardSquares();
            
        }//cierre if player master

    }// cierre start

    /// <summary>
    /// Crea todos los objetos que representaran a los otros jugadores en la pantalla de cada jugador
    /// </summary>
    void createdOthersPlayers()
    {
        GameObject aux = new GameObject();

        for (int i = 1; i< PhotonNetwork.room.playerCount+1; i++)
        {
            if(i != _mineId)
            {
                aux = Instantiate(_otherPlayer, new Vector3(INICIAL_POSITION_X + (i * 1.01f), 4.5f, 0), Quaternion.identity);
                aux.GetComponent<OthersPlayersData>().IdOfThePlayerThatRepresents = i;

            }// cierre 
            else
            {
                aux = null;
            }
            _otherPlayersList.Add(aux);

        }//cierre for


    }// cierre createdOthersPlayers
    
    /// <summary>
    /// Usado luego de finalizar turno para dar comienzo al turno del siguiente jugador en orden
    /// </summary>
    public void nextTurn()
    {
        finishTurn();
        //photonView.RPC("finishTurn", PhotonPlayer.Find(_indexTurn));
        if (IndexTurn != PhotonNetwork.room.playerCount)
        {
            IndexTurn++;
        }// cierre if
        else
        {
            IndexTurn = 1;
        }// cierre else

        photonView.RPC("mineTurn", PhotonTargets.All,IndexTurn);
        
    }// cierre nextTurn

    /// <summary>
    /// Cuando comienza mi turno es llamado este metodo
    /// </summary>
    void StarTurn()
    {
        _myturn.active = true;
        gameObject.GetComponent<ControlCharacterLocation>().locateCharacterOnTheEdge();
    }//cierre starTurn

    /// <summary>
    /// Es llamado cuando finaliza el turno
    /// </summary>
    [PunRPC]
    void finishTurn()
    {
        _myturn.active = false;
        photonView.RPC("finishTurnOtherComputers", PhotonTargets.Others,_mineId);
    }//cierre finishTurn
    
    /// <summary>
    /// Antes de llamar metodo starTurn este metodo es llamado para saber si es mi turno
    /// el parametro ID es el ID del jugador al cual le corresponde el turno y este ID es guardado
    /// es la variable indexturn para saber el ID del player del cual es el turno
    /// </summary>
    /// <param name="ID"></param>
    [PunRPC]
    void mineTurn(int ID)
    {
        this.IndexTurn = ID;
        if (ID == this._mineId)
        {
            StarTurn();
        }//cierre if
        else
        {
            _otherPlayersList[ID - 1].gameObject.GetComponent<OthersPlayersData>().starTurn(); 
        }//cierre else
    }//cierre mineTurn
    
    /// <summary>
    /// Es usado para informar a los otros jugadores que termino el turno de alguien que no son ellos
    /// </summary>
    /// <param name="ID"></param>
    [PunRPC]
    void finishTurnOtherComputers(int ID)
    {
        _otherPlayersList[ID - 1].gameObject.GetComponent<OthersPlayersData>().finishTurn();
    }//cierre finishTurnOtherComputers

    /// <summary>
    /// get y set de variable indexTurn
    /// </summary>
    public int IndexTurn
    {
        get
        {
            return _indexTurn;
        }

        set
        {
            _indexTurn = value;
        }
    }
}
