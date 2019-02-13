using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour
{

    void Start()
    {

        Debug.Log("Conectando al sevidor...");
        PhotonNetwork.ConnectUsingSettings("0.0.0");
    }

    private void OnConnectedToMaster()
    {
        Debug.Log("Conectado al Master");

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    private void OnJoinedLobby()
    {

        Debug.Log("Joined to Default Lobby");

    }

}
