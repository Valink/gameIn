Shader "Unlit/Stars"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StarColor("Star Color", Color) = (1, 1,  1, 1)
        _StarDensity("Star Density", float) = 5
        _BlinkingSpeed("Blinking Speed", Range(.001, .1)) = .05
        _Brightness("Brightness", float) = 2
    }
    SubShader
    {
        Tags { "RenderQueue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask RGB
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest LEqual
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            float hash(float2 p)
            {
                p = 50.0 * frac(p * 0.3183099 + float2(0.71, 0.113));
                return -1.0 + 2.0 * frac(p.x * p.y * (p.x + p.y));
            }

            float perlin(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);

                float2 u = f * f * (3.0 - 2.0 * f);

                return lerp(lerp(hash(i + float2(0.0, 0.0)), hash(i + float2(1.0, 0.0)), u.x), lerp(hash(i + float2(0.0, 1.0)), hash(i + float2(1.0, 1.0)), u.x), u.y);
            }

            /// <summary>
            /// Book of Shaders' random functions
            /// </summary>
            float2 random2(float2 p) {
                return frac(sin(float2(dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)))) * 43758.5453);
            }

            /// <summary>
            /// Voronoi noise algorithm (from multiple online sources)
            /// </summary>
            float voronoi(float2 value, float minDistToCell) {
                float2 baseCell = floor(value);

                // Voronoi noise base algorithm
                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        float2 cell = baseCell + float2(x, y);
                        float2 cellPosition = cell + random2(cell);
                        float2 toCell = cellPosition - value;
                        float distToCell = length(toCell);

                        minDistToCell = min(distToCell, minDistToCell);
                    }
                }

                return minDistToCell;
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _StarDensity;
            float _BlinkingSpeed;
            fixed4 _StarColor;
            float _Brightness;

            float fun(float2 pt) {
                return smoothstep(.94, .95, (1 - voronoi(pt, 8)) + cos(_Time.w + perlin(pt)) * sin(_Time.w + perlin(pt)) * _BlinkingSpeed) * _Brightness;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 pt = i.uv * _StarDensity;
                fixed4 col = lerp(fixed4(0, 0, 0, 0), _StarColor, fun(pt));

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
