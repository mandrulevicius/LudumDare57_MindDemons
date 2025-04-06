using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct Stats
{
    [ReadOnly] [AllowNesting] public string id;
    [ReadOnly] [AllowNesting] public string name;
    
    public float maxHealth;
    public float health;
    
    [ReadOnly] [AllowNesting] public Vector2 velocity;
    public float speed;
    public float speedBoostMultiplier; // for quick testing
    
    public float touchDamage;
    public float rangedDamage;
}

public class Entity : MonoBehaviour
{
    public Stats stats;
    [ReadOnly] public Rigidbody2D rb;
    
    [SerializeField] GameObject explosionPrefab;
    
    // public LayerMask targetLayers;
    public GameObject movementTarget;
    [ReadOnly] public Vector2 movementTargetPosition;
    [ReadOnly] public Vector2 moveDirectionVector;
    
    // AI needs lookTarget to know where to look.
    [ReadOnly] public Vector2 lookDirectionVector;
    
    
    [ReadOnly] public bool isSprinting;
    [ReadOnly] public bool isDashing;

    float _speedPerTick;
    
    [ReadOnly] public bool inCombat;
    [ReadOnly] public bool isShooting;
    
    
    // ============================= INIT =============================
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        stats.id = Guid.NewGuid().ToString();
        stats.name = gameObject.name;
        UpdateEntity();
    }
    
    
    // =========================== TRIGGERS ===========================
    void OnCollisionEnter2D(Collision2D other)
    {
        // Bullet hits anything and dies
        if (gameObject.CompareTag("Bullet"))
        {
            ApplyDamage(1);
        }
        
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        
        // entity hits wall and does nothing
        if (otherEntity == null)
        {
            return;
        }

        // only bullets damage entities
        if (!otherEntity.CompareTag("Bullet")) return;
        
        float damage = CalcDamage(otherEntity);
        
        Interactions.Add(
            stats,
            otherEntity.stats,
            damage,
            rb.linearVelocity
        );
        
        ApplyDamage(damage);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ApplyDamage(1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // gravity here
        // Debug.Log($"{gameObject.name} triggered by collider: {other.gameObject.name}");
    }

    
    // =========================== UPDATES ============================
    void FixedUpdate()
    {
        UpdateEntity();
        
        if (movementTarget) movementTargetPosition = movementTarget.transform.position;
        if (movementTargetPosition != Vector2.zero) moveDirectionVector = (movementTargetPosition - (Vector2)transform.position).normalized;

        if (moveDirectionVector != Vector2.zero) CalcSpeed();
        if (moveDirectionVector != Vector2.zero) transform.position += (Vector3) moveDirectionVector * _speedPerTick;
        // if (moveDirectionVector != Vector2.zero) rb.linearVelocity = moveDirectionVector * _speedPerTick;
        // else rb.linearVelocity = Vector2.zero;
        // if (moveDirectionVector != Vector2.zero) rb.AddForce(moveDirectionVector * _speedPerTick, ForceMode2D.Impulse);
        // if (!isStabilizing) rb.linearDamping = 0;
        // else rb.linearDamping = _speedPerTick;
    }
    
    
     // ========================== FUNCTIONS ===========================
    void UpdateEntity()
    {
        if (stats.maxHealth <= 0 || stats.health <= 0) Die();
        stats.velocity = rb.linearVelocity;
    }
    
    void CalcSpeed()
    {
        _speedPerTick = stats.speed * Time.fixedDeltaTime;
        if (!isSprinting) return;
        _speedPerTick *= stats.speedBoostMultiplier;
    }

    float CalcDamage(Entity otherEntity)
    {
        return otherEntity.stats.touchDamage;
    }

    public void ApplyDamage(float damage)
    {
        var hitSound = GetComponent<AudioSource>();
        if (hitSound) GetComponent<AudioSource>().Play();
        // apply hit effect - hit effect should have sound on damage, particles, show damage numbers, etc.
        stats.health -= damage;
    }

    void Die()
    {
        if (explosionPrefab)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<Explosion>().endSize = transform.localScale.x * 5;
            explosion.GetComponent<Explosion>().lifetime = transform.localScale.x;
        }
        Destroy(gameObject, 0f);
    }
}
