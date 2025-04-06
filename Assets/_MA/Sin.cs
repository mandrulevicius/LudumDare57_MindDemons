using System;
using UnityEngine;

public class Sin : MonoBehaviour
{
    private Entity entity;
    public AudioClip audioClip;
    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    private void FixedUpdate()
    {
        if (entity.inCombat) entity.isShooting = true;
    }

    private void OnDestroy()
    {
        GameObject.FindWithTag("Events").GetComponent<InteractionEvents>().PlayGenericClip();
    }
}
