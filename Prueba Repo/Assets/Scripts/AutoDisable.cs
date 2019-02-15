using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour {

    [SerializeField] float time;
	// Use this for initialization
	void Start () {
        Invoke("disable", time);
	}


    private void disable()
    {
        gameObject.SetActive(false);
    }

}
