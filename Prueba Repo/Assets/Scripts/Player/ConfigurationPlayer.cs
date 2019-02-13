﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationPlayer : Photon.PunBehaviour
{
    private PlayerData _currentPlayer;
    [SerializeField] private Character[] characters;
    private void Start()
    {
        _currentPlayer = FindObjectOfType<PlayerData>();
        if (photonView.isMine)
        {
            photonView.RPC("loadDataObjets", PhotonTargets.All, _currentPlayer.CharacterSelected._IDCharacter);

        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        if(FindObjectOfType<PlayerData>().CharactersInGame.Length != PhotonNetwork.room.PlayerCount)
            FindObjectOfType<PlayerData>().CharactersInGame = new GameObject[PhotonNetwork.room.PlayerCount];

        FindObjectOfType<PlayerData>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1] = gameObject;



    }

    [PunRPC]
    private void loadDataObjets(int IDcharacter)
    {
        GetComponent<SpriteRenderer>().sprite = characters[IDcharacter-1]._iconUnSelected;
    }


    [PunRPC]
    public void endSelectionBox()
    {
        GetComponent<PhotonTransformView>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}