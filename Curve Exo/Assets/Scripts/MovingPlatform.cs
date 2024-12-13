using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] bool useInFunc = false;
    [SerializeField] bool useOutFunc = false;
    [SerializeField] bool useInOutFunc = false;

    [SerializeField] bool useUpForward = false;
    [SerializeField] bool useRightForward = false;
    //[SerializeField] bool useUpForward = false;

    [SerializeField] bool useUpForwardOscillation = false;
    [SerializeField] bool useRightForwardOscillation = false;
    //[SerializeField] bool useUpForwardOscillation = false;

    [SerializeField] bool inverseDirection = false;

    [SerializeField] bool canMove = false;



    [SerializeField] float speed = 5f;
    [SerializeField] float frequency = 2f;
    [SerializeField] float amplitude = 5f;
    [SerializeField] float interpolatedValue = 0f;
    [SerializeField] float easedT = 0f;

    [SerializeField] Vector3 startPos = Vector3.zero;
    [SerializeField] Vector3 finalPos = Vector3.zero;

    [SerializeField] float timer = 0f;


    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        IncreaseTimer();
        //Move();
        LerpTest();
    }

    private void IncreaseTimer()
    {
        timer += Time.deltaTime;
        //interpolatedValue = Mathf.PingPong(timer, 1f);
    }

    void Move()
    {

        if (!canMove) return;

        //float _t = Mathf.Clamp01(interpolatedValue);
        //float _easedT = 0f;
        //if (useInFunc) _easedT = InFunc(_t);
        //if (useOutFunc) _easedT = OutFunc(_t);
        //if (useInOutFunc) _easedT = InOutFunc(_t);

        //Vector3 _linearMovement = Vector3.zero;
        //if (useUpForward)
        //    _linearMovement = startPos + (inverseDirection ? -transform.up : transform.up) * speed * _easedT;
        //if (useRightForward)
        //    _linearMovement = startPos + (inverseDirection ? -transform.right : transform.right) * speed * _easedT;

        //float _oscillation = Mathf.Sin(frequency * 2f * Mathf.PI * _t) * amplitude;
        //Vector3 _oscillationOffset = transform.up * _oscillation;

        //transform.position = _linearMovement + _oscillationOffset;

        float _t = Mathf.Clamp01(interpolatedValue);
        float _easedT = 0f;
        if (useInFunc) _easedT = InFunc(_t);
        if (useOutFunc) _easedT = OutFunc(_t);
        if (useInOutFunc) _easedT = InOutFunc(_t);

        Vector3 _linearMovement = Vector3.zero;
        if (useUpForward)
            _linearMovement = startPos + (inverseDirection ? -transform.forward : transform.forward) * speed * _easedT;
        if (useRightForward)
            _linearMovement = startPos + (inverseDirection ? -transform.right : transform.right) * speed * _easedT;

        float _oscillation = Mathf.Sin(frequency * 2f * Mathf.PI * _t) * amplitude;
        //Vector3 _oscillationOffset = transform.up * _oscillation;

        Vector3 _oscillationOffset = Vector3.zero;
        if (useUpForwardOscillation)
            _oscillationOffset = transform.up * _oscillation;
        if (useRightForwardOscillation)
            _oscillationOffset = transform.right * _oscillation;

        transform.position = _linearMovement + _oscillationOffset;

    }

    void LerpTest()
    {
        if (!canMove) return;



        float _t = timer * speed;
        easedT = 0f;
        if (useInFunc) easedT = InFunc(_t);
        if (useOutFunc) easedT = OutFunc(_t);
        if (useInOutFunc) easedT = InOutFunc(_t);
        easedT = Mathf.Clamp01(easedT);

        transform.position = Vector3.Lerp(startPos, finalPos, easedT);
        if(easedT >= 1f)
        {
            timer = 0f;
            Vector3 _temp = startPos;
            startPos = finalPos;
            finalPos = _temp;
        }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPos, 5f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(finalPos, 5f);
    }
}
