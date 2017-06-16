// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CircleForce" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture", any) = "" {}
	}

		SubShader{

			Tags{ "ForceSupported" = "True" "RenderType" = "Overlay" }

			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZWrite Off
			Fog{ Mode Off }
			ZTest Always

			Pass{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest

				#include "UnityCG.cginc"

				struct appdata_t {
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : POSITION;
					float4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

			sampler2D _MainTex;

			uniform float4 _MainTex_ST;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				const float pi = 3.14159;
				float x,y,r;
				float4 color;
				x = (1.5) * (i.texcoord.x - 0.5);
				y = (1.5) * (i.texcoord.y - 0.5);
				r = sqrt(x * x + y * y);
				if (r <= 0.75)
				{
					half d,x2,y2,x3,y3;
					d = r ? asin(r) / r : 0.0;
					x2 = d * x;
					y2 = d * y;
					x3 = fmod(x2 / (0.55 * pi) + 0.5, 1.0);
					y3 = fmod(y2 / (0.55 * pi) + 0.5, 1.0);
					color = tex2D(_MainTex, float2(x3,y3));
					return color;
				}
				return float4(0.0,0.0,0.0,0.0);
			}
			ENDCG
		}
	}
}