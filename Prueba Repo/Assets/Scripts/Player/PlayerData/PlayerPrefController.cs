using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerPrefController
{
    const string PLAYERNAME_KEY = "PlayerName";
    private static PlayerPrefController _instance;

    /// <summary>
    /// Obtener la instancia del playerprefController
    /// </summary>
    public static PlayerPrefController GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerPrefController();            
        }

        return _instance;
    }


    private void Awake()
    {
        


    }

    private string GenerateRandomName()
    {
        return "Jugador#" + UnityEngine.Random.Range(1000, 9999);

    }

   

    /// <summary>
    /// Guardar el nombre en un playerpref
    /// </summary>
    public void SavePlayerName(string name)
    {
        PlayerPrefs.SetString(PLAYERNAME_KEY, name);
    }

    /// <summary>
    /// Obtener el nombre guardado en el playerpref
    /// </summary>
    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(PLAYERNAME_KEY);
    }


    /// <summary>
    /// Resetear todos los player pref
    /// </summary>
    public void ResetAllPlayerPref()
    {
        PlayerPrefs.DeleteAll();
    }

}
