using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlBait : Photon.PunBehaviour
{
    private int _numberBaitCoin = 1;
    private int _numberBaitPoop = 1;
    [SerializeField] private GameObject _baitCoin;
    [SerializeField] private GameObject _baitPoop;
    [SerializeField] private GameObject _perder;
    [PunRPC]
    public void restarBaits()
    {
        BaitBehaviour[] aux = FindObjectsOfType<BaitBehaviour>();

        for (int i =0; i<aux.Length;i++)
        {
            Destroy(aux[i].gameObject);
        }

        _numberBaitCoin = 1;
        _numberBaitPoop = 1;
        _baitCoin.GetComponent<Bait>().changeNumberBaits();
        _baitPoop.GetComponent<Bait>().changeNumberBaits();
    }

    public void changeUINumberBaits(Bait.typeBait typeBait)
    {
        switch (typeBait)
        {
            case Bait.typeBait.COIN:
                _baitCoin.GetComponent<Bait>().changeNumberBaits();
                break;

            case Bait.typeBait.POOP:
                _baitPoop.GetComponent<Bait>().changeNumberBaits();
                break;

        }
    }

    public int NumberBaitPoop
    {
        get
        {
            return _numberBaitPoop;
        }

        set
        {
            _numberBaitPoop = value;
        }
    }

    public int NumberBaitCoin
    {
        get
        {
            return _numberBaitCoin;
        }

        set
        {
            _numberBaitCoin = value;
        }
    }
}
