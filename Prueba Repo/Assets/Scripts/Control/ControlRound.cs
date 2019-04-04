using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Contendra toda la informacion de la partida 
/// </summary>
public class ControlRound : Photon.PunBehaviour
{

    private bool _allowMove = false; // controla que solo pueda usar una carta de movimiento por turno
    public int _noMovementPlayes = 0;
    public int _numberOfCardsUsed = 0;
    private int _numberRounds = 0;
    [SerializeField] GameObject[] _movementsCards;
    public OthersPlayersData[] _othersPlayersData;
    private bool _finishPointProcedures = true;
    private bool _finishRound = false;
    private bool _firstRound = true;
    private int _charactersInBoard = -1;
    [PunRPC]
    /// <summary>
    /// vuelve a activar las cartas de movimiento
    /// </summary>
    public void reactiveMovementsCards()
    {
        // mis cartas
        for (int i = 0; i < _movementsCards.Length; i++)
        {
            _movementsCards[i].SetActive(true);
        }

        // cardas que de los otros jugadores
        _othersPlayersData = FindObjectsOfType<OthersPlayersData>();
        for (int i = 0; i < _othersPlayersData.Length; i++)
        {
            _othersPlayersData[i].activeAllMoveCards();
        }

        _noMovementPlayes = 0;
        _numberOfCardsUsed = 0;

        GetComponent<ControlTokens>().photonView.RPC("resetTokens", PhotonTargets.All);// quita los tokens obtenidos esta ronda


    }



    public void useLetter()
    {
        _numberOfCardsUsed++;

        if (_numberOfCardsUsed == 5)
        {
            photonView.RPC("newPlayerWithoutMovements", PhotonTargets.All);
        }
    }

    
    public void finishFirstRound()
    {
        
        if (_charactersInBoard != PhotonNetwork.room.PlayerCount)
        {
            _charactersInBoard++;

            if (_charactersInBoard == PhotonNetwork.room.PlayerCount)
            {
                _firstRound = false;
            }

        }
    }

    [PunRPC]
    public void newPlayerWithoutMovements()
    {
        _noMovementPlayes++;
        Debug.Log("numero de players sin movimientos " + _noMovementPlayes);
    }

    [PunRPC]
    public void finishRound()
    {
        _numberRounds++;
        if (_numberRounds == 4)
        {
            SceneManager.LoadScene("ResultOfTheGame");
        }
    }

    public bool endOfTheRound()
    {
        if (_noMovementPlayes == PhotonNetwork.room.PlayerCount)
        {
            _finishRound = true;
            FindObjectOfType<ControlBait>().photonView.RPC("restarBaits", PhotonTargets.All);
            
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

    public bool FinishPointProcedures
    {
        get
        {
            return _finishPointProcedures;
        }

        set
        {
            _finishPointProcedures = value;
        }
    }

    public bool FinishRound
    {
        get
        {
            return _finishRound;
        }

        set
        {
            _finishRound = value;
        }
    }

    public int NumberRounds
    {
        get
        {
            return _numberRounds;
        }

        set
        {
            _numberRounds = value;
        }
    }

    public bool FirstRound
    {
        get
        {
            return _firstRound;
        }

        set
        {
            _firstRound = value;
        }
    }
}
