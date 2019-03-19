using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation {

    private int _redTokens;
    private int _blueTokens;
    private int _greenTokens;
    private int _yellowTokens;
    private int _score;
    private int _coinBaitCount;
    private int _poopBaitCount;
    private GameObject _character;

    public PlayerInformation()
    {
        _redTokens = 0;
        _blueTokens = 0;
        _greenTokens = 0;
        _yellowTokens = 0;
        _score = 5;
        _character = null;
    }

    public int RedTokens
    {
        get
        {
            return _redTokens;
        }

        set
        {
            _redTokens = value;
        }
    }

    public int BlueTokens
    {
        get
        {
            return _blueTokens;
        }

        set
        {
            _blueTokens = value;
        }
    }

    public int GreenTokens
    {
        get
        {
            return _greenTokens;
        }

        set
        {
            _greenTokens = value;
        }
    }

    public int YellowTokens
    {
        get
        {
            return _yellowTokens;
        }

        set
        {
            _yellowTokens = value;
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
        }
    }

    public GameObject Character
    {
        get
        {
            return _character;
        }

        set
        {
            _character = value;
        }
    }

    public int CoinBaitCount
    {
        get
        {
            return _coinBaitCount;
        }

        set
        {
            _coinBaitCount = value;
        }
    }

    public int PoopBaitCount
    {
        get
        {
            return _poopBaitCount;
        }

        set
        {
            _poopBaitCount = value;
        }
    }
}
