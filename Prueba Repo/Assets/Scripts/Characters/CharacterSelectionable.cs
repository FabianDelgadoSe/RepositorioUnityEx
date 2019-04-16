﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterSelectionable : Photon.PunBehaviour
{

    [SerializeField] Character _character;
    private LobbyManager _lobbyManager;
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
        _lobbyManager = FindObjectOfType<LobbyManager>();
    }

    /// <summary>
    /// Le da la propiedad de ese personaje al player que se le envio
    /// </summary>
    /// <param name="Player"></param>
    [PunRPC]
    public void setCharacterSelection(PhotonPlayer Player)
    {
        PlayerSelect = Player;
        _isSelected = true;
        GetComponent<Image>().sprite = _character._iconSelected;
        _name.text = Player.NickName.ToString();
        _lobbyManager.allPlayerSelectCharacter();
    }


    /// <summary>
    /// quita le quita la propiedad de ese personaje a player que se envio
    /// </summary>
    [PunRPC]
    private void removeCharacterToPlayer()
    {
        PlayerSelect = null;
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
        if (_lobbyManager.AllowPick)
        {
            if (!_isSelected && _playerData.CharacterSelected == null)
            {
                _playerData.CharacterSelected = _character;
                FindObjectOfType<LobbyManager>().SelectedCharacter = gameObject;
                photonView.RPC("setCharacterSelection", PhotonTargets.All, PhotonNetwork.player);

            }
            else if (!_isSelected && _playerData.CharacterSelected != null)
            {
                // le quita deselecciona el personaje anterior
                FindObjectOfType<LobbyManager>().SelectedCharacter.GetComponent<CharacterSelectionable>().
                    photonView.RPC("removeCharacterToPlayer", PhotonTargets.All);

                // selecciona el nuevo personaje
                _playerData.CharacterSelected = _character;
                FindObjectOfType<LobbyManager>().SelectedCharacter = gameObject;
                photonView.RPC("setCharacterSelection", PhotonTargets.All, PhotonNetwork.player);
            }
            else if (_isSelected && PhotonNetwork.player == PlayerSelect)
            {
                _playerData.CharacterSelected = null;
                FindObjectOfType<LobbyManager>().SelectedCharacter = null;
                photonView.RPC("removeCharacterToPlayer", PhotonTargets.All);
            }
        }
    }

    public PhotonPlayer PlayerSelect
    {
        get
        {
            return _playerSelect;
        }

        set
        {
            _playerSelect = value;
        }
    }

}
