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
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }

        if (FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1] == null)
        {
            PlayerInformation playerInformation = new PlayerInformation();
            FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1] = playerInformation;
        }

        FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Character = gameObject;

        if (FindObjectOfType<ControlTurn>().MyTurn)
        {
            photonView.RPC("loadName", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player.NickName);
        }
    }

    [PunRPC]
    private void loadName(string name)
    {

        if (FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1] == null)
        {
            PlayerInformation playerInformation = new PlayerInformation();
            FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1] = playerInformation;
            FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Character = gameObject;
        }

        FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Name = name;

        if (!FindObjectOfType<ControlTurn>().MyTurn)
        {
            OthersPlayersData[] aux = FindObjectsOfType<OthersPlayersData>();
            
            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].IdOfThePlayerThatRepresents == FindObjectOfType<ControlTurn>().IndexTurn)
                {
                    aux[i].assignName();
                }
            }
        }

    }


    [PunRPC]
    private void loadDataObjets(int IDcharacter)
    {
        GetComponent<SpriteRenderer>().sprite = characters[IDcharacter - 1]._iconUnSelected;

        if (!FindObjectOfType<ControlTurn>().MyTurn)
        {
            OthersPlayersData[] aux = FindObjectsOfType<OthersPlayersData>();
            GetComponent<Animator>().runtimeAnimatorController = characters[IDcharacter - 1]._animator;

            for (int i = 0; i < aux.Length; i++)
            {
                if (aux[i].IdOfThePlayerThatRepresents == FindObjectOfType<ControlTurn>().IndexTurn)
                {
                    GetComponent<ControlTokensPlayer>().PortraitThatRepresents = aux[i].gameObject;
                    aux[i].Character = characters[IDcharacter - 1];
                    aux[i].assignFace();
                }
            }
        }

        FindObjectOfType<PlayerDataInGame>().Characters[FindObjectOfType<ControlTurn>().IndexTurn - 1] = characters[IDcharacter - 1];
    }

   
    [PunRPC]
    public void endSelectionBox(Vector3 playerPosition)
    {
        transform.position = playerPosition;
        GetComponent<SpriteRenderer>().enabled = true;
        //GetComponent<BoxCollider2D>().size =
    }

}
