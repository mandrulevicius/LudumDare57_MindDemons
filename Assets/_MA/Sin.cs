using UnityEngine;

public class Sin : MonoBehaviour
{
    private Entity entity;
    public AudioClip audioClip;
    [SerializeField] private GameObject healthBar;
    private float maxHealth;
    private float currentHealth;
    private float scalex;

    [SerializeField] public GameObject body;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        if (entity)
            maxHealth = entity.stats.health;
        if (healthBar) scalex = healthBar.transform.localScale.x;
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        if(gameObject.tag!="Boss")
        GameObject.FindWithTag("Events").GetComponent<InteractionEvents>().PlayGenericClip();
    }

    private void FixedUpdate()
    {
        if (!entity) return;
            currentHealth = entity.stats.health;
        if (body) body.transform.rotation = Quaternion.Euler(0, entity.lookDirectionVector.x < 0 ? 180 : 0, 0);
        if (healthBar)
        {
            Vector3 scale = healthBar.transform.localScale;
            scale.x = scalex * (currentHealth / maxHealth);
            healthBar.transform.localScale = scale;
        }
    }
}