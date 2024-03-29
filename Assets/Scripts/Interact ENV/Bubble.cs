using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENV : MonoBehaviour, IDamagable
{
    private bool isAttacked = false;
    private float lessDelay = 5f;
    private float couldown = 0f;
    private float stepLessCD = 0.5f;
    private float nextTimeToStep = 0f;

    private Vector3 originSize;
    public void Start()
    {
        originSize = GetComponent<Transform>().localScale;
    }
    public void Update()
    {
        couldown -= Time.deltaTime;
        if(isAttacked && couldown <= 0)
        {
            Transform transform = GetComponent<Transform>();
            if(transform.localScale != originSize && nextTimeToStep <= Time.time)
            {
                transform.localScale -= Vector3.one * 2;
                nextTimeToStep = Time.time + stepLessCD;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        Transform transform = GetComponent<Transform>();
        transform.localScale += Vector3.one * 2;
        couldown = lessDelay;
        isAttacked = true;
    }
}
