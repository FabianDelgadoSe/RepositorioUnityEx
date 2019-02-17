using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    [SerializeField] float time;
	// Use this for initialization
	void Start () {
        Invoke("destroy", time);
	}


    private void destroy()
    {
        gameObject.SetActive(false);
    }

}
