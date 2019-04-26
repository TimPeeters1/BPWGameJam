Shader "Custom/ToonShaderHicham" 
 {
	Properties 
	{
	_RampTex ("Ramp", 2D) = "white" {}
	_MainTex("Mask",2D) = "black" {}
	_baseColor("Base Color", Color) = (0,0,0,1)
	_largeDet("Color 02", Color) = (0,0,0,1)
	_midDet("Color 03", Color) = (0,0,0,1)
	_smDet("Color 04", Color) = (0,0,0,1)
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }		
		LOD 200
		CGPROGRAM
		#pragma surface surf Toon

		 sampler2D _RampTex;

		half4 LightingToon (SurfaceOutput s, fixed3
		lightDir, fixed atten) 
		{
			half NdotL = dot (s.Normal, lightDir);
			NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten;
			c.a = s.Alpha;

			return c;
		}
		 struct Input {
         float2 uv_MainTex;
      };

      sampler2D _MainTex,_MaskTex;
	  fixed4 _baseColor, _largeDet, _midDet, _smDet;

		void surf(Input IN, inout SurfaceOutput o) 
		{

		float3 mask = tex2D(_MainTex, IN.uv_MainTex).rgb;
	

		float3 C = _baseColor.rgb;
		float3 L = _largeDet * mask.r;
		float3 M = _midDet * mask.g;
		float3 S = _smDet * mask.b;

		float3 P1 = lerp(C, L, mask.r);
		float3 P2 = lerp(P1,M, mask.g);

		 o.Albedo = lerp(P2,S,mask.b);
		}

		ENDCG
	} 
	FallBack "Diffuse"
}
