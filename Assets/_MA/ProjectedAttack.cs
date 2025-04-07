using UnityEngine;

public class ProjectedAttack : MonoBehaviour
{
    public GameObject projectedPrefab;
    Entity parentEntity;
    
    public float fireRate = 3f;
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
        GameObject projectedArea = Instantiate(projectedPrefab, transform.position, Quaternion.identity);
        projectedArea.GetComponent<Entity>().stats.touchDamage = parentEntity.stats.rangedDamage;
        projectedArea.layer = gameObject.layer;
        projectedArea.transform.position = parentEntity.lookTargetPosition;
    }
}
