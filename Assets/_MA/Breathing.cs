using System;
using NaughtyAttributes;
using UnityEngine;

public class Breathing : MonoBehaviour
{
    [SerializeField] private float expandDuration = 2f;
    private float currentTime;

    [SerializeField] private float xScaleModifier = 0.05f;
    [SerializeField] private float yScaleModifier = 0.02f;

    [ReadOnly] private Vector3 breatheIn = Vector3.one;
    [ReadOnly] private Vector3 breatheOut = Vector3.one;
    private bool breathingIn;
    
    private Vector3 startScale;
    private Vector3 targetScale;

    [ReadOnly] private Vector3 breatheInPos;
    [ReadOnly] private Vector3 breatheOutPos;

    private Vector3 startPos;
    private Vector3 targetPos;
    
    public bool adjustY = false;
    
    public GameObject body;

    private void Awake()
    {
        breatheIn = transform.localScale;
        breatheOut = transform.localScale;
        
        breatheIn.x -= xScaleModifier * breatheIn.x;
        breatheIn.y += yScaleModifier * breatheIn.y;
        breatheOut.x += xScaleModifier * breatheOut.x;
        breatheOut.y -= yScaleModifier * breatheOut.y;
        
        transform.localScale = breatheOut;
        startScale = breatheOut;
        targetScale = breatheIn;

        if (!body) return;
        breatheInPos = body.transform.position;
        breatheInPos.y += breatheIn.y / 10;
        breatheOutPos = body.transform.position;
        breatheOutPos.y -= breatheOut.y / 10;
        
        body.transform.position = breatheOutPos;
        startPos = breatheOutPos;
        targetPos = breatheInPos;
    }

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        transform.localScale = Vector3.Lerp(startScale, targetScale, currentTime / expandDuration);
        if (adjustY && body) body.transform.position = Vector3.Lerp(startPos, targetPos, currentTime / expandDuration);
        if (!(currentTime >= expandDuration)) return;
        currentTime = 0f;
        breathingIn = !breathingIn;
        startScale = targetScale;
        targetScale = breathingIn ? breatheIn : breatheOut;
        if (!adjustY || !body) return;
        startPos = targetPos;
        targetPos = breathingIn ? breatheInPos : breatheOutPos;
    }
}