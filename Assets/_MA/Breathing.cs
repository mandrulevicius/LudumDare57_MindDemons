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

    private void Awake()
    {
        breatheIn.x -= xScaleModifier;
        breatheIn.y += yScaleModifier;
        breatheOut.x += xScaleModifier;
        breatheOut.y -= yScaleModifier;
        
        transform.localScale = breatheOut;
        startScale = breatheOut;
        targetScale = breatheIn;
        
        breatheInPos = transform.position;
        breatheInPos.y += breatheIn.y / 10;
        breatheOutPos = transform.position;
        breatheOutPos.y -= breatheOut.y / 10;
        
        transform.position = breatheOutPos;
        startPos = breatheOutPos;
        targetPos = breatheInPos;
    }

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        transform.localScale = Vector3.Lerp(startScale, targetScale, currentTime / expandDuration);
        if (adjustY) transform.position = Vector3.Lerp(startPos, targetPos, currentTime / expandDuration);
        if (!(currentTime >= expandDuration)) return;
        currentTime = 0f;
        breathingIn = !breathingIn;
        startScale = targetScale;
        targetScale = breathingIn ? breatheIn : breatheOut;
        if (!adjustY) return;
        startPos = targetPos;
        targetPos = breathingIn ? breatheInPos : breatheOutPos;
    }
}