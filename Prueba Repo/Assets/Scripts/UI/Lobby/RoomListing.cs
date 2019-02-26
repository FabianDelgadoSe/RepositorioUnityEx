using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomListing : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _roomNameText;
    [SerializeField] private GameObject _connectingTextPrefab;

    public bool Update { get; set; }

    public string _roomName { get; private set; }

    private void Start () {
		
	}

    public void SetRoomNameText(string text)
    {
        _roomName = text;
        _roomNameText.text = _roomName;

    }

    public void OnClickJoinRoom()
    {
        Debug.Log("Entrando al room: " + _roomName);
        Instantiate(_connectingTextPrefab);

        PhotonNetwork.JoinRoom(_roomName);
    }

    void OnJoinedRoom()
    {
        Debug.Log("He entrado al room");
        gameObject.GetComponent<ChangeScene>().chanScene();
    }

    //GET SET

    public TextMeshProUGUI RoomNameText
    {
        get
        {
            return _roomNameText;
        }

        set
        {
            _roomNameText = value;
        }
    }

   
}
