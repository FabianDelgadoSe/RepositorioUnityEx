using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Esta clase tiene el proposito de contener todas las funciones utilizadas para 
/// la configuracion del tablero.
/// </summary>
public class ConfigurationBoard : Photon.PunBehaviour {
    
    private GameObject[] _boardSquaresArray;

    private const float GAP_Y = 1.45f ;
    private const float GAP_X = 1.28f;

    [SerializeField] private Sprite _boardSquareBlue;
    [SerializeField] private Sprite _boardSquareRed;
    [SerializeField] private Sprite _boardSquareYellow;
    [SerializeField] private Sprite _boardSquareGreen;
    [SerializeField] private Sprite _boardSquareWall;

    /// <summary>
    /// LLamada un metodo para darle ciertos valores a los cuadros
    /// </summary>
    void Start () {
        loadSpritesSquare();
    }// cierre de la funcion Start 

    /// <summary>
    /// Llama el metodo de reinicio para todos tableros en la red
    /// </summary>
    public void restarBoardSyn()
    {
        photonView.RPC("restarBoard", PhotonTargets.All);
    }

    /// <summary>
    /// Es genera un nuevo tablero pero para todos en la red
    /// </summary>
    [PunRPC]
	public void restarBoard()
    {
        changeColorBoardSquares();
        generateWalls();
    }

    public void loadSpritesSquare()
    {
        _boardSquaresArray = GameObject.FindGameObjectsWithTag("Square");

        for (int i = 0; i < _boardSquaresArray.Length; i++)
        {
            _boardSquaresArray[i].GetComponent<Square>().BoardSquareBlue = _boardSquareBlue;
            _boardSquaresArray[i].GetComponent<Square>().BoardSquareRed = _boardSquareRed;
            _boardSquaresArray[i].GetComponent<Square>().BoardSquareGreen = _boardSquareGreen;
            _boardSquaresArray[i].GetComponent<Square>().BoardSquareYellow = _boardSquareYellow;
            _boardSquaresArray[i].GetComponent<Square>().BoardSquareWall = _boardSquareWall;
        }
    }

    /// <summary>
    /// Metodo encargado de cambiar el color a cada una de las diferentes casillas 
    /// </summary>
    public void changeColorBoardSquares()
    {
        int _codeColor;
        
        for (int i = 0; i<_boardSquaresArray.Length; i++)
        {
            _codeColor = Random.RandomRange(1, 5);
            _boardSquaresArray[i].GetComponent<Square>().Index = _codeColor;
            _boardSquaresArray[i].GetComponent<PhotonView>().RPC("changeSprite", PhotonTargets.All);

        }//cierre for que recorre el arreglo

    } //cierre de la funcion changeColorBoardSquares

    /// <summary>
    /// Define la cantidad de muros en el nivel al igual que general esta cantidad en una posicion random
    /// evitando que se cree un muro en una casilla que ya contenia uno
    /// </summary>
    public void generateWalls()
    {
        int _quantityWalls = Random.RandomRange(1, 5);
        int _indexBoardSquare;

        while (_quantityWalls > 0)
        {
            _indexBoardSquare = Random.RandomRange(0, _boardSquaresArray.Length);

            if (!_boardSquaresArray[_indexBoardSquare].GetComponent<Square>().IsWall)
            {
                _boardSquaresArray[_indexBoardSquare].GetComponent<Square>().Index = 5;
                _boardSquaresArray[_indexBoardSquare].GetComponent<PhotonView>().RPC("changeSprite", PhotonTargets.All);
                _quantityWalls--;

            }// cierre if

        }//cierre while
        
    }// Cierre de generateWalls
    
}// cierre de la clase 
