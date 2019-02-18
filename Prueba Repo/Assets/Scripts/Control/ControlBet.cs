using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBet : Photon.PunBehaviour {

    private Square.typesSquares _bet;
    [SerializeField] private GameObject _panelBet;
    private int _numberPlayerWithBet = 0;
    private bool _betMade = false;
    private ControlTurn _controlTurn;

    private void Start()
    {
        _controlTurn = FindObjectOfType<ControlTurn>();
    }

    public void starBet()
    {
        _panelBet.SetActive(true);
    }

    public void betResult(int redTokens, int blueTokens, int greenTokens, int yellowTokens)
    {
        switch (_bet)
        {
            case Square.typesSquares.BLUE:
                if (blueTokens>redTokens && blueTokens>greenTokens && blueTokens>yellowTokens)
                {
                    SSTools.ShowMessage("Ganaste la apuesta",SSTools.Position.bottom,SSTools.Time.threeSecond);
                }
                break;
            case Square.typesSquares.GREEN:
                if (greenTokens > redTokens && greenTokens > blueTokens && greenTokens > yellowTokens)
                {
                    SSTools.ShowMessage("Ganaste la apuesta", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }
                break;
            case Square.typesSquares.RED:
                if (redTokens > greenTokens && redTokens > blueTokens && redTokens > yellowTokens)
                {
                    SSTools.ShowMessage("Ganaste la apuesta", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }
                break;
            case Square.typesSquares.YELLOW:
                if (yellowTokens > redTokens && yellowTokens > blueTokens && yellowTokens > greenTokens)
                {
                    SSTools.ShowMessage("Ganaste la apuesta", SSTools.Position.bottom, SSTools.Time.threeSecond);
                }
                break;
        }
    }

    [PunRPC]
    private void selectBet()
    {
        _numberPlayerWithBet++;
        if (PhotonNetwork.room.PlayerCount == _numberPlayerWithBet)
        {
            _betMade = true;
            _controlTurn.mineTurn(_controlTurn.IndexTurn);
        }


    }


    public Square.typesSquares Bet
    {
        get
        {
            return _bet;
        }

        set
        {
            _bet = value;
        }
    }

    public bool BetMade
    {
        get
        {
            return _betMade;
        }

        set
        {
            _betMade = value;
        }
    }
}
