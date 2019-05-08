using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBetweenScenes : Photon.PunBehaviour
{

    private PlayerDataInGame _playerDataInGame;
    private int _numberPlayers;

    [SerializeField] private List<GameObject> _addTokenButtons = new List<GameObject>();

    private List<int> previousRedToken = new List<int>();
    private List<int> previousBlueToken = new List<int>();
    private List<int> previousGreenToken = new List<int>();
    private List<int> previousYellowToken = new List<int>();

    [Header("Paneles de tokens")]
    [SerializeField]
    List<GameObject> _redPanel = new List<GameObject>();
    [SerializeField] List<GameObject> _bluePanel = new List<GameObject>();
    [SerializeField] List<GameObject> _yellowPanel = new List<GameObject>();
    [SerializeField] List<GameObject> _greenPanel = new List<GameObject>();

    private void OnEnable()
    {
        _playerDataInGame = FindObjectOfType<PlayerDataInGame>();
        _numberPlayers = PhotonNetwork.room.PlayerCount;
        SetPanels();
    }

    private void SetPanels()
    {

        previousRedToken.Clear();
        previousBlueToken.Clear();
        previousYellowToken.Clear();
        previousGreenToken.Clear();

        for (int i = 0; i < _numberPlayers; i++)
        {
            _redPanel[_playerDataInGame.CharactersInGame[i].RedTokens].
                   GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            previousRedToken.Add(_playerDataInGame.CharactersInGame[i].RedTokens);

            _bluePanel[_playerDataInGame.CharactersInGame[i].BlueTokens].
                    GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            previousBlueToken.Add(_playerDataInGame.CharactersInGame[i].BlueTokens);

            _yellowPanel[_playerDataInGame.CharactersInGame[i].YellowTokens].
                    GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            previousYellowToken.Add(_playerDataInGame.CharactersInGame[i].YellowTokens);

            _greenPanel[_playerDataInGame.CharactersInGame[i].GreenTokens].
                    GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            previousGreenToken.Add(_playerDataInGame.CharactersInGame[i].GreenTokens);
        }
    }

    IEnumerator DesactiveObjectInvoke(GameObject _go, int indexToken, int indexList)
    {
        yield return new WaitForSeconds(1);
        _go.SetActive(false);
        ActiveIcon(indexToken, indexList);
    }

    private void ActiveIcon(int indexToken, int indexList)
    {
        switch (indexToken)//1-RED 2-BLUE 3-YELLOW 4-GREEN
        {
            case 1:
                _redPanel[_playerDataInGame.CharactersInGame[indexList].RedTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(indexList, _playerDataInGame.CharactersInGame[indexList].Character.GetComponent<SpriteRenderer>().sprite);
                break;

            case 2:
                _bluePanel[_playerDataInGame.CharactersInGame[indexList].BlueTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(indexList, _playerDataInGame.CharactersInGame[indexList].Character.GetComponent<SpriteRenderer>().sprite);
                break;

            case 3:
                _yellowPanel[_playerDataInGame.CharactersInGame[indexList].YellowTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(indexList, _playerDataInGame.CharactersInGame[indexList].Character.GetComponent<SpriteRenderer>().sprite);
                break;

            case 4:
                _greenPanel[_playerDataInGame.CharactersInGame[indexList].GreenTokens].
               GetComponent<PanelDataIconPlayer>().positionIcon(indexList, _playerDataInGame.CharactersInGame[indexList].Character.GetComponent<SpriteRenderer>().sprite);
                break;

            default:
                break;
        }

    }

    [PunRPC]
    public void UpdatePanels()
    {
        for (int i = 0; i < _numberPlayers; i++)
        {

            //red
            if (_playerDataInGame.CharactersInGame[i].RedTokens != previousRedToken[i])
            {
                _redPanel[previousRedToken[i]].GetComponent<PanelDataIconPlayer>().DeactiveIcon(i);
                ActiveIcon(1, i);
            }
            else
            {
                _redPanel[_playerDataInGame.CharactersInGame[i].RedTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            }
            previousRedToken[i] = (_playerDataInGame.CharactersInGame[i].RedTokens);

            //blue
            if (_playerDataInGame.CharactersInGame[i].BlueTokens != previousBlueToken[i])
            {
                _bluePanel[previousBlueToken[i]].GetComponent<PanelDataIconPlayer>().DeactiveIcon(i);
                ActiveIcon(2, i);
            }
            else
            {
                _bluePanel[_playerDataInGame.CharactersInGame[i].BlueTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            }
            previousBlueToken[i] = (_playerDataInGame.CharactersInGame[i].BlueTokens);

            //yellow
            if (_playerDataInGame.CharactersInGame[i].YellowTokens != previousYellowToken[i])
            {
                _yellowPanel[previousYellowToken[i]].GetComponent<PanelDataIconPlayer>().DeactiveIcon(i);
                ActiveIcon(3, i);
            }
            else
            {
                _yellowPanel[_playerDataInGame.CharactersInGame[i].YellowTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            }
            previousYellowToken[i] = (_playerDataInGame.CharactersInGame[i].YellowTokens);

            //green
            if (_playerDataInGame.CharactersInGame[i].GreenTokens != previousGreenToken[i])
            {
                _greenPanel[previousGreenToken[i]].GetComponent<PanelDataIconPlayer>().DeactiveIcon(i);
                ActiveIcon(4, i);
            }
            else
            {
                _greenPanel[_playerDataInGame.CharactersInGame[i].GreenTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);
            }
            previousGreenToken[i] = (_playerDataInGame.CharactersInGame[i].GreenTokens);

        }
    }

    public void ActiveAddButtons()
    {
        foreach (GameObject button in _addTokenButtons)
        {
            button.SetActive(true);
        }

    }

    public void desactiveAddButtons()
    {
        foreach (GameObject button in _addTokenButtons)
        {
            button.SetActive(false);
        }

    }

}
