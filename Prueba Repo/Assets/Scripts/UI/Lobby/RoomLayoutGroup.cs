using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour
{

    [SerializeField] private GameObject _roomListingPrefab;

    private List<RoomListing> _roomListingButtons = new List<RoomListing>();

    private void Start()
    {
        OnReceiveRoomListUpdate();
    }

    public void OnReceiveRoomListUpdate()
    {
        Debug.Log("Receiving rooms");

        RoomInfo[] _rooms = PhotonNetwork.GetRoomList();

        Debug.Log("Rooms received: " + _rooms.Length);

        foreach (RoomInfo room in _rooms)
        {
            RoomReceived(room);
        }

        RemoveOldRooms();
    }

    private void RoomReceived(RoomInfo room)
    {
        int _index = _roomListingButtons.FindIndex(x => x._roomName == room.Name);

        if (_index == -1)
        {
            if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
            {
                Debug.Log("Instantiate a roomListing");
                GameObject _roomListingObj = Instantiate(_roomListingPrefab);
                _roomListingObj.transform.SetParent(transform);

                RoomListing _roomlisting = _roomListingObj.GetComponent<RoomListing>();
                _roomListingButtons.Add(_roomlisting);

                _index = (_roomListingButtons.Count - 1);
            }
        }

        if (_index != -1)
        {
            RoomListing _roomListing = _roomListingButtons[_index];
            _roomListing.SetRoomNameText(room.Name);
            _roomListing.Update = true;
        }
    }

    private void RemoveOldRooms()
    {
        List<RoomListing> _removeRooms = new List<RoomListing>();

        foreach (RoomListing roomListing in RoomListingButtons)
        {
            if (!roomListing.Update)
            {
                _removeRooms.Add(roomListing);
            }
            else
            {
                roomListing.Update = false;
            }
        }

        foreach (RoomListing roomListing in _removeRooms)
        {
            GameObject _roomListingObj = roomListing.gameObject;
            RoomListingButtons.Remove(roomListing);
            Destroy(_roomListingObj);
        }

    }

    //GET SET

    public GameObject RoomListingPrefab
    {
        get
        {
            return _roomListingPrefab;
        }

        set
        {
            _roomListingPrefab = value;
        }
    }

    public List<RoomListing> RoomListingButtons
    {
        get
        {
            return _roomListingButtons;
        }

        set
        {
            _roomListingButtons = value;
        }
    }
}
