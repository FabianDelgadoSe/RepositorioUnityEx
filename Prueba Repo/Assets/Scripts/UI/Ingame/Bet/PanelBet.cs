using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBet : MonoBehaviour
{

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

    private Square.typesSquares _gemSelected;

    private void Start()
    {
        _controlBet = FindObjectOfType<ControlBet>();
    }

    private void OnEnable()
    {
        FindObjectOfType<PanelInformation>().showMessages(PanelInformation.Messages.SELECT_PREDICTION);

        foreach (GameObject gem in _gemsImages)
        {
            gem.SetActive(true);
        }

        Image imagePanelBet = _panelBet.GetComponent<Image>();
        imagePanelBet.color = new Color(imagePanelBet.color.r, imagePanelBet.color.g, imagePanelBet.color.b, 1f);

        _iconMision.SetActive(true);
        _datos.SetActive(false);
    }

    public void selectBet(string gem)
    {
        Debug.Log("AJA");
        FindObjectOfType<ControlTurn>().Bemade = true;

        switch (gem)
        {
            case "RED":
                StartGemAnimation(_gemsImages[2]);
                _gemSelected = Square.typesSquares.RED;
                break;
            case "GREEN":
                StartGemAnimation(_gemsImages[3]);
                _gemSelected = Square.typesSquares.GREEN;
                break;
            case "BLUE":
                StartGemAnimation(_gemsImages[1]);
                _gemSelected = Square.typesSquares.BLUE;
                break;
            case "YELLOW":
                StartGemAnimation(_gemsImages[0]);
                _gemSelected = Square.typesSquares.YELLOW;
                break;
            default:
                Debug.Log("opcion erronea");
                break;
        }

        _baits.SetActive(true);
        _cards.SetActive(true);

        if (FindObjectOfType<ControlRound>().NumberRounds > 0)
            _datos.SetActive(true);

    }

    public void SetPanelsAndBet()
    {
        switch (_gemSelected)
        {
            case Square.typesSquares.RED:
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.RED, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.RED);
                _bet.SetActive(false);
                break;
            case Square.typesSquares.BLUE:
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.BLUE, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.BLUE);
                _bet.SetActive(false);
                break;
            case Square.typesSquares.GREEN:
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.GREEN, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.GREEN);
                _bet.SetActive(false);
                break;
            case Square.typesSquares.YELLOW:
                _controlBet.photonView.RPC("selectBet", PhotonTargets.All, Square.typesSquares.YELLOW, FindObjectOfType<ControlTurn>().MineId);
                FindObjectOfType<BetImage>().showMyBet(Square.typesSquares.YELLOW);
                _bet.SetActive(false);
                break;
            default:
                break;
        }

    }

    private void StartGemAnimation(GameObject _gemImage)
    {

        _auxGemAnim.GetComponent<Image>().sprite = _gemImage.GetComponent<Image>().sprite;

        GameObject a = Instantiate(_auxGemAnim, _gemImage.transform.position, Quaternion.identity);
        a.transform.SetParent(_canvas.transform, false);
        a.GetComponent<BetAnimation>()._betFinalPosition = _betDestiny;
        a.transform.position = _gemImage.transform.position;

        foreach (GameObject gem in _gemsImages)
        {
            gem.SetActive(false);
        }

        Image imagePanelBet = _panelBet.GetComponent<Image>();
        imagePanelBet.color = new Color(imagePanelBet.color.r, imagePanelBet.color.g, imagePanelBet.color.b, 0f);

        if (FindObjectOfType<ControlTutorial>())
        {
            if (FindObjectOfType<ControlRound>().NumberRounds == 1)
            {
                if (!FindObjectOfType<ControlTutorial>().ExplicationBlock)
                {
                    FindObjectOfType<ControlTutorial>().explicationBlock();
                }
            }
        }

        if (FindObjectOfType<ControlTutorial>())
        {
            if (FindObjectOfType<BehaviourGhost>())
            {
                FindObjectOfType<ControlTutorial>().explicationGhost();
            }
        }

    }

}
