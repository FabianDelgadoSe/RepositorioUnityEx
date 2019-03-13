using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controla todo lo relacionado con los turnos de los jugadores
/// </summary>
public class ControlTurn : Photon.PunBehaviour
{
    [SerializeField] private GameObject _myturn; // objeto que aparece cuando es mi turno

    [Header("Objetos que muestran la informacion de otros jugadores")]
    [SerializeField] private GameObject _otherPlayer; // prefab que representa a otros players
    [SerializeField] private GameObject[] _fatherOtherPlayersLeft;
    [SerializeField] private GameObject[] _fatherOtherPlayersRigth;

    [Header("Objeto padre de todas las cartas")]
    [SerializeField] private GameObject _cards; //objeto padre de todas las Cartas

    private List<GameObject> _otherPlayersList = new List<GameObject>();// imagenes que representan a los otros player
    private int _indexTurn; // el numero de la persona que tiene el turno
    private int _mineId;  // minumero de turno
    private bool _myTurn = false; // confirma si es mi turno
    private bool _firstTurn = true; // variable usada para saber si es el primer turno
    private bool _allowSelectCardMove = true; //verifica si ya se seleccionado una carta de movimiento


    /// <summary>
    /// inicializa variables como mineId crea los objetos que representan los otros jugadores en la pantalla de cada
    /// dispositivo e inicia los turnos de manera aleatoria
    /// </summary>
    void Start()
    {

        _mineId = PhotonNetwork.player.ID;

        createdOthersPlayers();

        /* el player master es el que elegira el que tendra el primer turno
         * el if inicia el primer turno de manera aleatoria
        */
        if (PhotonNetwork.masterClient == PhotonNetwork.player)
        {
            IndexTurn = Random.RandomRange(1, PhotonNetwork.room.playerCount + 1);
            photonView.RPC("mineTurn", PhotonTargets.All, IndexTurn);
            FindObjectOfType<ConfigurationBoard>().changeColorBoardSquares();

        }//cierre if player master

    }// cierre start


    /// <summary>
    /// Crea todos los objetos que representaran a los otros jugadores en la pantalla de cada jugador
    /// </summary>
    void createdOthersPlayers()
    {
        GameObject aux = new GameObject();
        _otherPlayersList.Add(aux);
        bool _flag = true;

        for (int i = 1; _otherPlayersList.Count - 1 < PhotonNetwork.room.playerCount - 1; i++)
        {
            if ((_otherPlayersList.Count - 1) * 2 < (PhotonNetwork.room.playerCount - 1))
            {
                aux = _fatherOtherPlayersLeft[i - 1];
                aux.SetActive(true);
            }
            else
            {

                if (!_fatherOtherPlayersRigth[0].activeInHierarchy)
                {
                    aux = _fatherOtherPlayersRigth[0];
                    aux.SetActive(true);
                }
                else
                {
                    aux = _fatherOtherPlayersRigth[1];
                    aux.SetActive(true);
                }

            }            

            if (MineId == PhotonNetwork.room.playerCount)
            {
                aux.GetComponent<OthersPlayersData>().IdOfThePlayerThatRepresents = i;
            }
            else
            {
                if (MineId + i <= PhotonNetwork.room.playerCount && _flag)
                {
                    aux.GetComponent<OthersPlayersData>().IdOfThePlayerThatRepresents = MineId + i;
                }
                else if (_flag)
                {
                    i = 1;
                    aux.GetComponent<OthersPlayersData>().IdOfThePlayerThatRepresents = i;
                    _flag = false;
                }
                else
                {
                    aux.GetComponent<OthersPlayersData>().IdOfThePlayerThatRepresents = i;
                }
            }

            _otherPlayersList.Add(aux);

        }//cierre for


    }// cierre createdOthersPlayers


    /// <summary>
    /// Usado luego de finalizar turno para dar comienzo al turno del siguiente jugador en orden
    /// </summary>
    public void nextTurn()
    {
        if ((!FindObjectOfType<ControlRound>().AllowMove) && (FindObjectOfType<ControlRound>().NumberOfCardsUsed > 0 || FirstTurn))
        {
            finishTurn();
            if (IndexTurn != PhotonNetwork.room.playerCount)
            {
                IndexTurn++;
            }// cierre if
            else
            {
                IndexTurn = 1;
            }// cierre else

            photonView.RPC("mineTurn", PhotonTargets.All, IndexTurn);
        }
    }// cierre nextTurn


    /// <summary>
    /// Cuando comienza mi turno es llamado este metodo
    /// </summary>
    void StarTurn()
    {
        if (!FindObjectOfType<ControlRound>().endOfTheRound())
        {
            _myTurn = true;
            _myturn.SetActive(true);
               //permite usar las cartas de movimientos

            if (FirstTurn)
            {
                // para crear el personaje y posicionarlo
                gameObject.GetComponent<ControlCharacterLocation>().enabled = true;
            }
            else
            {
                FindObjectOfType<ControlRound>().AllowMove = true;
            }
        }
        else
        {
            FindObjectOfType<ControlRound>().photonView.RPC("reactiveMovementsCards",PhotonTargets.All);
            FindObjectOfType<ConfigurationBoard>().changeColorBoardSquares();
            FindObjectOfType<ConfigurationBoard>().generateWalls();
            FindObjectOfType<PlayerRepositioning>().ReviewPlayersOnWall = true;
            FindObjectOfType<PlayerRepositioning>().PlayerInWall();
        }

    }//cierre starTurn


    /// <summary>
    /// Es llamado cuando finaliza el turno
    /// </summary>
    [PunRPC]
    void finishTurn()
    {
        _myTurn = false;
        _myturn.active = false;
        AllowSelectCardMove = true;
        photonView.RPC("finishTurnOtherComputers", PhotonTargets.Others, _mineId);
        if (FirstTurn)
        {
            FirstTurn = false;
            _cards.active = true;
        }

    }//cierre finishTurn


    /// <summary>
    /// Antes de llamar metodo starTurn este metodo es llamado para saber si es mi turno
    /// el parametro ID es el ID del jugador al cual le corresponde el turno y este ID es guardado
    /// es la variable indexturn para saber el ID del player del cual es el turno
    /// </summary>
    /// <param name="ID"></param>
    [PunRPC]
    public void mineTurn(int ID)
    {
        this.IndexTurn = ID;

        if (preparationound())
        {
            if (ID == this._mineId)
            {

                StarTurn();
            }//cierre if
            else
            {
                for (int i = 1; i < _otherPlayersList.Count; i++)
                {
                    _otherPlayersList[i].GetComponent<OthersPlayersData>().starTurn(ID);
                }
            }//cierre else
        }
    }//cierre mineTurn


    public bool preparationound()
    {
        if (FindObjectOfType<ControlBet>().BetMade)
        {
            return true;
        }
        else
        {
            FindObjectOfType<ControlBet>().starBet();
            return false;
        }
    }

    /// <summary>
    /// Es usado para informar a los otros jugadores que termino el turno de alguien que no son ellos
    /// </summary>
    /// <param name="ID"></param>
    [PunRPC]
    void finishTurnOtherComputers(int ID)
    {
        for (int i = 1; i < _otherPlayersList.Count; i++)
        {
            _otherPlayersList[i].GetComponent<OthersPlayersData>().finishTurn();
        }
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


    /// <summary>
    /// get y set de varible myTurn
    /// </summary>
    public bool MyTurn
    {
        get
        {
            return _myTurn;
        }

        set
        {
            _myTurn = value;
        }
    }


    /// <summary>
    /// get y set de varible firstTurn
    /// </summary>
    public bool FirstTurn
    {
        get
        {
            return _firstTurn;
        }

        set
        {
            _firstTurn = value;
        }
    }

    /// <summary>
    /// get y set de varible mineId
    /// </summary>
    public int MineId
    {
        get
        {
            return _mineId;
        }

        set
        {
            _mineId = value;
        }
    }

    public GameObject Myturn
    {
        get
        {
            return _myturn;
        }

        set
        {
            _myturn = value;
        }
    }

    public bool AllowSelectCardMove
    {
        get
        {
            return _allowSelectCardMove;
        }

        set
        {
            _allowSelectCardMove = value;
        }
    }
}
