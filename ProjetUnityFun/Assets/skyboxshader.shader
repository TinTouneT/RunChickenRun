Shader "Cg shader for screen overlays" {
   Properties {
      _MainTex ("Texture", Rect) = "white" {}
      _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
      _X ("X", Float) = 0.0
      _Y ("Y", Float) = 0.0
      _axe ("axe", Float) = 0.0
      test ("test",Float) = 0.0
      _Width ("Width", Float) = 128
      _Height ("Height", Float) = 128
      
   }
   SubShader {
      Tags { "Queue" = "Background" } // render after everything else
 	
      Pass {
         Blend SrcAlpha OneMinusSrcAlpha // use alpha blending
         Zwrite Off
 
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 		
         #include "UnityCG.cginc" 
           // defines float4 _ScreenParams with x = width;  
           // y = height; z = 1 + 1.0/width; w = 1 + 1.0/height
 
         // User-specified uniforms
         uniform sampler2D _MainTex;
         uniform float4 _Color;
         uniform float _X;
         uniform float _Y;
         uniform float _Width;
         uniform float _Height;
         uniform float _axe;
         uniform float test;
         
 
         struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 tex : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float2 rasterPosition = float2(
               _X + _ScreenParams.x / 2.0 
               + _Width * (input.vertex.x + 0.5),
               _Y + _ScreenParams.y / 2.0 
               + _Height * (input.vertex.y + 0.5));
            output.pos = float4(
               2.0 * rasterPosition.x / _ScreenParams.x - 1.0,
               2.0 * rasterPosition.y / _ScreenParams.y - 1.0,
               0.0, // near plane is at -1.0 or at 0.0
               1.0);
 
            output.tex = float4(-input.vertex.x, 
               input.vertex.y + 0.5, 0.0, 0.0);
               // for a cube, vertex.x and vertex.y 
               // are -0.5 or 0.5
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         		input.tex.y = input.tex.y *0.5;
          		input.tex.x = input.tex.x;
			    float2 p = -1.0 + (_ScreenParams.x*2) * input.tex.xy / _ScreenParams.xy;
			    float2 uv;
			    
			    p.x = (0.3+test)/(p.x+1-(_axe));
			   // p.y = p.y*test;
				//return float4(_axe,_axe,_axe,1);
				/*p.y += 0.5*cos(_Time.y*1.64);*/
			    float r = sqrt( dot(p,p) );
			    float a = _Time.y - p.y/p.x + 0.1*cos(r*(-5.0+2.0*sin(_Time.y/4.0))) + -5.0*sin(_Time.y/7.0);
			    
			    float s = (smoothstep( 0.7, 0.0, 1.5+1.4*cos(14.0*-a)*sin(_Time.y/6.0) ))+1.0;
			    uv.x = _Time.y + 0.2*( r + 1.8*s );
			    uv.x = uv.x -0.25;
			   // uv.x = uv.x+1.5;
			    uv.y = -_Time.y + sin(_Time.y/2.575) + 10.0*a/3.1416;
			    float w = (0.5 + 0.5*s)*r*r;
			    float3 col = tex2D(_MainTex,uv).xyz;
				col.x *= 1.0-0.5*sin(0.5*_Time.y);
				col.y *= 1.0+0.5*cos(0.7*_Time.y);
				col.z *= 1.0+0.5*sin(1.1*_Time.y+1.5);

			    return float4(col*w*0.6+(test/2.5),1.0);
         }
 
         ENDCG
      }
      	
   }
}