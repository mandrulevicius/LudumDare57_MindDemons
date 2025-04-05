Shader "Custom/ProceduralFireball"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 0.5, 0, 1) // Fire's base color
        _SecondaryColor ("Secondary Color", Color) = (1, 0.2, 0, 1) // Fire's highlight color
        _ScrollSpeed ("Scroll Speed", Float) = 0.5 // Speed of the scrolling effect
        _DistortionStrength ("Distortion Strength", Float) = 0.2 // Intensity of the distortion
        _EmissionIntensity ("Emission Intensity", Float) = 2.0 // Glow effect
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 300

        Pass
        {
            Blend SrcAlpha One // Additive blending
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
            float _DistortionStrength;
            float _EmissionIntensity;

            // Procedural noise generation
            float hash(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 34.56);
                return frac(p.x * p.y);
            }

            float perlin(float2 p)
            {
                float2 i = floor(p); // Integer part
                float2 f = frac(p); // Fractional part

                // Compute noise contributions from the 4 corners
                float a = hash(i);
                float b = hash(i + float2(1.0, 0.0));
                float c = hash(i + float2(0.0, 1.0));
                float d = hash(i + float2(1.0, 1.0));

                // Interpolation
                float2 u = f * f * (3.0 - 2.0 * f); // Smoothstep
                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }

            // Distortion function
            float2 distortion(float2 uv, float time)
            {
                float noise = perlin(uv * 10.0 + time);
                return uv + float2(noise - 0.5, noise - 0.5) * _DistortionStrength;
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
                // Animate the UVs
                float time = _Time.y * _ScrollSpeed;
                float2 uv = distortion(i.uv, time);

                // Generate noise
                float noiseValue = perlin(uv * 5.0 + time);

                // Blend between the two colors
                fixed4 fireColor = lerp(_SecondaryColor, _MainColor, noiseValue);

                // Apply emission
                fireColor.rgb *= _EmissionIntensity;

                return fireColor;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Transparent"
}
