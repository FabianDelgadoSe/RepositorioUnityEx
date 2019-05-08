using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetPlayerStatsPanels : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _redTokens;
    [SerializeField] private TMP_Text _blueTokens;
    [SerializeField] private TMP_Text _yellowTokens;
    [SerializeField] private TMP_Text _greenTokens;
    [SerializeField] private Image _icon;


    public void LoadData(string name, string red, string blue, string yellow, string green, Sprite icon) {

        _playerName.text = name;
        _redTokens.text = red;
        _blueTokens.text = blue;
        _yellowTokens.text = yellow;
        _greenTokens.text = green;
        _icon.sprite = icon;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
