using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Todo lo que hacen las flechas de direccion
/// </summary>
public class Arrow : MonoBehaviour {

    private adress enumAdress;
    [SerializeField]private typeArrow enumTypeArrow;
    private const float GAP_X = 1f;
    private const float GAP_Y = 1.2f;
    private GameObject _player;
    private int _steps;

    public enum adress
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public enum typeArrow
    {
        NORMAL,
        SPECIAL
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
                transform.Translate(new Vector3(0, -GAP_Y, -3));
                transform.Rotate(new Vector3(0,0,90));
                
                break;

            case adress.LEFT:
                transform.Translate(new Vector3(-GAP_X, 0, -3));
                transform.Rotate(Vector3.zero);
                
                break;

            case adress.RIGHT:
                transform.Translate(new Vector3(GAP_X, 0, -3));
                transform.Rotate(new Vector3(0, 0, 180));
               
                break;

            case adress.UP:
                transform.Translate(new Vector3(0, GAP_Y, -3));
                transform.Rotate(new Vector3(0, 0, -90));
               
                break;
        }
    }

    private void OnMouseDown()
    {
        switch (enumTypeArrow)
        {
            case typeArrow.NORMAL:
                normalMovementPlayer();
                break;

            case typeArrow.SPECIAL:
                playerRepositioningMovement();
                break;
        }
    }

    public void normalMovementPlayer()
    {
        FindObjectOfType<PlayerDataInGame>().CharactersInGame[FindObjectOfType<ControlTurn>().IndexTurn - 1].Character.
            GetComponent<SpriteRenderer>().color = Color.white;

        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.LATER_MOVE); // mensaje
        FindObjectOfType<ControlTurn>().Myturn.SetActive(true);

        _player.GetComponent<PlayerMove>().CreatedArrow = false;
        _player.GetComponent<PlayerMove>().photonView.RPC("receiveAdress", PhotonTargets.All, enumAdress, Steps);
        _player.GetComponent<PlayerMove>().photonView.RPC("calculatePointToMove", PhotonTargets.All,true);
        Arrow[] aux = FindObjectsOfType<Arrow>();

        for (int i = 0; i < aux.Length; i++)
            Destroy(aux[i].gameObject);
    }

    public void playerRepositioningMovement()
    {

        _player.GetComponent<PlayerMove>().photonView.RPC("repositioning", PhotonTargets.All, enumAdress);
        FindObjectOfType<PlayerRepositioning>().photonView.RPC("PlayerInWall",PhotonTargets.All);
        Arrow[] aux = FindObjectsOfType<Arrow>();

        for (int i = 0; i < aux.Length; i++)
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


    public adress EnumAdress
    {
        get
        {
            return enumAdress;
        }

        set
        {
            enumAdress = value;
        }
    }

    public int Steps
    {
        get
        {
            return _steps;
        }

        set
        {
            _steps = value;
        }
    }
}
