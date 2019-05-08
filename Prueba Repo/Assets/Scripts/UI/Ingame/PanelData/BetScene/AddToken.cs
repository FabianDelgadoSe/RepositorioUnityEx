using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToken : MonoBehaviour
{

    [SerializeField] private GameObject _icon;

    public void AddTokens() {

        _icon.GetComponent<Animator>().SetBool("Desappear", true);
        //_icon.GetComponent<Animator>().SetBool("Desappear", false);

    }


}
