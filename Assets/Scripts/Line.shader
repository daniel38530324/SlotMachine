Shader "UI/UILineGlow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _GlowIntensity ("Glow Intensity", Float) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Cull Off
        Lighting Off
        ZWrite Off
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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _GlowIntensity;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 取得貼圖顏色
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // 高亮效果：將顏色乘上 GlowIntensity (保留透明度)
                fixed3 glowColor = _Color.rgb * _GlowIntensity;

                fixed4 finalColor = texColor * _Color.a;      // 保留貼圖 alpha
                finalColor.rgb = texColor.rgb * _Color.rgb + glowColor; 
                finalColor.a = texColor.a * _Color.a;

                return finalColor;
            }
            ENDCG
        }
    }
}
