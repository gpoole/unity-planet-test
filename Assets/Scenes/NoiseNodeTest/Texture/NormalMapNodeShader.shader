Shader "Hidden/NodeEditor/Texture/NormalMapNodeShader"
{
	Properties
	{
		_MainTex ("Heightmap", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
            #include "NodeShader.cginc"

			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 size = float2(2.0,0.0);
                int3 off = int3(-1,0,1);

                float s11 = tex2D(_MainTex, i.uv).r;
                float s01 = tex2D(_MainTex, i.uv + off.xy).r;
                float s21 = tex2D(_MainTex, i.uv + off.zy).r;
                float s10 = tex2D(_MainTex, i.uv + off.yx).r;
                float s12 = tex2D(_MainTex, i.uv + off.yz).r;
                float3 va = normalize(float3(size.xy, s21 - s01));
                float3 vb = normalize(float3(size.yx, s12 - s10));
                float4 normal = float4(cross(va, vb), s11);

				return normal;
			}
			ENDCG
		}
	}
}
