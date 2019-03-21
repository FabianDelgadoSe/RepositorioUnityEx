using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bait : Photon.PunBehaviour
{
    [SerializeField] private typeBait _baitType;
    [SerializeField] private TMP_Text _numberBaits;

    [Header("prefab de minicebo")]
    [SerializeField] private GameObject _miniBait;
    private int _index;
    private ControlBait _controlBait;
    private ControlTurn _controlTurn;

    private void Start()
    {
        _controlTurn = FindObjectOfType<ControlTurn>();
        _controlBait = FindObjectOfType<ControlBait>();
        _index = FindObjectOfType<ControlTurn>().IndexTurn - 1;
        changeNumberBaits();
    }

    public enum typeBait
    {
        COIN,
        POOP
    }

    public typeBait BaitType
    {
        get
        {
            return _baitType;
        }

        set
        {
            _baitType = value;
        }
    }

    public void createdMiniBait()
    {
        if (_controlTurn.AllowToPlaceBait)
        {

            GameObject aux;

            switch (_baitType)
            {
                case typeBait.COIN:
                    if (_controlBait.NumberBaitCoin > 0) {

                        _controlBait.NumberBaitCoin--;
                        aux = Instantiate(_miniBait, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
                        aux.GetComponent<MiniBait>().Bait = gameObject;
                        aux.GetComponent<MiniBait>().TypeBait = _baitType;
                        _controlBait.changeUINumberBaits(_baitType);
                    }
                    break;

                case typeBait.POOP:
                    if (_controlBait.NumberBaitPoop > 0)
                    {
                        _controlBait.NumberBaitPoop--;
                        aux = Instantiate(_miniBait, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
                        aux.GetComponent<MiniBait>().Bait = gameObject;
                        aux.GetComponent<MiniBait>().TypeBait = _baitType;
                        _controlBait.changeUINumberBaits(_baitType);
                    }
                    break;
            }
        }
    }

    public void changeNumberBaits()
    {
        switch (_baitType)
        {
            case Bait.typeBait.COIN:
                _numberBaits.text = "X" + _controlBait.NumberBaitCoin;
                break;

            case Bait.typeBait.POOP:
                _numberBaits.text = "X" + _controlBait.NumberBaitPoop;
                break;
        }

    }

}
