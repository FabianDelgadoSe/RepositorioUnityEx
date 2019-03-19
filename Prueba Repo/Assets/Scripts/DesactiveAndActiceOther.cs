using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pasado un tiempo se desactiva y activa otro boton
/// </summary>
public class DesactiveAndActiceOther : MonoBehaviour
{
    [SerializeField] private GameObject _otherObject;
    [SerializeField] private float _time;

    private void OnEnable()
    {
        Debug.Log("toco esperar");
        Invoke("desactiveOtherObject", _time);
    }

    public void desactiveOtherObject()
    {
        Debug.Log("se acabo la espera ");
        //_otherObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
