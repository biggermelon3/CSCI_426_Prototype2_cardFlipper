// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TAPro/DragonShell"
{
	Properties
	{
		[HDR]_Color0("Color 0", Color) = (1,1,1,0)
		_ShellHeight("ShellHeight", Range( 0 , 1)) = 0
		_Num("Num", Float) = 1
		_AlphaClip("AlphaClip", Float) = 0
		_WhiteRange("WhiteRange", Range( 0 , 1)) = 0
		_NoiseMap("NoiseMap", 2D) = "white" {}
		_Distort("Distort", Float) = 0
		_DistortUVScale("DistortUVScale", Vector) = (0,0,0,0)
		_Alpha("Alpha", Range( 0 , 1)) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Front
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct MeshData
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct V2FData
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float _ShellHeight;
			uniform float4 _Color0;
			uniform sampler2D _NoiseMap;
			uniform float2 _DistortUVScale;
			uniform float _Distort;
			uniform float _Num;
			uniform float _AlphaClip;
			uniform float _WhiteRange;
			uniform float _Alpha;

			
			V2FData vert ( MeshData v )
			{
				V2FData o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( v.ase_normal * _ShellHeight );
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (V2FData i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 texCoord16 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 texCoord29 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_23_0 = ( sin( ( ( ( ( texCoord16.x + texCoord16.y ) + ( tex2D( _NoiseMap, ( texCoord29 * _DistortUVScale ) ).r * _Distort ) ) * _Num ) + _Time.y ) ) - _AlphaClip );
				clip( temp_output_23_0 );
				float4 appendResult43 = (float4(( _Color0 + ( step( 0.0 , temp_output_23_0 ) * step( temp_output_23_0 , _WhiteRange ) ) ).rgb , _Alpha));
				
				
				finalColor = appendResult43;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18800
79.42857;282.2857;2194.286;1160.714;-24.49597;-43.5907;1;True;False
Node;AmplifyShaderEditor.Vector2Node;33;-1081.116,887.8931;Inherit;False;Property;_DistortUVScale;DistortUVScale;7;0;Create;True;0;0;0;False;0;False;0,0;2,5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-1101.116,776.8931;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-846.1163,788.8931;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;25;-722.1163,751.8931;Inherit;True;Property;_NoiseMap;NoiseMap;5;0;Create;True;0;0;0;False;0;False;-1;None;479a00044c2fa184a88c3e0bfe20b738;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-532.1163,983.8931;Inherit;False;Property;_Distort;Distort;6;0;Create;True;0;0;0;False;0;False;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-480.959,569.4242;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-310.1163,817.8931;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-181.6594,585.8241;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;35.88367,618.8931;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-15.25928,753.8241;Inherit;False;Property;_Num;Num;2;0;Create;True;0;0;0;False;0;False;1;57.58;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;166.7407,646.8241;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;41;-29.25244,887.4333;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;302.7476,689.4333;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;438.7874,687.2744;Inherit;False;Property;_AlphaClip;AlphaClip;3;0;Create;True;0;0;0;False;0;False;0;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;15;432.7407,601.8241;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;472.6285,817.2266;Inherit;False;Property;_WhiteRange;WhiteRange;4;0;Create;True;0;0;0;False;0;False;0;0.119;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;23;594.7874,608.2744;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;35;846.6285,630.2266;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;36;856.6285,769.2266;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;681.1656,262.247;Inherit;False;Property;_Color0;Color 0;0;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;0,0.5094167,0.8312753,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;1007.628,683.2266;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;39;968.6285,301.2266;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClipNode;22;1134.787,312.2744;Inherit;False;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;44;1351.172,511.0008;Inherit;False;Property;_Alpha;Alpha;8;0;Create;True;0;0;0;False;0;False;0;0.05;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;1333.72,820.502;Inherit;False;Property;_ShellHeight;ShellHeight;1;0;Create;True;0;0;0;False;0;False;0;0.03;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;9;1420.72,670.5027;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;43;1681.172,374.0008;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;1647.719,673.5027;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1935.698,378.1461;Float;False;True;-1;2;ASEMaterialInspector;100;1;TAPro/DragonShell;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;1;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;30;0;29;0
WireConnection;30;1;33;0
WireConnection;25;1;30;0
WireConnection;27;0;25;1
WireConnection;27;1;28;0
WireConnection;17;0;16;1
WireConnection;17;1;16;2
WireConnection;26;0;17;0
WireConnection;26;1;27;0
WireConnection;18;0;26;0
WireConnection;18;1;19;0
WireConnection;40;0;18;0
WireConnection;40;1;41;0
WireConnection;15;0;40;0
WireConnection;23;0;15;0
WireConnection;23;1;24;0
WireConnection;35;1;23;0
WireConnection;36;0;23;0
WireConnection;36;1;37;0
WireConnection;38;0;35;0
WireConnection;38;1;36;0
WireConnection;39;0;2;0
WireConnection;39;1;38;0
WireConnection;22;0;39;0
WireConnection;22;1;23;0
WireConnection;43;0;22;0
WireConnection;43;3;44;0
WireConnection;10;0;9;0
WireConnection;10;1;11;0
WireConnection;0;0;43;0
WireConnection;0;1;10;0
ASEEND*/
//CHKSM=0798C1AE53EAB502716E642F27EBEA9371254BC3