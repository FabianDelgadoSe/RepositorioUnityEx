using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Creado para contener funciones que administren el lobby
/// </summary>
public class LobbyManager : Photon.PunBehaviour
{

    [SerializeField] private TextMeshProUGUI _playersCountText;
    [SerializeField] private TextMeshProUGUI _roomNameText;

    Room _currentRoom;

    public TextMeshProUGUI PlayersCountText
    {
        get
        {
            return _playersCountText;
        }

        set
        {
            _playersCountText = value;
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
            photonView.RPC("SetLobbyUI", PhotonTargets.All);

    }


    /// <summary>
    /// Obtiene la variable del NetworkManager para extraer el playercount y mandarlo al txt, obtiene nombre del room
    /// </summary>    
    [PunRPC]
    public void SetLobbyUI()
    {
        _playersCountText.text = "Players: " + _currentRoom.PlayerCount.ToString();
        _roomNameText.text = "Sala: " + _currentRoom.Name;
    }



    /// <summary>
    /// Evalua si todos los jugadores pickearon un personaje, si es así, cambia la escena
    /// </summary>  
    public void ChangeSceneIfAllPlayersSelect()
    {
        List<CharacterSelectionable> _charactersSelectionable = FindObjectsOfType<CharacterSelectionable>().ToList();
       
        int _charactersSelectedCount = 0;

        foreach (CharacterSelectionable charactersSelectionable in _charactersSelectionable) {

            if (charactersSelectionable._isSelected)
            {
                _charactersSelectedCount++;
            }
        }
   
        if (_charactersSelectedCount == _currentRoom.playerCount) {

            FindObjectOfType<ChangeScene>().chansy();
        }
        else
        {
            Debug.Log("Faltan jugadores por seleccionar");
        }

    }

}
