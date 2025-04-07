using System;
using NaughtyAttributes;
using UnityEngine;

public class Player : MonoBehaviour
{
    Entity playerEntity;
    InteractionEvents eventer;
    int tick;
    [ReadOnly] private bool wasHit;
    public bool hasDash;
    
    // ============================= INIT =============================
    void Awake()
    {
        playerEntity = GetComponent<Entity>();
        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        
    }

    // =========================== TRIGGERS ===========================
    void OnCollisionEnter2D(Collision2D other)
    {
        // bullets handled in entity
        if (other.gameObject.CompareTag("Bullet")) return;
        
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") &&
            other.gameObject.layer != LayerMask.NameToLayer("Sin")) return;
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        
        playerEntity.ApplyDamage(otherEntity.stats.touchDamage);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") &&
            other.gameObject.layer != LayerMask.NameToLayer("Sin")) return;
        if (tick != 0 && wasHit) return;
        wasHit = true;
        tick++;
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        playerEntity.ApplyDamage(otherEntity.stats.touchDamage);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") &&
            other.gameObject.layer != LayerMask.NameToLayer("Sin")) return;
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        playerEntity.ApplyDamage(otherEntity.stats.touchDamage);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") &&
            other.gameObject.layer != LayerMask.NameToLayer("Sin")) return;
        if (tick != 0 && wasHit) return;
        wasHit = true;
        tick++;
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        playerEntity.ApplyDamage(otherEntity.stats.touchDamage);
    }
    
    private void OnDestroy()
    {
        eventer.mentalSlider.value = 0;
    }
    
    // =========================== UPDATES ============================
    void FixedUpdate()
    {
        float angle = Mathf.Atan2(playerEntity.lookDirectionVector.y, playerEntity.lookDirectionVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        eventer.mentalSlider.value = playerEntity.stats.health/playerEntity.stats.maxHealth;
        tick++;
        if (tick <= 50) return;
        wasHit = false;
        tick = 0;
    }
}
