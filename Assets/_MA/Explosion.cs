using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Explosion : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Light2D explosionLight;
    
    
    [ReadOnly][SerializeField] float size = 0f;
    public float lifetime = 1f;
    public float endSize = 1f;

    float sizeStep = 1f;
    float alphaStep = 1f;
    float lightIntensityStep = 1f;
    float lightRadiusStep = 1f;
    float colorStep = 1f;

    
    // ============================= INIT =============================
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Material mat = spriteRenderer.material;
        // mat.SetColor("_EmissionColor", Color.white * 5f);
        explosionLight = GetComponent<Light2D>();
        size = 0;
        transform.localScale = new Vector3(0, 0, 0);
    }

    void Start()
    {
        explosionLight.intensity = endSize * 10f;
        explosionLight.pointLightOuterRadius = 0f;
        
        sizeStep = Mathf.Lerp(0f, endSize, Time.fixedDeltaTime / lifetime);
        alphaStep = Mathf.Lerp(0f, 1f, Time.fixedDeltaTime / lifetime);
        lightIntensityStep = Mathf.Lerp(0f, explosionLight.intensity, Time.fixedDeltaTime / lifetime);
        lightRadiusStep = Mathf.Lerp(0f, endSize * 2f, Time.fixedDeltaTime / lifetime);
        colorStep = Mathf.Lerp(0f, 4f, Time.fixedDeltaTime / lifetime);
        
        
    }

    
    // =========================== UPDATES ============================
    void FixedUpdate()
    {
        size += sizeStep;
        if(size >= endSize) Destroy(gameObject);
        transform.localScale = new Vector3(size, size, size);
        
        Color color = spriteRenderer.color;
        color.a -= alphaStep;
        if (color.b > 0) color.b -= colorStep;
        else if (color.g > 0) color.g -= colorStep;
        else if (color.r > 0) color.r -= colorStep;
        spriteRenderer.color = color;

        if (explosionLight.intensity > 0) explosionLight.intensity -= lightIntensityStep;
        explosionLight.pointLightOuterRadius += lightRadiusStep;
    }
}
