using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prueba : Photon.MonoBehaviour
{
    private Arrow.adress _direction;
    private void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectDirection();
        }
    }

    private void selectDirection()
    {
        int auxNumber = Random.Range(1, 5);
        Debug.Log(auxNumber);

        switch (auxNumber)
        {
            case 1:
                _direction = Arrow.adress.DOWN;
                break;

            case 2:
                _direction = Arrow.adress.LEFT;
                break;

            case 3:
                _direction = Arrow.adress.RIGHT;
                break;

            case 4:
                _direction = Arrow.adress.UP;
                break;
        }
    }
}
