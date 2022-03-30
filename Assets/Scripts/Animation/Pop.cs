using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    private Vector3 _initialScale;
    private float _timeLeft;
    private static float _animationTime = 0.3f;
    private static float _magnitude = 0.3f;

    private void Awake()
    {
        _initialScale = transform.localScale;
        _timeLeft = _animationTime;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;
        transform.localScale = _initialScale + _magnitude * Vector3.one * Mathf.Sin(Mathf.PI * _timeLeft / _animationTime);
        if (_timeLeft < 0f)
        {
            transform.localScale = _initialScale;
            Destroy(this);
        }
    }
}
