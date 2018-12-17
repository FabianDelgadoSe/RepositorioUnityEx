using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Obtiene la variable del NetworkManager para extraer el playercount y mandarlo al txt
    [PunRPC]
    public void showPlayersConnected()
    {
        Debug.Log("Tomar room del networkmanager");
        _currentRoom = this.GetComponent<NetworkManager>().currentRoom;
        Debug.Log("Mostrar player conectados");
        playersCountText.text = "PlayerCount\n " + _currentRoom.PlayerCount.ToString();



    }
}
