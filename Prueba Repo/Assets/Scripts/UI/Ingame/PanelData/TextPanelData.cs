using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextPanelData : MonoBehaviour
{
    [SerializeField] private enumTypeData _typeData;

    private int _indexPlayer;
    private PlayerData _playerData;

    public enum enumTypeData{
        NAME_PLAYER,
        SCORE,
        RED_TOKEN,
        GREEN_TOKEN,
        BLUE_TOKEN,
        YELLOW_TOKEN,
    }
    private void OnEnable()
    {
        _playerData = FindObjectOfType<PlayerData>();

        switch (_typeData)
        {
            case enumTypeData.SCORE:
                Debug.Log(_playerData == null);
                GetComponent<TMP_Text>().text  = _playerData.CharactersInGame[IndexPlayer].Score.ToString();
                break;

            case enumTypeData.NAME_PLAYER:
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
