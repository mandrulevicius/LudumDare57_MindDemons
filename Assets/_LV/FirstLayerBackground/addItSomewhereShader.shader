Shader "Custom/ForSmth"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 0.4, 0, 1) // Base fire color
        _GlowColor ("Glow Color", Color) = (1, 0.7, 0, 1) // Outer glow
        _CoreColor ("Core Color", Color) = (1, 1, 0, 1) // Inner core
        _ScrollSpeed ("Scroll Speed", Float) = 1.0 // Fire animation speed
        _DistortionStrength ("Distortion Strength", Float) = 0.1 // Heat distortion
        _NoiseScale ("Noise Scale", Float) = 10.0 // Scale of the noise
        _EmissionIntensity ("Emission Intensity", Float) = 3.0 // Brightness of the glow
        _EdgeBlurStrength ("Edge Blur Strength", Float) = 0.5 // Amount of blur at edges
        _EdgeBlurFalloff ("Edge Blur Falloff", Float) = 0.3 // Controls the softness of the edge blur
        _BlurRadius ("Blur Radius", Float) = 0.5 // Radius of the blur effect
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType" = "Transparent" }
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
            float4 _GlowColor;
            float4 _CoreColor;
            float _ScrollSpeed;
            float _DistortionStrength;
            float _NoiseScale;
            float _EmissionIntensity;
            float _EdgeBlurStrength;
            float _EdgeBlurFalloff;
            float _BlurRadius;

            // Simple noise generation function for flicker and distortion
            float hash(float2 p)
            {
                p = frac(p * float2(123.45, 678.91));
                p += dot(p, p + 34.56);
                return frac(p.x * p.y);
            }

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

            // Apply distortion to the UVs for movement effects
            float2 distortUV(float2 uv, float time)
            {
                float noiseValue = noise(uv * _NoiseScale + time);
                return uv + float2(noiseValue - 0.5, noiseValue - 0.5) * _DistortionStrength;
            }

            // Calculate the angle of the fragment relative to the view direction
            float calculateEdgeBlur(float2 uv)
            {
                // Distance from center of the UV space (0.5, 0.5)
                float distFromCenter = length(uv - float2(0.5, 0.5));

                // Only apply blur near the edges, based on the distance from center
                float edgeFactor = smoothstep(0.8, 1.0, distFromCenter); // The blur will intensify toward the edge

                // Apply more falloff to make it smoother
                edgeFactor = pow(edgeFactor, _EdgeBlurFalloff);

                return edgeFactor;
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

                // Distorted UVs to simulate fire movement
                float2 uv = distortUV(i.uv, time);

                // Calculate edge blur based on view angle (distance from the center)
                float edgeBlur = calculateEdgeBlur(uv);

                // Noise effect for flickering
                float fireNoise = noise(uv * _NoiseScale + time);

                // Compute fire intensity, combining glow, main color, and noise
                float fireIntensity = 1.0 - length(uv - float2(0.5, 0.5));
                fixed4 fireColor = lerp(_GlowColor, _MainColor, fireNoise * fireIntensity);
                fireColor = lerp(fireColor, _CoreColor, pow(fireIntensity, 3.0));

                // Apply the edge blur to the alpha channel for soft blending at edges
                fireColor.a *= edgeBlur;

                // Emission effect: enhance brightness based on the alpha and intensity
                fireColor.rgb *= _EmissionIntensity * fireColor.a;

                return fireColor;
            }
            ENDCG
        }
    }

    FallBack "Unlit/Transparent"
}
