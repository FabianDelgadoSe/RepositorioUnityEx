using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IconsLoseCoin : MonoBehaviour
{
    private OthersPlayersData _father;
    private MyLostPoint _myFather;
        
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (_father != null)
        {
            _father.NumberLostPoint--;
            _father.visualLostToken();
        }
        else
        {
            _myFather.NumberLostPoint--;
            _myFather.visualLostToken();
        }

    }

    public OthersPlayersData Father
    {
        get
        {
            return _father;
        }

        set
        {
            _father = value;
        }
    }

    public MyLostPoint MyFather
    {
        get
        {
            return _myFather;
        }

        set
        {
            _myFather = value;
        }
    }
}
