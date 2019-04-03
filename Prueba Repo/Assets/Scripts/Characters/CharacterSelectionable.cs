using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterSelectionable : Photon.PunBehaviour
{

    [SerializeField] Character _character;

    PlayerDataInGame _playerData;
    private PlayerDataInGame _currentPlayer;
    public bool _isSelected = false;
    [SerializeField] private TextMeshProUGUI _name;
    private PhotonPlayer _playerSelect;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = _character._iconUnSelected;
        _name.text = _character._name;
        _playerData = FindObjectOfType<PlayerDataInGame>();
    }

    /// <summary>
    /// Le da la propiedad de ese personaje al player que se le envio
    /// </summary>
    /// <param name="Player"></param>
    [PunRPC]
    public void setCharacterSelection(PhotonPlayer Player)
    {
        _playerSelect = Player;
        _isSelected = true;
        GetComponent<Image>().sprite = _character._iconSelected;
        _name.text = Player.NickName.ToString();
        FindObjectOfType<LobbyManager>().allPlayerSelectCharacter();
    }


    /// <summary>
    /// quita le quita la propiedad de ese personaje a player que se envio
    /// </summary>
    /// <param name="Player"></param>
    [PunRPC]
    private void removeCharacterToPlayer(PhotonPlayer Player)
    {
        _playerSelect = null;
        _isSelected = false;
        GetComponent<Image>().sprite = _character._iconUnSelected;
        _name.text = _character.name;
        FindObjectOfType<LobbyManager>().allPlayerSelectCharacter();
    }

    /// <summary>
    /// Evalua el que tiene que hacer cuando se da clic sobre un personaje
    /// </summary>
    public void characterClicked()
    {
        if (!_isSelected && _playerData.CharacterSelected == null)
        {
            _playerData.CharacterSelected = _character;
            FindObjectOfType<LobbyManager>().SelectedCharacter = gameObject;
            photonView.RPC("setCharacterSelection", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player);

        }
        else if(!_isSelected && _playerData.CharacterSelected != null)
        {
            // le quita deselecciona el personaje anterior
            FindObjectOfType<LobbyManager>().SelectedCharacter.GetComponent<CharacterSelectionable>().
                photonView.RPC("removeCharacterToPlayer", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player);

            // selecciona el nuevo personaje
            _playerData.CharacterSelected = _character;
            FindObjectOfType<LobbyManager>().SelectedCharacter = gameObject;
            photonView.RPC("setCharacterSelection", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player);
        }
        else if (_isSelected && PhotonNetwork.player == _playerSelect)
        {
            _playerData.CharacterSelected = null;
            FindObjectOfType<LobbyManager>().SelectedCharacter = null;
            photonView.RPC("removeCharacterToPlayer", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player);
        }
    }


}
