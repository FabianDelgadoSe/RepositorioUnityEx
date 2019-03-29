using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TextPanelData : MonoBehaviour
{
    [SerializeField] private enumTypeData _typeData;

    public int _indexPlayer;
    private PlayerDataInGame _playerData;
    public enum enumTypeData
    {
        NAME_PLAYER,
        SCORE,
        TOKEN,
        RED_TOKEN,
        GREEN_TOKEN,
        BLUE_TOKEN,
        YELLOW_TOKEN,
        NOTHING
    }


    private void OnEnable()
    {
        loadData();
    }

    public void loadData()
    {
        _playerData = FindObjectOfType<PlayerDataInGame>();

        switch (_typeData)
        {
            case enumTypeData.SCORE:
                GetComponent<TMP_Text>().text = _playerData.CharactersInGame[IndexPlayer].Score.ToString();
                break;

            case enumTypeData.NAME_PLAYER:
                GetComponent<TMP_Text>().text = _playerData.CharactersInGame[IndexPlayer].Character.GetPhotonView().owner.NickName;
                break;

            case enumTypeData.RED_TOKEN:
                GetComponent<TMP_Text>().text = _playerData.CharactersInGame[IndexPlayer].RedTokens.ToString();
                break;

            case enumTypeData.BLUE_TOKEN:

                GetComponent<TMP_Text>().text = _playerData.CharactersInGame[IndexPlayer].BlueTokens.ToString();
                break;

            case enumTypeData.GREEN_TOKEN:

                GetComponent<TMP_Text>().text = _playerData.CharactersInGame[IndexPlayer].GreenTokens.ToString();
                break;

            case enumTypeData.YELLOW_TOKEN:

                GetComponent<TMP_Text>().text = _playerData.CharactersInGame[IndexPlayer].YellowTokens.ToString();
                break;

        }
    }
    public enumTypeData TypeData
    {
        get
        {
            return _typeData;
        }

        set
        {
            _typeData = value;
        }
    }

    public int IndexPlayer
    {
        get
        {
            return _indexPlayer;
        }

        set
        {
            _indexPlayer = value;
        }
    }
}
