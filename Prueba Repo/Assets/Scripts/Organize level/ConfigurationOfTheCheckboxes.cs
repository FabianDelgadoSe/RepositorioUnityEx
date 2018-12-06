using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Esta clase tiene el proposito de contener todas las funciones utilizadas para 
/// la configuracion del tablero.
/// </summary>
public class ConfigurationOfTheCheckboxes : MonoBehaviour {
    
    private GameObject[] _boardSquares;

    [SerializeField] private Sprite _boardSquareBlue;
    [SerializeField] private Sprite _boardSquareRed;
    [SerializeField] private Sprite _boardSquareYellow;
    [SerializeField] private Sprite _boardSquareGreen;
    [SerializeField] private Sprite _boardSquareWall;

    /// <summary>
    /// Este metodo start es usado para llamar diferentes metodos para realizar la configuracion inicial
    /// del tablero.
    /// </summary>
    void Start () {
        findBoardSquares();
        changeColorBoardSquares();
        generateWalls();
	}// cierre de la funcion Start 
	
	

    /// <summary>
    /// Metodo encargado de buscar todas las casillas del tablero.
    /// </summary>
    public void findBoardSquares()
    {
        _boardSquares = GameObject.FindGameObjectsWithTag("CheckBoxes");

    }//cierre de la funcion findBoardSquares

    /// <summary>
    /// Metodo encargado de cambiar el color a cada una de las diferentes casillas 
    /// </summary>
    public void changeColorBoardSquares()
    {
        int _codeColor;

        for (int i = 0; i<_boardSquares.Length; i++)
        {
            _codeColor = Random.RandomRange(1, 5);

            switch (_codeColor)
            {
                case 1:
                    _boardSquares[i].GetComponent<SpriteRenderer>().sprite = _boardSquareBlue;
                    break;

                case 2:
                    _boardSquares[i].GetComponent<SpriteRenderer>().sprite = _boardSquareGreen;
                    break;

                case 3:
                    _boardSquares[i].GetComponent<SpriteRenderer>().sprite = _boardSquareRed;
                    break;

                case 4:
                    _boardSquares[i].GetComponent<SpriteRenderer>().sprite = _boardSquareYellow;
                    break;

                default:
                    Debug.Log("salio del rango " + _codeColor);
                    break;

            }//cierre switch que escoge colores

        }//cierre for que recorre el arreglo

    } //cierre de la funcion changeColorBoardSquares

    /// <summary>
    /// Define la cantidad de muros en el nivel al igual que general esta cantidad en en una posicion random
    /// evitando que se cree un muro en una casilla que ya contenia uno
    /// </summary>
    public void generateWalls()
    {
        int _quantityWalls = Random.RandomRange(1, 5);
        int _indexBoardSquare;

        while (_quantityWalls > 0)
        {
            _indexBoardSquare = Random.RandomRange(0, 35);
            if (_boardSquares[_indexBoardSquare].GetComponent<SpriteRenderer>().sprite != _boardSquareWall)
            {
                _boardSquares[_indexBoardSquare].GetComponent<SpriteRenderer>().sprite = _boardSquareWall;
                _quantityWalls--;
            }// cierre if

        }//cierre while
        
    }// Cierre de generateWalls

}// cierre de la clase 
