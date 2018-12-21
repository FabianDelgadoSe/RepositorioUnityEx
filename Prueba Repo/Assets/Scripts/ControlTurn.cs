using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTurn : Photon.PunBehaviour {
    [SerializeField] private GameObject _myturn;

    private PhotonPlayer[] _playersArray;
    private int _indexTurn;
	void Start () {
        if (PhotonNetwork.masterClient == PhotonNetwork.player)
        {
            _indexTurn = Random.RandomRange(0, PhotonNetwork.room.playerCount);
            _playersArray = PhotonNetwork.playerList;
            photonView.RPC("StarTurn", _playersArray[_indexTurn]);
            FindObjectOfType<ConfigurationBoard>().changeColorBoardSquares();
            Invoke("nextTurn", 2);
        }
        
    }

    void nextTurn()
    {
        photonView.RPC("finishTurn", _playersArray[_indexTurn]);
        if (_indexTurn != PhotonNetwork.room.playerCount-1)
        {
            _indexTurn++;
        }else
        {
            _indexTurn = 0;
        }
        photonView.RPC("StarTurn", _playersArray[_indexTurn]);
        Invoke("nextTurn", 2);
    }

    [PunRPC]
    void StarTurn()
    {
        _myturn.active = true;       
    }

    [PunRPC]
    void finishTurn()
    {
        _myturn.active = false;
    }


}
