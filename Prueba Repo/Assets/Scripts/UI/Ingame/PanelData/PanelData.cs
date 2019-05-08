using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelData : Photon.PunBehaviour
{

    private List<GameObject> _scorePlayers = new List<GameObject>();
    [Header("prefab de score")]
    [SerializeField] private GameObject[] _objScore;
    [Header("token")]
    [SerializeField] private GameObject[] _objRedToken;
    [SerializeField] private GameObject[] _objBlueToken;
    [SerializeField] private GameObject[] _objGreenToken;
    [SerializeField] private GameObject[] _objYellowToken;
    [Header("objeto padre que tiene a los scores")]
    [SerializeField] private GameObject _fatherScore;
    [Header("panel que contiene todo")]
    [SerializeField] private GameObject _panelData;
    private PlayerDataInGame _playerDataInGame;
    [Header("el que tiene el animator")]
    [SerializeField] private GameObject _fatherAnimator;


    private bool _createdScore = false;

    private int _numberPlayers;

    private void Start()
    {
        _playerDataInGame = FindObjectOfType<PlayerDataInGame>();
        _numberPlayers = PhotonNetwork.room.PlayerCount;
    }

    public void createdObjectScore()
    {
        _fatherAnimator.GetComponent<Animator>().SetBool("Active",true);
        Invoke("finishAnimation", 0.6f);

        if (!_createdScore)
        {
            for (int i = 0; i < _numberPlayers; i++)
            {
                _objScore[i].SetActive(true);
            }

            _createdScore = true;
        }

    }

    public void finishAnimation()
    {
        if (_panelData.GetActive())
        {
            _panelData.SetActive(false);
        }
        else
        {
            _panelData.SetActive(true);
        }
    }


    public void activeObjectTokens()
    {

            for (int i = 0; i < _numberPlayers; i++)
            {
                _objRedToken[_playerDataInGame.CharactersInGame[i].RedTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);

            _objBlueToken[_playerDataInGame.CharactersInGame[i].BlueTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);

            _objGreenToken[_playerDataInGame.CharactersInGame[i].GreenTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);

            _objYellowToken[_playerDataInGame.CharactersInGame[i].YellowTokens].
                GetComponent<PanelDataIconPlayer>().positionIcon(i, _playerDataInGame.CharactersInGame[i].Character.GetComponent<SpriteRenderer>().sprite);

        }
            
        
    }

    public void desactivePanel()
    {
        _fatherAnimator.GetComponent<Animator>().SetBool("Active", false);
        Invoke("finishAnimation", 0.6f);
        
    }

 
}
