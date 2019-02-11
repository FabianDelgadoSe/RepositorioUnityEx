using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Scrip provicional hasta que se cree correctamente el paso de escenas
/// </summary>
public class ChangeScene : Photon.PunBehaviour
{

    private const string LOBBY = "Lobby";
    private const string MAIN = "Main";
    private const string PRELOBBY = "Pre-Lobby";
    private const string SELECTROOM = "SelectRoom";

    public enum Scenes
    {
        none, Lobby, Main, PreLobby, SelectRoom
    }

    public Scenes _sceneToChange = Scenes.none;

    public void chansy()
    {
    
        switch (_sceneToChange)
        {
            case Scenes.Lobby:
                photonView.RPC("changeScene", PhotonTargets.All, LOBBY);
                break;
            case Scenes.Main:
                photonView.RPC("changeScene", PhotonTargets.All, MAIN);
                break;
            case Scenes.PreLobby:
                photonView.RPC("changeScene", PhotonTargets.All, PRELOBBY);
                break;
            case Scenes.SelectRoom:
                photonView.RPC("changeScene", PhotonTargets.All, SELECTROOM);
                break;
            case Scenes.none:
                //AÑADIR FEEDBACK DE NINGUNA SELECCIÓN
                break;

        }

        //photonView.RPC("changeScene", PhotonTargets.All);
    }

    [PunRPC]
    void changeScene(string sceneName)
    {
        SceneManager.LoadScene("Main");
    }
}
