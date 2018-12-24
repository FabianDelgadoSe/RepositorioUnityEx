using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Clase que contendra la informacion de otros jugadore
/// </summary>
public class OthersPlayersData : MonoBehaviour {

    [SerializeField] private Sprite _sprtMineturn;
    [SerializeField] private Sprite _sprtNotMyturn;
    private int _idOfThePlayerThatRepresents;


    /// <summary>
    /// Es llamado cuando inicie el turno del jugador al cual representa
    /// </summary>
    public void starTurn()
    {
        GetComponent<SpriteRenderer>().sprite = _sprtMineturn;
    }

    /// <summary>
    /// Es llamado cuando finalice el turno del jugador el cual representa
    /// </summary>
    public void finishTurn()
    {
        GetComponent<SpriteRenderer>().sprite = _sprtNotMyturn;
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

}
