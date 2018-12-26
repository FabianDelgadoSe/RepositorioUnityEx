using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionable : Photon.PunBehaviour
{

    [SerializeField] Character _character;


    private bool _isSelected = false;
    [SerializeField] private TextMeshProUGUI _name;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().sprite = _character._iconUnSelected;
        _name.text = _character._name;

    }

    [PunRPC]
    public void setCharacterSelection()
    {
        if (_isSelected)
        {
            _isSelected = true;
            GetComponent<Image>().sprite = _character._iconSelected;

        }
    }

    public void characterClicked()
    {
        photonView.RPC("setCharacterSelection", PhotonTargets.All);
    }

}
