using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetAnimation : MonoBehaviour
{
    [SerializeField] public Transform _betFinalPosition;
    public Transform _startPosition;
    private float speed = 0.08f;

    private float _rapeVelocity;
    private float _time = 0;

    // Start is called before the first frame update
    void Start()
    {

       

    }

    // Update is called once per frame
    void Update()
    {
        _rapeVelocity = 1f / Vector3.Distance(transform.position, _betFinalPosition.position) * speed;
        _time += Time.deltaTime * _rapeVelocity;
        transform.position = Vector3.Lerp(transform.position, _betFinalPosition.position, speed);

        if (_time >= 1)
        {
            Debug.Log("LLego el objeto, destuir");
            DestroyObject(gameObject);

        }
    }
}
