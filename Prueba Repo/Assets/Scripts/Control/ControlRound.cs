using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Contendra toda la informacion de la partida 
/// </summary>
public class ControlRound : Photon.PunBehaviour {

    private bool _allowMove = false; // controla que solo pueda usar una carta de movimiento por turno
    public int _noMovementPlayes = 0;
    public int _numberOfCardsUsed = 0;
    [SerializeField] GameObject[] _movementsCards;
    public OthersPlayersData[] _othersPlayersData;

    [PunRPC]
    /// <summary>
    /// vuelve a activar las cartas de movimiento
    /// </summary>
    public void reactiveMovementsCards()
    {
        // mis cartas
        for (int i = 0; i < _movementsCards.Length ; i++)
        {
            _movementsCards[i].SetActive(true);
        }

        // cardas que de los otros jugadores
        _othersPlayersData = FindObjectsOfType<OthersPlayersData>();
        for (int i = 0; i<_othersPlayersData.Length; i++)
        {
            _othersPlayersData[i].activeAllMoveCards();
        }

        _noMovementPlayes = 0;
        _numberOfCardsUsed = 0;
        
        GetComponent<ControlTokens>().resetTokens(); // quita los tokens obtenidos esta ronda
    }

    

    public void useLetter()
    {
        _numberOfCardsUsed++;

        if (_numberOfCardsUsed == 5)
        {
            photonView.RPC("newPlayerWithoutMovements",PhotonTargets.All);
        }
    }

    [PunRPC]
    public void newPlayerWithoutMovements()
    {
        _noMovementPlayes++;
    }


    public bool endOfTheRound()
    {
        if (_noMovementPlayes == PhotonNetwork.room.PlayerCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //get y set
    public bool AllowMove
    {
        get
        {
            return _allowMove;
        }

        set
        {
            _allowMove = value;
        }
    }

    public int NumberOfCardsUsed
    {
        get
        {
            return _numberOfCardsUsed;
        }

        set
        {
            _numberOfCardsUsed = value;
        }
    }
}
