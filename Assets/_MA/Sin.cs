using System;
using UnityEngine;

public class Sin : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    private void FixedUpdate()
    {
        if (entity.inCombat) entity.isShooting = true;
    }
}
