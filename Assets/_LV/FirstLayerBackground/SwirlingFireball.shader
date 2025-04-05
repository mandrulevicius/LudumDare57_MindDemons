Shader "Custom/SwirlingFireball"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 0.4, 0, 1) // Base fire color
        _SecondaryColor ("Secondary Color", Color) = (1, 0.1, 0, 1) // Highlight color
        _ScrollSpeed ("Scroll Speed", Float) = 1.0 // Speed of swirling
        _SwirlStrength ("Swirl Strength", Float) = 5.0 // Intensity of the swirl
        _NoiseStrength ("Noise Strength", Float) = 0.5 // Intensity of noise
        _EmissionIntensity ("Emission Intensity", Float) = 2.0 // Glow effect
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
            float4 _SecondaryColor;
            float _ScrollSpeed;
            float _SwirlStrength;
            float _NoiseStrength;
            float _EmissionIntensity;

            // Hash function for random-like noise
            float hash(float2 p)
            {
                p = frac(p * float2(123.45, 678.91));
                p += dot(p, p + 34.56);
                return frac(p.x * p.y);
            }

            // Radial noise function
            float radialNoise(float2 uv)
            {
                float radius = length(uv - 0.5); // Distance from center
                float angle = atan2(uv.y - 0.5, uv.x - 0.5); // Angle around the center

                float noise = sin(angle * 10.0 + radius * 20.0); // Swirling effect
                noise += hash(uv * 10.0); // Add randomness
                return noise * 0.5 + 0.5; // Normalize
            }

            // Swirl effect for UVs
            float2 swirl(float2 uv, float time)
            {
                float2 center = float2(0.5, 0.5);
                float2 delta = uv - center;

                float radius = length(delta);
                float angle = atan2(delta.y, delta.x) + time * _ScrollSpeed * radius * _SwirlStrength;

                float2 rotated = float2(cos(angle), sin(angle)) * radius;
                return center + rotated;
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
                // Swirl the UVs
                float time = _Time.y;
                float2 uv = swirl(i.uv, time);

                // Generate radial noise
                float noise = radialNoise(uv);

                // Combine noise with colors
                fixed4 fireColor = lerp(_SecondaryColor, _MainColor, noise);

                // Apply emission
                fireColor.rgb *= _EmissionIntensity;

                return fireColor;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
}
