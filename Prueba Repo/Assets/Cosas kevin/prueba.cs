using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prueba : Photon.MonoBehaviour
{
    public int tamaño;
    public int ID;
    public int creados = 0;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            operacion();
    }

    public void operacion()
    {
        bool bandera = true;
        for (int i = 1; creados < tamaño-1; i++)
        {

            if (ID == tamaño)
            {
                Debug.Log(i);
            }
            else
            {
                if (ID + i <= tamaño && bandera)
                {
                    Debug.Log(ID + i);
                }
                else if(bandera)
                {
                    i = 1;
                    Debug.Log(i);
                    bandera = false;
                }
                else
                {
                    Debug.Log(i);
                }

            }

            creados++;

        }//cierre f
    }

}
