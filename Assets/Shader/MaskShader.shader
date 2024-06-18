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
                // �ؽ�ó ���ø�
                half4 color = tex2D(_MainTex, i.uv);

                // �������κ����� �Ÿ� ��� (���� ����)
                float2 center = float2(0.5, 0.5); // ���� ��ġ
                float radiusX = 0.08; // ���� X�� ������
                float radiusY = 0.145; // ���� Y�� ������

                float2 normalizedUV = i.uv - center;
                normalizedUV.y /= (radiusY / radiusX); // Y�� ���� ����

                float dist = length(normalizedUV);

                // �߾� ���� �κ��� �����ϰ� ����
                if (dist <= radiusX)
                {
                    color.a = 0.0;
                }
                else
                {
                    // ������ �κ��� ���������� ������
                    color.rgb = float3(0, 0, 0);
                    color.a = 1.0;
                }

                return color;
            }
            ENDCG
        }
    }
}
