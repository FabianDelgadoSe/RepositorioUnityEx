using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelData : Photon.PunBehaviour
{

    private List<GameObject> _scorePlayers = new List<GameObject>();
    [Header("prefab de score")]
    [SerializeField] private GameObject[] _objScore;
    [Header("prefab de token")]
    [SerializeField] private GameObject[] _objRedToken;
    [SerializeField] private GameObject[] _objBlueToken;
    [SerializeField] private GameObject[] _objGreenToken;
    [SerializeField] private GameObject[] _objYellowToken;
    [Header("objeto padre que tiene a los scores")]
    [SerializeField] private GameObject _fatherScore;
    [Header("panel que contiene todo")]
    [SerializeField] private GameObject _panelData;
    [Header("objeto padre que tiene a los Tokesn")]
    [SerializeField] private GameObject _fatherRedTokens;
    [SerializeField] private GameObject _fatherBlueTokens;
    [SerializeField] private GameObject _fatherGreenTokens;
    [SerializeField] private GameObject _fatherYellowTokens;

    private bool _createdTokens = false;
    private bool _createdScore = false;

    private int _numberPlayers;

    private void Start()
    {
        _numberPlayers = PhotonNetwork.room.PlayerCount;
    }

    public void createdObjectScore()
    {
        _panelData.SetActive(true);

        if (!_createdScore)
        {
            for (int i = 0; i < _numberPlayers; i++)
            {
                _objScore[i].SetActive(true);
            }

            _createdScore = true;
        }

        activeObjectTokens();
    }
    public void activeObjectTokens()
    {
        if (!_createdTokens)
        {
            for (int i = 0; i < _numberPlayers; i++)
            {
                _objRedToken[i].SetActive(true);
                _objGreenToken[i].SetActive(true);
                _objBlueToken[i].SetActive(true);
                _objYellowToken[i].SetActive(true);

            }
            _createdTokens = true;
        }
    }

    public void desactivePanel()
    {
        _panelData.SetActive(false);
    }
}
