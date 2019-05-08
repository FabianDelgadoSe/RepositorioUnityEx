using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBet : MonoBehaviour {

    [SerializeField] private GameObject _panelBet;
    [SerializeField] private GameObject _bet;
    private ControlBet _controlBet;

    [SerializeField] private List<GameObject> _gemsImages = new List<GameObject>(); //0-amarillo 1-azul 2-roja 3-verde

    [SerializeField] private Transform _betDestiny;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _auxGemAnim;


    [Header("Cosas que debe activar luego de hacer la apuesta")]
    [SerializeField] private GameObject _iconMision;
    [SerializeField] private GameObject _baits;
    [SerializeField] private GameObject _cards;
    [SerializeField] private GameObject _datos;

    public Image image;

    private void Start()
    {
        _controlBet = FindObjectOfType<ControlBet>();
    }

    private void OnEnable()
    {
        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.SELECT_PREDICTION);
        _iconMision.SetActive(true);
        _datos.SetActive(false);
    }

    public void selectBet(string gem)
    {
        FindObjectOfType<ControlTurn>().Bemade = true;
        switch (gem)
        {
            case "RED":

                StartGemAnimation(_gemsImages[2]);


                
               _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.RED, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.RED);
                _bet.SetActive(false);
                break;
            case "GREEN":
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.GREEN, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.GREEN);
                _bet.SetActive(false);
                break;
            case "BLUE":
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.BLUE, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.BLUE);
                _bet.SetActive(false);
                break;
            case "YELLOW":
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.YELLOW,FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.YELLOW);
                _bet.SetActive(false);
                break;
            default:
                Debug.Log("opcion erronea");
                break;
        }

        _baits.SetActive(true);
        _cards.SetActive(true);
        _datos.SetActive(true);
    }

    private void StartGemAnimation(GameObject _gemImage)
    {
        _auxGemAnim.GetComponent<Image>().sprite = _gemImage.GetComponent<Image>().sprite;

        GameObject a = Instantiate(_auxGemAnim, _gemImage.transform.position, Quaternion.identity);
        a.transform.SetParent(_canvas.transform, false);
        a.GetComponent<BetAnimation>()._betFinalPosition = _betDestiny;
        a.transform.position = _gemImage.transform.position;
        //a.GetComponent<RectTransform>().position = _gemImage.GetComponent<RectTransform>().position;
        //a.GetComponent<Animator>().enabled = true;
       
    }

}
