using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Controla lo que pueden hacer las cartas de movimiento
/// </summary>
public class Card : MonoBehaviour
{


    [SerializeField] private GameObject _miniCard;
    [Range(1, 5)] [SerializeField] private int _number;
    /// <summary>
    /// Funcion llamada cuando se arrastra solobre la carta
    /// </summary>
    public void moveCard()
    {
        if (FindObjectOfType<ControlTurn>().MyTurn && FindObjectOfType<ControlRound>().AllowMove && FindObjectOfType<ControlTurn>().AllowSelectCardMove)
        {
            GameObject aux;
            aux = Instantiate(_miniCard, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
            aux.GetComponent<MiniCard>().Card = gameObject;
            aux.GetComponent<MiniCard>().NumberSteps = _number;
            gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// Es llamda cuando se da clic sobre una carta
    /// </summary>
    public void selectCard()
    {
        if (FindObjectOfType<ControlTurn>().MyTurn && FindObjectOfType<ControlRound>().AllowMove && FindObjectOfType<ControlTurn>().AllowSelectCardMove)
            GetComponent<Image>().color = Color.green;
    }


    /// <summary>
    /// Cuando se suelta y la carta pero no sobre un player se llama esta funcion la cual hace que se active de nuevo la carta
    /// </summary>
    /// <param name="reactivate"></param>
    public void deselectCard(bool reactivate)
    {
        if (FindObjectOfType<ControlTurn>().MyTurn )
        {
            if (reactivate)
            {
                gameObject.SetActive(true);
            }
            else
                gameObject.SetActive(false);
        }

    }

    public void reactiveCard()
    {
        GetComponent<Image>().color = Color.white;
    }

    private void OnEnable()
    {
        GetComponent<Image>().color = Color.white;
        deselectCard(true);
    }
}
