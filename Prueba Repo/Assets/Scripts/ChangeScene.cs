using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Scrip provicional hasta que se cree correctamente el paso de escenas
/// </summary>
public class ChangeScene : Photon.PunBehaviour {


    public void chansy()
    {
        photonView.RPC("changeScene", PhotonTargets.All);
    }

    [PunRPC]
    void changeScene()
    {
        SceneManager.LoadScene("Main");
    }   
}
