Shader "Custom/Mandelbrot" 
{	
	SubShader 
	{
		Pass 
		{
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            float4 frag(v2f_img i) : SV_Target 
			{
                float2 c;
                float2 z = float2((sin(_Time.y * 2.31) * 0.14f), (sin(_Time.y * 1.73) * 0.14f));
                c.x = ((1.0-i.uv.x)*4.0) - 2.75;
                c.y = (i.uv.y*2.5)-1.25;
                float iteration = 0.0;
                const float _MaxIter = 20.0;
                const float PI = 3.14159;
				for ( iteration = 0.0; iteration < _MaxIter; iteration += 1.0) 
				{
                    float x = ((z.x * z.x - z.y * z.y) + c.x);
					float y = ((z.y * z.x + z.x * z.y) + c.y);

					if((x * x + y * y) > 4.0f) break;
					z.x = x;
					z.y = y;
                }
                float val = fmod((iteration/_MaxIter),1.0);
                float4 color;
                float t1,t2,t3;
				t1 = _Time.y * 0.61f;
				t2 = _Time.y * 1.21f;
				t3 = _Time.y * 0.87f;
				color.r = sin(t1) - 0.345f - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,sin(t1) - 0.345f) + -sin(t1) - 0.345f - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,-sin(t1) - 0.345f);
                color.g = cos(t2) - 0.345f - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,cos(t2) - 0.345f) + -cos(t2) - 0.345f - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,-cos(t2) - 0.345f);
                color.b = sin(t3) - 0.345f - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,sin(t3) - 0.345f) + -cos(t3) - 0.345f - clamp((3.0*abs(fmod(2.0*val,1.0)-0.5)) ,0.0,-cos(t3) - 0.345f);
                color.a = 1.0;

                return color;
            }
            ENDCG
        }
    }
}