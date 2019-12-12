Shader "Custom/TestShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_Gloss ("Gloss", Float ) = 1
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_Ramp ("Ramp", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

		Pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			sampler2D _MainTex;
			sampler2D _Ramp;

			struct Input
			{
				float2 uv_MainTex;
			};

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv0 : TEXCOORD0;
			};

			struct VertexOutput
			{
				float4 clipSpacePos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float worldPos : TEXCOORD2;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			float _Gloss;

			VertexOutput vert(VertexInput v) {
				VertexOutput o;
				o.uv0 = v.uv0;
				o.normal = v.normal;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.clipSpacePos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float4 frag (VertexOutput o) : SV_Target {
		
				float2 uv = o.uv0;
				float3 normal = normalize(o.normal);

				float3 lightDir = _WorldSpaceLightPos0.xyz;
				float3 lightColour = _LightColor0.rgb;

				float lightFallOff = max(0, dot(lightDir, normal));
				lightFallOff = step(0.5, lightFallOff);
				float3 directionDiffuseLight = lightColour * lightFallOff;

				float3 ambientLight = float3(0.1, 0.1, 0.1);

				float3 camPos = _WorldSpaceCameraPos;
				float3 fragToCam = camPos - o.worldPos;
				float3 viewDir = normalize(fragToCam);
				float3 viewReflect = reflect(-viewDir, normal);
				float specularFalloff = max(0, dot(viewReflect, lightDir));
				specularFalloff = pow(specularFalloff, _Gloss);
				float3 directSpecular = specularFalloff * lightColour;

				float3 diffuseLight = ambientLight + directionDiffuseLight;
				float3 finalSurfaceColour = diffuseLight * _Color.rgb + directSpecular;

				return float4(finalSurfaceColour, 0);
			}
			ENDCG
		}
	}
}
