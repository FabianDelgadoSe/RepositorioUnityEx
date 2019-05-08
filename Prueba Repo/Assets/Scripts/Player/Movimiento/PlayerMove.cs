using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Todo lo relacionado con el movimiento del personaje
/// </summary>
public class PlayerMove : Photon.PunBehaviour
{

    public int _numberSteps; // cantidad de pasos a mover
    [SerializeField] GameObject _arrow; // prefabs de las flechas a donde se puede mover
    public GameObject _square; // la casilla donde esta parado el player
    public bool _move = false; // verificacion para ver si es valido moverse
    private Arrow.adress adress; // direccion a la cual me tengo que mover
    private PhotonPlayer idOwner; // computador propietario de este objeto
    private Vector3 _starPoint;  // punto de inicio de movimiento
    private Vector3 _endPoint;  // punto final de movimiento
    private float _rapeVelocity;  // guarda el calculo del movimiento para el lerp
    private float _time = 0;  // tiempo es usado para el lerp de movimiento
    private const float VELOCITY_MOVE = 5; // Velocidad con la que se mueve el personaje
    public GameObject _playerDrag;  // para cuando esta empujando saber cual objeto esta detras de el;
    public ControlTurn _controlTurn; // Para no estar buscando el script tantas veces;
    private bool repositionPlayer = false;
    private bool _createdArrow = false; // permite saber si en este player se crearon flechas
    private GameObject _card; // permite saber cual carta tiene que reactivar
    [SerializeField]private int _lostPoints = 0;
    /// <summary>
    /// revisa si el player pertenerce a esta pantalla
    /// </summary>
    private void Start()
    {
        _controlTurn = FindObjectOfType<ControlTurn>();
        if (isMyPlayer())
        {
            IdOwner = PhotonNetwork.player;
            photonView.RPC("saveOwener", PhotonTargets.All, IdOwner); // tengo como que llamen todos para evitar un error que alguno no lo recibian
        }
    }


    /// <summary>
    /// Mueve el player siempre que move sea true
    /// </summary>
    private void Update()
    {

        if (_move)
        {
            _time += Time.deltaTime * _rapeVelocity;
            transform.position = Vector3.Lerp(_starPoint, _endPoint, _time);

            if (_time >= 1)
            {

                _time = 0;
                calculatePointToMove();

            }
        }

    }

    /// <summary>
    /// guarda en los otros jugadores el ID del propietario de ese personaje
    /// </summary>
    /// <param name="ID"></param>
    [PunRPC]
    public void saveOwener(PhotonPlayer player)
    {
        IdOwner = player;
    }

    /// <summary>
    /// Revisa si la player este player es mio es decir pertenece a esta pantalla 
    /// </summary>
    /// <returns></returns>
    public bool isMyPlayer()
    {
        if (photonView.isMine)
        {
            return true;
        }
        else
        {
            return false;
            // volver a activar la carta 
        }
    }

    [PunRPC]
    public void receiveAdress(Arrow.adress adress ,int steps)
    {
        this.adress = adress;
        repositionPlayer = false;

        int index = FindObjectOfType<ControlTurn>().IndexTurn;

        // se hace de esta forma por que muchos jugadores pueden mover una misma ficha entonces nunca se esta seguro de 
        //cual la esta moviendo
        if (!FindObjectOfType<ControlTurn>().MyTurn)
        {
            OthersPlayersData[] aux = FindObjectsOfType<OthersPlayersData>();

            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].IdOfThePlayerThatRepresents == index)
                {
                    aux[i].selectMovements(steps);
                }
            }
        }

        NumberSteps = steps;
    }

    /// <summary>
    /// Crea 4 flechas para moverse es llamadado desde miniCard
    /// </summary>
    public void createArrows( int steps)
    {
        FindObjectOfType<ControlTurn>().AllowSelectCardMove = false; // evita que se puedan seleccionar mas movimientos cuando las flechas estan creadas
        CreatedArrow = true;
        GameObject aux;
        // flecha de abajo
        aux = Instantiate(_arrow, transform.position, Quaternion.identity);
        aux.GetComponent<Arrow>().EnumAdress = Arrow.adress.DOWN;
        aux.GetComponent<Arrow>().Player = gameObject;
        aux.GetComponent<Arrow>().Steps = steps;

        // Flecha de arriba
        aux = Instantiate(_arrow, transform.position, Quaternion.identity);
        aux.GetComponent<Arrow>().EnumAdress = Arrow.adress.UP;
        aux.GetComponent<Arrow>().Player = gameObject;
        aux.GetComponent<Arrow>().Steps = steps;

        //Flecha de la derecha
        aux = Instantiate(_arrow, transform.position, Quaternion.identity);
        aux.GetComponent<Arrow>().EnumAdress = Arrow.adress.RIGHT;
        aux.GetComponent<Arrow>().Player = gameObject;
        aux.GetComponent<Arrow>().Steps = steps;

        //Flecha de la izquierda
        aux = Instantiate(_arrow, transform.position, Quaternion.identity);
        aux.GetComponent<Arrow>().EnumAdress = Arrow.adress.LEFT;
        aux.GetComponent<Arrow>().Player = gameObject;
        aux.GetComponent<Arrow>().Steps = steps;

    }

    [PunRPC]
    public void repositioning(Arrow.adress adress)
    {
        this.adress = adress;
        NumberSteps = 1;
        repositionPlayer = true;
        calculatePointToMove(false); // no es un movimiento normal
    }

    [PunRPC]
    /// <summary>
    ///Obtiene los puntos a los cuales se movera la ficha al igual que todos los valores para 
    /// imprementar un lerp en el Update, el parametro define si es un movimietno normal o es 
    /// producto de un reposicionamiento por estar en un muro
    /// </summary>
    /// <param name="normalMove"></param>
    public void calculatePointToMove(bool normalMove = true)
    {

        if (NumberSteps >= 1)
        {
            _starPoint = transform.position;
            GetComponent<Animator>().SetBool("Move", true);
            switch (adress)
            {
                case Arrow.adress.DOWN:

                    //si la casilla no esta en el borde o es un muro pierdo punto de una vez
                    if (Square.GetComponent<Square>()._squareDown != null && !Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsWall)
                    {
                        // si hay un player en la siguiente casilla lo mueve sino yo me muevo normalmente
                        if (!Square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsOccupied)
                        {
                            _endPoint = Square.GetComponent<Square>()._squareDown.transform.position;
                            Move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                            Square.GetComponent<Square>().IsOccupied = false;
                            Square.GetComponent<Square>().Player = null;

                            Square = Square.GetComponent<Square>()._squareDown;
                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;

                            NumberSteps--;
                        }
                        else
                        {
                            if (!Square.GetComponent<Square>()._squareDown.GetComponent<Square>().HasBoss)
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player;
                                aux.GetComponent<PlayerMove>().NumberSteps = 1;
                                aux.GetComponent<PlayerMove>().Adress = adress;
                                aux.GetComponent<PlayerMove>().PlayerDrag = gameObject;
                                aux.GetComponent<PlayerMove>().calculatePointToMove();
                                Move = false; // me dejo de mover hasta que el otro me diga si puedo
                            }
                            else
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareDown.GetComponent<Square>().Player;
                                aux.GetComponent<BehaviourGhost>().Steps = 1;
                                aux.GetComponent<BehaviourGhost>().Direction = Adress;
                                aux.GetComponent<BehaviourGhost>().PlayerDrag = gameObject;
                                aux.GetComponent<BehaviourGhost>().move();
                                Move = false;
                            }


                        }
                    }
                    else
                    {
                        Move = false;
                        FindObjectOfType<ControlRound>().AllowMove = false; // no deja seguir usando cartas de movimiento
                        GetComponent<Animator>().SetBool("Move", false);

                        if (_playerDrag == null)
                        {
                            lostPoints();

                            if (_controlTurn.MyTurn)
                            {
                                FindObjectOfType<ControlRound>().useLetter();
                                FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);
                                _controlTurn.AllowToPlaceBait = true;
                            }
                        }

                        //si esta en una casilla con un cebo es por que es un bueno entonces que se active
                        if (_square.GetComponent<Square>().HaveBait)
                        {
                            _square.GetComponent<Square>().BaitInGame.GetComponent<BaitBehaviour>().behaviourCoin();
                        }

                        if (_playerDrag != null && _playerDrag.GetComponent<PlayerMove>())
                        {
                            _playerDrag.GetComponent<PlayerMove>().onBait();
                            _playerDrag.GetComponent<PlayerMove>().lostPoints();
                            _playerDrag.GetComponent<PlayerMove>().calculatePointToMove();
                            PlayerDrag = null;
                        }

                      

                    }
                    break;

                case Arrow.adress.UP:
                    if (Square.GetComponent<Square>()._squareUp != null && !Square.GetComponent<Square>()._squareUp.GetComponent<Square>().IsWall)
                    {

                        if (!Square.GetComponent<Square>()._squareUp.GetComponent<Square>().IsOccupied)
                        {
                            _endPoint = Square.GetComponent<Square>()._squareUp.transform.position;
                            Move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                            Square.GetComponent<Square>().IsOccupied = false;
                            Square.GetComponent<Square>().Player = null;

                            Square = Square.GetComponent<Square>()._squareUp;
                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;

                            NumberSteps--;
                        }
                        else
                        {
                            if (!Square.GetComponent<Square>()._squareUp.GetComponent<Square>().HasBoss)
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player;
                                aux.GetComponent<PlayerMove>().NumberSteps = 1;
                                aux.GetComponent<PlayerMove>().Adress = adress;
                                aux.GetComponent<PlayerMove>().PlayerDrag = gameObject;
                                aux.GetComponent<PlayerMove>().calculatePointToMove();
                                Move = false; // me dejo de mover hasta que el otro me diga si puedo
                            }
                            else
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareUp.GetComponent<Square>().Player;
                                aux.GetComponent<BehaviourGhost>().Steps = 1;
                                aux.GetComponent<BehaviourGhost>().Direction = Adress;
                                aux.GetComponent<BehaviourGhost>().PlayerDrag = gameObject;
                                aux.GetComponent<BehaviourGhost>().move();
                                Move = false;
                            }
                        }
                    }

                    else
                    {
                        Move = false;
                        FindObjectOfType<ControlRound>().AllowMove = false;
                        GetComponent<Animator>().SetBool("Move", false);

                        if ( _playerDrag == null)
                        {
                            lostPoints();

                            if (_controlTurn.MyTurn)
                            {
                                FindObjectOfType<ControlRound>().useLetter();
                                FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);
                                _controlTurn.AllowToPlaceBait = true;
                            }
                        }

                        if (_square.GetComponent<Square>().HaveBait)
                        {
                            _square.GetComponent<Square>().BaitInGame.GetComponent<BaitBehaviour>().behaviourCoin();
                        }

                        if (_playerDrag != null && _playerDrag.GetComponent<PlayerMove>())
                        {
                            _playerDrag.GetComponent<PlayerMove>().onBait();
                            _playerDrag.GetComponent<PlayerMove>().lostPoints();
                            _playerDrag.GetComponent<PlayerMove>().calculatePointToMove();
                            PlayerDrag = null;
                        }

                       

                    }
                    break;

                case Arrow.adress.LEFT:
                    GetComponent<SpriteRenderer>().flipX = true;
                    if (Square.GetComponent<Square>()._squareLeft != null && !Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().IsWall)
                    {

                        if (!Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().IsOccupied)
                        {
                            _endPoint = Square.GetComponent<Square>()._squareLeft.transform.position;
                            Move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                            Square.GetComponent<Square>().IsOccupied = false;
                            Square.GetComponent<Square>().Player = null;

                            Square = Square.GetComponent<Square>()._squareLeft;
                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;

                            NumberSteps--;
                        }
                        else
                        {
                            if (!Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().HasBoss)
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player;
                                aux.GetComponent<PlayerMove>().NumberSteps = 1;
                                aux.GetComponent<PlayerMove>().Adress = adress;
                                aux.GetComponent<PlayerMove>().PlayerDrag = gameObject;
                                aux.GetComponent<PlayerMove>().calculatePointToMove();
                                Move = false; // me dejo de mover hasta que el otro me diga si puedo
                            }
                            else
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareLeft.GetComponent<Square>().Player;
                                aux.GetComponent<BehaviourGhost>().Steps = 1;
                                aux.GetComponent<BehaviourGhost>().Direction = Adress;
                                aux.GetComponent<BehaviourGhost>().PlayerDrag = gameObject;
                                aux.GetComponent<BehaviourGhost>().move();
                                Move = false;
                            }
                        }
                    }
                    else
                    {
                        Move = false;
                        FindObjectOfType<ControlRound>().AllowMove = false;
                        GetComponent<Animator>().SetBool("Move", false);

                        if (_playerDrag == null)
                        {
                            lostPoints();

                            if (_controlTurn.MyTurn)
                            {
                                FindObjectOfType<ControlRound>().useLetter();
                                FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);
                                _controlTurn.AllowToPlaceBait = true;
                            }
                        }

                        if (_square.GetComponent<Square>().HaveBait)
                        {
                            _square.GetComponent<Square>().BaitInGame.GetComponent<BaitBehaviour>().behaviourCoin();
                        }

                        if (_playerDrag != null && _playerDrag.GetComponent<PlayerMove>())
                        {
                            _playerDrag.GetComponent<PlayerMove>().onBait();
                            _playerDrag.GetComponent<PlayerMove>().lostPoints();
                            _playerDrag.GetComponent<PlayerMove>().calculatePointToMove();
                            PlayerDrag = null;
                        }


                    }
                    break;

                case Arrow.adress.RIGHT:
                    GetComponent<SpriteRenderer>().flipX = false;
                    if (Square.GetComponent<Square>()._squareRigh != null && !Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().IsWall)
                    {

                        if (!Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().IsOccupied)
                        {
                            _endPoint = Square.GetComponent<Square>()._squareRigh.transform.position;
                            Move = true;
                            _rapeVelocity = 1f / Vector3.Distance(_starPoint, _endPoint) * VELOCITY_MOVE;
                            Square.GetComponent<Square>().IsOccupied = false;
                            Square.GetComponent<Square>().Player = null;

                            Square = Square.GetComponent<Square>()._squareRigh;
                            Square.GetComponent<Square>().Player = gameObject;
                            Square.GetComponent<Square>().IsOccupied = true;

                            NumberSteps--;
                        }
                        else
                        {

                            if (!Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().HasBoss)
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player;
                                aux.GetComponent<PlayerMove>().NumberSteps = 1;
                                aux.GetComponent<PlayerMove>().Adress = adress;
                                aux.GetComponent<PlayerMove>().PlayerDrag = gameObject;
                                aux.GetComponent<PlayerMove>().calculatePointToMove();
                                Move = false; // me dejo de mover hasta que el otro me diga si puedo
                            }
                            else
                            {
                                GameObject aux = Square.GetComponent<Square>()._squareRigh.GetComponent<Square>().Player;
                                aux.GetComponent<BehaviourGhost>().Steps = 1;
                                aux.GetComponent<BehaviourGhost>().Direction = Adress;
                                aux.GetComponent<BehaviourGhost>().PlayerDrag = gameObject;
                                aux.GetComponent<BehaviourGhost>().move();
                                Move = false;
                            }
                        }
                    }
                    else
                    {
                        Move = false;
                        FindObjectOfType<ControlRound>().AllowMove = false;
                        GetComponent<Animator>().SetBool("Move", false);

                        if (_playerDrag == null)
                        {
                            lostPoints();

                            if (_controlTurn.MyTurn)
                            {
                                FindObjectOfType<ControlRound>().useLetter();
                                FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares);

                                _controlTurn.AllowToPlaceBait = true;
                            }
                        }

                        if (_square.GetComponent<Square>().HaveBait)
                        {
                            _square.GetComponent<Square>().BaitInGame.GetComponent<BaitBehaviour>().behaviourCoin();
                        }

                        if (_playerDrag !=null && _playerDrag.GetComponent<PlayerMove>())
                        {
                            _playerDrag.GetComponent<PlayerMove>().onBait();
                            _playerDrag.GetComponent<PlayerMove>().lostPoints();
                            _playerDrag.GetComponent<PlayerMove>().calculatePointToMove();
                            PlayerDrag = null;
                        }

                 

                    }

                    break;
            }

        }
        else
        {

            Move = false;
            GetComponent<Animator>().SetBool("Move", false);

            if (!repositionPlayer)
            {
                FindObjectOfType<ControlRound>().AllowMove = false; // evita que se use otra carta de movimiento


                if (PlayerDrag == null)
                {
                    if (_square.GetComponent<Square>().HaveBait)
                    {
                        _square.GetComponent<Square>().BaitInGame.GetComponent<BaitBehaviour>().behaviourCoin();
                    }

                    if (FindObjectOfType<ControlTurn>().MyTurn) {

                        FindObjectOfType<ControlRound>().useLetter();
                        FindObjectOfType<ControlTokens>().earnToken(Square.GetComponent<Square>().EnumTypesSquares); // hace que solo el player que se movio primero sea el que sume una gema                   
                        _controlTurn.AllowToPlaceBait = true;
                    }
                }
                else
                {
                    // para cuandoes empujado
                    PlayerDrag.GetComponent<PlayerMove>().calculatePointToMove();                    

                    if (PlayerDrag.GetComponent<PlayerMove>().NumberSteps == 0 && PlayerDrag.GetComponent<PlayerMove>().PlayerDrag == null)
                    {
                        if (_square.GetComponent<Square>().HaveBait)
                        {
                            _square.GetComponent<Square>().BaitInGame.GetComponent<BaitBehaviour>().behaviourCoin();
                        }
                    }

                    PlayerDrag = null;
                }

            }

        }



    }

    public void onBait()
    {
        if(_square.GetComponent<Square>().HaveBait)
            _square.GetComponent<Square>().BaitInGame.GetComponent<BaitBehaviour>().behaviourCoin();
        
        if (_playerDrag != null && _playerDrag.GetComponent<PlayerMove>())
        {
            _playerDrag.GetComponent<PlayerMove>().onBait();
        }
    }

    /// <summary>
    /// revisa si este es el objeto que tiene que perder puntos y si este pertenece a este dispositivo
    /// </summary>
    public void lostPoints()
    {

        if (PlayerDrag == null)
        {
            if (FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Score > 0)
            {
                if (FindObjectOfType<ControlTurn>().MyTurn)
                {
                    FindObjectOfType<MyLostPoint>().NumberLostPoint += NumberSteps;
                    FindObjectOfType<MyLostPoint>().visualLostToken();
                }
                else
                {
                    OthersPlayersData[] aux = FindObjectsOfType<OthersPlayersData>();

                    for (int i = 0; i < aux.Length; i++)
                    {
                        if (aux[i].IdOfThePlayerThatRepresents == FindObjectOfType<ControlTurn>().IndexTurn)
                        {
                            aux[i].NumberLostPoint += NumberSteps;
                            aux[i].visualLostToken();
                        }
                    }
                }

                FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Score -= NumberSteps;

                if (FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Score < 0)
                    FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Score = 0;
            }
        }
        else
        {
            PlayerDrag.GetComponent<PlayerMove>().lostPoints();
            PlayerDrag = null;
        }

        NumberSteps = 0;
    }


    public void OnMouseDown()
    {

        if (CreatedArrow)
        {
            FindObjectOfType<ControlTurn>().AllowSelectCardMove = true;
            Card.GetComponent<Card>().deselectCard(true);
            Card = null;
            _createdArrow = false;

            //borra las cartas
            Arrow[] aux = FindObjectsOfType<Arrow>();

            for (int i = 0; i < aux.Length; i++)
                Destroy(aux[i].gameObject);
        }
    }

    /////////////////////////// GET Y SET ///////////////

    /// <summary>
    /// get y set de numberSteps
    /// </summary>
    public int NumberSteps
    {
        get
        {
            return _numberSteps;
        }

        set
        {
            _numberSteps = value;
        }
    }


    /// <summary>
    /// get y set del enum adress
    /// </summary>
    public Arrow.adress Adress
    {
        get
        {
            return adress;
        }

        set
        {
            adress = value;
        }
    }


    /// <summary>
    /// get y set de square
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


    /// <summary>
    /// get y set de move
    /// </summary>
    public bool Move
    {
        get
        {
            return _move;
        }

        set
        {
            _move = value;
        }
    }

    /// <summary>
    /// get y set de idOwner
    /// </summary>
    public PhotonPlayer IdOwner
    {
        get
        {
            return idOwner;
        }

        set
        {
            idOwner = value;
        }
    }

    public GameObject PlayerDrag
    {
        get
        {
            return _playerDrag;
        }

        set
        {
            _playerDrag = value;
        }
    }

    public GameObject Card
    {
        get
        {
            return _card;
        }

        set
        {
            _card = value;
        }
    }

    public bool CreatedArrow
    {
        get
        {
            return _createdArrow;
        }

        set
        {
            _createdArrow = value;
        }
    }

    public int LostPoints
    {
        get
        {
            return _lostPoints;
        }

        set
        {
            _lostPoints = value;
        }
    }
}
