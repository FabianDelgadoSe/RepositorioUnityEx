using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Clase nesesaria para toda la configuracion cuando se conecta con el servidor
/// </summary>
public class NetworkManager : Photon.MonoBehaviour
{

    public string _version;
    public Room currentRoom;


    /// <summary>
    /// se conecta con el servidor con la version que se le pase
    /// </summary>
	void Start()
    {
        PhotonNetwork.ConnectUsingSettings(_version);
    }

    /// <summary>
    /// Tiene el proposito de crear una sala
    /// </summary>
    void OnConnectedToMaster()
    {
        // nombre de la sala, opciones de la sala y algo que no se que es
        PhotonNetwork.JoinOrCreateRoom("unica", new RoomOptions() { maxPlayers = 6 }, null);

    }

    /// <summary>
    /// Se llama cuando se crear un nuevo cuadro
    /// </summary>
    void OnCreatedRoom()
    {
        Debug.Log("cree sala");
    }

    /// <summary>
    /// Se llama cuando se entra a una sala,
    /// contiene una funcion para que se sincroniza todas las scenas de la room
    /// </summary>
    void OnJoinedRoom()
    {
        if (PhotonNetwork.room != null)
        {
            Debug.Log("GuardandoRoom");
            currentRoom = PhotonNetwork.room;
        }

        Debug.Log("entre a la sala");

        //Evalua que haya un campo de texto que llenar
        if (this.GetComponent<LobbyManager>().PlayersCountText != null) {

            //llama la funcion de la clase LobbyManager
            this.GetComponent<PhotonView>().RPC("showPlayersConnected", PhotonTargets.All);

        }

        PhotonNetwork.automaticallySyncScene = true;


    }



}
