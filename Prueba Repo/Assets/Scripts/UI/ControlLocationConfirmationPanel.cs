using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLocationConfirmationPanel : MonoBehaviour
{

    private GameObject _player;

    private void Start()
    {
        transform.SetParent(FindObjectOfType<Canvas>().transform,false);
        
    }

    public void confirmationButton()
    {
        Destroy(_player.GetComponent<locateCharacter>());
        FindObjectOfType<ControlTurn>().nextTurn();
        Destroy(gameObject);
    }

    public void denyButton()
    {
        _player.transform.position = new Vector3(-6, 2, 0);
        Destroy(gameObject);
    }


    public GameObject Player
    {
        get
        {
            return _player;
        }

        set
        {
            _player = value;
        }
    }
}
