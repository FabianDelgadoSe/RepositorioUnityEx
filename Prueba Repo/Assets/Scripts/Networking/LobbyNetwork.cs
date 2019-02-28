using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{
    public GameObject _panelConectando;

    void Start()
    {
        if (PhotonNetwork.connected)
        {
            _panelConectando.SetActive(false);
        }
        else
        {
            Debug.Log("Conectando al sevidor...");
            PhotonNetwork.ConnectUsingSettings("0.0.0");
        }
        
    }

    /// <summary>
    /// Se ejecuta el Photon se conectado al Master
    /// </summary>
    private void OnConnectedToMaster()
    {
        Debug.Log("Conectado al Master");

        PhotonNetwork.player.name = PlayerPrefController.GetInstance().GetPlayerName();
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }


    /// <summary>
    /// Se ejecuta cuando se entra al un lobby
    /// </summary>
    private void OnJoinedLobby()
    {
        _panelConectando.SetActive(false);
        Debug.Log("Joined to Default Lobby");

    }

}
