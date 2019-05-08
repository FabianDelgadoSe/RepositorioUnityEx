using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetPlayerRankPanels : MonoBehaviour
{


    [SerializeField] private Image _charImage;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _playerName;

    public void LoadData(string name, string score, Sprite icon)
    {
        _score.text = score;
        _playerName.text = name;
        _charImage.sprite = icon;
    }

    
}
