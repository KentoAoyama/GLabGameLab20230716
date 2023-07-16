Shader "Custom/WaterSurface"
{
    Properties
    {
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {}
        [HDR]_Color ("Color", Color) = (1, 1, 1, 1)
        [Header(Voronoi)]
        _Range ("Range", Range(0, 1)) = 0.2
        _Scale ("VoronoiScale", Float) = 3
        [HDR]_ShinessColor ("ShinessColor", Color) = (1, 1, 1, 1)
        [Header(Blur)]
        _NoiseTex ("NoiseTexture", 2D) = "black" {}
        _ScrollSpeed ("ScrollSpeed", Float) = 1
        _ScrollDir ("ScrollDir", Vector) = (1, 1, 0, 0)
        _BlurStrength ("BlurStrength", Float) = 1
        _BlurDir ("BlurDir", Vector) = (1, 1, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend One OneMinusSrcAlpha

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Library/WaterSurfaceInceInput.hlsl"
        ENDHLSL

        Pass
        {
            Tags
            {
                "LightMode" = "Universal2D"
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Library/CellularNoise.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 voronoiSeed : TEXCOORD1;
                float2 noiseuv : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.voronoiSeed = o.uv * _Scale;
                o.noiseuv = TRANSFORM_TEX(v.uv, _NoiseTex) + _ScrollDir.xy * _Time.x * _ScrollSpeed;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * _Color;
                float noise = SAMPLE_TEXTURE2D(_NoiseTex, sampler_NoiseTex, i.noiseuv).r - 0.5f;

                i.voronoiSeed += _BlurDir.xy * _BlurStrength * noise;

                float cell = cellularNoise(i.voronoiSeed);
                col += _ShinessColor * pow(cell, 5);
                col.rgb = col.a;

                return col;
            }
            ENDHLSL
        }
    }
}
