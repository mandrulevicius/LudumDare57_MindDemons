Shader "Custom/AdvancedFireball"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 0.4, 0, 1) // Base fire color
        _GlowColor ("Glow Color", Color) = (1, 0.7, 0, 1) // Outer glow
        _CoreColor ("Core Color", Color) = (1, 1, 0, 1) // Inner core color
        _ScrollSpeed ("Scroll Speed", Float) = 1.0 // Animation speed
        _DistortionStrength ("Distortion Strength", Float) = 0.1 // Heat distortion strength
        _SwirlStrength ("Swirl Strength", Float) = 5.0 // Swirl intensity
        _NoiseScale ("Noise Scale", Float) = 10.0 // Scale of the noise
        _EmissionIntensity ("Emission Intensity", Float) = 3.0 // Glow intensity
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 300

        Pass
        {
            Blend SrcAlpha One // Additive blending for glowing effect
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _MainColor;
            float4 _GlowColor;
            float4 _CoreColor;
            float _ScrollSpeed;
            float _DistortionStrength;
            float _SwirlStrength;
            float _NoiseScale;
            float _EmissionIntensity;

            // Random noise function
            float hash(float2 p)
            {
                p = frac(p * float2(123.45, 678.91));
                p += dot(p, p + 34.56);
                return frac(p.x * p.y);
            }

            // Smooth noise
            float noise(float2 uv)
            {
                float2 i = floor(uv);
                float2 f = frac(uv);

                float a = hash(i);
                float b = hash(i + float2(1.0, 0.0));
                float c = hash(i + float2(0.0, 1.0));
                float d = hash(i + float2(1.0, 1.0));

                float2 smooth = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(a, b, smooth.x), lerp(c, d, smooth.x), smooth.y);
            }

            // Swirl effect for UVs
            float2 swirl(float2 uv, float time)
            {
                float2 center = float2(0.5, 0.5);
                float2 delta = uv - center;

                float radius = length(delta);
                float angle = atan2(delta.y, delta.x) + radius * _SwirlStrength * sin(time * 0.5);

                float2 rotated = float2(cos(angle), sin(angle)) * radius;
                return center + rotated;
            }

            // Distortion effect for UVs
            float2 distort(float2 uv, float time)
            {
                float noiseValue = noise(uv * _NoiseScale + time);
                return uv + float2(noiseValue - 0.5, noiseValue - 0.5) * _DistortionStrength;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float time = _Time.y * _ScrollSpeed;

                // Apply swirl and distortion
                float2 uv = distort(swirl(i.uv, time), time);

                // Core fire effect (intense at the center)
                float radialGradient = 1.0 - length(i.uv - float2(0.5, 0.5)) * 2.0;
                radialGradient = saturate(radialGradient);

                // Noise-based flicker
                float noiseValue = noise(uv * _NoiseScale + time);

                // Combine layers for fire
                float fireIntensity = radialGradient * noiseValue;
                fixed4 fireColor = lerp(_GlowColor, _MainColor, fireIntensity);
                fireColor = lerp(fireColor, _CoreColor, pow(radialGradient, 3.0));

                // Apply emission
                fireColor.rgb *= _EmissionIntensity;

                return fireColor;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
}
