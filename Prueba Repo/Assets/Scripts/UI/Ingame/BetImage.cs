using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetImage : MonoBehaviour {

    [SerializeField] private Sprite _tokenRed;
    [SerializeField] private Sprite _tokenGreen;
    [SerializeField] private Sprite _tokenBlue;
    [SerializeField] private Sprite _tokenYellow;


    public void showMyBet(Square.typesSquares token)
    {
        switch (token)
        {
            case Square.typesSquares.BLUE:
                GetComponent<Image>().sprite = _tokenBlue;
                break;

            case Square.typesSquares.GREEN:
                GetComponent<Image>().sprite = _tokenGreen;
                break;

            case Square.typesSquares.RED:
                GetComponent<Image>().sprite = _tokenRed;
                break;

            case Square.typesSquares.YELLOW:
                GetComponent<Image>().sprite = _tokenYellow;
                break;

        }
    }
}
