Shader "Planet Test/Lit planet shader" {
	Properties {
		_MainTex ("Ground texture", 2D) = "white" {}
		_SnowTex ("Snow texture", 2D) = "white" {}
		_SnowHeight ("Snow height", Range(0, 1)) = 0.8
		_SnowEdgeWidth ("Snow edge width", Range(0, 0.1)) = 0.01
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_NormalMap ("Normal map", 2D) = "white" {}
		_HeightMap ("Height map", 2D) = "white" {}
		_HeightMapMidpoint ("Heightmap colour midpoint", Range(0, 1)) = 0.5
		_Offset ("Offset amount", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SnowTex;
        sampler2D _HeightMap;
        sampler2D _NormalMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMap;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

        float _Offset;
        float _HeightMapMidpoint;
        
        float _SnowHeight;
        float _SnowEdgeWidth;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
            
        void vert(inout appdata_full v) {
            fixed4 height = tex2Dlod(_HeightMap, float4(v.texcoord.xy, 0, 0));
            v.vertex.xyz += v.normal * (height.r - _HeightMapMidpoint) * _Offset;
        }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 surface = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 height = tex2D(_HeightMap, IN.uv_MainTex);
			fixed4 snow = tex2D(_SnowTex, IN.uv_MainTex);
			
			float snowBlend = smoothstep(_SnowHeight, 1, height.r);
			
			o.Albedo = (surface.rgb * (1 - snowBlend)) + (snow.rgb * snowBlend);

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
//			o.Alpha = c.a;
            o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
