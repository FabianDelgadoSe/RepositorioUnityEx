using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Clase que contendra la informacion de otros jugadore
/// </summary>
public class OthersPlayersData : MonoBehaviour {

  
    [SerializeField] private GameObject[] _movements;
    private int _idOfThePlayerThatRepresents;
    [SerializeField] private GameObject[] _tokensArray;
    private int _numberTokens = 0;

    [Header("Sprites de los tokens")]
    [SerializeField] private Sprite _sprtRedToken;
    [SerializeField] private Sprite _sprtBlueToken;
    [SerializeField] private Sprite _sprtGreenToken;
    [SerializeField] private Sprite _sprtYellowToken;

    private Character _character;

    [Header("GameObject que muestra la cara")]
    [SerializeField] private GameObject _face;
    [Header("GameObject marco")]
    [SerializeField] private GameObject _pictureFrame;
    /// <summary>
    /// Es llamado cuando inicie el turno del jugador al cual representa
    /// </summary>
    public void starTurn(int ID)
    {
        if(ID == _idOfThePlayerThatRepresents)
            _pictureFrame.GetComponent<Image>().color = Color.green;
    }

    /// <summary>
    /// Es llamado cuando finalice el turno del jugador el cual representa
    /// </summary>
    public void finishTurn()
    {
        _pictureFrame.GetComponent<Image>().color = Color.white;
    }



    public void selectMovements(int movement)
    {
        _movements[movement - 1].SetActive(false);
    }


    public void getToken(Square.typesSquares typesSquares)
    {
        switch (typesSquares)
        {
            case Square.typesSquares.BLUE:
                _tokensArray[_numberTokens].GetComponent<Image>().sprite = _sprtBlueToken;

                break;

            case Square.typesSquares.GREEN:
                _tokensArray[_numberTokens].GetComponent<Image>().sprite = _sprtGreenToken;

                break;

            case Square.typesSquares.RED:
                _tokensArray[_numberTokens].GetComponent<Image>().sprite = _sprtRedToken;

                break;

            case Square.typesSquares.YELLOW:
                _tokensArray[_numberTokens].GetComponent<Image>().sprite = _sprtYellowToken;

                break;


        }

        _tokensArray[_numberTokens].SetActive(true);
        _numberTokens++;
    }


    public void assignFace()
    {
        _face.GetComponent<Image>().sprite = Character._faceCharacter;
        _face.GetComponent<Image>().enabled = true;
    }

    public int IdOfThePlayerThatRepresents
    {
        get
        {
            return _idOfThePlayerThatRepresents;
        }

        set
        {
            _idOfThePlayerThatRepresents = value;
        }
    }

    public Character Character
    {
        get
        {
            return _character;
        }

        set
        {
            _character = value;
        }
    }
}
