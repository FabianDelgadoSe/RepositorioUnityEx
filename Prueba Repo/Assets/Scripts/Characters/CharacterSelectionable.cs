using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterSelectionable : Photon.PunBehaviour
{

    [SerializeField] Character _character;
    private LobbyManager _lobbyManager;
    PlayerDataInGame _playerData;
    private PlayerDataInGame _currentPlayer;
    public bool _isSelected = false;
    [SerializeField] private TextMeshProUGUI _name;
    private PhotonPlayer _playerSelect;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _selecColor;

    [SerializeField] private GameObject _raySelection;
    private Animator _animLine;


    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = _character._faceCharacter;
        _playerData = FindObjectOfType<PlayerDataInGame>();
        _lobbyManager = FindObjectOfType<LobbyManager>();
        _animLine = _raySelection.GetComponent<Animator>();
    }

    /// <summary>
    /// Le da la propiedad de ese personaje al player que se le envio
    /// </summary>
    /// <param name="Player"></param>
    [PunRPC]
    public void setCharacterSelection(PhotonPlayer Player)
    {
        // la segunda parte del if evita que se sobre escriban datos
        if (Player == PhotonNetwork.player && PlayerSelect == null)
        {
            _playerData.CharacterSelected = _character;
            FindObjectOfType<LobbyManager>().SelectedCharacter = gameObject;
        }

        // este if extra evita que cuando se pickeen al tiempo se sobreescriban datos
        if (!_isSelected && PlayerSelect == null)
        {
            _animLine.SetBool("Selected", true);
            PlayerSelect = Player;
            _isSelected = true;
            GetComponent<Image>().color = _selecColor;
            _name.text = Player.NickName.ToString();
            _lobbyManager.allPlayerSelectCharacter();
        }

    }


    /// <summary>
    /// quita le quita la propiedad de ese personaje a player que se envio
    /// </summary>
    [PunRPC]
    private void removeCharacterToPlayer()
    {
        _animLine.SetBool("Selected", false);
        PlayerSelect = null;
        _isSelected = false;
        GetComponent<Image>().color = _normalColor;
        _name.text = "";
        FindObjectOfType<LobbyManager>().allPlayerSelectCharacter();
    }

    /// <summary>
    /// Evalua el que tiene que hacer cuando se da clic sobre un personaje
    /// </summary>
    public void characterClicked()
    {


        if (_lobbyManager.AllowPick && Input.touchCount<=1)
        {
            if (!_isSelected && _playerData.CharacterSelected == null)
            {
                photonView.RPC("setCharacterSelection", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player);

            }
            else if (!_isSelected && _playerData.CharacterSelected != null)
            {
                // le quita deselecciona el personaje anterior
                FindObjectOfType<LobbyManager>().SelectedCharacter.GetComponent<CharacterSelectionable>().
                    photonView.RPC("removeCharacterToPlayer", PhotonTargets.AllBufferedViaServer);

                // selecciona el nuevo personaje
                photonView.RPC("setCharacterSelection", PhotonTargets.AllBufferedViaServer, PhotonNetwork.player);
            }
            else if (_isSelected && PhotonNetwork.player == PlayerSelect)
            {
                _playerData.CharacterSelected = null;
                FindObjectOfType<LobbyManager>().SelectedCharacter = null;
                photonView.RPC("removeCharacterToPlayer", PhotonTargets.AllBufferedViaServer);
            }
        }
    }

    public PhotonPlayer PlayerSelect
    {
        get
        {
            return _playerSelect;
        }

        set
        {
            _playerSelect = value;
        }
    }

}
