using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private GameObject _menuFirstTime;
    [SerializeField] private GameObject _menuLogged;


    [SerializeField] private TextMeshProUGUI _loggedText;

    [SerializeField] private TextMeshProUGUI _temporalPlayerName;

    // Start is called before the first frame update
    void Start()
    {

        CheckMenuStatus();

    }

    /// <summary>
    /// Para activar o desactivar un objeto
    /// </summary>
    private void SetMenus(bool menuFirstTimeStatus, bool menuLoggedStatus)
    {
        _menuFirstTime.SetActive(menuFirstTimeStatus);
        _menuLogged.SetActive(menuLoggedStatus);
    }

    /// <summary>
    /// Se ejecuta cuando se registra el nombre en el Menu de inicio por primera vez
    /// </summary>
    public void RegisterName()
    {

        if (_temporalPlayerName.text.Length != 1)
        {
            PlayerPrefController.GetInstance().SavePlayerName(_temporalPlayerName.text);
            SetMenus(false, true);
            CheckMenuStatus();
        }
        else
        {
            //SSTools.ShowMessage("Requiere nombre", SSTools.Position.bottom, SSTools.Time.twoSecond);
           
        }
        
    }

    /// <summary>
    /// Evalua en que estado debe cargarse el menu
    /// </summary>
    private void CheckMenuStatus() {

        if (PlayerPrefController.GetInstance().GetPlayerName() == "")
        {
            SetMenus(true, false);
        }
        else
        {
            SetMenus(false, true);
            _loggedText.text = "Bienvenido " + PlayerPrefController.GetInstance().GetPlayerName() + ".";
        }
    }

    public void PlayerPrefDeleteCall()
    {
        Application.LoadLevel(Application.loadedLevel);
        PlayerPrefController.GetInstance().ResetAllPlayerPref();
    }
}
