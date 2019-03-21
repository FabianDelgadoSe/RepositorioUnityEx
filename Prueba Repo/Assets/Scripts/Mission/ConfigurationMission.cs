using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationMission : Photon.PunBehaviour
{
    private enumMission _mision = enumMission.NONE; 
    private string _missionDescription; 

    /// <summary>
    /// todas las misiones 
    /// </summary>
    public enum enumMission
    {
        NONE,
        ANY_3_RED,
        ANY_3_GREEN,
        ANY_3_BLUE,
        ANY_3_YELLOW,
        ANY_2_BLUE_2_RED,
        ANY_2_BLUE_2_YELLOW,
        ANY_2_BLUE_2_GREEN,
        ANY_2_RED_2_YELLOW,
        ANY_2_RED_2_GREEN,
        ANY_2_YELLOW_2_GREEN,
        RIGHT_MAJOR_RED,
        RIGHT_MAJOR_YELLOW,
        RIGHT_MAJOR_GREEN,
        RIGHT_MAJOR_BLUE,
        ANY_ALL_COLORS
    }

    private const string ANY_3_RED = "Cualquier jugador obtenga 3 tokens Rojos";
    private const string ANY_3_GREEN = "Cualquier jugador obtenga 3 tokens Verdes";
    private const string ANY_3_BLUE = "Cualquier jugador obtenga 3 tokens Azules";
    private const string ANY_3_YELLOW = "Cualquier jugador obtenga 3 tokens Amarillos";
    private const string ANY_2_BLUE_2_RED = "Cualquier jugador obtenga 2 tokens Azules y 2 tokens Rojos";
    private const string ANY_2_BLUE_2_YELLOW = "Cualquier jugador obtenga 2 tokens Azules y 2 tokens Amarillos";
    private const string ANY_2_BLUE_2_GREEN = "Cualquier jugador obtenga 2 tokens Azules y 2 tokens Verde";
    private const string ANY_2_RED_2_YELLOW = "Cualquier jugador obtenga 2 tokens Rojos y 2 tokens Amarillos";
    private const string ANY_2_RED_2_GREEN = "Cualquier jugador obtenga 2 tokens Rojos y 2 tokens Verde";
    private const string ANY_2_YELLOW_2_GREEN = "Cualquier jugador obtenga 2 tokens Amarillos y 2 tokens Verde";
    private const string RIGHT_MAJOR_RED = "El jugador de la Derecha obtenga NO tenga la mayor cantidad de tokens Rojos";
    private const string RIGHT_MAJOR_YELLOW = "El jugador de la Derecha obtenga NO tenga la mayor cantidad de tokens Amarillos";
    private const string RIGHT_MAJOR_GREEN = "El jugador de la Derecha obtenga NO tenga  la mayor cantidad de tokens Verde";
    private const string RIGHT_MAJOR_BLUE = "El jugador de la Derecha obtenga NO tenga  la mayor cantidad de tokens Azules ";
    private const string ANY_ALL_COLORS = "Cualquier jugador Obtenga un token de cada color";

    
    /// <summary>
    /// Le asigna el tipo de mision que debe cumplir y el texto que describe la mision
    /// </summary>
    public void selectMision()
    {
        int index = Random.Range(1,16); //no toma el 16

        switch (index)
        {
            case 1:
                _mision = enumMission.ANY_3_RED;
                _missionDescription = ANY_3_RED;
                break;

            case 2:
                _mision = enumMission.ANY_3_GREEN;
                _missionDescription = ANY_3_GREEN;
                break;

            case 3:
                _mision = enumMission.ANY_3_BLUE;
                _missionDescription = ANY_3_BLUE;
                break;

            case 4:
                _mision = enumMission.ANY_3_YELLOW;
                _missionDescription = ANY_3_YELLOW;
                break;

            case 5:
                _mision = enumMission.ANY_2_BLUE_2_RED;
                _missionDescription = ANY_2_BLUE_2_RED;
                break;

            case 6:
                _mision = enumMission.ANY_2_BLUE_2_YELLOW;
                _missionDescription = ANY_2_BLUE_2_YELLOW;
                break;

            case 7:
                _mision = enumMission.ANY_2_BLUE_2_GREEN;
                _missionDescription = ANY_2_BLUE_2_GREEN;
                break;

            case 8:
                _mision = enumMission.ANY_2_RED_2_YELLOW;
                _missionDescription = ANY_2_RED_2_YELLOW;
                break;

            case 9:
                _mision = enumMission.ANY_2_RED_2_GREEN;
                _missionDescription = ANY_2_RED_2_GREEN;
                break;

            case 10:
                _mision = enumMission.ANY_2_YELLOW_2_GREEN;
                _missionDescription = ANY_2_YELLOW_2_GREEN;
                break;

            case 11:
                _mision = enumMission.RIGHT_MAJOR_RED;
                _missionDescription = RIGHT_MAJOR_RED;
                break;

            case 12:
                _mision = enumMission.RIGHT_MAJOR_YELLOW;
                _missionDescription = RIGHT_MAJOR_YELLOW;
                break;

            case 13:
                _mision = enumMission.RIGHT_MAJOR_GREEN;
                _missionDescription = RIGHT_MAJOR_GREEN;
                break;

            case 14:
                _mision = enumMission.RIGHT_MAJOR_BLUE;
                _missionDescription = RIGHT_MAJOR_BLUE;
                break;

            case 15:
                _mision = enumMission.ANY_ALL_COLORS;
                _missionDescription = ANY_ALL_COLORS;
                break;
        }

        FindObjectOfType<ControlMission>().Mision = _mision;
        FindObjectOfType<ControlMission>().MissionDescription = _missionDescription;
    }



}
