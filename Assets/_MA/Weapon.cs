using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    Entity parentEntity;

    public float shotForce = 1f;
    
    public float fireRate = 1f;
    int tick = 0;

    void Awake()
    {
        parentEntity = GetComponentInParent<Entity>();
    }

    void FixedUpdate()
    {
        if (!parentEntity.inCombat) return;
        tick++;
        if (tick <= fireRate * 50) return;
        tick = 0;
        Shoot();
    }

    void Shoot()
    {
        GetComponent<AudioSource>().Play();
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = parentEntity.lookDirectionVector;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.layer = gameObject.layer;
        rb.linearVelocity = direction * shotForce + parentEntity.stats.velocity;
    }
}