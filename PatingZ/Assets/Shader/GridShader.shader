Shader "Custom/GridShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _GridColor ("Grid Color", Color) = (1, 1, 1, 1)
        _GridSize ("Grid Size", Range(0.01, 0.1)) = 0.01
        _GridThickness ("Grid Thickness", Range(0.01, 0.1)) = 0.1
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _GridSize;
            float _GridThickness;
            float4 _GridColor;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 uv = i.uv.xy;
                float2 pixelPos = floor(uv / _GridSize) * _GridSize;
                float2 withinPixelPos = frac(uv / _GridSize) * _GridSize;
                float2 withinPixelPosX = float2(withinPixelPos.x, 0);
                float2 withinPixelPosY = float2(0, withinPixelPos.y);
                float2 withinPixelPosXDist = fwidth(withinPixelPosX);
                float2 withinPixelPosYDist = fwidth(withinPixelPosY);
                float2 horzDist = min(withinPixelPosX - withinPixelPosXDist, (withinPixelPosXDist - withinPixelPosX) + _GridThickness * withinPixelPosXDist);
                float2 vertDist = min(withinPixelPosY - withinPixelPosYDist, (withinPixelPosYDist - withinPixelPosY) + _GridThickness * withinPixelPosYDist);
                float2 dist = min(horzDist, vertDist);
                float2 mask = step(dist, 0.5 * withinPixelPosXDist + 0.5 * withinPixelPosYDist);
                fixed4 gridColor = lerp(_GridColor, tex2D(_MainTex, pixelPos), tex2D(_MainTex, pixelPos).a);
                fixed4 tileColor = tex2D(_MainTex, i.uv);
                return lerp(gridColor, tileColor, tileColor.a) * mask.x * mask.y;
            }            


            ENDCG
        }
    }
    FallBack "Diffuse"
}