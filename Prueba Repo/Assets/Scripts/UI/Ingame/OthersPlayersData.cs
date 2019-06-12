using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] private Sprite _hole;

    private Character _character;

    [Header("GameObject que muestra la cara")]
    [SerializeField] private GameObject _face;
    [Header("GameObject marco")]
    [SerializeField] private GameObject _pictureFrame;
    [SerializeField] private Color _pictureFrameColor;

    [Header("Score")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _nameText;

    [Header("perder puntos")]
    [SerializeField] private GameObject _pointCreated;
    [SerializeField] private GameObject _lostTokens;
    private int _numberLostPoint = 0;

    /// <summary>
    /// Es llamado cuando inicie el turno del jugador al cual representa
    /// </summary>
    public void starTurn(int ID)
    {
        if(ID == _idOfThePlayerThatRepresents)
            _pictureFrame.GetComponent<Image>().color = _pictureFrameColor;
    }

    /// <summary>
    /// Es llamado cuando finalice el turno del jugador el cual representa
    /// </summary>
    public void finishTurn()
    {
        _pictureFrame.GetComponent<Image>().color = Color.white;
    }

    public void activeAllMoveCards()
    {
        for (int i = 0; i<_movements.Length;i++)
        {
            _movements[i].SetActive(true);
        }
    }


    public void selectMovements(int movement)
    {
        _movements[movement - 1].SetActive(false);
    }

    public void deleteObtainedTokens()
    {
        for (int i = 0; i< _tokensArray.Length; i++)
        {
            _tokensArray[i].GetComponent<Image>().sprite = _hole;
        }
        _numberTokens = 0;
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

    public void visualLostToken()
    {
        if(NumberLostPoint > 0)
        {
            GameObject aux = Instantiate(_lostTokens, _pointCreated.transform.position, Quaternion.identity);
            aux.GetComponent<IconsLoseCoin>().Father = GetComponent<OthersPlayersData>();
        }
    }

    public void assignFace()
    {
        _face.GetComponent<Image>().sprite = Character._faceCharacter;
        _face.GetComponent<Image>().enabled = true;
    }

    public void assignName()
    {
        _nameText.text = FindObjectOfType<PlayerDataInGame>().CharactersInGame[IdOfThePlayerThatRepresents - 1].Name;
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

    public void loseToken(int index)
    {
        for (int i = index; i < _numberTokens; i++)
        {
            if (i + 1 < _numberTokens)
            {
                _tokensArray[i].GetComponent<Image>().sprite = _tokensArray[i + 1].GetComponent<Image>().sprite;
            }
            else
            {
                _tokensArray[i].GetComponent<Image>().sprite = _hole;
                
            }

        }
        _numberTokens--;
        FindObjectOfType<PlayerDataInGame>().CharactersInGame[IdOfThePlayerThatRepresents - 1].Character
            .GetComponent<ControlTokensPlayer>().ObtainedTokens.RemoveAt(index);

        FindObjectOfType<PlayerDataInGame>().CharactersInGame[IdOfThePlayerThatRepresents - 1].Character
            .GetComponent<ControlTokensPlayer>().NumberTokens--;
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

    public int NumberLostPoint
    {
        get
        {
            return _numberLostPoint;
        }

        set
        {
            _numberLostPoint = value;
        }
    }
}
