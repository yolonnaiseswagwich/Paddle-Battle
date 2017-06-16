// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CellShader"{
	Properties {
		_Color1 ("Color1", Color) = (1,1,1,1)
		_Color2 ("Color2", Color) = (1,1,1,1)
		_Color3 ("Color3", Color) = (1,1,1,1)
		_MyVar ("MyVar", Vector) =   (0,0,0,0)
		_MainTex ("Texture", any) = "" {}
	}
	SubShader {

		Tags { "ForceSupported" = "True" "RenderType"="Overlay" } 
		
		Lighting Off 
		Blend SrcAlpha OneMinusSrcAlpha 
		Cull Off 
		ZWrite Off 
		Fog { Mode Off } 
		ZTest Always 
		
		Pass {	
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			uniform float4 MyVar;
			uniform float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			half4 frag (v2f i) : COLOR
			{
                const half pi = 3.14159;
				half x,y,r;
				x = (8.1) * (i.texcoord.x - (0.5f + MyVar.x * 0.013));
				y = (8.1) * (i.texcoord.y - (0.5f + MyVar.y * 0.022));
				r = x*x + y*y; 
				half k = 0.875 + (MyVar.z * x * y) * 0.125f;
				if (r <= k*k)
					return half4(1.0,1.0 * r,0.0,1.0f);
				x = (2.1) * (i.texcoord.x - 0.5f);
				y = (2.1) * (i.texcoord.y - 0.5f);
				r = x * x + y * y;
				k = 0.975 + (MyVar.w * x * y) * 0.1f;
				if (r <= k*k)
					return half4(1.0,1.0f * (0.7 - r * 0.7),0.0,1.0f * (0.3 + r * 0.7));
				return half4(0.0,0.0,0.0,0.0f);
				//return tex2D(_MainTex, i.texcoord);
			}
			ENDCG 
		}
	} 	
}		