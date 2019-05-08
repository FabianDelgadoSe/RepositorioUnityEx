using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prueba : Photon.MonoBehaviour
{
    public GameObject a;
    public Character sad;

    private void Start()
    {
        a.GetComponent<Animator>().runtimeAnimatorController = sad._animator;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            a.GetComponent<Animator>().SetBool("Coin", true);
            Invoke("B", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            a.GetComponent<Animator>().SetBool("Move",true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            a.GetComponent<Animator>().SetBool("Poop", true);

            Invoke("A",0.5f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            a.GetComponent<Animator>().SetBool("Move", false);
        }

    }

    public void B()
    {
        a.GetComponent<Animator>().SetBool("Coin", false);
    }

    public void A()
    {
        a.GetComponent<Animator>().SetBool("Poop", false);
    }

  
}
