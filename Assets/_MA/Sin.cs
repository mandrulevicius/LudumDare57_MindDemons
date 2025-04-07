using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Sin : MonoBehaviour
{
    private Entity entity;
    public AudioClip audioClip;
    [SerializeField] private GameObject healthBar;
    private float maxHealth;
    private float currentHealth;
    private float scalex;
    
    private void Awake()
    {
        entity = GetComponent<Entity>();
        maxHealth = entity.stats.health;
        scalex = healthBar.transform.localScale.x;
    }

    private void OnDestroy()
    {
        GameObject.FindWithTag("Events").GetComponent<InteractionEvents>().PlayGenericClip();
    }

    private void FixedUpdate()
    {
        currentHealth = entity.stats.health;
        transform.rotation = Quaternion.Euler(0, entity.lookDirectionVector.x < 0 ? 180 : 0, 0);
        if(healthBar)
        {
            Vector3 scale = healthBar.transform.localScale;
            scale.x = scalex * (currentHealth / maxHealth);
            healthBar.transform.localScale = scale;
        }
    }
}
