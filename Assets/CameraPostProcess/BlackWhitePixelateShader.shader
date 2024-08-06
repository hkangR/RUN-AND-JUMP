Shader "Custom/BlackWhitePixelateShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", Float) = 10.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _PixelSize;
            
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate pixel size in texture space
                float2 pixelSize = float2(_PixelSize / _ScreenParams.x, _PixelSize / _ScreenParams.y);
                
                // Quantize the UV coordinates to create a pixelation effect
                float2 uv = floor(i.uv / pixelSize) * pixelSize;
                
                // Sample the texture
                fixed4 col = tex2D(_MainTex, uv);
                
                // Convert to grayscale
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                col.rgb = gray.xxx;
                
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}