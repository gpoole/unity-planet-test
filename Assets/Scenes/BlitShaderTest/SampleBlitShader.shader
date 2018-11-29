Shader "Test/Sample blit shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BrushSize ("Brush size", Range(0, 1)) = 1
		_BrushAlpha ("Brush alpha", Range(0, 1)) = 1
		_BrushDirection ("Brush direction", Int) = 0
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
				float2 uv_Brush : TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv_Brush : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			texture2D _BrushTex;
			SamplerState brush_point_clamp_sampler;
			float4 _BrushTex_ST;
			float _BrushSize;
			int _BrushDirection;
			float _BrushAlpha;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//				o.uv_Brush = TRANSFORM_TEX(v.uv_Brush, _BrushTex);
                o.uv_Brush = 0.5 + ((v.uv_Brush.xy - _BrushTex_ST.zw) * _BrushTex_ST.xy);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 brush = _BrushTex.Sample(brush_point_clamp_sampler, i.uv_Brush);

//				float2 brushPos = float2(_BrushPosition[0], _BrushPosition[1]);
//				float dist = min(min(distance(i.uv.xy, brushPos), distance(i.uv.xy + float2(1, 0), brushPos)), distance(i.uv.xy - float2(1, 0), brushPos));
//				float alpha = _BrushDrawing * step(dist, _BrushSize) * _BrushColor.a;
//				col.rgb = (col.rgb * (1 - alpha)) + (_BrushColor.rgb * alpha);
//                col.rgb = lerp(col.rgb, _BrushColor.rgb * brush.a, _BrushDrawing * brush.a * _BrushColor.a);
                col.r += _BrushDirection * _BrushAlpha * brush.r;
				
				return col;
			}
			ENDCG
		}
	}
}
