using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelInformation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _textBetweenLevels;

    #region textos que se muestran durante la partida
    private const string EMPTY = "";
    private const string LOCATE_CHARACTER = "Ubica tu personaje en una casilla en el borde del tablero";
    private const string START_MY_TURN = "Es tu turno, mueve un personaje que no sea el tuyo";
    private const string START_TURN_OTHER_PLAYER = "Es turno de ";
    private const string LATER_MOVE = "Coloca un cebo o finaliza tu turno";
    private const string LATER_PUT_BAIT = "Ya no tienes nada más que hacer, termina tu turno";
    private const string OTHER_PLAYER_LOCATE_CHARACTER = "Un jugador está ubicando su ficha";
    private const string RELOCATION_CHARACTER = "Hay jugadores reubicando sus fichas";
    private const string SELECT_PREDICTION = "¿Qué gema será la que más se obtenga en esta ronda?";
    private const string OTHER_PLAYERS_THINKING_PREDICTION = "Espera un momento, los otros jugadores están pensando sus predicciones";
    private const string WIN_MISSION = "Cumpliste tu misión, ahora selecciona cuál gema quieres como recompensa";
    private const string OTHER_WIN_MISSION = "Un jugador cumplió su misión y está seleccionando su recompensa";
    private const string SUN_TOKENS_WIN_BET = "INCREIBLE ACERTASTE LA PREDICCION";
    private const string SUN_TOKENS_LOSE_BET = "Se sumaron las gemas obtenidas en esta ronda";
    private const string LOSE_TOKEN = "Un jugador está eligiendo qué gema entregarle al fantasma";
    #endregion

    public enum Messages
    {
        EMPTY,
        LOCATE_CHARACTER,
        START_MY_TURN,
        START_TURN_OTHER_PLAYER,
        LATER_MOVE,
        LATER_PUT_BAIT,
        OTHER_PLAYER_LOCATE_CHARACTER,
        RELOCATION_CHARACTER,
        OTHER_PLAYERS_THINKING_PREDICTION,
        WIN_MISSION,
        OTHER_WIN_MISSION,
        SUN_TOKENS_WIN_BET,
        SUN_TOKENS_LOSE_BET,
        LOSE_TOKEN,
        SELECT_PREDICTION
    }

    public void showMessages(Messages messemessages)
    {

        switch (messemessages)
        {
            case Messages.SELECT_PREDICTION:
                _text.text = SELECT_PREDICTION;
                break;
            case Messages.LOSE_TOKEN:
                _text.text = LOSE_TOKEN;
                break;
            case Messages.SUN_TOKENS_WIN_BET:
                _textBetweenLevels.text = SUN_TOKENS_WIN_BET;
                break;
            case Messages.SUN_TOKENS_LOSE_BET:
                _textBetweenLevels.text = SUN_TOKENS_LOSE_BET;
                break;
            case Messages.OTHER_WIN_MISSION:
                _textBetweenLevels.text = OTHER_WIN_MISSION;
                break;
            case Messages.WIN_MISSION:
                _textBetweenLevels.text = WIN_MISSION;
                break;
            case Messages.OTHER_PLAYERS_THINKING_PREDICTION:
                _text.text = OTHER_PLAYERS_THINKING_PREDICTION;
                break;
            case Messages.RELOCATION_CHARACTER:
                _text.text = RELOCATION_CHARACTER;
                break;
            case Messages.START_MY_TURN:
                _text.text = START_MY_TURN;
                break;
            case Messages.START_TURN_OTHER_PLAYER:
                _text.text = START_TURN_OTHER_PLAYER +
                    FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Name;
                break;
            case Messages.LOCATE_CHARACTER:
                _text.text = LOCATE_CHARACTER;
                break;
            case Messages.LATER_PUT_BAIT:
                _text.text = LATER_PUT_BAIT;
                break;
            case Messages.LATER_MOVE:
                if (FindObjectOfType<ControlBait>().NumberBaitCoin > 0 && FindObjectOfType<ControlBait>().NumberBaitPoop > 0)
                {
                    _text.text = LATER_MOVE;
                }
                else
                {
                    showMessages(Messages.LATER_PUT_BAIT);
                }
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
