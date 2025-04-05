using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public struct Stats
{
    [ReadOnly] [AllowNesting] public string id;
    [ReadOnly] [AllowNesting] public string name;
    
    [Header("Base")]
    [Tooltip("Max health, contact damage multiplier")] public float mass;
    [Tooltip("Volume = Mass / Density")] public float density;
    [Tooltip("Scale")] [ReadOnly] [AllowNesting] public float volume; 
    
    [Header("Contact damage modifiers")]
    [Tooltip("Percentage damage modifier")] public float strength;
    [Tooltip("Flat damage modifier")] public float hardness;
    
    [Header("Current")]
    [Tooltip("Current healthy mass")] public float health;
    [Tooltip("Current velocity")] [ReadOnly] [AllowNesting] public Vector2 velocity;
    // maybe velocity should be on entity, but then would have to pass to interaction separately
    
    [Header("Movement")] // rocks dont have movement, fuel
    public float speed;
    public float speedBoostMultiplier;
    public float fuelCapacity;
    public float fuel;
    
    [Header("Energy")] // rocks don't have energyCapacity
    [Tooltip("Combat ability max resource")] public float energyCapacity;
    [Tooltip("Combat ability resource")] public float energy;
}

public class Entity : MonoBehaviour
{
    public Stats stats;
    [ReadOnly] public Rigidbody2D rb;
    
    // public LayerMask targetLayers;
    public GameObject movementTarget;
    [ReadOnly] public Vector2 movementTargetPosition;
    [ReadOnly] public Vector2 moveDirectionVector;
    
    // AI needs lookTarget to know where to look.
    [ReadOnly] public Vector2 lookDirectionVector;
    
    
    public bool isJumping;
    public bool isDashing;

    float _speedPerTick;
    
    
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
        Entity otherEntity = other.gameObject.GetComponent<Entity>();
        float damage = CalcDamage(otherEntity);

        Interactions.Add(
            stats,
            otherEntity.stats,
            damage,
            rb.linearVelocity
        );
        
        ApplyDamage(damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // gravity here
        Debug.Log($"{gameObject.name} triggered by collider: {other.gameObject.name}");
    }

    
    // =========================== UPDATES ============================
    void FixedUpdate()
    {
        UpdateEntity();
        if (movementTarget) movementTargetPosition = movementTarget.transform.position;
        if (movementTargetPosition != Vector2.zero) moveDirectionVector = (movementTargetPosition - (Vector2)transform.position).normalized;

        if (moveDirectionVector != Vector2.zero) CalcSpeed();
        if (moveDirectionVector != Vector2.zero) transform.position += (Vector3) moveDirectionVector * _speedPerTick;
        // if (moveDirectionVector != Vector2.zero) rb.AddForce(moveDirectionVector * _speedPerTick, ForceMode2D.Impulse);
        // if (!isStabilizing) rb.linearDamping = 0;
        // else rb.linearDamping = _speedPerTick;
    }
    
    
     // ========================== FUNCTIONS ===========================
    void UpdateEntity()
    {
        // ok for now, but should only update when stats change. LinearVelocity can be left as is.
        // should also update health proportionally to mass change
        if (stats.strength <= 0 || stats.density <= 0 || stats.mass <= 0 || stats.health <= 0) Die();
        
        stats.volume = stats.mass / stats.density;
        
        rb.mass = stats.mass;
        transform.localScale = new Vector3(stats.volume, stats.volume, stats.volume);
        
        stats.velocity = rb.linearVelocity;
    }
    
    void CalcSpeed()
    {
        _speedPerTick = stats.speed * Time.fixedDeltaTime;
        if (!isDashing || stats.fuel <= 0) return;
        stats.fuel -= Time.fixedDeltaTime;
        _speedPerTick *= stats.speedBoostMultiplier;
    }

    float CalcDamage(Entity otherEntity)
    {
        Vector2 relativeVelocity = otherEntity.stats.velocity - stats.velocity;
        float relativeMomentum = otherEntity.stats.mass * relativeVelocity.magnitude;
        
        float strengthRatio = otherEntity.stats.strength / stats.strength;
        float hardnessDifference = otherEntity.stats.hardness - stats.hardness;
        
        return relativeMomentum * strengthRatio + hardnessDifference;
        // return (relativeMomentum + hardnessDifference) * strengthRatio;
    }

    void ApplyDamage(float damage)
    {
        GetComponent<AudioSource>().Play();
        // apply hit effect - hit effect should have sound on damage, particles, show damage numbers, etc.
        stats.health -= damage;
    }

    void Die()
    {
        Destroy(gameObject, 0f);
    }
}
