using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script para que los obetos de destruyan pasado cierto tiempo
/// </summary>
public class AutoDestroy : MonoBehaviour {

    [SerializeField] float time;
	// Use this for initialization
	void Start () {
        Invoke("destroy", time);
	}

    /// <summary>
    /// destruye al objeto que tiene el script
    /// </summary>
    private void destroy()
    {
        Destroy(gameObject);
    }

}
