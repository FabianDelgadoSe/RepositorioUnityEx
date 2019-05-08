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
    private const string PRELOBBY = "Pre-lobby";
    private const string SELECTORCREATE = "SelectOrCreate";
    private const string SELECTROOM = "SelectRoom";
    private const string INGAME = "InGame";


    public enum Scenes
    {
        none, Lobby, Main, PreLobby, SelectOrCreate, SelectRoom, InGame
    }

    public Scenes _sceneToChange;

    [PunRPC]
    /// <summary>
    /// ChangeScene personal
    /// </summary>
    public void chanScene()
    {
        switch (_sceneToChange)
        {
            case Scenes.Lobby:
                changeScene(LOBBY);
                break;
            case Scenes.Main:
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LeaveLobby();
                Destroy(FindObjectOfType<PlayerDataInGame>().gameObject);
                changeScene(MAIN);
                break;
            case Scenes.PreLobby:
                changeScene(PRELOBBY);
                break;
            case Scenes.SelectOrCreate:
                if (PhotonNetwork.inRoom)
                {
                    PhotonNetwork.LeaveRoom();
                    Destroy(FindObjectOfType<PlayerDataInGame>().gameObject);
                }
                changeScene(SELECTORCREATE);
                break;
            case Scenes.SelectRoom:
                changeScene(SELECTROOM);
                break;
            case Scenes.InGame:
                //crea las casillas del arreglo de personajes
                if (FindObjectOfType<PlayerDataInGame>().CharactersInGame.Length != PhotonNetwork.room.PlayerCount)
                    FindObjectOfType<PlayerDataInGame>().CharactersInGame = new PlayerInformation[PhotonNetwork.room.PlayerCount];

                FindObjectOfType<PlayerDataInGame>().Characters = new Character[PhotonNetwork.room.PlayerCount];

                changeScene(INGAME);
                break;
            case Scenes.none:
                //AÑADIR FEEDBACK DE NINGUNA SELECCIÓN
                break;

        }

    }

    /// <summary>
    /// ChangeScene que es llamado por un RPC, para que lo muestre a todos
    /// </summary>
    public void chansy()
    {

        switch (_sceneToChange)
        {
            case Scenes.Lobby:
                photonView.RPC("changeSceneSyn", PhotonTargets.All, LOBBY);
                break;
            case Scenes.Main:
                photonView.RPC("changeSceneSyn", PhotonTargets.All, MAIN);
                break;
            case Scenes.PreLobby:
                photonView.RPC("changeSceneSyn", PhotonTargets.All, PRELOBBY);
                break;
            case Scenes.SelectOrCreate:
                photonView.RPC("changeSceneSyn", PhotonTargets.All, SELECTORCREATE);
                break;
            case Scenes.SelectRoom:
                photonView.RPC("changeSceneSyn", PhotonTargets.All, SELECTROOM);
                break;
            case Scenes.InGame:
                PhotonNetwork.room.IsOpen = false; // bloqeua la sala
                PhotonNetwork.room.IsVisible = false; // no la deja ver

                photonView.RPC("chanScene", PhotonTargets.All);
                break;
            case Scenes.none:
                break;

        }

    }


    [PunRPC]
    void changeSceneSyn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
















































































