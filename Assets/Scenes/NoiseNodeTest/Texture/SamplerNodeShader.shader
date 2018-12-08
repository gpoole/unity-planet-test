Shader "Hidden/NodeEditor/Texture/SamplerNodeShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SampleFrom ("Sample from", 2D) = "white" {}
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
            sampler2D _SampleFrom;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 source = tex2D(_MainTex, i.uv);
				
				fixed4 col = fixed4(0, 0, 0, 1);
				col.r = tex2D(_SampleFrom, float2(source.r, 0)).r;
				col.g = tex2D(_SampleFrom, float2(source.g, 0.5)).g;
				col.b = tex2D(_SampleFrom, float2(source.b, 1)).b;

				return col;
			}
			ENDCG
		}
	}
}
