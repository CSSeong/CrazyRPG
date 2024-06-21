Shader "Custom/BlurryCircleShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _LightGage ("Light Gauge", Float) = 1.0
        _BlurRadius ("Blur Radius", Float) = 0.02
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

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // �ؽ�ó ���ø�
                half4 color = tex2D(_MainTex, i.uv);

                // �������κ����� �Ÿ� ��� (���� ����)
                float2 center = float2(0.5, 0.5); // ���� ��ġ
                float radiusX = 0.16f / 2.5f * _LightGage + 0.008f; // ���� X�� ������
                float radiusY = 0.29f / 2.5f * _LightGage + 0.0145f; // ���� Y�� ������

                float2 normalizedUV = i.uv - center;
                normalizedUV.y /= (radiusY / radiusX); // Y�� ���� ����

                float dist = length(normalizedUV);

                // ���� �߾� �κ��� �����ϰ� ����
                if (dist <= radiusX)
                {
                    color.a = 0.0;
                }
                else if (dist <= radiusX + _BlurRadius)
                {
                    // ��� �κ��� �����ϰ� ����� ���� ���
                    float blurAmount = smoothstep(radiusX, radiusX + _BlurRadius, dist);
                    color.rgb = lerp(color.rgb, half4(0, 0, 0, 1), blurAmount + 0.95f);
                    color.a = 0 + blurAmount; // ��� �κ��� ���� �����ϰ� ����
                }
                else
                {
                    // ���� �ܺδ� ���������� ä��
                    color = half4(0, 0, 0, 1);
                }

                return color;
            }
            ENDCG
        }
    }
}
