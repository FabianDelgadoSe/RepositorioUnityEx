using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{

    public enum BaitType
    {
        Poop, Coin
    }

    [SerializeField] private GameObject _miniBait;

    public BaitType baitType;

    /// <summary>
    /// Funcion llamada cuando se arrastra sobre el cebo
    /// </summary>
    public void moveBait()
    {
        if (FindObjectOfType<ControlTurn>().MyTurn && FindObjectOfType<ControlRound>().AllowMove && FindObjectOfType<ControlTurn>().AllowSelectCardMove)
        {
            GameObject aux;
            aux = Instantiate(_miniBait, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Quaternion.identity);
            aux.GetComponent<MiniBait>().Bait = gameObject;
            //gameObject.SetActive(false);
        }
    }
    
    
    /// <summary>
    /// Para reducir la cantidad de cebos que tiene el jugador
    /// </summary>
    private void decreaseBait()
    {
        switch (baitType)
        {
            case Bait.BaitType.Coin:
               
                break;

            case Bait.BaitType.Poop:

                break;

        }

        
    }
         
    //get set

    public BaitType BaitType1
    {
        get
        {
            return baitType;
        }

        set
        {
            baitType = value;
        }
    }

}
