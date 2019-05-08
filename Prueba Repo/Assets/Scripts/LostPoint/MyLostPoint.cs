using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLostPoint : MonoBehaviour
{

    [Header("perder puntos")]
    [SerializeField] private GameObject _pointCreated;
    [SerializeField] private GameObject _lostTokens;
    private int _numberLostPoint = 0;

    public void visualLostToken()
    {
        if (NumberLostPoint > 0)
        {
            GameObject aux = Instantiate(_lostTokens, _pointCreated.transform.position, Quaternion.identity);
            aux.GetComponent<IconsLoseCoin>().MyFather = GetComponent<MyLostPoint>();
        }
    }

    public int NumberLostPoint
    {
        get
        {
            return _numberLostPoint;
        }

        set
        {
            _numberLostPoint = value;
        }
    }
    

}
