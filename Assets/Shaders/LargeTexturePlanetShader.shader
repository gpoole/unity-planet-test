Shader "Planet Test/Large texture surface shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_HeightMaps ("Heightmaps", 2DArray) = "" {}
		_GridWidth ("Heightmap grid width", Float) = 2
		_GridHeight ("Heightmap grid height", Float) = 2
		_Offset ("Offset amount", Float) = 1
		_TessellationUniform ("Tessellation Uniform", Range(1, 64)) = 1
		_WireframeColor ("Wireframe Color", Color) = (0, 0, 0)
		_WireframeSmoothing ("Wireframe Smoothing", Range(0, 10)) = 1
		_WireframeThickness ("Wireframe Thickness", Range(0, 10)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex tesselationVert
            #pragma fragment frag
            #pragma hull hull
            #pragma domain domain
            #pragma target 5.0
            #include "UnityCG.cginc"

            struct VertexData {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 barycentricCoordinates : TEXCOORD1;
            };

            struct FragmentData {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 barycentricCoordinates : TEXCOORD1;
            };

            struct TessellationControlPoint {
                float4 vertex : INTERNALTESSPOS;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct TessellationFactors {
                float edge[3] : SV_TessFactor;
                float inside : SV_InsideTessFactor;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            UNITY_DECLARE_TEX2DARRAY(_HeightMaps);
            float _GridWidth;
            float _GridHeight;

            float _Offset;
            float _TessellationUniform;

            float _WireframeThickness;
            float _WireframeSmoothing;
            fixed4 _WireframeColor;
            
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

            FragmentData vert(VertexData vertexData) {
                float3 heightUv = toHeightMapUv(vertexData.uv.xy);
                fixed4 height = UNITY_SAMPLE_TEX2DARRAY_LOD(_HeightMaps, heightUv, 0);
                float4 vertex = vertexData.vertex + (float4(vertexData.normal.xyz, 0) * (height.r - 0.5) * _Offset);

                FragmentData f;
                f.vertex = UnityObjectToClipPos(vertex);
                f.uv = TRANSFORM_TEX(vertexData.uv, _MainTex);
                f.barycentricCoordinates = vertexData.barycentricCoordinates;

                return f;
            }
            
            fixed4 frag(FragmentData fragmentData) : SV_Target {
                float3 uv = toHeightMapUv(fragmentData.uv);
                fixed4 textureColor = UNITY_SAMPLE_TEX2DARRAY(_HeightMaps, uv);

                float3 barys;
                barys.xy = fragmentData.barycentricCoordinates;
                barys.z = 1 - barys.x - barys.y;
                float3 deltas = fwidth(barys);
                float3 smoothing = deltas * _WireframeSmoothing;
                float3 thickness = deltas * _WireframeThickness;
                barys = smoothstep(thickness, thickness + smoothing, barys);
                float minBary = min(barys.x, min(barys.y, barys.z));
                textureColor = lerp(_WireframeColor, textureColor, minBary);

                return textureColor;
            }

            TessellationFactors patchConstant(InputPatch<TessellationControlPoint, 3> patch) {
                float3 p0 = mul(unity_ObjectToWorld, patch[0].vertex).xyz;
                float3 p1 = mul(unity_ObjectToWorld, patch[1].vertex).xyz;
                float3 p2 = mul(unity_ObjectToWorld, patch[2].vertex).xyz;
                TessellationFactors f;
                f.edge[0] = _TessellationUniform;
                f.edge[1] = _TessellationUniform;
                f.edge[2] = _TessellationUniform;
                f.inside = (f.edge[0] + f.edge[1] + f.edge[2]) * 0.333;
                return f;
            }

            [UNITY_domain("tri")]
            [UNITY_outputcontrolpoints(3)]
            [UNITY_outputtopology("triangle_cw")]
            [UNITY_partitioning("fractional_odd")]
            [UNITY_patchconstantfunc("patchConstant")]
            TessellationControlPoint hull(InputPatch<TessellationControlPoint, 3> vertexData, uint index : SV_OutputControlPointID) {
                return vertexData[index];
            }

            [UNITY_domain("tri")]
            FragmentData domain(
                TessellationFactors factors,
                OutputPatch<TessellationControlPoint, 3> patch,
                float3 barycentricCoordinates : SV_DomainLocation
            ) {
                VertexData data;

                #define DOMAIN_INTERPOLATE(fieldName) data.fieldName = \
                    patch[0].fieldName * barycentricCoordinates.x + \
                    patch[1].fieldName * barycentricCoordinates.y + \
                    patch[2].fieldName * barycentricCoordinates.z;

                DOMAIN_INTERPOLATE(vertex)
                DOMAIN_INTERPOLATE(normal)
                DOMAIN_INTERPOLATE(tangent)
                DOMAIN_INTERPOLATE(uv)
                
                data.barycentricCoordinates = barycentricCoordinates;

                return vert(data);
            }

            TessellationControlPoint tesselationVert(VertexData v) {
                TessellationControlPoint p;
                p.vertex = v.vertex;
                p.normal = v.normal;
                p.tangent = v.tangent;
                p.uv = v.uv;
                return p;
            }
            ENDCG
        }
	}
}
