using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAndDesactive : MonoBehaviour
{

    [SerializeField] private GameObject _objActive;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            desactive();
        }
    }

    private void OnMouseDown()
    {
        desactive();   
    }

    public void desactive()
    {
        if (_objActive.active)
        {
            _objActive.SetActive(false);
        }
        else
        {
            _objActive.SetActive(true);
        }
    }
}
