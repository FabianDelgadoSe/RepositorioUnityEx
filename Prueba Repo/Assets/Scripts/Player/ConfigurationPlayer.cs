using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationPlayer : Photon.PunBehaviour
{
    private PlayerDataInGame _currentPlayer;
    [SerializeField] private Character[] characters;
    private void Start()
    {
        _currentPlayer = FindObjectOfType<PlayerDataInGame>();
        if (photonView.isMine)
        {
            photonView.RPC("loadDataObjets", PhotonTargets.All, _currentPlayer.CharacterSelected._IDCharacter);
            FindObjectOfType<ControlTokens>().Player = gameObject; // le envia al control de tokens a que player tiene que revisar 

        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        if(FindObjectOfType<PlayerDataInGame>().CharactersInGame.Length != PhotonNetwork.room.PlayerCount)
            FindObjectOfType<PlayerDataInGame>().CharactersInGame = new PlayerInformation[PhotonNetwork.room.PlayerCount];


        FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn-1] = new PlayerInformation();
        FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Character = gameObject;

        

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

        if (!FindObjectOfType<ControlTurn>().MyTurn)
        {
            OthersPlayersData[] aux = FindObjectsOfType<OthersPlayersData>();

            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].IdOfThePlayerThatRepresents == FindObjectOfType<ControlTurn>().IndexTurn)
                {
                    GetComponent<ControlTokensPlayer>().PortraitThatRepresents = aux[i].gameObject;
                }
            }
        }
    }
}
