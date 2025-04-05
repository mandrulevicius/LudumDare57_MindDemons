Shader "Custom/SeamlessFireball"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 0.4, 0, 1) // Base fire color
        _GlowColor ("Glow Color", Color) = (1, 0.7, 0, 1) // Outer glow color
        _NoiseScale ("Noise Scale", Float) = 10.0 // Scale of the noise
        _DistortionStrength ("Distortion Strength", Float) = 0.1 // Distortion intensity
        _EdgeFade ("Edge Fade Strength", Float) = 1.0 // Controls how much the edges fade
        _EmissionIntensity ("Emission Intensity", Float) = 3.0 // Brightness of the fireball
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" }
        LOD 300

        Pass
        {
            Blend SrcAlpha One // Additive blending for glowing effect
            Cull Off
            ZWrite Off // Disable depth writing to avoid sorting issues

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
            };

            float4 _MainColor;
            float4 _GlowColor;
            float _NoiseScale;
            float _DistortionStrength;
            float _EdgeFade;
            float _EmissionIntensity;

            // A simple noise function for flickering and distortion
            float noise(float2 uv)
            {
                return frac(sin(dot(uv.xy, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; // World position
                o.normal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal)); // World normal
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos); // View direction
                float3 normal = normalize(i.normal); // Normalized normal

                // Dot product to calculate how much the fragment faces the camera
                float facingFactor = saturate(dot(viewDir, normal));

                // Radial fade based on distance from sphere center
                float2 uv = i.uv - 0.5; // Center UV
                float radialDist = length(uv) * 2.0; // Distance from center
                float edgeFade = smoothstep(1.0 - _EdgeFade, 1.0, radialDist); // Fade edges

                // Combine noise for dynamic appearance
                float distortion = noise(i.uv * _NoiseScale + _Time.y);
                float2 distortedUV = i.uv + (distortion - 0.5) * _DistortionStrength;

                // Base fireball color
                float fireIntensity = 1.0 - radialDist;
                fixed4 fireColor = lerp(_GlowColor, _MainColor, fireIntensity);
                fireColor.rgb *= _EmissionIntensity; // Add emission brightness

                // Combine edge fade and facing factor for seamless blending
                fireColor.a *= edgeFade * facingFactor;

                return fireColor;
            }
            ENDCG
        }
    }

    FallBack "Unlit/Transparent"
}
