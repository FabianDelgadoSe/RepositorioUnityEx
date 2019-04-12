using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prueba : Photon.MonoBehaviour
{
    public List<int> a = new List<int>();
    private void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            a.RemoveAt(3);
        }
    }

    
}
