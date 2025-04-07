using System;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    public float timeToDamage = 1.5f;
    private float currentTime;
    
    public GameObject expandingDot;

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime < 0) return;
        float t = Mathf.Clamp01(currentTime / timeToDamage);
        expandingDot.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
        if (currentTime > timeToDamage)
        {
            currentTime = -1;
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<Entity>().ApplyDamage(1);
        }
    }
}
