Shader "Custom/BlurryCircleShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _LightGage ("Light Gauge", Float) = 1.0
        _BlurRadius ("Blur Radius", Float) = 0.02
        _RadiusX ("Radius X", Float) = 0.1
        _RadiusY ("Radius Y", Float) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
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
            float _LightGage;
            float _BlurRadius;
            float _RadiusX;
            float _RadiusY;

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

                float2 normalizedUV = i.uv - center;
                normalizedUV.y /= (_RadiusY / _RadiusX); // Y축 비율 보정

                float dist = length(normalizedUV);

                // 원의 중앙 부분을 투명하게 만듦
                if (dist <= _RadiusX)
                {
                    color.a = 0.0;
                }
                else if (dist <= _RadiusX + _BlurRadius)
                {
                    // 경계 부분을 투명하게 만들기 위해 계산
                    float blurAmount = smoothstep(_RadiusX, _RadiusX + _BlurRadius, dist);
                    color.rgb = lerp(color.rgb, half4(0, 0, 0, 1), blurAmount + 0.95f);
                    color.a = 0 + blurAmount; // 경계 부분을 점점 투명하게 만듦
                }
                else
                {
                    // 원의 외부는 검은색으로 채움
                    color = half4(0, 0, 0, 1);
                }

                return color;
            }
            ENDCG
        }
    }
}
