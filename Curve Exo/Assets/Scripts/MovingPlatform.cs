using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] bool useInFunc = false;
    [SerializeField] bool useOutFunc = false;
    [SerializeField] bool useInOutFunc = false;

    [SerializeField] bool canMove = false;



    [SerializeField] float speed = 5f;
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
        LerpTest();
    }

    private void IncreaseTimer()
    {
        timer += Time.deltaTime;
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
