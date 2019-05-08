using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StartMatch : MonoBehaviour
{
    [SerializeField] private TMP_Text _startMatchText;
    private LobbyManager _lobbyManager;

    private float _countDown = 3;

    private void OnEnable()
    {
        _countDown = 3;
        Debug.Log("countdown reset?: " + _countDown);
        _lobbyManager = FindObjectOfType<LobbyManager>();

        _startMatchText.text = "Comenzando en" + "\n" + _countDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (_countDown >= 0)
        {
            _countDown -= Time.deltaTime;
            _startMatchText.text = "Comenzando en" + "\n" + Math.Round(_countDown, 0);
        }
        else
        {
            _startMatchText.text = "Comenzando en" + "\n" + 0;

            if (_lobbyManager.CanStartMatch) {

                FindObjectOfType<ChangeScene>().chansy();

            }
        }
        
        
        




    }


}
