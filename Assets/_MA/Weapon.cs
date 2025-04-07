using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    Entity parentEntity;

    public float shotForce = 1f;
    
    public float fireRate = 1f;
    int tick = 0;

    private bool wasShooting;

    void Awake()
    {
        parentEntity = GetComponentInParent<Entity>();
    }

    void FixedUpdate()
    {
        if (!wasShooting && parentEntity.isShooting && tick == 0)
        {
            Shoot();
            wasShooting = true;
        }
        tick++;
        if (!parentEntity.isShooting)
        {
            wasShooting = false;
            return;
        }
        if (tick <= fireRate * 50 / (1 + parentEntity.stats.attackSpeed)) return;
        tick = 1;
        Shoot();
    }

    void Shoot()
    {
        GetComponent<AudioSource>().Play();
        var bulletPos = transform.position;
        if (bulletPrefab.gameObject.name == "Fart") bulletPos.z += 1;
        else bulletPos.z -= 1;
        GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
        bullet.GetComponent<Entity>().stats.touchDamage = parentEntity.stats.rangedDamage;
        Vector2 direction = parentEntity.lookDirectionVector;
        if (gameObject.name == "LustWeapon") bullet.GetComponent<Entity>().movementTarget = parentEntity.lookTarget;
        else bullet.GetComponent<Entity>().moveDirectionVector = parentEntity.lookDirectionVector;
        // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.layer = gameObject.layer;
        // rb.linearVelocity = direction * shotForce + parentEntity.stats.velocity;
    }
}