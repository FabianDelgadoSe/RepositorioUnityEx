using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelData : Photon.PunBehaviour {
   
    private List<GameObject> _scorePlayers = new List<GameObject>();
    [Header("prefab de score")]
    [SerializeField]private GameObject _objScore;
    [Header ("objeto padre que tiene a los scores")]
    [SerializeField] private GameObject _fatherScore;
    [Header("panel que contiene todo")]
    [SerializeField] private GameObject _panelData;

    private int _numberPlayers;

    private void Start()
    {
        _numberPlayers = PhotonNetwork.room.PlayerCount;
    }

    public void createdObjectScore()
    {
        _panelData.SetActive(true);


        while (_scorePlayers.Count != _numberPlayers)
        {
            _scorePlayers.Add(Instantiate(_objScore));
            _scorePlayers[_scorePlayers.Count - 1].transform.SetParent(_fatherScore.transform);
            _scorePlayers[_scorePlayers.Count - 1].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _scorePlayers[_scorePlayers.Count - 1].GetComponent<RectTransform>().position = new Vector3(0,0,0);
            _scorePlayers[_scorePlayers.Count - 1].GetComponentInChildren<TextPanelData>().IndexPlayer = _scorePlayers.Count - 1;
        }
    }

    public void desactivePanel()
    {
        _panelData.SetActive(false);
    }
}
