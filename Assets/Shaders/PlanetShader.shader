Shader "Planet Test/Planet surface"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HeightMap ("Height map", 2D) = "white" {}
		_HeightMapMidpoint ("Heightmap colour midpoint", Range(0, 1)) = 0.5
		_Offset ("Offset amount", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct VertexData {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct FragmentData {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _HeightMap;
            float4 _MainTex_ST;

            float _Offset;
            float _HeightMapMidpoint;
            
            FragmentData vert(VertexData vertexData) {
                FragmentData f;

                f.uv = TRANSFORM_TEX(vertexData.uv, _MainTex);

                fixed4 height = tex2Dlod(_HeightMap, float4(f.uv, 0, 0));
                float4 vertex = vertexData.vertex + (float4(vertexData.normal.xyz, 0) * (height.r - _HeightMapMidpoint) * _Offset);

                f.vertex = UnityObjectToClipPos(vertex);

                return f;
            }
            
            fixed4 frag(FragmentData fragmentData) : SV_Target {
                fixed4 textureColor = tex2D(_MainTex, fragmentData.uv);

                return textureColor;
            }
            ENDCG
        }
	}
}
