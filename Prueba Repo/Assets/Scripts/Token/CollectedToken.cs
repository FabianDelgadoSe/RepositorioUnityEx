using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// comportamiento de la gema que obtiene el jugador cada turno
/// </summary>
public class CollectedToken : MonoBehaviour {

    [SerializeField] private Sprite _redGem;
    [SerializeField] private Sprite _blueGem;
    [SerializeField] private Sprite _greenGem;
    [SerializeField] private Sprite _yellowGem;

    private GameObject _player;
    private Square.typesSquares enumtypesSquares;

    /// <summary>
    /// isntancia la posicion del onjeto y la variable enumtypesSquare para luego cambiar de sprite
    /// </summary>
    void Start () {

        enumtypesSquares = _player.GetComponent<PlayerMove>().Square.GetComponent<Square>().EnumTypesSquares;
        transform.position = _player.transform.position;

        changeSprite();
	}

    /// <summary>
    /// cambia el sprite de pentiendo del enum que se le paso al momento de su creacion
    /// </summary>
    public void changeSprite()
    {
        switch (enumtypesSquares)
        {
            case Square.typesSquares.BLUE:
                GetComponent<SpriteRenderer>().sprite = _blueGem;
                break;
            case Square.typesSquares.RED:
                GetComponent<SpriteRenderer>().sprite = _redGem;
                break;
            case Square.typesSquares.GREEN:
                GetComponent<SpriteRenderer>().sprite = _greenGem;
                break;
            case Square.typesSquares.YELLOW:
                GetComponent<SpriteRenderer>().sprite = _yellowGem;
                break;
        }
    }

    /// <summary>
    /// lo hace elevarse un poco
    /// </summary>
	void Update () {
        transform.Translate(Vector2.up * Time.deltaTime);
	}


    public Square.typesSquares EnumtypesSquares
    {
        get
        {
            return enumtypesSquares;
        }

        set
        {
            enumtypesSquares = value;
        }
    }

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
    }
}
