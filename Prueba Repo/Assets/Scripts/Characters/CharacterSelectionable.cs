using System.Collections;
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
    private bool _isSelected = false;
    [SerializeField] private TextMeshProUGUI _name;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = _character._iconUnSelected;
        _name.text = _character._name;
        _playerData = FindObjectsOfType<PlayerData>();

    }

    [PunRPC]
    public void setCharacterSelection()
    {
        if (!_isSelected)
        {
            assignCharacterToPlayer();

            _isSelected = true;
            GetComponent<Image>().sprite = _character._iconSelected;
            _name.text = _playerData[0].PlayerName;

        }
    }

    private void assignCharacterToPlayer()
    {


        Debug.Log("Player con PlayerData: " + _playerData.Length);
        _playerData[0].CharacterSelected = _character;


    }

    public void characterClicked()
    {
        photonView.RPC("setCharacterSelection", PhotonTargets.All);
    }

}
