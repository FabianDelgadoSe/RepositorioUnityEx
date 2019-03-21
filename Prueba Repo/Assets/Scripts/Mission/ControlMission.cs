using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlMission : Photon.PunBehaviour
{
    [Header ("mensaje inicial de que mision tienes")]
    [SerializeField] private GameObject _showMision;
    [SerializeField] private TMP_Text _textDescriptionMission;

    [Header("donde se leera la mision el resto de partida")]
    [SerializeField] private TMP_Text _textMission;
    [SerializeField] private GameObject _iconMission;

    [Header("Panel de seleccion de recompensa de mision")]
    [SerializeField] private GameObject _panelWinMission;

    private PlayerDataInGame _PlayerDataInGame;
    private int _charactersThatGotMyMission = 0;

    public ConfigurationMission.enumMission _mision = ConfigurationMission.enumMission.NONE;
    private string _missionDescription;
    private bool _reviewMission=true;
    private int numberTokens;
    private int indexAux;

    

    private void Start()
    {
        _PlayerDataInGame = FindObjectOfType<PlayerDataInGame>();
    }

    public void selectPrizeMission(string token)
    {
        switch (token)
        {
            case "RED":
                FindObjectOfType<ControlTokens>().photonView.RPC("earnExtraTokens", PhotonTargets.All, Square.typesSquares.RED,PhotonNetwork.player.ID);
                break;

            case "GREEN":
                FindObjectOfType<ControlTokens>().photonView.RPC("earnExtraTokens", PhotonTargets.All, Square.typesSquares.GREEN, PhotonNetwork.player.ID);
                break;

            case "BLUE":
                FindObjectOfType<ControlTokens>().photonView.RPC("earnExtraTokens", PhotonTargets.All, Square.typesSquares.BLUE, PhotonNetwork.player.ID);
                break;

            case "YELLOW":
                FindObjectOfType<ControlTokens>().photonView.RPC("earnExtraTokens", PhotonTargets.All, Square.typesSquares.YELLOW, PhotonNetwork.player.ID);
                break;

            default:
                SSTools.ShowMessage("coinside", SSTools.Position.bottom, SSTools.Time.threeSecond);
                break;
        }

        if (FindObjectOfType<ControlTurn>().MineId < _PlayerDataInGame.CharactersInGame.Length)
        {
            photonView.RPC("ReviewMission", _PlayerDataInGame.CharactersInGame[FindObjectOfType<ControlTurn>().MineId].Character.GetComponent<PlayerMove>().IdOwner);
        }
        else
        {
            photonView.RPC("ReviewMission", _PlayerDataInGame.CharactersInGame[0].Character.GetComponent<PlayerMove>().IdOwner);
        }
        _panelWinMission.SetActive(false);
    }

   [PunRPC]
   public void desactivePanelMission()
    {
        _iconMission.SetActive(false);
    }

    [PunRPC]
    public void ReviewMission()
    {
        if (_reviewMission)
        {
            _reviewMission = false;
            if (resultMission(_mision))
            {
                _panelWinMission.SetActive(true);
            }
            else
            {
                SSTools.ShowMessage("No gane la mision",SSTools.Position.bottom,SSTools.Time.threeSecond);

                if (FindObjectOfType<ControlTurn>().MineId < _PlayerDataInGame.CharactersInGame.Length)
                {
                    photonView.RPC("ReviewMission", _PlayerDataInGame.CharactersInGame[FindObjectOfType<ControlTurn>().MineId].Character.GetComponent<PlayerMove>().IdOwner);
                }
                else
                {
                    photonView.RPC("ReviewMission", _PlayerDataInGame.CharactersInGame[0].Character.GetComponent<PlayerMove>().IdOwner);
                }
                
            }
        }
        else
        {
            FindObjectOfType<ControlRound>().photonView.RPC("reactiveMovementsCards", PhotonTargets.All);
        }
    }

    public void distributeMissions()
    {
        FindObjectOfType<ConfigurationMission>().selectMision();
        _textDescriptionMission.text = MissionDescription;
        _showMision.SetActive(true);
        _textMission.text = MissionDescription;
        _reviewMission = true;
        Invoke("finishDistributeMissions",5);
    }

    public void finishDistributeMissions()
    {
        FindObjectOfType<ControlTurn>().mineTurn(FindObjectOfType<ControlTurn>().IndexTurn);
    }

    public bool missionReceived()
    {
        if (Mision == ConfigurationMission.enumMission.NONE)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
       

    /// <summary>
    /// contiene todas las evaluaciones sobre las misiones y recibe la mision que debe evaluar
    /// </summary>
    /// <param name="mission"></param>
    /// <returns></returns>
    public bool resultMission(ConfigurationMission.enumMission mission)
    {
        _charactersThatGotMyMission = 0;

        switch (mission)
        {
            case ConfigurationMission.enumMission.ANY_3_RED:
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken >= 3)
                    {
                        _charactersThatGotMyMission++;
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_3_GREEN:
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken >= 3)
                    {
                        _charactersThatGotMyMission++;
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_3_BLUE:
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken >= 3)
                    {
                        _charactersThatGotMyMission++;
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_3_YELLOW:
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken >= 3)
                    {
                        _charactersThatGotMyMission++;
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_2_BLUE_2_RED:

                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken >= 2)
                    {
                        if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken >= 2)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_2_BLUE_2_YELLOW:

                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken >= 2)
                    {
                        if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken >= 2)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_2_BLUE_2_GREEN:

                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken >= 2)
                    {
                        if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken >= 2)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_2_RED_2_YELLOW:

                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken >= 2)
                    {
                        if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken >= 2)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_2_RED_2_GREEN:

                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken >= 2)
                    {
                        if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken >= 2)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }
                break;

            case ConfigurationMission.enumMission.ANY_2_YELLOW_2_GREEN:

                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken >= 2)
                    {
                        if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken >= 2)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }
                break;

            case ConfigurationMission.enumMission.RIGHT_MAJOR_RED:

                if (FindObjectOfType<ControlTurn>().MineId -2 <0)
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 1;                    
                }
                else
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 2;
                }

                numberTokens = _PlayerDataInGame.CharactersInGame[indexAux].Character.GetComponent<ControlTokensPlayer>().RedToken;

                //evalua a cuantos jugares supero en el numero de tokens
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (i != indexAux)
                    {
                        if (numberTokens > _PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }

                // si tuvo mayor numero de ese tokens para todos los jugadores menos el entonces gana
                if (_charactersThatGotMyMission == _PlayerDataInGame.CharactersInGame.Length-1)
                {
                    _charactersThatGotMyMission = 1;
                }
                else
                {
                    _charactersThatGotMyMission = 0;
                }

                break;

            case ConfigurationMission.enumMission.RIGHT_MAJOR_BLUE:

                if (FindObjectOfType<ControlTurn>().MineId - 2 < 0)
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 1;
                }
                else
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 2;
                }


                numberTokens = _PlayerDataInGame.CharactersInGame[indexAux].Character.GetComponent<ControlTokensPlayer>().BlueToken;

                //evalua a cuantos jugares supero en el numero de tokens
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (i != indexAux)
                    {
                        if (numberTokens > _PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }
                // si tuvo mayor numero de ese tokens para todos los jugadores menos el entonces gana
                if (_charactersThatGotMyMission == _PlayerDataInGame.CharactersInGame.Length - 1)
                {
                    _charactersThatGotMyMission = 1;
                }
                else
                {
                    _charactersThatGotMyMission = 0;
                }
                break;

            case ConfigurationMission.enumMission.RIGHT_MAJOR_GREEN:


                if (FindObjectOfType<ControlTurn>().MineId - 2 < 0)
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 1;
                }
                else
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 2;
                }


                numberTokens = _PlayerDataInGame.CharactersInGame[indexAux].Character.GetComponent<ControlTokensPlayer>().GreenToken;

                //evalua a cuantos jugares supero en el numero de tokens
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (i != indexAux)
                    {
                        if (numberTokens > _PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }

                // si tuvo mayor numero de ese tokens para todos los jugadores menos el entonces gana
                if (_charactersThatGotMyMission == _PlayerDataInGame.CharactersInGame.Length - 1)
                {
                    _charactersThatGotMyMission = 1;
                }
                else
                {
                    _charactersThatGotMyMission = 0;
                }
                break;

            case ConfigurationMission.enumMission.RIGHT_MAJOR_YELLOW:

                if (FindObjectOfType<ControlTurn>().MineId - 2 < 0)
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 1;
                }
                else
                {
                    indexAux = FindObjectOfType<ControlTurn>().MineId - 2;
                }


                numberTokens = _PlayerDataInGame.CharactersInGame[indexAux].Character.GetComponent<ControlTokensPlayer>().YellowToken;

                //evalua a cuantos jugares supero en el numero de tokens
                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (i != indexAux)
                    {
                        if (numberTokens > _PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken)
                        {
                            _charactersThatGotMyMission++;
                        }
                    }
                }

                // si tuvo mayor numero de ese tokens para todos los jugadores menos el entonces gana
                if (_charactersThatGotMyMission == _PlayerDataInGame.CharactersInGame.Length - 1)
                {
                    _charactersThatGotMyMission = 1;
                }
                else
                {
                    _charactersThatGotMyMission = 0;
                }
                break;


            case ConfigurationMission.enumMission.ANY_ALL_COLORS:

                for (int i = 0; i < _PlayerDataInGame.CharactersInGame.Length; i++)
                {
                    if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().YellowToken >= 1)
                    {
                        if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().GreenToken >= 1)
                        {
                            if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().BlueToken >= 1)
                            {
                                if (_PlayerDataInGame.CharactersInGame[i].Character.GetComponent<ControlTokensPlayer>().RedToken >= 1)
                                {
                                    _charactersThatGotMyMission++;
                                }
                            }
                        }
                    }
                }
                break;
        }

        Mision = ConfigurationMission.enumMission.NONE;

        if (_charactersThatGotMyMission > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    //GET Y SET
    public ConfigurationMission.enumMission Mision
    {
        get
        {
            return _mision;
        }

        set
        {
            _mision = value;
        }
    }

    public string MissionDescription
    {
        get
        {
            return _missionDescription;
        }

        set
        {
            _missionDescription = value;
        }
    }

    public GameObject IconMission
    {
        get
        {
            return _iconMission;
        }

        set
        {
            _iconMission = value;
        }
    }
}
