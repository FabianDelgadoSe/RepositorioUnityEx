﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterSelectionable : Photon.PunBehaviour
{

    [SerializeField] Character _character;

    PlayerData[] _playerData;
    private PlayerData _currentPlayer;
    public bool _isSelected = false;
    [SerializeField] private TextMeshProUGUI _name;
    private PhotonPlayer _playerSelect;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = _character._iconUnSelected;
        _name.text = _character._name;
        _playerData = FindObjectsOfType<PlayerData>();
        Debug.Log(_playerData[0].CharacterSelected);
    }

    [PunRPC]
    public void setCharacterSelection(PhotonPlayer Player)
    {
        _playerSelect = Player;
        _isSelected = true;
        GetComponent<Image>().sprite = _character._iconSelected;
        _name.text = Player.ID.ToString();
    }


    private void assignCharacterToPlayer()
    {
     
            _playerData[0].CharacterSelected = _character;


    }

    [PunRPC]
    private void removeCharacterToPlayer(PhotonPlayer Player)
    {
        _playerSelect = null;
        _isSelected = false;
        GetComponent<Image>().sprite = _character._iconUnSelected;
        _name.text = _character.name;
        
    }

    public void characterClicked()
    {
        if (!_isSelected && _playerData[0].CharacterSelected == null)
        {
            assignCharacterToPlayer();
            photonView.RPC("setCharacterSelection", PhotonTargets.All, PhotonNetwork.player);

        }
        else if (_isSelected && PhotonNetwork.player == _playerSelect)
        {
            _playerData[0].CharacterSelected = null;
            photonView.RPC("removeCharacterToPlayer", PhotonTargets.All, PhotonNetwork.player);
        }
    }



}
