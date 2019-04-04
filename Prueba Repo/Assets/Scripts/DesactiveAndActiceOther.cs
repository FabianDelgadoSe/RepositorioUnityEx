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
        Invoke("desactiveOtherObject", _time);

        if (_otherObject!=null)
        {
            if (_otherObject.GetActive())
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void desactiveOtherObject()
    {
        Debug.Log("se acabo la espera ");
        gameObject.SetActive(false);
    }

}
