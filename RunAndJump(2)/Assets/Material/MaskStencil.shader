Shader "Custom/MaskStencil"
{
    SubShader
    {
        Tags { "Queue"="Geometry" } // Render this shader after most other things have been rendered
        ColorMask 0 // Do not draw any visible colors
        ZWrite Off // Do not write to the Z-Buffer
        Stencil
        {
            Ref 1          
            Comp always   
            Pass IncrSat
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(0, 0, 0, 1); // Output invisible color
            }
            ENDCG
        }
    }
}