using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepositioning : Photon.PunBehaviour
{
    private GameObject _square;
    private bool _reviewPlayersOnWall = true;
    private PlayerDataInGame _playerData;
    private List<GameObject> _charactesInWall = new List<GameObject>();
    [SerializeField] private GameObject _especialArrow;
    private bool _repositionPlayer = false;
  
    private void Start()
    {
        _playerData = FindObjectOfType<PlayerDataInGame>();
    }

    [PunRPC]
    public void PlayerInWall()
    {
        
        if (FindObjectOfType<ControlTurn>().MyTurn) {


            if (_reviewPlayersOnWall)
            {
                for (int i = 0; i < _playerData.CharactersInGame.Length; i++)
                {

                    if (_playerData.CharactersInGame[i].Character.GetComponent<PlayerMove>().Square.GetComponent<Square>()._enumTypesSquares == Square.typesSquares.WALL)
                    {
                        _charactesInWall.Add(_playerData.CharactersInGame[i].Character);
                    }

                }
                _reviewPlayersOnWall = false;
            }
            
            if (_charactesInWall.Count > 0)
            {
                photonView.RPC("createdEspecialArrows", _charactesInWall[0].GetComponent<PlayerMove>().IdOwner);
                _charactesInWall.RemoveAt(0);
            }
            else
            {
                photonView.RPC("finishRespositionPlayers",PhotonTargets.AllBuffered);
                FindObjectOfType<ControlTurn>().photonView.RPC("mineTurn",PhotonTargets.AllBuffered, FindObjectOfType<ControlTurn>().IndexTurn); ;
            }
        }
    }

    [PunRPC]
    public void finishRespositionPlayers()
    {
        _repositionPlayer = false;
    }

    /// <summary>
    /// Crea las flechas especiales en la pantalla de la persona propietaria de ese personaje
    /// </summary>
    [PunRPC]
    public void createdEspecialArrows()
    {
        GameObject arrow;
        GameObject square = _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character.GetComponent<PlayerMove>().Square;
  
        if (square.GetComponent<Square>()._squareDown != null && !square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsOccupied)
        {
            if (!square.GetComponent<Square>()._squareDown.GetComponent<Square>().IsWall)
            {
                // flecha de abajo
                arrow = Instantiate(_especialArrow, _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character.transform.position, Quaternion.identity);
                arrow.GetComponent<Arrow>().EnumAdress = Arrow.adress.DOWN;
                arrow.GetComponent<Arrow>().Player = _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character;

            }
        }

        if (square.GetComponent<Square>()._squareUp != null && !square.GetComponent<Square>()._squareUp.GetComponent<Square>().IsOccupied)
        {
            if (!square.GetComponent<Square>()._squareUp.GetComponent<Square>().IsWall)
            {
                // Flecha de arriba
                arrow = Instantiate(_especialArrow, _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character.transform.position, Quaternion.identity);
                arrow.GetComponent<Arrow>().EnumAdress = Arrow.adress.UP;
                arrow.GetComponent<Arrow>().Player = _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character;

            }
        }

        if (square.GetComponent<Square>()._squareRigh != null && !square.GetComponent<Square>()._squareRigh.GetComponent<Square>().IsOccupied)
        {
            if (!square.GetComponent<Square>()._squareRigh.GetComponent<Square>().IsWall)
            {
                //Flecha de la derecha
                arrow = Instantiate(_especialArrow, _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character.transform.position, Quaternion.identity);
                arrow.GetComponent<Arrow>().EnumAdress = Arrow.adress.RIGHT;
                arrow.GetComponent<Arrow>().Player = _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character;

            }
        }

        if (square.GetComponent<Square>()._squareLeft != null && !square.GetComponent<Square>()._squareLeft.GetComponent<Square>().IsOccupied)
        {
            if (!square.GetComponent<Square>()._squareLeft.GetComponent<Square>().IsWall)
            {
                //Flecha de la izquierda
                arrow = Instantiate(_especialArrow, _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character.transform.position, Quaternion.identity);
                arrow.GetComponent<Arrow>().EnumAdress = Arrow.adress.LEFT;
                arrow.GetComponent<Arrow>().Player = _playerData.CharactersInGame[PhotonNetwork.player.ID - 1].Character;

            }
        }
    }



    public bool ReviewPlayersOnWall
    {
        get
        {
            return _reviewPlayersOnWall;
        }

        set
        {
            _reviewPlayersOnWall = value;
        }
    }

    public bool RepositionPlayer
    {
        get
        {
            return _repositionPlayer;
        }

        set
        {
            _repositionPlayer = value;
        }
    }
}
