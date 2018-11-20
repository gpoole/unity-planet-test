Shader "Planet Test/PlanetSurfaceShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HeightMaps ("Heightmaps", 2DArray) = "" {}
		_GridWidth ("Heightmap grid width", Float) = 2
		_GridHeight ("Heightmap grid height", Float) = 2
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
            #pragma target 5.0
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            UNITY_DECLARE_TEX2DARRAY(_HeightMaps);
            float _GridWidth;
            float _GridHeight;

            float _Offset;
            
            float3 toHeightMapUv(float2 uv) {
                float2 mapSize = float2(_GridWidth, _GridHeight);
                
                // The UVs of the standard Unity sphere are upside down for our purposes where 0 = top
                float2 flippedUv = float2(uv.x, 1 - uv.y);
                
                // Figure out which grid square this UV falls under so we can figure out which height map
                // to use
                int2 mapCoords = floor(flippedUv * mapSize);

                // Convert grid coordinates into an index into the array of height maps
                float mapIndex = (_GridWidth * mapCoords.y) + mapCoords.x;
                
                // Adjust our original UV coordinates so they're relative to the section of the
                // sphere we're dealing with
                float2 relativeUv = (uv - mapCoords) * mapSize;

                return float3(relativeUv, mapIndex);
            }

            v2f vert(appdata v) {
                float3 heightUv = toHeightMapUv(v.texcoord.xy);
                fixed4 height = UNITY_SAMPLE_TEX2DARRAY_LOD(_HeightMaps, heightUv, 0);
                float4 vertex = v.vertex + (float4(v.normal.xyz, 0) * height.r * _Offset);

                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

                return o;
            }
            
            fixed4 frag(v2f IN) : SV_Target {
                v2f o;
                float3 uv = toHeightMapUv(IN.uv);
                fixed4 col = UNITY_SAMPLE_TEX2DARRAY(_HeightMaps, uv);
                return col;
            }
            ENDCG
        }
	}
}
