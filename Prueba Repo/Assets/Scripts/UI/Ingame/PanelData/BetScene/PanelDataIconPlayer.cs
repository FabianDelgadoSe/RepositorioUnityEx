using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelDataIconPlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] _iconPlayer;


    public void positionIcon(int index,Sprite sprite)
    {
        _iconPlayer[index].SetActive(true);
        _iconPlayer[index].GetComponent<Image>().sprite = sprite;
    }

    private void OnDisable()
    {
        for (int i = 0; i< _iconPlayer.Length;i++)
        {
            _iconPlayer[i].SetActive(false);
        }
    }

    public void DeactiveIcon(int indexPlayer) {


        _iconPlayer[indexPlayer].GetComponent<Animator>().SetBool("Desappear", true);
        StartCoroutine(DeactiveInvoke(indexPlayer));

    }

    IEnumerator DeactiveInvoke(int indexPlayer) {

        yield return new WaitForSeconds(1);
        _iconPlayer[indexPlayer].SetActive(false);
    }
    
}
