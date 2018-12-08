Shader "Hidden/NodeEditor/Texture/PassFilterNodeShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		[Enum(High,0,Low,1,Range,2)] _PassMode ("Pass mode", Float) = 0
		_Min ("Min", Float) = 0
		_Max ("Max", Float) = 1
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
			
			static const float MODE_HIGH = 0;
			static const float MODE_LOW = 1;
			static const float MODE_RANGE = 2;
			static const fixed4 ERROR_COLOUR = fixed4(255, 0, 0, 1);

			sampler2D _MainTex;
			int _PassMode = 0;
			float _Min;
			float _Max;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 minimum = fixed4(_Min, _Min, _Min, 1);
				fixed4 maximum = fixed4(_Max, _Max, _Max, 1);

                if (_PassMode == MODE_HIGH) {
                    return max(minimum, col);
                } else if (_PassMode == MODE_LOW) {
                    return min(maximum, col);
                } else if (_PassMode == MODE_RANGE) {
                    return max(minimum, min(maximum, col));
                }

				return ERROR_COLOUR;
			}
			ENDCG
		}
	}
}
