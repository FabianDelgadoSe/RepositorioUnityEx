using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prueba : Photon.MonoBehaviour
{
    Arrow.adress adress = Arrow.adress.RIGHT;
    PhotonPlayer photonPlayer;


    private void Start()
    {
        photonPlayer = PhotonNetwork.player;
        startConnection();
    }
    /// <summary>
    /// se conecta con el servidor con la version que se le pase
    /// </summary>
	public void startConnection()
    {
        
         PhotonNetwork.ConnectUsingSettings("1");

    }

    /// <summary>
    /// Tiene el proposito de crear una sala
    /// </summary>
    void OnConnectedToMaster()
    {
        // nombre de la sala, opciones de la sala y algo que no se que es
        PhotonNetwork.JoinOrCreateRoom("kevin", new RoomOptions() { maxPlayers = 6 }, null);

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
        photonPlayer = PhotonNetwork.player;
    }


    public void presionar()
    {
        photonView.RPC("llegar",PhotonTargets.Others,adress,null);
    }


    [PunRPC]
    public void llegar(Arrow.adress adress, PhotonPlayer photonPlayer)
    {
        this.photonPlayer = photonPlayer;
        SSTools.ShowMessage(adress + " player " + this.photonPlayer,SSTools.Position.bottom,SSTools.Time.twoSecond);
    }

}
