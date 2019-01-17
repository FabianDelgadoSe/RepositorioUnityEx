using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationPlayer : Photon.PunBehaviour
{
    private PlayerData _currentPlayer;
    [SerializeField] private Character[] characters;
    private void Start()
    {
        _currentPlayer = FindObjectOfType<PlayerData>();
        if(photonView.isMine)
            photonView.RPC("loadDataObjets",PhotonTargets.All, _currentPlayer.CharacterSelected._IDCharacter);
        
    }

    [PunRPC]
    private void loadDataObjets(int IDcharacter)
    {
        SSTools.ShowMessage((IDcharacter - 1).ToString(), SSTools.Position.top, SSTools.Time.threeSecond);
        SSTools.ShowMessage((IDcharacter - 1).ToString(),SSTools.Position.bottom,SSTools.Time.threeSecond);
        GetComponent<SpriteRenderer>().sprite = characters[IDcharacter-1]._iconUnSelected;
    }
}
