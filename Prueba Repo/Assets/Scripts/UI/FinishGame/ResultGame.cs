using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResultGame : MonoBehaviour
{
    public List<int> _tokensList = new List<int>();
    public List<int> _scoreList = new List<int>();
    private PlayerDataInGame _playerDataInGame;
    private int _token = 0;
    private int _valueScore = 32;
    private int _playerscoincidence = 0;
    private int _acumulatedValued = 0;
    private bool _coincidence = false;

    [Header("todo lo que mostrara la informacion")]
    [SerializeField]
    private GameObject[] _names;
    [SerializeField] private GameObject[] _tokens;
    [SerializeField] private GameObject[] _imagenTokens;
    [SerializeField] private GameObject[] _givePoints;
    [SerializeField] private GameObject[] _score;
    [SerializeField] private GameObject[] _equals;
    [SerializeField] private GameObject[] _totalScore;

    [Header("sprite Tokens")]
    [SerializeField]
    private Sprite _redToken;
    [SerializeField] private Sprite _blueToken;
    [SerializeField] private Sprite _greenToken;
    [SerializeField] private Sprite _yellowToken;

    [SerializeField] private GameObject _blackScreen;

    private List<bool> _usedNames = new List<bool>();

    //------------NEW VARIABLES-----------------

    private int _panelIndex = 0;


    [Header("Panels")]
    [SerializeField]
    private GameObject _firstPanel;
    [SerializeField] private GameObject _secondPanel;
    [SerializeField] private GameObject _thirdPanel;

    [SerializeField] private Sprite _defaultIcon;

    [Header("First Panel Objects")]
    [SerializeField]
    private TMP_Text _MyRankTxt;
    [SerializeField] private GameObject _myCharImage;

    [Header("Second Panel Objects")]
    [SerializeField]
    private List<GameObject> _playersRanks = new List<GameObject>();
    List<int> _playerDataScores = new List<int>();
    List<string> _playerDataNames = new List<string>();
    List<Sprite> _playerDataIcons = new List<Sprite>();


    [Header("Third Panel Objects")]
    [SerializeField]
    private List<GameObject> _playerStats = new List<GameObject>();
    private List<int> _redTokenSortedByPlayer = new List<int>();
    private List<int> _blueTokenSortedByPlayer = new List<int>();
    private List<int> _yellowTokenSortedByPlayer = new List<int>();
    private List<int> _greenTokenSortedByPlayer = new List<int>();

    private void Start()
    {
        _playerDataInGame = FindObjectOfType<PlayerDataInGame>();
        //activeUI();

        for (int i = 0; i < 4; i++)
        {
            loadTokens();
            givePoints();
            loadListBoolNames();
            loadScore();
            _token++;
        }

        LoadSortedAndIconsSortered();
        SetFirstPanel(true);
    }

    private void SetFirstPanel(bool active)
    {
        if (active)
        {
            _firstPanel.SetActive(true);

            int myScoreAux = _playerDataInGame.CharactersInGame[PhotonNetwork.player.ID - 1].Score;

            for (int i = 0; i < _playerDataScores.Count; i++)
            {
                if (_playerDataScores[i] == myScoreAux)
                {
                    _MyRankTxt.text = (i + 1).ToString();
                }
            }

            _myCharImage.GetComponent<Image>().sprite = _playerDataInGame._characterSelected._faceCharacter;
        }
        else
        {
            _firstPanel.SetActive(false);
        }
    }

    private void SetSecondPanel(bool active)
    {
        if (active)
        {
            _secondPanel.SetActive(true);

            for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
            {
                _playersRanks[i].SetActive(true);
                if (_playersRanks[i].GetComponent<SetPlayerRankPanels>())
                {
                    _playersRanks[i].GetComponent<SetPlayerRankPanels>().LoadData(_playerDataNames[i], _playerDataScores[i].ToString(), _playerDataIcons[i]);
                }
            }
        }
        else
        {
            _secondPanel.SetActive(false);
        }
    }

    private void SetThirdPanel(bool active)
    {
        if (active)
        {
            _thirdPanel.SetActive(true);

            //FILL RED TOKEN TOTAL LIST

            foreach (int scoreAux in _playerDataScores)
            {
                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    if (scoreAux ==_playerDataInGame.CharactersInGame[i].Score)
                    {
                        _redTokenSortedByPlayer.Add(_playerDataInGame.CharactersInGame[i].RedTokens);
                    }
                }                
            }

            //FILL BLUE TOKEN TOTAL LIST

            foreach (int scoreAux in _playerDataScores)
            {
                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    if (scoreAux == _playerDataInGame.CharactersInGame[i].Score)
                    {
                        _blueTokenSortedByPlayer.Add(_playerDataInGame.CharactersInGame[i].BlueTokens);
                    }
                }
            }

            //FILL GREEN TOTAL LIST

            foreach (int scoreAux in _playerDataScores)
            {
                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    if (scoreAux == _playerDataInGame.CharactersInGame[i].Score)
                    {
                        _greenTokenSortedByPlayer.Add(_playerDataInGame.CharactersInGame[i].GreenTokens);
                    }
                }
            }

            //FILL YELLOW TOTAL LIST

            foreach (int scoreAux in _playerDataScores)
            {
                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    if (scoreAux == _playerDataInGame.CharactersInGame[i].Score)
                    {
                        _yellowTokenSortedByPlayer.Add(_playerDataInGame.CharactersInGame[i].YellowTokens);
                    }
                }
            }



            for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
            {
                _playerStats[i].SetActive(true);
                if (_playerStats[i].GetComponent<SetPlayerStatsPanels>())
                {
                    _playerStats[i].GetComponent<SetPlayerStatsPanels>().LoadData(_playerDataNames[i], _redTokenSortedByPlayer[i].ToString(), _blueTokenSortedByPlayer[i].ToString(), _yellowTokenSortedByPlayer[i].ToString(), _greenTokenSortedByPlayer[i].ToString(), _playerDataIcons[i]);
                }
            }

        }
        else
        {
            _thirdPanel.SetActive(false);


        }
    }

    private void LoadSortedAndIconsSortered()//mayor a menor
    {
        _playerDataScores.Clear();
        _playerDataNames.Clear();

        foreach (PlayerInformation playerData in _playerDataInGame.CharactersInGame)
        {
            _playerDataScores.Add(playerData.Score);
        }

        _playerDataScores.Sort();
        _playerDataScores.Reverse();

        foreach (int scoresAux in _playerDataScores)
        {
            for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
            {
                if (_playerDataInGame.CharactersInGame[i].Score == scoresAux)
                {
                    _playerDataNames.Add(_playerDataInGame.CharactersInGame[i].Name);
                    _playerDataIcons.Add(_playerDataInGame.Characters[i]._faceCharacter);
                }
            }
        }
    }

    //--------------OLD METHODS--------------------

    public void nextGem()
    {

        _blackScreen.GetComponent<Animator>().SetBool("NextGem", true);
        Invoke("quitBlackScreen", 1);
    }

    public void quitBlackScreen()
    {
        _blackScreen.GetComponent<Animator>().SetBool("NextGem", false);
        //_token++;

        /*if (_token <= 3)
        {
            loadTokens();
            givePoints();
            loadListBoolNames();
            loadScore();
        }*/

        _panelIndex++;

        switch (_panelIndex)
        {
            case 1:
                SetFirstPanel(false);
                SetSecondPanel(true);
                break;

            case 2:
                SetSecondPanel(false);
                SetThirdPanel(true);
                break;

            case 3:
                PhotonNetwork.LeaveRoom();
                break;

            default:
                break;
        }
    }

    void OnLeftRoom()
    {
        FindObjectOfType<ChangeScene>().chanScene();
    }

    public void activeUI()
    {
        for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
        {
            _names[i].SetActive(true);
            _tokens[i].SetActive(true);
            _imagenTokens[i].SetActive(true);
            _givePoints[i].SetActive(true);
            _score[i].SetActive(true);
            _equals[i].SetActive(true);
            _totalScore[i].SetActive(true);
        }
    }


    public void loadScore()
    {
        for (int i = 0; i < _tokensList.Count; i++)
        {
            bool _auxGivePoints = false;

            for (int u = 0; u < _playerDataInGame.CharactersInGame.Length; u++)
            {
                switch (_token)
                {
                    case 0:
                        if (_tokensList[i] == _playerDataInGame.CharactersInGame[u].RedTokens && !_usedNames[u])
                        {
                            if (!_auxGivePoints)
                            {
                                _usedNames[u] = true;
                                /*
                                _names[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Name;
                                _tokens[i].GetComponent<TMP_Text>().text = _tokensList[i].ToString();
                                _imagenTokens[i].GetComponent<Image>().sprite = _redToken;
                                _givePoints[i].GetComponent<TMP_Text>().text = (_scoreList[i].ToString()) + "+";
                                _score[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Score.ToString();
                                _totalScore[i].GetComponent<TMP_Text>().text = (_playerDataInGame.CharactersInGame[u].Score + _scoreList[i]).ToString();
                                */

                                _playerDataInGame.CharactersInGame[u].Score += _scoreList[i]; // ahora si suma el score
                            }
                        }

                        break;

                    case 1:
                        if (_tokensList[i] == _playerDataInGame.CharactersInGame[u].BlueTokens && !_usedNames[u])
                        {
                            if (!_auxGivePoints)
                            {
                                _usedNames[u] = true;
                                /*
                                _names[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Name;
                                _tokens[i].GetComponent<TMP_Text>().text = _tokensList[i].ToString();
                                _imagenTokens[i].GetComponent<Image>().sprite = _blueToken;
                                _givePoints[i].GetComponent<TMP_Text>().text = (_scoreList[i].ToString()) + "+";
                                _score[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Score.ToString();
                                _totalScore[i].GetComponent<TMP_Text>().text = (_playerDataInGame.CharactersInGame[u].Score + _scoreList[i]).ToString();
                                */

                                _playerDataInGame.CharactersInGame[u].Score += _scoreList[i]; // ahora si suma el score
                            }
                        }

                        break;

                    case 2:
                        if (_tokensList[i] == _playerDataInGame.CharactersInGame[u].GreenTokens && !_usedNames[u])
                        {
                            if (!_auxGivePoints)
                            {
                                _usedNames[u] = true;
                                /*
                                _names[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Name;
                                _tokens[i].GetComponent<TMP_Text>().text = _tokensList[i].ToString();
                                _imagenTokens[i].GetComponent<Image>().sprite = _greenToken;
                                _givePoints[i].GetComponent<TMP_Text>().text = (_scoreList[i].ToString()) + "+";
                                _score[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Score.ToString();
                                _totalScore[i].GetComponent<TMP_Text>().text = (_playerDataInGame.CharactersInGame[u].Score + _scoreList[i]).ToString();
                                */

                                _playerDataInGame.CharactersInGame[u].Score += _scoreList[i]; // ahora si suma el score
                            }
                        }

                        break;

                    case 3:

                        if (_tokensList[i] == _playerDataInGame.CharactersInGame[u].YellowTokens && !_usedNames[u])
                        {
                            if (!_auxGivePoints)
                            {
                                _usedNames[u] = true;
                                /*
                                _names[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Name;
                                _tokens[i].GetComponent<TMP_Text>().text = _tokensList[i].ToString();
                                _imagenTokens[i].GetComponent<Image>().sprite = _yellowToken;
                                _givePoints[i].GetComponent<TMP_Text>().text = (_scoreList[i].ToString()) + "+";
                                _score[i].GetComponent<TMP_Text>().text = _playerDataInGame.CharactersInGame[u].Score.ToString();
                                _totalScore[i].GetComponent<TMP_Text>().text = (_playerDataInGame.CharactersInGame[u].Score + _scoreList[i]).ToString();
                                */

                                _playerDataInGame.CharactersInGame[u].Score += _scoreList[i]; // ahora si suma el score
                            }
                        }
                        break;
                }

            }
        }

    }

    public void loadListBoolNames()
    {
        _usedNames.Clear();
        for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
        {
            _usedNames.Add(false);
        }
    }

    public void givePoints()
    {
        _tokensList.Sort();
        _tokensList.Reverse();
        _scoreList.Clear();

        _playerscoincidence = 0;
        _acumulatedValued = 0;
        _valueScore = 32;
        _coincidence = false;

        for (int i = 0; i < _tokensList.Count; i++)
        {
            if (_tokensList[i] != 0)
            {


                if (_tokensList.Count > i + 1)
                {
                    if (_tokensList[i] == _tokensList[i + 1])
                    {
                        //coinciden valores se tiene que sacar el promedio
                        _playerscoincidence++;
                        _acumulatedValued += _valueScore;
                        _coincidence = true;
                        _scoreList.Add(0);
                    }
                    else
                    {
                        if (!_coincidence)
                        {
                            _scoreList.Add(_valueScore);
                        }
                        else
                        {
                            _acumulatedValued += _valueScore;
                            _acumulatedValued /= _playerscoincidence + 1;
                            _scoreList.Add(0);

                            for (int u = 0; u <= _playerscoincidence; u++)
                            {
                                _scoreList[i - u] = _acumulatedValued;
                            }

                            _playerscoincidence = 0;
                            _acumulatedValued = 0;
                            _coincidence = false;

                        }
                    }
                }
                // cuando es la ultima casilla
                else if (_tokensList.Count == i + 1)
                {
                    if (!_coincidence)
                    {
                        _scoreList.Add(_valueScore);
                    }
                    else
                    {
                        _acumulatedValued += _valueScore;
                        _acumulatedValued /= _playerscoincidence + 1;
                        _scoreList.Add(0);

                        for (int u = 0; u <= _playerscoincidence; u++)
                        {
                            _scoreList[i - u] = _acumulatedValued;
                        }

                        _playerscoincidence = 0;
                        _acumulatedValued = 0;
                        _coincidence = false;

                    }

                }
                else
                {
                    _scoreList.Add(_valueScore);
                    //coloca el puntaje de una
                }
            }
            else
            {
                _scoreList.Add(0);
            }
            _valueScore -= 4;
        }
    }

    public void loadTokens()
    {
        _tokensList.Clear();

        switch (_token)
        {
            //token rojo
            case 0:

                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    _tokensList.Add(_playerDataInGame.CharactersInGame[i].RedTokens);
                }               
                break;

            //token blue
            case 1:

                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    _tokensList.Add(_playerDataInGame.CharactersInGame[i].BlueTokens);
                }                
                break;

            //token green
            case 2:

                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    _tokensList.Add(_playerDataInGame.CharactersInGame[i].GreenTokens);
                }               
                break;

            //token yellow
            case 3:

                for (int i = 0; i < _playerDataInGame.CharactersInGame.Length; i++)
                {
                    _tokensList.Add(_playerDataInGame.CharactersInGame[i].YellowTokens);
                }               
                break;
        }
    }


}
