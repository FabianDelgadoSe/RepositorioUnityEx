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
    [SerializeField] private Button _sbuttonPlay;
    private GameObject _selectedCharacter;
    Room _currentRoom;
    [SerializeField] private bool _allowPick = false;
    [SerializeField] private int _numberPlayersInRoom = 0;


    /// <summary>
    /// Funcion usada para inicializar el room y decirle a todas las personas conectadas que 
    /// llamen el metedo showPlayerConnected
    /// </summary>
    void Start()
    {
        _currentRoom = PhotonNetwork.room;

        photonView.RPC("SetLobbyUI", PhotonTargets.All,PhotonNetwork.player);
    }


    /// <summary>
    /// Obtiene la variable del NetworkManager para extraer el playercount y mandarlo al txt, obtiene nombre del room
    /// </summary>    
    [PunRPC]
    public void SetLobbyUI(PhotonPlayer player)
    {
        _playersCountText.text = "Players: " + _currentRoom.PlayerCount.ToString();
        _roomNameText.text = "Sala: " + _currentRoom.Name;
        someoneSelected(player);
        photonView.RPC("allPlayerSelectCharacter", PhotonTargets.All);
    }

    [PunRPC]
    public void allPlayerSelectCharacter()
    {
        List<CharacterSelectionable> _charactersSelectionable = FindObjectsOfType<CharacterSelectionable>().ToList();

        int _charactersSelectedCount = 0;

        foreach (CharacterSelectionable charactersSelectionable in _charactersSelectionable)
        {

            if (charactersSelectionable._isSelected)
            {
                _charactersSelectedCount++;
            }
        }

        if (_charactersSelectedCount == _currentRoom.playerCount)
        {

            _sbuttonPlay.gameObject.SetActive(true);
        }
        else
        {
            _sbuttonPlay.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// Usado para verificar si un jugador ya habia pickeado un personaje
    /// </summary>
    public void someoneSelected(PhotonPlayer player)
    {
        _allowPick = false;

        if (FindObjectOfType<PlayerDataInGame>().CharacterSelected != null)
        {
            CharacterSelectionable[] aux = FindObjectsOfType<CharacterSelectionable>();
            bool pick = false;
            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].PlayerSelect == PhotonNetwork.player)
                {
                    if (!pick)
                    {
                        aux[i].photonView.RPC("setCharacterSelection", player, PhotonNetwork.player);

                        pick = true;
                    }
                    else
                    {
                        aux[i].photonView.RPC("removeCharacterToPlayer", PhotonTargets.All);
                    }
                }
            }
        }

        photonView.RPC("updatePicks", player);
    }

    [PunRPC]
    public void updatePicks()
    {
        Debug.Log(_numberPlayersInRoom);
        _numberPlayersInRoom++;
        if (_numberPlayersInRoom == PhotonNetwork.room.PlayerCount)
        {
            _numberPlayersInRoom = 0;
            photonView.RPC("againAllowPick", PhotonTargets.All);
        }
    }

    [PunRPC]
    public void againAllowPick()
    {
        _allowPick = true;
    }

    /// <summary>
    /// Evalua si todos los jugadores pickearon un personaje, si es así, cambia la escena
    /// </summary>  
    public void ChangeSceneIfAllPlayersSelect()
    {
        List<CharacterSelectionable> _charactersSelectionable = FindObjectsOfType<CharacterSelectionable>().ToList();

        int _charactersSelectedCount = 0;

        foreach (CharacterSelectionable charactersSelectionable in _charactersSelectionable)
        {

            if (charactersSelectionable._isSelected)
            {
                _charactersSelectedCount++;
            }
        }

        if (_charactersSelectedCount == _currentRoom.playerCount)
        {

            FindObjectOfType<ChangeScene>().chansy();
        }
        else
        {
            SSTools.ShowMessage("Faltan jugadores por seleccionar", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }

    }



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

    public GameObject SelectedCharacter
    {
        get
        {
            return _selectedCharacter;
        }

        set
        {
            _selectedCharacter = value;
        }
    }

    public bool AllowPick
    {
        get
        {
            return _allowPick;
        }

        set
        {
            _allowPick = value;
        }
    }
}
