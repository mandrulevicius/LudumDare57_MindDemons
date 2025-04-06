using UnityEngine;

public class Player : MonoBehaviour
{
    Entity entity;
    
    // ============================= INIT =============================
    void Awake()
    {
        entity = GetComponent<Entity>();
    }
    
    // =========================== UPDATES ============================
    void FixedUpdate()
    {
        float angle = Mathf.Atan2(entity.lookDirectionVector.y, entity.lookDirectionVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
