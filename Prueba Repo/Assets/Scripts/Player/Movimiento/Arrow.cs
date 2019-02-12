using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Todo lo que hacen las flechas de direccion
/// </summary>
public class Arrow : MonoBehaviour {

    public adress enumAdress;
    private const float GAP_X = 1f;
    private const float GAP_Y = 1.2f;
    private GameObject _player;
    
    public enum adress
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    private void Start()
    {
        configurationArrow();
    }

    public void configurationArrow()
    {
        switch (enumAdress)
        {
            case adress.DOWN:
                transform.Translate(new Vector3(0, -GAP_Y, 0));
                transform.Rotate(new Vector3(0,0,90));
                
                break;

            case adress.LEFT:
                transform.Translate(new Vector3(-GAP_X, 0, 0));
                transform.Rotate(Vector3.zero);
                
                break;

            case adress.RIGHT:
                transform.Translate(new Vector3(GAP_X, 0, 0));
                transform.Rotate(new Vector3(0, 0, 180));
               
                break;

            case adress.UP:
                transform.Translate(new Vector3(0, GAP_Y, 0));
                transform.Rotate(new Vector3(0, 0, -90));
               
                break;
        }
    }

    private void OnMouseDown()
    {
        
        _player.GetComponent<PlayerMove>().photonView.RPC("receiveAdress", PhotonTargets.All, enumAdress);
        _player.GetComponent<PlayerMove>().photonView.RPC("calculatePointToMove",PhotonTargets.All);
        Arrow[] aux= FindObjectsOfType<Arrow>();

        for(int i = 0; i<aux.Length;i++)
            Destroy(aux[i].gameObject);

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
