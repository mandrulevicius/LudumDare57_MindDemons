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

    private void OnDestroy()
    {
        GameObject.FindWithTag("Events").GetComponent<InteractionEvents>().PlayGenericClip();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, entity.lookDirectionVector.x < 0 ? 180 : 0, 0);
    }
}
