Shader "Custom/MaskShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // 텍스처 샘플링
                half4 color = tex2D(_MainTex, i.uv);

                // 원점으로부터의 거리 계산 (원형 유지)
                float2 center = float2(0.5, 0.5); // 원점 위치
                float radiusX = 0.08; // 원의 X축 반지름
                float radiusY = 0.145; // 원의 Y축 반지름

                float2 normalizedUV = i.uv - center;
                normalizedUV.y /= (radiusY / radiusX); // Y축 비율 보정

                float dist = length(normalizedUV);

                // 중앙 원형 부분을 투명하게 만듦
                if (dist <= radiusX)
                {
                    color.a = 0.0;
                }
                else
                {
                    // 나머지 부분을 검은색으로 가리기
                    color.rgb = float3(0, 0, 0);
                    color.a = 1.0;
                }

                return color;
            }
            ENDCG
        }
    }
}
