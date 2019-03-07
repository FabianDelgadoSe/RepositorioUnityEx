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


    /// <summary>
    /// Es llamado cuando inicie el turno del jugador al cual representa
    /// </summary>
    public void starTurn(int ID)
    {
        if(ID == _idOfThePlayerThatRepresents)
            GetComponent<Image>().color = Color.green;
    }

    /// <summary>
    /// Es llamado cuando finalice el turno del jugador el cual representa
    /// </summary>
    public void finishTurn()
    {
        GetComponent<Image>().color = Color.white;
    }



    public void selectMovements(int movement)
    {
        _movements[movement - 1].SetActive(false);
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
