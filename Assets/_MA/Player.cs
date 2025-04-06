using UnityEngine;

public class Player : MonoBehaviour
{
    Entity playerEntity;
    int tick = 0;
    
    // ============================= INIT =============================
    void Awake()
    {
        playerEntity = GetComponent<Entity>();
    }
    
    // =========================== UPDATES ============================
    void FixedUpdate()
    {
        float angle = Mathf.Atan2(playerEntity.lookDirectionVector.y, playerEntity.lookDirectionVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        
        tick++;
        if (tick <= 50) return;
        tick = 0;
    }
    
    // =========================== TRIGGERS ===========================
    void OnCollisionEnter2D(Collision2D other)
    {
        // bullets handled in entity
        if (other.gameObject.CompareTag("Bullet")) return;

        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") ||
            other.gameObject.layer != LayerMask.NameToLayer("Sin")) return;
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        
        playerEntity.ApplyDamage(otherEntity.stats.touchDamage);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy") ||
            other.gameObject.layer != LayerMask.NameToLayer("Sin")) return;
        if (tick != 0) return;
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        playerEntity.ApplyDamage(otherEntity.stats.touchDamage);
    }
}
