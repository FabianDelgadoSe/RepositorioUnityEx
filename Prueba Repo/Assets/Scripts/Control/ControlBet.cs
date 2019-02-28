using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBet : Photon.PunBehaviour {

    private Square.typesSquares[] _bets;
    [SerializeField] private GameObject _panelBet;
    private int _numberPlayerWithBet = 0;
    private bool _betMade = false;
    private ControlTurn _controlTurn;
    private PlayerDataInGame _playerdata;
    private Square.typesSquares _winningBet = Square.typesSquares.WALL;

    private void Start()
    {
        _controlTurn = FindObjectOfType<ControlTurn>();
        _playerdata = FindObjectOfType<PlayerDataInGame>();
        _bets = new Square.typesSquares[PhotonNetwork.room.PlayerCount];
    }

    public void starBet()
    {
        _panelBet.SetActive(true);
    }


    public void betResult(int redTokens, int blueTokens, int greenTokens, int yellowTokens)
    {
        _winningBet = Square.typesSquares.WALL; 

        if (blueTokens > redTokens && blueTokens > greenTokens && blueTokens > yellowTokens)
        {
            _winningBet = Square.typesSquares.BLUE;
        }
        else if (greenTokens > redTokens && greenTokens > blueTokens && greenTokens > yellowTokens)
        {
            _winningBet = Square.typesSquares.GREEN;
        }
        else if (redTokens > greenTokens && redTokens > blueTokens && redTokens > yellowTokens)
        {
            _winningBet = Square.typesSquares.RED;
        }
        else if (yellowTokens > redTokens && yellowTokens > blueTokens && yellowTokens > greenTokens)
        {
            _winningBet = Square.typesSquares.YELLOW;
        }
        else
        {
            SSTools.ShowMessage("Nadie gana la apuesta", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }

        for (int i = 0; i<Bets.Length;i++) {

            if (Bets[i] == _winningBet) {
                switch (Bets[i])
                {
                    case Square.typesSquares.BLUE:
                        _playerdata.CharactersInGame[i].BlueTokens++;
                        break;

                    case Square.typesSquares.GREEN:
                        _playerdata.CharactersInGame[i].GreenTokens++;
                        break;

                    case Square.typesSquares.RED:
                        _playerdata.CharactersInGame[i].RedTokens++;
                        break;

                    case Square.typesSquares.YELLOW:
                        _playerdata.CharactersInGame[i].YellowTokens++;
                        break;
                }
            }
        }
    }
    
    
    [PunRPC]
    private void selectBet(Square.typesSquares bet, int index)
    {
        _numberPlayerWithBet++;
        Bets[index-1] = bet;
        Debug.Log("el jugador " + index + " aposto por " + Bets[index-1]);        
        
        if (PhotonNetwork.room.PlayerCount == _numberPlayerWithBet)
        {
            _betMade = true;
            _controlTurn.mineTurn(_controlTurn.IndexTurn);
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

    public Square.typesSquares[] Bets
    {
        get
        {
            return _bets;
        }

        set
        {
            _bets = value;
        }
    }
}
