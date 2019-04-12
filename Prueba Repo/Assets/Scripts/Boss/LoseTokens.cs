using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseTokens : Photon.PunBehaviour
{
    private Square.typesSquares _typeTokens;
    [SerializeField]private GameObject[] _tokens;
    [SerializeField] private Sprite _blueTokens;
    [SerializeField] private Sprite _redTokens;
    [SerializeField] private Sprite _greenTokens;
    [SerializeField] private Sprite _yellowTokens;
    private GameObject _player;
    private int numberTokens;

    private int _redToken = 0;
    private int _blueToken = 0;
    private int _greenToken = 0;
    private int _yellowToken = 0;

    private void OnEnable()
    {

        numberTokens = _player.GetComponent<ControlTokensPlayer>().ObtainedTokens.Count;

        for (int i = 0; i< numberTokens;i++)
        {
            _typeTokens = _player.GetComponent<ControlTokensPlayer>().ObtainedTokens[i];
            _tokens[i].SetActive(true);

            switch (_typeTokens)
            {
                case Square.typesSquares.BLUE:
                    _tokens[i].GetComponent<Image>().sprite = _blueTokens;
                    break;

                case Square.typesSquares.GREEN:
                    _tokens[i].GetComponent<Image>().sprite = _greenTokens;
                    break;

                case Square.typesSquares.RED:
                    _tokens[i].GetComponent<Image>().sprite = _redTokens;
                    break;

                case Square.typesSquares.YELLOW:
                    _tokens[i].GetComponent<Image>().sprite = _yellowTokens;
                    break;
            }
        }


    }

    public void giveTokenToGhost(int index)
    {
        FindObjectOfType<BehaviourGhost>().photonView.RPC("loadnumberplayerTouch",PhotonTargets.AllBuffered);   
        _player.GetComponent<ControlTokensPlayer>().photonView.RPC("loseToken",PhotonTargets.AllBuffered, index);
        gameObject.SetActive(false);
    }


    public Square.typesSquares TypeTokens
    {
        get
        {
            return _typeTokens;
        }

        set
        {
            _typeTokens = value;
        }
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
