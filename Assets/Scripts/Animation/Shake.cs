using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private Vector3 _initialPosition;
    private float _timeLeft;
    private static float _freqeuncy = 8f;
    private static float _magnitude = 0.1f;
    private void Awake()
    {
        _initialPosition = transform.position;
        _timeLeft = 0.325f;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        transform.position = _initialPosition + _magnitude * Vector3.left * Mathf.Sin(2*Mathf.PI * _timeLeft * _freqeuncy);
        if (_timeLeft < 0f)
        {
            transform.position = _initialPosition;
            Destroy(this);
        }
    }
}
