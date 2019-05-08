using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDoor : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private GameObject _door;
    [SerializeField] private int _timeToChangeScene;

    public void starAnimations()
    {
        //GetComponent<AudioSource>().Play();
        _blackScreen.SetActive(true);
        //_door.GetComponent<Animator>().enabled = true;
        Invoke("chanceGame", _timeToChangeScene);
    }

    public void chanceGame()
    {
        GetComponent<ChangeScene>().chanScene();
    }
}
