using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 4f;
    public float finalSizeMultiplier = 5f;
    private Vector3 initialScale;
    private Vector3 scaleStep;
    private float opacityStep;
    private int tick;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        initialScale = transform.localScale;
        scaleStep = initialScale * finalSizeMultiplier / (lifeTime * 50);
        opacityStep = 0.5f / (lifeTime * 50);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        tick++;
        transform.localScale += scaleStep;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - opacityStep);
        if (tick <= lifeTime * 50) return;
        Destroy(gameObject, 0f);
    }
}
