// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Blend" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("MainTex", any) = "" {}
		_BumpMap ("BumpMap", any) = "" {}
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
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _BumpMap;

			uniform float4 _MainTex_ST;
			uniform float4 _BumpMap_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			float4 frag (v2f i) : COLOR
			{
				float4 Temp =  tex2D(_BumpMap, i.texcoord);
				Temp.a = clamp(Temp.r  + _Time.y * 0.75f, 0.0, 1.0);
				return tex2D(_MainTex, i.texcoord) * Temp.a;
			}
			ENDCG 
		}
	} 	
}