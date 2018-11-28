Shader "Test/Sample blit shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BrushDrawing ("Brush drawing enabled", Int) = 0
		_BrushSize ("Brush size", Range(0, 1)) = 1
		_BrushColor ("Brush color", Color) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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

			sampler2D _MainTex;
			sampler2D _BrushTex;
			float _BrushSize;
			int _BrushDrawing;
			fixed4 _BrushColor;
			float4 _MainTex_ST;
			float _BrushPosition[2];
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float2 brushPos = float2(_BrushPosition[0], _BrushPosition[1]);
//				float dist = min(min(distance(i.uv.xy, brushPos), distance(i.uv.xy + float2(1, 0), brushPos)), distance(i.uv.xy - float2(1, 0), brushPos));
				float alpha = _BrushDrawing * step(dist, _BrushSize) * _BrushColor.a;
				col.rgb = (col.rgb * (1 - alpha)) + (_BrushColor.rgb * alpha);
				
				return col;
			}
			ENDCG
		}
	}
}
