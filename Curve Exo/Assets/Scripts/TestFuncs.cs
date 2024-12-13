using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFuncs : MonoBehaviour
{

    [SerializeField] bool useInFunc = false;
    [SerializeField] bool useOutFunc = false;
    [SerializeField] bool useInOutFunc = false;

    [SerializeField] float speed = 5f;
    [SerializeField] float frequency = 2f;
    [SerializeField] float amplitude = 5f;
    [SerializeField] float interpolatedValue = 0f;

    [SerializeField] Vector3 startPos = Vector3.zero;

    float timer = 0f;
    

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        IncreaseTimer();
        Move();  
    }

    private void IncreaseTimer()
    {
        timer += Time.deltaTime;
        interpolatedValue = Mathf.PingPong(timer, 1f);
    }

    void Move()
    {

        float _t = Mathf.Clamp01(interpolatedValue);
        float _easedT = 0f;
        if(useInFunc) _easedT = InFunc(_t);
        if(useOutFunc) _easedT = OutFunc(_t);
        if(useInOutFunc) _easedT = InOutFunc(_t);

        Vector3 _linearMovement = startPos + transform.forward * speed * _easedT;

        float _oscillation = Mathf.Sin(frequency * 2f * Mathf.PI * _t) * amplitude;
        Vector3 _oscillationOffset = transform.up * _oscillation;

        transform.position = _linearMovement + _oscillationOffset;
    }

    float InFunc(float _t)
    {
        return EaseCubicCurves.EaseInCubic(_t);
    }
    float OutFunc(float _t)
    {
        return EaseCubicCurves.EaseOutCubic(_t);
    }
    float InOutFunc(float _t)
    {
        return EaseCubicCurves.EaseInOutCubic(_t);
    }
}
