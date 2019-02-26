using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBet : MonoBehaviour {

    [SerializeField] private GameObject _panelBet;
    [SerializeField] private GameObject _bet;
    private ControlBet _controlBet;
    private void Start()
    {
        _controlBet = FindObjectOfType<ControlBet>();
    }

    public void hidePanel()
    {
        if (_panelBet.GetActive())
        {
            _panelBet.SetActive(false);
        }
        else
        {
            _panelBet.SetActive(true);
        }
    }

    public void selectBet(string gem)
    {
        switch (gem)
        {
            case "RED":
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.RED, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.RED);
                _bet.SetActive(false);
                break;
            case "GREEN":
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.GREEN, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.GREEN);
                _bet.SetActive(false);
                break;
            case "BLUE":
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.BLUE, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.BLUE);
                _bet.SetActive(false);
                break;
            case "YELLOW":
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.YELLOW,FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.YELLOW);
                _bet.SetActive(false);
                break;
            default:
                Debug.Log("opcion erronea");
                break;
        }

        
    }

}
