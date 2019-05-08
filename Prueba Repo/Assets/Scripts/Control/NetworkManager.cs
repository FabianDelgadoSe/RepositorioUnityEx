using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// Clase nesesaria para toda la configuracion cuando se conecta con el servidor
/// </summary>
public class NetworkManager : Photon.MonoBehaviour
{

    public string _version;
    [SerializeField] private TMP_Text _roomName;
    [SerializeField] private GameObject _connectingTextPrefab;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _blackScreenOut;

    /// <summary>
    /// se conecta con el servidor con la version que se le pase
    /// </summary>
	public void startConnection()
    {
        if (_roomName.text != "")
        {
            _blackScreenOut.SetActive(true);
            _connectingTextPrefab.SetActive(true);
            _backButton.SetActive(false);
            Invoke("CreateRoom", 1);
        }
            //PhotonNetwork.ConnectUsingSettings(_version);
    }

    private void CreateRoom()
    {
        PhotonNetwork.CreateRoom(_roomName.text, new RoomOptions() { maxPlayers = 6 }, null);

    }

    /// <summary>
    /// Tiene el proposito de crear una sala
    /// </summary>
    void OnConnectedToMaster()
    {
        // nombre de la sala, opciones de la sala y algo que no se que es
        PhotonNetwork.CreateRoom(_roomName.text, new RoomOptions() { maxPlayers = 6 }, null);
        
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

        Debug.Log("entre a la sala");
        SceneManager.LoadScene("Lobby");

        PhotonNetwork.Instantiate("PlayerPrefab", Vector3.zero, Quaternion.identity, 0);

        PhotonNetwork.automaticallySyncScene = true;


    }



}

