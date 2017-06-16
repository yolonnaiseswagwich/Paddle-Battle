
Shader "Custom/Julia" 
{	
		Properties {
			_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MyVar ("MyVar", Vector) =   (0,0,0,0)
		_MyVarb ("MyVarb", Vector) =   (0,0,0,0)
		}
	SubShader 
	{	
		Tags { "ForceSupported" = "True" "RenderType"="Overlay" } 
		
		Lighting Off 
		Blend SrcAlpha OneMinusSrcAlpha 
		Cull Off 
		ZWrite Off 
		Fog { Mode Off } 
		ZTest Always 
		Pass 
		{
            CGPROGRAM
			#pragma target 3.0
            #pragma vertex vert_img
            #pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			sampler2D _MainTex;
			uniform float4 MyVar;
			uniform float4 MyVarb;
			uniform float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = v.vertex;
				o.uv = v.texcoord;
				return o;
			}

			float4 frag (v2f i) : COLOR
			{
                float2 z;
				z.x = 3.0 * (i.uv.x - 0.5);
                z.y = 2.0 * (i.uv.y - 0.5);
				float iteration;
				for ( iteration = 0.0; iteration < 17.0f; iteration += 1.0) 
				{
                    float x = ((z.x * z.x - z.y * z.y) + MyVar.x);
					float y = ((z.y * z.x + z.x * z.y) + MyVar.y);

					if((x * x + y * y) > 4.0f) break;
					z.x = x;
					z.y = y;
                }
				for ( iteration = 0.0; iteration < 17.0f; iteration += 1.0) 
				{
                    float x = ((z.x * z.x - z.y * z.y) +  MyVar.x);
					float y = ((z.y * z.x + z.x * z.y) +  MyVar.y);

					if((x * x + y * y) > 4.0f) break;
					z.x = -x;
					z.y = -y;
                }
                float val = fmod((iteration/ 17.0f),1.0);
                float4 color;
				color.r = MyVarb.x - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,MyVarb.x);
                color.g = MyVarb.y - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,MyVarb.y);
                color.b = MyVarb.z - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,MyVarb.z);
				color.b += MyVarb.w - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,MyVarb.w);
                color.a = 1.0;
                return color;
            }
            ENDCG
        }
    }
}