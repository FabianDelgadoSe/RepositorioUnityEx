using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{


    [SerializeField] private GameObject _miniCard;
    [Range(1, 5)] [SerializeField] private int _number;

    public void moveCard()
    {
        if (FindObjectOfType<ControlTurn>().MyTurn)
        {
            GameObject aux;
            aux = Instantiate(_miniCard, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
            aux.GetComponent<MiniCard>().Card = gameObject;
            aux.GetComponent<MiniCard>().NumberSteps = _number;
            gameObject.active = false;
        }

    }

    public void selectCard()
    {
        if (FindObjectOfType<ControlTurn>().MyTurn)
            GetComponent<Image>().color = Color.green;
    }

    public void deselectCard(bool reactivate)
    {
        if (FindObjectOfType<ControlTurn>().MyTurn)
        {
            if (reactivate)
            {
                GetComponent<Image>().color = Color.white;
                gameObject.active = true;
            }
            else
                Destroy(gameObject);
        }

    }

    public void reactiveCard()
    {
        Debug.Log("soy la carta " + _number);
    }

    private void OnEnable()
    {
        deselectCard(true);
    }
}
