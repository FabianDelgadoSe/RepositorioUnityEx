using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlTokens : Photon.PunBehaviour {

    [SerializeField] private Image _redTokens;
    [SerializeField] private Image _greenTokens;
    [SerializeField] private Image _blueTokens;
    [SerializeField] private Image _yellowTokens;

    private GameObject _player;

    public void drawMyTokensValues()
    {
        /*_player = FindObjectOfType<PlayerData>().CharactersInGame[GetComponent<ControlTurn>().IndexTurn - 1];
        _player.GetComponent<ControlTokensPlayer>().newToken();
        if (_player.GetComponent<PhotonView>().isMine)
        {
            _redTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().RedToken;
            _greenTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().GreenToken;
            _blueTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().BlueToken;
            _yellowTokens.GetComponentInChildren<TMP_Text>().text = "x" + Player.GetComponent<ControlTokensPlayer>().YellowToken;
        }

    */
    }

    public void resetTokens()
    {
        Player.GetComponent<ControlTokensPlayer>().RedToken = 0;
        Player.GetComponent<ControlTokensPlayer>().GreenToken = 0;
        Player.GetComponent<ControlTokensPlayer>().BlueToken = 0;
        Player.GetComponent<ControlTokensPlayer>().YellowToken = 0;
    }


    public GameObject Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }
}
