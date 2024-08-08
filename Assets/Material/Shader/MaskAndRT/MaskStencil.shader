Shader "Custom/MaskStencil"
{
    Properties
    {
        _CreationTime ("Creation Time", Float) = 0.0
        _CurrentTime ("Current Time", Float) = 0.0
        _AnimationTime ("Animation Time", Range(0, 10)) = 2.0
        _GridNum ("GridNum", Range (1,2048)) = 64
        _Progess ("Progress", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Geometry" }
        //ColorMask 0 
        ZWrite Off 
        Cull Off

        Pass
        {
            Stencil
            {
                Ref 1
                Comp always
                Pass keep
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_nostencil
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag_nostencil (v2f i) : SV_Target
            {
                return fixed4(1, 1, 1, 1);
            }
            ENDCG
        }

        Pass
        {
            Stencil
            {
                Ref 1          
                Comp always   
                Pass IncrSat
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_stencil
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            uniform float _CreationTime;
            uniform float _CurrentTime;
            uniform float _AnimationTime; 
            uniform float _Progess;
            uniform int _GridNum;

            half rand2(half2 seed)
            {
                return frac(sin(dot(seed, half2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag_stencil (v2f i) : SV_Target
            {
                float creationTime = _CreationTime;
                float currentTime = _CurrentTime;
                float animationTime = _AnimationTime; 
                float progress = _Progess;
                int GridNum = _GridNum;

                progress = (currentTime - creationTime)  / animationTime;
                
                int fullSquare = GridNum / 2;
                int xGrid = i.uv.x * GridNum;
                int yGrid = i.uv.y * GridNum;
                int xDisToCenter = abs(xGrid - fullSquare);
                int yDisToCenter = abs(yGrid - fullSquare);

                half randomValue = pow(rand2(half2(xGrid,yGrid)),-1);

                if (xDisToCenter <= floor(fullSquare * progress * randomValue) && yDisToCenter <= floor(fullSquare * progress * randomValue)
                || xDisToCenter <= floor(fullSquare * progress) && yDisToCenter <= floor(fullSquare * progress)
                ) 
                {
                    return fixed4(0, 0, 1, 1); 
                }

                discard;

                return fixed4(1, 1, 1, 1);
            }
            ENDCG
        }

        
    }
}