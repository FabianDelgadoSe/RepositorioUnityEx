using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelInformation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    #region textos que se muestran durante la partida
    private const string EMPTY = "";
    private const string LOCATE_CHARACTER = "Ubica tu personaje en una casilla en el borde del tablero";
    private const string START_MY_TURN = "Es tu turno, mueve un personaje que no sea el tuyo";
    private const string START_TURN_OTHER_PLAYER = "Es turno de ";
    private const string LATER_MOVE = "Coloca un cebo o finaliza tu turno";
    private const string LATER_PUT_BAIT = "Ya no tienes nada mas que hacer termina tu turno";
    private const string OTHER_PLAYER_LOCATE_CHARACTER = "Un jugador esta ubicando su ficha";
    #endregion

   public enum Messages
    {
        EMPTY,
        LOCATE_CHARACTER,
        START_MY_TURN,
        START_TURN_OTHER_PLAYER,
        LATER_MOVE,
        LATER_PUT_BAIT,
        OTHER_PLAYER_LOCATE_CHARACTER
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            Debug.Log(FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Name);
    }

    public void showMessages(Messages messemessages)
    {
        switch (messemessages)
        {
            case Messages.START_MY_TURN:
                _text.text = START_MY_TURN;
                break;
            case Messages.START_TURN_OTHER_PLAYER:
                _text.text = START_TURN_OTHER_PLAYER + 
                    FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn-1].Name;
                break;
            case Messages.LOCATE_CHARACTER:
                _text.text = LOCATE_CHARACTER;
                break;
            case Messages.LATER_PUT_BAIT:
                _text.text = LATER_PUT_BAIT;
                break;
            case Messages.LATER_MOVE:
                _text.text = LATER_MOVE;
                break;
            case Messages.EMPTY:
                _text.text = EMPTY;
                break;
            case Messages.OTHER_PLAYER_LOCATE_CHARACTER:
                _text.text = OTHER_PLAYER_LOCATE_CHARACTER;
                break;
        }
    }

}
