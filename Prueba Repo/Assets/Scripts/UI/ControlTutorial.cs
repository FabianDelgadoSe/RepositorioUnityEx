using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTutorial : MonoBehaviour
{
    [SerializeField] private Image[] _tutorial;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _myZone;
    [SerializeField] private bool _allowStartTurn = false;
    [SerializeField] private GameObject _buttonDatos;
    [SerializeField] private GameObject _tutorialDatos;
    [SerializeField] private GameObject _tutorialAllDatos;
    [SerializeField] private GameObject[] _tutorialMove;
    [SerializeField] private GameObject _buttonTutorialMove;
    [SerializeField] private GameObject _tutorialBlock;
    [SerializeField] private GameObject[] _tutorialGhost;
    [SerializeField] private GameObject _buttonTutorialGhost;
    private int index = 0;
    private bool _explicationMove = false;
    private bool _explicationMyZone = false;
    private bool _explicationBlock = false;
    private bool _explicationGhost = false;

    void Start()
    {

        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            nextTutorial();
            _button.SetActive(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void explicationBlock()
    {
        if (_tutorialBlock.GetActive())
        {
            _tutorialBlock.SetActive(false);
            ExplicationBlock = true;
        }
        else
        {
            _tutorialBlock.SetActive(true);
        }
    }

    public void explicationGhost()
    {
        if (Index < _tutorialGhost.Length)
        {
            _buttonTutorialGhost.SetActive(true);

            if (Index == 0)
            {
                _tutorialGhost[Index].SetActive(true);
            }
            else
            {
                _tutorialGhost[Index-1].SetActive(false);
                _tutorialGhost[Index].SetActive(true);
            }

            index++;
        }
        else
        {
            _buttonTutorialGhost.SetActive(false);
            _tutorialGhost[Index - 1].SetActive(false);
        }
       
    }

    public void descriptionMove()
    {
        _buttonTutorialMove.SetActive(true);
        _allowStartTurn = true;
        _explicationMove = true;

        Debug.Log("index " +  index);

        if (Index < _tutorialMove.Length)
        {
            // activas las diapositivas normalmente
            if (Index == 0)
            {
                _tutorialMove[Index].gameObject.SetActive(true);
            }
            else
            {
                _tutorialMove[Index - 1].gameObject.SetActive(false);
                _tutorialMove[Index].gameObject.SetActive(true);
            }

            Index++;
        }
        else
        {
            _buttonTutorialMove.SetActive(false);
            _tutorialMove[Index - 1].gameObject.SetActive(false);
            Index = 0;
            FindObjectOfType<ControlTurn>().StarTurn();
        }
    }

    public void nextTutorial()
    {

        if (Index == _tutorial.Length)
        {
            _button.SetActive(false);
            _tutorial[Index - 1].gameObject.SetActive(false);
            PlayerPrefs.SetInt("Tutorial", 1);
            FindObjectOfType<ControlMission>().distributeMissions();
            Index = 0;
        }
        else
        {
            // activas las diapositivas normalmente
            if (Index == 0)
            {
                _tutorial[Index].gameObject.SetActive(true);
            }
            else
            {
                _tutorial[Index - 1].gameObject.SetActive(false);
                _tutorial[Index].gameObject.SetActive(true);
            }

            Index++;
        }
    }

    public void explicationMyZone()
    {
        _myZone.SetActive(true);
        _buttonDatos.SetActive(false);
    }

    public void finishFirstPartTutorial()
    {
        _buttonDatos.SetActive(true);
        _myZone.SetActive(false);
        _tutorialDatos.SetActive(true);
    }

    public void showExplicationData()
    {
        _tutorialDatos.SetActive(false);
        _tutorialAllDatos.SetActive(true);
    }

    public void finishBasicTutorial()
    {
        if (!_explicationMyZone) {
            _tutorialAllDatos.SetActive(false);
            _explicationMyZone = true;

            if (FindObjectOfType<ControlTurn>().MyTurn)
            {
                descriptionMove();
            }
        }
    }

    public bool AllowStartTurn
    {
        get
        {
            return _allowStartTurn;
        }

        set
        {
            _allowStartTurn = value;
        }
    }

    public bool ExplicationMove
    {
        get
        {
            return _explicationMove;
        }

        set
        {
            _explicationMove = value;
        }
    }

    public bool ExplicationMyZone
    {
        get
        {
            return _explicationMyZone;
        }

        set
        {
            _explicationMyZone = value;
        }
    }

    public bool ExplicationBlock
    {
        get
        {
            return _explicationBlock;
        }

        set
        {
            _explicationBlock = value;
        }
    }

    public int Index
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }
}
