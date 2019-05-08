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
        FindObjectOfType<ControlTurn>().Bemade = false;

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

        for (int i = 0; i<Bets.Length;i++) {

            if (i == PhotonNetwork.player.ID - 1)
            {
                if (Bets[i] == _winningBet)
                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.SUN_TOKENS_WIN_BET);
                else
                    FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.SUN_TOKENS_LOSE_BET);
            }



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

        // despues de encontrar a al ganador de la apuesta se reinician los valores para la apuesta
        _betMade = false;
        _numberPlayerWithBet = 0;
        FindObjectOfType<BetImage>().finishBet();
    }
    
    
    [PunRPC]
    private void selectBet(Square.typesSquares bet, int index)
    {
        _numberPlayerWithBet++;
        Bets[index-1] = bet;
 
        if (PhotonNetwork.room.PlayerCount == _numberPlayerWithBet)
        {
            FindObjectOfType<ControlRound>().photonView.RPC("reactiveMovementsCards", PhotonTargets.All);
            _betMade = true;
            _controlTurn.mineTurn(_controlTurn.IndexTurn);
            _numberPlayerWithBet = 0;

        }
        else if (FindObjectOfType<ControlTurn>().Bemade)
        {
            FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.OTHER_PLAYERS_THINKING_PREDICTION);
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
