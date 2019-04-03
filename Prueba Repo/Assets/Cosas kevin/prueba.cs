using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prueba : Photon.MonoBehaviour
{
    public List<int> _tokensList = new List<int>();
    public List<int> _scoreList = new List<int>();

    public int _playerscoincidence;

    private int _acumulatedValued;
    private bool _coincidence;
    private int _valueScore = 32;

    private void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ComoSeLlame();
        }
    }

    public void ComoSeLlame()
    {

        _tokensList.Sort();
        _tokensList.Reverse();

        _playerscoincidence = 0;
        _acumulatedValued = 0;
        _coincidence = false;

        for (int i = 0; i < _tokensList.Count; i++)
        {
            Debug.Log("Como se llame");
            if (_tokensList[i] != 0)
            {


                if (_tokensList.Count > i + 1)
                {
                    if (_tokensList[i] == _tokensList[i + 1])
                    {
                        //coinciden valores se tiene que sacar el promedio
                        _playerscoincidence++;
                        _acumulatedValued += _valueScore;
                        _coincidence = true;
                        _scoreList.Add(0);
                    }
                    else
                    {
                        if (!_coincidence)
                        {
                            _scoreList.Add(_valueScore);
                        }
                        else
                        {
                            _acumulatedValued += _valueScore;
                            _acumulatedValued /= _playerscoincidence + 1;
                            _scoreList.Add(0);

                            for (int u = 0; u <= _playerscoincidence; u++)
                            {
                                _scoreList[i - u] = _acumulatedValued;
                            }

                            _playerscoincidence = 0;
                            _acumulatedValued = 0;
                            _coincidence = false;

                        }
                    }
                }
                // cuando es la ultima casilla
                else if (_tokensList.Count == i + 1)
                {
                    if (!_coincidence)
                    {
                        _scoreList.Add(_valueScore);
                    }
                    else
                    {
                        _acumulatedValued += _valueScore;
                        _acumulatedValued /= _playerscoincidence + 1;
                        _scoreList.Add(0);

                        for (int u = 0; u <= _playerscoincidence; u++)
                        {
                            _scoreList[i - u] = _acumulatedValued;
                        }

                        _playerscoincidence = 0;
                        _acumulatedValued = 0;
                        _coincidence = false;

                    }

                }
                else
                {
                    _scoreList.Add(_valueScore);
                    //coloca el puntaje de una
                }
            }
            else
            {
                _scoreList.Add(0);

            }
            _valueScore -= 4;
        }
    }
}
