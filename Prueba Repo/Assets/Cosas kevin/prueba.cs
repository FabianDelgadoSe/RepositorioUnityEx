using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prueba : Photon.MonoBehaviour
{
    public List<int> a = new List<int>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            operacion();
    }

    public void operacion()
    {
        a.RemoveAt(0);
        Debug.Log("tamaño de la lista" + a);
    }

}
