Shader "Custom/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortionSpeed ("Distortion Speed", Range(0, 10)) = 1
        _DistortionStrength ("Distortion Strength", Range(0, 0.1)) = 0.02
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Tiling ("Tiling", Vector) = (1, 1, 0, 0)
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float _DistortionSpeed;
            float _DistortionStrength;
            float _Opacity;
            float4 _Tiling;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _Tiling.xy + _Tiling.zw;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 distortionOffset = float2(
                    sin(_Time.y * _DistortionSpeed),
                    cos(_Time.x * _DistortionSpeed)
                ) * _DistortionStrength;

                fixed4 c = tex2D(_MainTex, i.uv + distortionOffset);
                c.a *= _Opacity;
                return c;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
