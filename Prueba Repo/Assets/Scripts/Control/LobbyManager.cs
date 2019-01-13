using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Creado para contener funciones que administren el lobby
/// </summary>
public class LobbyManager : Photon.PunBehaviour
{

    [SerializeField] private TextMeshProUGUI playersCountText;
    Room _currentRoom;

    public TextMeshProUGUI PlayersCountText
    {
        get
        {
            return playersCountText;
        }

        set
        {
            playersCountText = value;
        }
    }

    /// <summary>
    /// Funcion usada para inicializar el room y decirle a todas las personas conectadas que 
    /// llamen el metedo showPlayerConnected
    /// </summary>
    void Start()
    {
        _currentRoom = PhotonNetwork.room;
        if (_currentRoom.PlayerCount >= 1)
            photonView.RPC("showPlayersConnected", PhotonTargets.All);

    }


    /// <summary>
    /// Obtiene la variable del NetworkManager para extraer el playercount y mandarlo al txt 
    /// </summary>    
    [PunRPC]
    public void showPlayersConnected()
    {
        playersCountText.text = "PlayerCount\n " + _currentRoom.PlayerCount.ToString();
    }
}
