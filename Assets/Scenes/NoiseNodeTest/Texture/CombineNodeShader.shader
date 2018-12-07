Shader "Hidden/NodeEditor/Texture/CombineNodeShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_CombineTex ("Combine with", 2D) = "white" {}
		[Enum(Add,0,Subtract,1,Multiply,2,Divide,3)] _CombineMode ("Combine mode", Float) = 0
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

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			static const float MODE_ADD = 0;
			static const float MODE_SUBTRACT = 1;
			static const float MODE_MULTIPLY = 2;
			static const float MODE_DIVIDE = 3;
			static const fixed3 ERROR_COLOUR = fixed3(255, 0, 0);

			sampler2D _MainTex;
			sampler2D _CombineTex;
			int _CombineMode = 0;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 a = tex2D(_MainTex, i.uv);
				fixed4 b = tex2D(_CombineTex, i.uv);
				fixed3 col = ERROR_COLOUR;
				
				if (_CombineMode == MODE_ADD) {
				    col = a.rgb + b.rgb;
				} else if (_CombineMode == MODE_SUBTRACT) {
				    col = a.rgb - b.rgb;
				} else if (_CombineMode == MODE_MULTIPLY) {
				    col = a.rgb * b.rgb;
				} else if (_CombineMode == MODE_DIVIDE) {
				    col = a.rgb / b.rgb;
				}

				return fixed4(col, 1);
			}
			ENDCG
		}
	}
}
