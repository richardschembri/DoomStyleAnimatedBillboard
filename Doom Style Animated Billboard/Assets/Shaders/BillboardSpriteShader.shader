// Shader code developed by: Risthart - https://github.com/Risthart
Shader "Custom/BillboardSpriteShader" {
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_ScaleX("Scale X", Float) = 1.0
		_ScaleY("Scale Y", Float) = 1.0
		_OffsetY("Offset Y", Float) = 0.0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
			"DisableBatching" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				uniform float _ScaleX;
				uniform float _ScaleY;
				uniform float _OffsetY;

				appdata_img vert(appdata_img IN)
				{
					appdata_img OUT;
					float4 model = mul(UNITY_MATRIX_M, float4(0, (IN.vertex.y + _OffsetY) * _ScaleY, 0.0, 1.0));
					float4 view = mul(UNITY_MATRIX_V, model);
					float4 proj = mul(UNITY_MATRIX_P, view
						+ float4(IN.vertex.x, 0.0, 0.0, 0.0)
						* float4(_ScaleX, 1.0, 1.0, 1.0));
					OUT.vertex = proj;

					OUT.texcoord = IN.texcoord;

					return OUT;
				}

				sampler2D _MainTex;
				fixed4 frag(appdata_img IN) : COLOR
				{
					fixed4 color = tex2D(_MainTex, IN.texcoord);
					color.rgb *= color.a;

					return color;
				}
			ENDCG
		}
	}
}
