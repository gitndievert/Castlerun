// Compiled shader for PC, Mac & Linux Standalone, uncompressed size: 209.2KB

// Skipping shader variants that would not be included into build of current scene.

Shader "Flag" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB) Trans (A)", 2D) = "white" { }
 _BumpMap ("Normalmap", 2D) = "bump" { }
}
SubShader { 
 LOD 300
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }


 // Stats for Vertex shader:
 //       d3d11 : 44 avg math (34..64)
 //    d3d11_9x : 54 avg math (44..64)
 //        d3d9 : 47 avg math (35..75)
 //      opengl : 17 avg math (16..19), 2 avg texture (2..3)
 // Stats for Fragment shader:
 //       d3d11 : 16 avg math (14..19), 2 avg texture (2..3)
 //    d3d11_9x : 14 math, 2 texture
 //        d3d9 : 22 avg math (19..25), 2 avg texture (2..3)
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  cull off
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask RGB
  GpuProgramID 21386
Program "vp" {
SubProgram "opengl " {
// Stats: 16 math, 2 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"!!GLSL
#ifdef VERTEX
uniform vec4 unity_SHAr;
uniform vec4 unity_SHAg;
uniform vec4 unity_SHAb;
uniform vec4 unity_SHBr;
uniform vec4 unity_SHBg;
uniform vec4 unity_SHBb;
uniform vec4 unity_SHC;

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec3 tmpvar_2;
  tmpvar_2 = (_Object2World * gl_Vertex).xyz;
  vec4 v_3;
  v_3.x = _World2Object[0].x;
  v_3.y = _World2Object[1].x;
  v_3.z = _World2Object[2].x;
  v_3.w = _World2Object[3].x;
  vec4 v_4;
  v_4.x = _World2Object[0].y;
  v_4.y = _World2Object[1].y;
  v_4.z = _World2Object[2].y;
  v_4.w = _World2Object[3].y;
  vec4 v_5;
  v_5.x = _World2Object[0].z;
  v_5.y = _World2Object[1].z;
  v_5.z = _World2Object[2].z;
  v_5.w = _World2Object[3].z;
  vec3 tmpvar_6;
  tmpvar_6 = normalize(((
    (v_3.xyz * gl_Normal.x)
   + 
    (v_4.xyz * gl_Normal.y)
  ) + (v_5.xyz * gl_Normal.z)));
  mat3 tmpvar_7;
  tmpvar_7[0] = _Object2World[0].xyz;
  tmpvar_7[1] = _Object2World[1].xyz;
  tmpvar_7[2] = _Object2World[2].xyz;
  vec3 tmpvar_8;
  tmpvar_8 = normalize((tmpvar_7 * TANGENT.xyz));
  vec3 tmpvar_9;
  tmpvar_9 = (((tmpvar_6.yzx * tmpvar_8.zxy) - (tmpvar_6.zxy * tmpvar_8.yzx)) * TANGENT.w);
  vec4 tmpvar_10;
  tmpvar_10.x = tmpvar_8.x;
  tmpvar_10.y = tmpvar_9.x;
  tmpvar_10.z = tmpvar_6.x;
  tmpvar_10.w = tmpvar_2.x;
  vec4 tmpvar_11;
  tmpvar_11.x = tmpvar_8.y;
  tmpvar_11.y = tmpvar_9.y;
  tmpvar_11.z = tmpvar_6.y;
  tmpvar_11.w = tmpvar_2.y;
  vec4 tmpvar_12;
  tmpvar_12.x = tmpvar_8.z;
  tmpvar_12.y = tmpvar_9.z;
  tmpvar_12.z = tmpvar_6.z;
  tmpvar_12.w = tmpvar_2.z;
  vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.xyz = tmpvar_6;
  vec3 x2_14;
  vec3 x1_15;
  x1_15.x = dot (unity_SHAr, tmpvar_13);
  x1_15.y = dot (unity_SHAg, tmpvar_13);
  x1_15.z = dot (unity_SHAb, tmpvar_13);
  vec4 tmpvar_16;
  tmpvar_16 = (tmpvar_6.xyzz * tmpvar_6.yzzx);
  x2_14.x = dot (unity_SHBr, tmpvar_16);
  x2_14.y = dot (unity_SHBg, tmpvar_16);
  x2_14.z = dot (unity_SHBb, tmpvar_16);
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_10;
  xlv_TEXCOORD2 = tmpvar_11;
  xlv_TEXCOORD3 = tmpvar_12;
  xlv_TEXCOORD4 = ((x2_14 + (unity_SHC.xyz * 
    ((tmpvar_6.x * tmpvar_6.x) - (tmpvar_6.y * tmpvar_6.y))
  )) + x1_15);
}


#endif
#ifdef FRAGMENT
uniform vec4 _WorldSpaceLightPos0;
uniform vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  worldN_1.x = dot (xlv_TEXCOORD1.xyz, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2.xyz, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3.xyz, normal_3);
  vec4 c_4;
  vec4 c_5;
  c_5.xyz = ((tmpvar_2.xyz * _LightColor0.xyz) * max (0.0, dot (worldN_1, _WorldSpaceLightPos0.xyz)));
  c_5.w = tmpvar_2.w;
  c_4.w = c_5.w;
  c_4.xyz = (c_5.xyz + (tmpvar_2.xyz * xlv_TEXCOORD4));
  gl_FragData[0] = c_4;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 45 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 18 [_BumpMap_ST]
Vector 17 [_MainTex_ST]
Vector 12 [unity_SHAb]
Vector 11 [unity_SHAg]
Vector 10 [unity_SHAr]
Vector 15 [unity_SHBb]
Vector 14 [unity_SHBg]
Vector 13 [unity_SHBr]
Vector 16 [unity_SHC]
"vs_2_0
def c19, 1, 0, 0, 0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v3, c17, c17.zwzw
mad oT0.zw, v3.xyxy, c18.xyxy, c18
dp4 oT1.w, c4, v0
dp4 oT2.w, c5, v0
dp4 oT3.w, c6, v0
mul r0.xyz, v2.y, c8
mad r0.xyz, c7, v2.x, r0
mad r0.xyz, c9, v2.z, r0
nrm r1.xyz, r0
mul r0.x, r1.y, r1.y
mad r0.x, r1.x, r1.x, -r0.x
mul r2, r1.yzzx, r1.xyzz
dp4 r3.x, c13, r2
dp4 r3.y, c14, r2
dp4 r3.z, c15, r2
mad r0.xyz, c16, r0.x, r3
mov r1.w, c19.x
dp4 r2.x, c10, r1
dp4 r2.y, c11, r1
dp4 r2.z, c12, r1
add oT4.xyz, r0, r2
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r2.xyz, r0, r1.zxyw
mad r2.xyz, r1.yzxw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov oT1.y, r2.x
mov oT1.z, r1.x
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r2.y
mov oT3.y, r2.z
mov oT2.z, r1.y
mov oT3.z, r1.z

"
}
SubProgram "d3d11 " {
// Stats: 44 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityLighting" 720
Vector 608 [unity_SHAr]
Vector 624 [unity_SHAg]
Vector 640 [unity_SHAb]
Vector 656 [unity_SHBr]
Vector 672 [unity_SHBg]
Vector 688 [unity_SHBb]
Vector 704 [unity_SHC]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
BindCB  "UnityPerDraw" 2
"vs_4_0
eefieceddjgogjgohdpkmhgnhccjdcfibjceecccabaaaaaafeajaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaapaaaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaa
ahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
haahaaaaeaaaabaanmabaaaafjaaaaaeegiocaaaaaaaaaaaamaaaaaafjaaaaae
egiocaaaabaaaaaacnaaaaaafjaaaaaeegiocaaaacaaaaaabdaaaaaafpaaaaad
pcbabaaaaaaaaaaafpaaaaadpcbabaaaabaaaaaafpaaaaadhcbabaaaacaaaaaa
fpaaaaaddcbabaaaadaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaad
pccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaagfaaaaadpccabaaaadaaaaaa
gfaaaaadpccabaaaaeaaaaaagfaaaaadhccabaaaafaaaaaagiaaaaacaeaaaaaa
diaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaacaaaaaaabaaaaaa
dcaaaaakpcaabaaaaaaaaaaaegiocaaaacaaaaaaaaaaaaaaagbabaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaacaaaaaaacaaaaaa
kgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaa
acaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaaldccabaaa
abaaaaaaegbabaaaadaaaaaaegiacaaaaaaaaaaaakaaaaaaogikcaaaaaaaaaaa
akaaaaaadcaaaaalmccabaaaabaaaaaaagbebaaaadaaaaaaagiecaaaaaaaaaaa
alaaaaaakgiocaaaaaaaaaaaalaaaaaadiaaaaaihcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiccaaaacaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaaegiccaaa
acaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaakhcaabaaa
aaaaaaaaegiccaaaacaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaaaaaaaaaa
dcaaaaakhcaabaaaaaaaaaaaegiccaaaacaaaaaaapaaaaaapgbpbaaaaaaaaaaa
egacbaaaaaaaaaaadgaaaaaficcabaaaacaaaaaaakaabaaaaaaaaaaadiaaaaai
hcaabaaaabaaaaaafgbfbaaaabaaaaaajgiecaaaacaaaaaaanaaaaaadcaaaaak
hcaabaaaabaaaaaajgiecaaaacaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaa
abaaaaaadcaaaaakhcaabaaaabaaaaaajgiecaaaacaaaaaaaoaaaaaakgbkbaaa
abaaaaaaegacbaaaabaaaaaabaaaaaahbcaabaaaaaaaaaaaegacbaaaabaaaaaa
egacbaaaabaaaaaaeeaaaaafbcaabaaaaaaaaaaaakaabaaaaaaaaaaadiaaaaah
hcaabaaaabaaaaaaagaabaaaaaaaaaaaegacbaaaabaaaaaadiaaaaaibcaabaaa
acaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaabaaaaaaadiaaaaaiccaabaaa
acaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaabbaaaaaadiaaaaaiecaabaaa
acaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaabcaaaaaadiaaaaaibcaabaaa
adaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaabaaaaaaadiaaaaaiccaabaaa
adaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaabbaaaaaadiaaaaaiecaabaaa
adaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaabcaaaaaaaaaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaaegacbaaaadaaaaaadiaaaaaibcaabaaaadaaaaaa
ckbabaaaacaaaaaackiacaaaacaaaaaabaaaaaaadiaaaaaiccaabaaaadaaaaaa
ckbabaaaacaaaaaackiacaaaacaaaaaabbaaaaaadiaaaaaiecaabaaaadaaaaaa
ckbabaaaacaaaaaackiacaaaacaaaaaabcaaaaaaaaaaaaahhcaabaaaacaaaaaa
egacbaaaacaaaaaaegacbaaaadaaaaaabaaaaaahbcaabaaaaaaaaaaaegacbaaa
acaaaaaaegacbaaaacaaaaaaeeaaaaafbcaabaaaaaaaaaaaakaabaaaaaaaaaaa
diaaaaahhcaabaaaacaaaaaaagaabaaaaaaaaaaaegacbaaaacaaaaaadiaaaaah
hcaabaaaadaaaaaaegacbaaaabaaaaaacgajbaaaacaaaaaadcaaaaakhcaabaaa
adaaaaaajgaebaaaacaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaadaaaaaa
diaaaaahhcaabaaaadaaaaaaegacbaaaadaaaaaapgbpbaaaabaaaaaadgaaaaaf
cccabaaaacaaaaaaakaabaaaadaaaaaadgaaaaafbccabaaaacaaaaaackaabaaa
abaaaaaadgaaaaafeccabaaaacaaaaaaakaabaaaacaaaaaadgaaaaafbccabaaa
adaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaaaeaaaaaabkaabaaaabaaaaaa
dgaaaaaficcabaaaadaaaaaabkaabaaaaaaaaaaadgaaaaaficcabaaaaeaaaaaa
ckaabaaaaaaaaaaadgaaaaafeccabaaaadaaaaaabkaabaaaacaaaaaadgaaaaaf
cccabaaaadaaaaaabkaabaaaadaaaaaadgaaaaafcccabaaaaeaaaaaackaabaaa
adaaaaaadgaaaaafeccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaahbcaabaaa
aaaaaaaabkaabaaaacaaaaaabkaabaaaacaaaaaadcaaaaakbcaabaaaaaaaaaaa
akaabaaaacaaaaaaakaabaaaacaaaaaaakaabaiaebaaaaaaaaaaaaaadiaaaaah
pcaabaaaabaaaaaajgacbaaaacaaaaaaegakbaaaacaaaaaabbaaaaaibcaabaaa
adaaaaaaegiocaaaabaaaaaacjaaaaaaegaobaaaabaaaaaabbaaaaaiccaabaaa
adaaaaaaegiocaaaabaaaaaackaaaaaaegaobaaaabaaaaaabbaaaaaiecaabaaa
adaaaaaaegiocaaaabaaaaaaclaaaaaaegaobaaaabaaaaaadcaaaaakhcaabaaa
aaaaaaaaegiccaaaabaaaaaacmaaaaaaagaabaaaaaaaaaaaegacbaaaadaaaaaa
dgaaaaaficaabaaaacaaaaaaabeaaaaaaaaaiadpbbaaaaaibcaabaaaabaaaaaa
egiocaaaabaaaaaacgaaaaaaegaobaaaacaaaaaabbaaaaaiccaabaaaabaaaaaa
egiocaaaabaaaaaachaaaaaaegaobaaaacaaaaaabbaaaaaiecaabaaaabaaaaaa
egiocaaaabaaaaaaciaaaaaaegaobaaaacaaaaaaaaaaaaahhccabaaaafaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 44 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityLighting" 720
Vector 608 [unity_SHAr]
Vector 624 [unity_SHAg]
Vector 640 [unity_SHAb]
Vector 656 [unity_SHBr]
Vector 672 [unity_SHBg]
Vector 688 [unity_SHBb]
Vector 704 [unity_SHC]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
BindCB  "UnityPerDraw" 2
"vs_4_0_level_9_1
eefiecedkmgpknlgoonfagkljnjnkgfekkaceingabaaaaaajaanaaaaaeaaaaaa
daaaaaaagiaeaaaaoaalaaaaniamaaaaebgpgodjdaaeaaaadaaeaaaaaaacpopp
niadaaaafiaaaaaaaeaaceaaaaaafeaaaaaafeaaaaaaceaaabaafeaaaaaaakaa
acaaabaaaaaaaaaaabaacgaaahaaadaaaaaaaaaaacaaaaaaaeaaakaaaaaaaaaa
acaaamaaahaaaoaaaaaaaaaaaaaaaaaaaaacpoppfbaaaaafbfaaapkaaaaaiadp
aaaaaaaaaaaaaaaaaaaaaaaabpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabia
abaaapjabpaaaaacafaaaciaacaaapjabpaaaaacafaaadiaadaaapjaaeaaaaae
aaaaadoaadaaoejaabaaoekaabaaookaaeaaaaaeaaaaamoaadaaeejaacaaeeka
acaaoekaafaaaaadaaaaabiaacaaaajabcaaaakaafaaaaadaaaaaciaacaaaaja
bdaaaakaafaaaaadaaaaaeiaacaaaajabeaaaakaafaaaaadabaaabiaacaaffja
bcaaffkaafaaaaadabaaaciaacaaffjabdaaffkaafaaaaadabaaaeiaacaaffja
beaaffkaacaaaaadaaaaahiaaaaaoeiaabaaoeiaafaaaaadabaaabiaacaakkja
bcaakkkaafaaaaadabaaaciaacaakkjabdaakkkaafaaaaadabaaaeiaacaakkja
beaakkkaacaaaaadaaaaahiaaaaaoeiaabaaoeiaceaaaaacabaaahiaaaaaoeia
afaaaaadaaaaabiaabaaffiaabaaffiaaeaaaaaeaaaaabiaabaaaaiaabaaaaia
aaaaaaibafaaaaadacaaapiaabaacjiaabaakeiaajaaaaadadaaabiaagaaoeka
acaaoeiaajaaaaadadaaaciaahaaoekaacaaoeiaajaaaaadadaaaeiaaiaaoeka
acaaoeiaaeaaaaaeaaaaahiaajaaoekaaaaaaaiaadaaoeiaabaaaaacabaaaiia
bfaaaakaajaaaaadacaaabiaadaaoekaabaaoeiaajaaaaadacaaaciaaeaaoeka
abaaoeiaajaaaaadacaaaeiaafaaoekaabaaoeiaacaaaaadaeaaahoaaaaaoeia
acaaoeiaafaaaaadaaaaapiaaaaaffjaalaaoekaaeaaaaaeaaaaapiaakaaoeka
aaaaaajaaaaaoeiaaeaaaaaeaaaaapiaamaaoekaaaaakkjaaaaaoeiaaeaaaaae
aaaaapiaanaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadmaaaaappiaaaaaoeka
aaaaoeiaabaaaaacaaaaammaaaaaoeiaafaaaaadaaaaahiaabaaffjaapaamjka
aeaaaaaeaaaaahiaaoaamjkaabaaaajaaaaaoeiaaeaaaaaeaaaaahiabaaamjka
abaakkjaaaaaoeiaaiaaaaadaaaaaiiaaaaaoeiaaaaaoeiaahaaaaacaaaaaiia
aaaappiaafaaaaadaaaaahiaaaaappiaaaaaoeiaabaaaaacabaaaboaaaaakkia
afaaaaadacaaahiaaaaaoeiaabaanciaaeaaaaaeacaaahiaabaamjiaaaaamjia
acaaoeibafaaaaadacaaahiaacaaoeiaabaappjaabaaaaacabaaacoaacaaaaia
abaaaaacabaaaeoaabaaaaiaafaaaaadadaaahiaaaaaffjaapaaoekaaeaaaaae
adaaahiaaoaaoekaaaaaaajaadaaoeiaaeaaaaaeadaaahiabaaaoekaaaaakkja
adaaoeiaaeaaaaaeadaaahiabbaaoekaaaaappjaadaaoeiaabaaaaacabaaaioa
adaaaaiaabaaaaacacaaaboaaaaaaaiaabaaaaacadaaaboaaaaaffiaabaaaaac
acaaacoaacaaffiaabaaaaacadaaacoaacaakkiaabaaaaacacaaaeoaabaaffia
abaaaaacadaaaeoaabaakkiaabaaaaacacaaaioaadaaffiaabaaaaacadaaaioa
adaakkiappppaaaafdeieefchaahaaaaeaaaabaanmabaaaafjaaaaaeegiocaaa
aaaaaaaaamaaaaaafjaaaaaeegiocaaaabaaaaaacnaaaaaafjaaaaaeegiocaaa
acaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaaabaaaaaa
fpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaaepccabaaa
aaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaa
gfaaaaadpccabaaaadaaaaaagfaaaaadpccabaaaaeaaaaaagfaaaaadhccabaaa
afaaaaaagiaaaaacaeaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaa
egiocaaaacaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaacaaaaaa
aaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaacaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pccabaaaaaaaaaaaegiocaaaacaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaaaaaaaaaa
akaaaaaaogikcaaaaaaaaaaaakaaaaaadcaaaaalmccabaaaabaaaaaaagbebaaa
adaaaaaaagiecaaaaaaaaaaaalaaaaaakgiocaaaaaaaaaaaalaaaaaadiaaaaai
hcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiccaaaacaaaaaaanaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaacaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhcaabaaaaaaaaaaaegiccaaaacaaaaaaaoaaaaaakgbkbaaa
aaaaaaaaegacbaaaaaaaaaaadcaaaaakhcaabaaaaaaaaaaaegiccaaaacaaaaaa
apaaaaaapgbpbaaaaaaaaaaaegacbaaaaaaaaaaadgaaaaaficcabaaaacaaaaaa
akaabaaaaaaaaaaadiaaaaaihcaabaaaabaaaaaafgbfbaaaabaaaaaajgiecaaa
acaaaaaaanaaaaaadcaaaaakhcaabaaaabaaaaaajgiecaaaacaaaaaaamaaaaaa
agbabaaaabaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaabaaaaaajgiecaaa
acaaaaaaaoaaaaaakgbkbaaaabaaaaaaegacbaaaabaaaaaabaaaaaahbcaabaaa
aaaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaaeeaaaaafbcaabaaaaaaaaaaa
akaabaaaaaaaaaaadiaaaaahhcaabaaaabaaaaaaagaabaaaaaaaaaaaegacbaaa
abaaaaaadiaaaaaibcaabaaaacaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaa
baaaaaaadiaaaaaiccaabaaaacaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaa
bbaaaaaadiaaaaaiecaabaaaacaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaa
bcaaaaaadiaaaaaibcaabaaaadaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaa
baaaaaaadiaaaaaiccaabaaaadaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaa
bbaaaaaadiaaaaaiecaabaaaadaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaa
bcaaaaaaaaaaaaahhcaabaaaacaaaaaaegacbaaaacaaaaaaegacbaaaadaaaaaa
diaaaaaibcaabaaaadaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabaaaaaaa
diaaaaaiccaabaaaadaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabbaaaaaa
diaaaaaiecaabaaaadaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabcaaaaaa
aaaaaaahhcaabaaaacaaaaaaegacbaaaacaaaaaaegacbaaaadaaaaaabaaaaaah
bcaabaaaaaaaaaaaegacbaaaacaaaaaaegacbaaaacaaaaaaeeaaaaafbcaabaaa
aaaaaaaaakaabaaaaaaaaaaadiaaaaahhcaabaaaacaaaaaaagaabaaaaaaaaaaa
egacbaaaacaaaaaadiaaaaahhcaabaaaadaaaaaaegacbaaaabaaaaaacgajbaaa
acaaaaaadcaaaaakhcaabaaaadaaaaaajgaebaaaacaaaaaajgaebaaaabaaaaaa
egacbaiaebaaaaaaadaaaaaadiaaaaahhcaabaaaadaaaaaaegacbaaaadaaaaaa
pgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaaakaabaaaadaaaaaadgaaaaaf
bccabaaaacaaaaaackaabaaaabaaaaaadgaaaaafeccabaaaacaaaaaaakaabaaa
acaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaaficcabaaaadaaaaaabkaabaaaaaaaaaaa
dgaaaaaficcabaaaaeaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaadaaaaaa
bkaabaaaacaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaadaaaaaadgaaaaaf
cccabaaaaeaaaaaackaabaaaadaaaaaadgaaaaafeccabaaaaeaaaaaackaabaaa
acaaaaaadiaaaaahbcaabaaaaaaaaaaabkaabaaaacaaaaaabkaabaaaacaaaaaa
dcaaaaakbcaabaaaaaaaaaaaakaabaaaacaaaaaaakaabaaaacaaaaaaakaabaia
ebaaaaaaaaaaaaaadiaaaaahpcaabaaaabaaaaaajgacbaaaacaaaaaaegakbaaa
acaaaaaabbaaaaaibcaabaaaadaaaaaaegiocaaaabaaaaaacjaaaaaaegaobaaa
abaaaaaabbaaaaaiccaabaaaadaaaaaaegiocaaaabaaaaaackaaaaaaegaobaaa
abaaaaaabbaaaaaiecaabaaaadaaaaaaegiocaaaabaaaaaaclaaaaaaegaobaaa
abaaaaaadcaaaaakhcaabaaaaaaaaaaaegiccaaaabaaaaaacmaaaaaaagaabaaa
aaaaaaaaegacbaaaadaaaaaadgaaaaaficaabaaaacaaaaaaabeaaaaaaaaaiadp
bbaaaaaibcaabaaaabaaaaaaegiocaaaabaaaaaacgaaaaaaegaobaaaacaaaaaa
bbaaaaaiccaabaaaabaaaaaaegiocaaaabaaaaaachaaaaaaegaobaaaacaaaaaa
bbaaaaaiecaabaaaabaaaaaaegiocaaaabaaaaaaciaaaaaaegaobaaaacaaaaaa
aaaaaaahhccabaaaafaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadoaaaaab
ejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
aaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapapaaaa
njaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaaahahaaaaoaaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaaabaaaaaaaaaaaaaaadaaaaaa
aeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaaadaaaaaaafaaaaaaapaaaaaa
oaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaaapaaaaaaojaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdejfeejepeoaafeebeoehefeofe
aaeoepfcenebemaafeeffiedepepfceeaaedepemepfcaaklepfdeheolaaaaaaa
agaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaa
keaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapaaaaaakeaaaaaaabaaaaaa
aaaaaaaaadaaaaaaacaaaaaaapaaaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaa
adaaaaaaapaaaaaakeaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaa
keaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaaahaiaaaafdfgfpfaepfdejfe
ejepeoaafeeffiedepepfceeaaklklkl"
}
SubProgram "opengl " {
// Stats: 19 math, 3 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 unity_DynamicLightmapST;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec4 xlv_TEXCOORD6;
void main ()
{
  vec4 tmpvar_1;
  vec4 tmpvar_2;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec3 tmpvar_3;
  tmpvar_3 = (_Object2World * gl_Vertex).xyz;
  vec4 v_4;
  v_4.x = _World2Object[0].x;
  v_4.y = _World2Object[1].x;
  v_4.z = _World2Object[2].x;
  v_4.w = _World2Object[3].x;
  vec4 v_5;
  v_5.x = _World2Object[0].y;
  v_5.y = _World2Object[1].y;
  v_5.z = _World2Object[2].y;
  v_5.w = _World2Object[3].y;
  vec4 v_6;
  v_6.x = _World2Object[0].z;
  v_6.y = _World2Object[1].z;
  v_6.z = _World2Object[2].z;
  v_6.w = _World2Object[3].z;
  vec3 tmpvar_7;
  tmpvar_7 = normalize(((
    (v_4.xyz * gl_Normal.x)
   + 
    (v_5.xyz * gl_Normal.y)
  ) + (v_6.xyz * gl_Normal.z)));
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  vec3 tmpvar_9;
  tmpvar_9 = normalize((tmpvar_8 * TANGENT.xyz));
  vec3 tmpvar_10;
  tmpvar_10 = (((tmpvar_7.yzx * tmpvar_9.zxy) - (tmpvar_7.zxy * tmpvar_9.yzx)) * TANGENT.w);
  vec4 tmpvar_11;
  tmpvar_11.x = tmpvar_9.x;
  tmpvar_11.y = tmpvar_10.x;
  tmpvar_11.z = tmpvar_7.x;
  tmpvar_11.w = tmpvar_3.x;
  vec4 tmpvar_12;
  tmpvar_12.x = tmpvar_9.y;
  tmpvar_12.y = tmpvar_10.y;
  tmpvar_12.z = tmpvar_7.y;
  tmpvar_12.w = tmpvar_3.y;
  vec4 tmpvar_13;
  tmpvar_13.x = tmpvar_9.z;
  tmpvar_13.y = tmpvar_10.z;
  tmpvar_13.z = tmpvar_7.z;
  tmpvar_13.w = tmpvar_3.z;
  tmpvar_2.zw = ((gl_MultiTexCoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_11;
  xlv_TEXCOORD2 = tmpvar_12;
  xlv_TEXCOORD3 = tmpvar_13;
  xlv_TEXCOORD6 = tmpvar_2;
}


#endif
#ifdef FRAGMENT
#extension GL_ARB_shader_texture_lod : enable
uniform vec4 _WorldSpaceLightPos0;
uniform sampler2D unity_DynamicLightmap;
uniform vec4 unity_DynamicLightmap_HDR;
uniform vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec4 xlv_TEXCOORD6;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  worldN_1.x = dot (xlv_TEXCOORD1.xyz, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2.xyz, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3.xyz, normal_3);
  vec4 tmpvar_4;
  tmpvar_4 = texture2D (unity_DynamicLightmap, xlv_TEXCOORD6.zw);
  vec4 c_5;
  vec4 c_6;
  c_6.xyz = ((tmpvar_2.xyz * _LightColor0.xyz) * max (0.0, dot (worldN_1, _WorldSpaceLightPos0.xyz)));
  c_6.w = tmpvar_2.w;
  c_5.w = c_6.w;
  c_5.xyz = (c_6.xyz + (tmpvar_2.xyz * pow (
    ((unity_DynamicLightmap_HDR.x * tmpvar_4.w) * tmpvar_4.xyz)
  , unity_DynamicLightmap_HDR.yyy)));
  gl_FragData[0] = c_5;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 35 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord2" TexCoord2
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_BumpMap_ST]
Vector 11 [_MainTex_ST]
Vector 10 [unity_DynamicLightmapST]
"vs_3_0
def c13, 0, 0, 0, 0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord6 o5
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v3, c11, c11.zwzw
mad o1.zw, v3.xyxy, c12.xyxy, c12
dp4 o2.w, c4, v0
dp4 o3.w, c5, v0
dp4 o4.w, c6, v0
mad o5.zw, v4.xyxy, c10.xyxy, c10
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov o2.x, r0.z
mul r1.xyz, c8.zxyw, v2.y
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov o2.y, r2.x
mov o2.z, r1.y
mov o3.x, r0.x
mov o4.x, r0.y
mov o3.y, r2.y
mov o4.y, r2.z
mov o3.z, r1.z
mov o4.z, r1.x
mov o5.xy, c13.x

"
}
SubProgram "d3d11 " {
// Stats: 34 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord2" TexCoord2
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
ConstBuffer "UnityLightmaps" 32
Vector 16 [unity_DynamicLightmapST]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
BindCB  "UnityLightmaps" 2
"vs_4_0
eefiecedbgefnhdphgolocnmpicpcgphbjcolpmeabaaaaaadeaiaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapadaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaapaaaaaakeaaaaaaagaaaaaaaaaaaaaaadaaaaaaafaaaaaa
apaaaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
faagaaaaeaaaabaajeabaaaafjaaaaaeegiocaaaaaaaaaaaamaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafjaaaaaeegiocaaaacaaaaaaacaaaaaafpaaaaad
pcbabaaaaaaaaaaafpaaaaadpcbabaaaabaaaaaafpaaaaadhcbabaaaacaaaaaa
fpaaaaaddcbabaaaadaaaaaafpaaaaaddcbabaaaafaaaaaaghaaaaaepccabaaa
aaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaa
gfaaaaadpccabaaaadaaaaaagfaaaaadpccabaaaaeaaaaaagfaaaaadpccabaaa
afaaaaaagiaaaaacaeaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaa
egiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaa
aaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaaaaaaaaaa
akaaaaaaogikcaaaaaaaaaaaakaaaaaadcaaaaalmccabaaaabaaaaaaagbebaaa
adaaaaaaagiecaaaaaaaaaaaalaaaaaakgiocaaaaaaaaaaaalaaaaaadiaaaaai
ccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaadiaaaaai
ccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaaiccaabaaa
abaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaaiecaabaaa
abaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaaibcaabaaa
abaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaahhcaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaa
egacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaa
aaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaaaaaaaaaa
dgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaaabaaaaaa
fgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaabaaaaaa
jgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaadcaaaaak
hcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaaegacbaaa
abaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaa
eeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaabaaaaaa
pgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaaegacbaaa
aaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaaaaaaaaaa
jgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaaakaabaaa
acaaaaaadiaaaaaihcaabaaaadaaaaaafgbfbaaaaaaaaaaaegiccaaaabaaaaaa
anaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaaabaaaaaaamaaaaaaagbabaaa
aaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaaabaaaaaa
aoaaaaaakgbkbaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaadaaaaaa
egiccaaaabaaaaaaapaaaaaapgbpbaaaaaaaaaaaegacbaaaadaaaaaadgaaaaaf
iccabaaaacaaaaaaakaabaaaadaaaaaadgaaaaafbccabaaaacaaaaaackaabaaa
abaaaaaadgaaaaafeccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaa
aeaaaaaaakaabaaaaaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaa
dgaaaaafbccabaaaaeaaaaaabkaabaaaabaaaaaadgaaaaaficcabaaaadaaaaaa
bkaabaaaadaaaaaadgaaaaaficcabaaaaeaaaaaackaabaaaadaaaaaadgaaaaaf
cccabaaaadaaaaaabkaabaaaacaaaaaadgaaaaafcccabaaaaeaaaaaackaabaaa
acaaaaaadcaaaaalmccabaaaafaaaaaaagbebaaaafaaaaaaagiecaaaacaaaaaa
abaaaaaakgiocaaaacaaaaaaabaaaaaadgaaaaaidccabaaaafaaaaaaaceaaaaa
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadoaaaaab"
}
SubProgram "opengl " {
// Stats: 16 math, 2 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLSL
#ifdef VERTEX
uniform vec4 unity_4LightPosX0;
uniform vec4 unity_4LightPosY0;
uniform vec4 unity_4LightPosZ0;
uniform vec4 unity_4LightAtten0;
uniform vec4 unity_LightColor[8];
uniform vec4 unity_SHAr;
uniform vec4 unity_SHAg;
uniform vec4 unity_SHAb;
uniform vec4 unity_SHBr;
uniform vec4 unity_SHBg;
uniform vec4 unity_SHBb;
uniform vec4 unity_SHC;

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec3 tmpvar_2;
  tmpvar_2 = (_Object2World * gl_Vertex).xyz;
  vec4 v_3;
  v_3.x = _World2Object[0].x;
  v_3.y = _World2Object[1].x;
  v_3.z = _World2Object[2].x;
  v_3.w = _World2Object[3].x;
  vec4 v_4;
  v_4.x = _World2Object[0].y;
  v_4.y = _World2Object[1].y;
  v_4.z = _World2Object[2].y;
  v_4.w = _World2Object[3].y;
  vec4 v_5;
  v_5.x = _World2Object[0].z;
  v_5.y = _World2Object[1].z;
  v_5.z = _World2Object[2].z;
  v_5.w = _World2Object[3].z;
  vec3 tmpvar_6;
  tmpvar_6 = normalize(((
    (v_3.xyz * gl_Normal.x)
   + 
    (v_4.xyz * gl_Normal.y)
  ) + (v_5.xyz * gl_Normal.z)));
  mat3 tmpvar_7;
  tmpvar_7[0] = _Object2World[0].xyz;
  tmpvar_7[1] = _Object2World[1].xyz;
  tmpvar_7[2] = _Object2World[2].xyz;
  vec3 tmpvar_8;
  tmpvar_8 = normalize((tmpvar_7 * TANGENT.xyz));
  vec3 tmpvar_9;
  tmpvar_9 = (((tmpvar_6.yzx * tmpvar_8.zxy) - (tmpvar_6.zxy * tmpvar_8.yzx)) * TANGENT.w);
  vec4 tmpvar_10;
  tmpvar_10.x = tmpvar_8.x;
  tmpvar_10.y = tmpvar_9.x;
  tmpvar_10.z = tmpvar_6.x;
  tmpvar_10.w = tmpvar_2.x;
  vec4 tmpvar_11;
  tmpvar_11.x = tmpvar_8.y;
  tmpvar_11.y = tmpvar_9.y;
  tmpvar_11.z = tmpvar_6.y;
  tmpvar_11.w = tmpvar_2.y;
  vec4 tmpvar_12;
  tmpvar_12.x = tmpvar_8.z;
  tmpvar_12.y = tmpvar_9.z;
  tmpvar_12.z = tmpvar_6.z;
  tmpvar_12.w = tmpvar_2.z;
  vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.xyz = tmpvar_6;
  vec3 x2_14;
  vec3 x1_15;
  x1_15.x = dot (unity_SHAr, tmpvar_13);
  x1_15.y = dot (unity_SHAg, tmpvar_13);
  x1_15.z = dot (unity_SHAb, tmpvar_13);
  vec4 tmpvar_16;
  tmpvar_16 = (tmpvar_6.xyzz * tmpvar_6.yzzx);
  x2_14.x = dot (unity_SHBr, tmpvar_16);
  x2_14.y = dot (unity_SHBg, tmpvar_16);
  x2_14.z = dot (unity_SHBb, tmpvar_16);
  vec4 tmpvar_17;
  tmpvar_17 = (unity_4LightPosX0 - tmpvar_2.x);
  vec4 tmpvar_18;
  tmpvar_18 = (unity_4LightPosY0 - tmpvar_2.y);
  vec4 tmpvar_19;
  tmpvar_19 = (unity_4LightPosZ0 - tmpvar_2.z);
  vec4 tmpvar_20;
  tmpvar_20 = (((tmpvar_17 * tmpvar_17) + (tmpvar_18 * tmpvar_18)) + (tmpvar_19 * tmpvar_19));
  vec4 tmpvar_21;
  tmpvar_21 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_17 * tmpvar_6.x) + (tmpvar_18 * tmpvar_6.y)) + (tmpvar_19 * tmpvar_6.z))
   * 
    inversesqrt(tmpvar_20)
  )) * (1.0/((1.0 + 
    (tmpvar_20 * unity_4LightAtten0)
  ))));
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_10;
  xlv_TEXCOORD2 = tmpvar_11;
  xlv_TEXCOORD3 = tmpvar_12;
  xlv_TEXCOORD4 = (((x2_14 + 
    (unity_SHC.xyz * ((tmpvar_6.x * tmpvar_6.x) - (tmpvar_6.y * tmpvar_6.y)))
  ) + x1_15) + ((
    ((unity_LightColor[0].xyz * tmpvar_21.x) + (unity_LightColor[1].xyz * tmpvar_21.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_21.z)
  ) + (unity_LightColor[3].xyz * tmpvar_21.w)));
}


#endif
#ifdef FRAGMENT
uniform vec4 _WorldSpaceLightPos0;
uniform vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  worldN_1.x = dot (xlv_TEXCOORD1.xyz, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2.xyz, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3.xyz, normal_3);
  vec4 c_4;
  vec4 c_5;
  c_5.xyz = ((tmpvar_2.xyz * _LightColor0.xyz) * max (0.0, dot (worldN_1, _WorldSpaceLightPos0.xyz)));
  c_5.w = tmpvar_2.w;
  c_4.w = c_5.w;
  c_4.xyz = (c_5.xyz + (tmpvar_2.xyz * xlv_TEXCOORD4));
  gl_FragData[0] = c_4;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 75 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
Matrix 8 [_Object2World] 3
Matrix 11 [_World2Object] 3
Matrix 4 [glstate_matrix_mvp]
Vector 26 [_BumpMap_ST]
Vector 25 [_MainTex_ST]
Vector 17 [unity_4LightAtten0]
Vector 14 [unity_4LightPosX0]
Vector 15 [unity_4LightPosY0]
Vector 16 [unity_4LightPosZ0]
Vector 0 [unity_LightColor0]
Vector 1 [unity_LightColor1]
Vector 2 [unity_LightColor2]
Vector 3 [unity_LightColor3]
Vector 20 [unity_SHAb]
Vector 19 [unity_SHAg]
Vector 18 [unity_SHAr]
Vector 23 [unity_SHBb]
Vector 22 [unity_SHBg]
Vector 21 [unity_SHBr]
Vector 24 [unity_SHC]
"vs_2_0
def c27, 1, 0, 0, 0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dp4 oPos.x, c4, v0
dp4 oPos.y, c5, v0
dp4 oPos.z, c6, v0
dp4 oPos.w, c7, v0
mad oT0.xy, v3, c25, c25.zwzw
mad oT0.zw, v3.xyxy, c26.xyxy, c26
dp4 r0.x, c9, v0
add r1, -r0.x, c15
mov oT2.w, r0.x
mul r0, r1, r1
dp4 r2.x, c8, v0
add r3, -r2.x, c14
mov oT1.w, r2.x
mad r0, r3, r3, r0
dp4 r2.x, c10, v0
add r4, -r2.x, c16
mov oT3.w, r2.x
mad r0, r4, r4, r0
rsq r2.x, r0.x
rsq r2.y, r0.y
rsq r2.z, r0.z
rsq r2.w, r0.w
mov r5.x, c27.x
mad r0, r0, c17, r5.x
mul r5.xyz, v2.y, c12
mad r5.xyz, c11, v2.x, r5
mad r5.xyz, c13, v2.z, r5
nrm r6.xyz, r5
mul r1, r1, r6.y
mad r1, r3, r6.x, r1
mad r1, r4, r6.z, r1
mul r1, r2, r1
max r1, r1, c27.y
rcp r2.x, r0.x
rcp r2.y, r0.y
rcp r2.z, r0.z
rcp r2.w, r0.w
mul r0, r1, r2
mul r1.xyz, r0.y, c1
mad r1.xyz, c0, r0.x, r1
mad r0.xyz, c2, r0.z, r1
mad r0.xyz, c3, r0.w, r0
mul r0.w, r6.y, r6.y
mad r0.w, r6.x, r6.x, -r0.w
mul r1, r6.yzzx, r6.xyzz
dp4 r2.x, c21, r1
dp4 r2.y, c22, r1
dp4 r2.z, c23, r1
mad r1.xyz, c24, r0.w, r2
mov r6.w, c27.x
dp4 r2.x, c18, r6
dp4 r2.y, c19, r6
dp4 r2.z, c20, r6
add r1.xyz, r1, r2
add oT4.xyz, r0, r1
dp3 r0.z, c8, v1
dp3 r0.x, c9, v1
dp3 r0.y, c10, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r1.xyz, r0, r6.zxyw
mad r1.xyz, r6.yzxw, r0.yzxw, -r1
mul r1.xyz, r1, v1.w
mov oT1.y, r1.x
mov oT1.z, r6.x
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r1.y
mov oT3.y, r1.z
mov oT2.z, r6.y
mov oT3.z, r6.z

"
}
SubProgram "d3d11 " {
// Stats: 64 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityLighting" 720
Vector 32 [unity_4LightPosX0]
Vector 48 [unity_4LightPosY0]
Vector 64 [unity_4LightPosZ0]
Vector 80 [unity_4LightAtten0]
Vector 96 [unity_LightColor0]
Vector 112 [unity_LightColor1]
Vector 128 [unity_LightColor2]
Vector 144 [unity_LightColor3]
Vector 160 [unity_LightColor4]
Vector 176 [unity_LightColor5]
Vector 192 [unity_LightColor6]
Vector 208 [unity_LightColor7]
Vector 608 [unity_SHAr]
Vector 624 [unity_SHAg]
Vector 640 [unity_SHAb]
Vector 656 [unity_SHBr]
Vector 672 [unity_SHBg]
Vector 688 [unity_SHBb]
Vector 704 [unity_SHC]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
BindCB  "UnityPerDraw" 2
"vs_4_0
eefiecedbelbkhpbcgcloccamnekdbcdpnhpbkagabaaaaaaamamaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaapaaaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaa
ahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
ciakaaaaeaaaabaaikacaaaafjaaaaaeegiocaaaaaaaaaaaamaaaaaafjaaaaae
egiocaaaabaaaaaacnaaaaaafjaaaaaeegiocaaaacaaaaaabdaaaaaafpaaaaad
pcbabaaaaaaaaaaafpaaaaadpcbabaaaabaaaaaafpaaaaadhcbabaaaacaaaaaa
fpaaaaaddcbabaaaadaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaagfaaaaad
pccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaagfaaaaadpccabaaaadaaaaaa
gfaaaaadpccabaaaaeaaaaaagfaaaaadhccabaaaafaaaaaagiaaaaacagaaaaaa
diaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaacaaaaaaabaaaaaa
dcaaaaakpcaabaaaaaaaaaaaegiocaaaacaaaaaaaaaaaaaaagbabaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaacaaaaaaacaaaaaa
kgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaa
acaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaaldccabaaa
abaaaaaaegbabaaaadaaaaaaegiacaaaaaaaaaaaakaaaaaaogikcaaaaaaaaaaa
akaaaaaadcaaaaalmccabaaaabaaaaaaagbebaaaadaaaaaaagiecaaaaaaaaaaa
alaaaaaakgiocaaaaaaaaaaaalaaaaaadiaaaaaihcaabaaaaaaaaaaafgbfbaaa
abaaaaaajgiecaaaacaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaajgiecaaa
acaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaaaaaaaaadcaaaaakhcaabaaa
aaaaaaaajgiecaaaacaaaaaaaoaaaaaakgbkbaaaabaaaaaaegacbaaaaaaaaaaa
baaaaaahicaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaf
icaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaa
aaaaaaaaegacbaaaaaaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaaaaaaaaa
diaaaaaibcaabaaaabaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaabaaaaaaa
diaaaaaiccaabaaaabaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaabbaaaaaa
diaaaaaiecaabaaaabaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaabcaaaaaa
diaaaaaibcaabaaaacaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaabaaaaaaa
diaaaaaiccaabaaaacaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaabbaaaaaa
diaaaaaiecaabaaaacaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaabcaaaaaa
aaaaaaahhcaabaaaabaaaaaaegacbaaaabaaaaaaegacbaaaacaaaaaadiaaaaai
bcaabaaaacaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabaaaaaaadiaaaaai
ccaabaaaacaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabbaaaaaadiaaaaai
ecaabaaaacaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabcaaaaaaaaaaaaah
hcaabaaaabaaaaaaegacbaaaabaaaaaaegacbaaaacaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaabaaaaaapgapbaaaaaaaaaaaegacbaaa
abaaaaaadiaaaaahhcaabaaaacaaaaaaegacbaaaaaaaaaaacgajbaaaabaaaaaa
dcaaaaakhcaabaaaacaaaaaajgaebaaaabaaaaaajgaebaaaaaaaaaaaegacbaia
ebaaaaaaacaaaaaadiaaaaahhcaabaaaacaaaaaaegacbaaaacaaaaaapgbpbaaa
abaaaaaadgaaaaafcccabaaaacaaaaaaakaabaaaacaaaaaadgaaaaafeccabaaa
acaaaaaaakaabaaaabaaaaaadiaaaaaihcaabaaaadaaaaaafgbfbaaaaaaaaaaa
egiccaaaacaaaaaaanaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaaacaaaaaa
amaaaaaaagbabaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaadaaaaaa
egiccaaaacaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaaadaaaaaadcaaaaak
hcaabaaaadaaaaaaegiccaaaacaaaaaaapaaaaaapgbpbaaaaaaaaaaaegacbaaa
adaaaaaadgaaaaaficcabaaaacaaaaaaakaabaaaadaaaaaadgaaaaafbccabaaa
adaaaaaaakaabaaaaaaaaaaadgaaaaafbccabaaaaeaaaaaabkaabaaaaaaaaaaa
dgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaadgaaaaafcccabaaaaeaaaaaa
ckaabaaaacaaaaaadgaaaaafeccabaaaadaaaaaabkaabaaaabaaaaaadgaaaaaf
iccabaaaadaaaaaabkaabaaaadaaaaaadgaaaaaficcabaaaaeaaaaaackaabaaa
adaaaaaadgaaaaafeccabaaaaeaaaaaackaabaaaabaaaaaadiaaaaahbcaabaaa
aaaaaaaabkaabaaaabaaaaaabkaabaaaabaaaaaadcaaaaakbcaabaaaaaaaaaaa
akaabaaaabaaaaaaakaabaaaabaaaaaaakaabaiaebaaaaaaaaaaaaaadiaaaaah
pcaabaaaacaaaaaajgacbaaaabaaaaaaegakbaaaabaaaaaabbaaaaaibcaabaaa
aeaaaaaaegiocaaaabaaaaaacjaaaaaaegaobaaaacaaaaaabbaaaaaiccaabaaa
aeaaaaaaegiocaaaabaaaaaackaaaaaaegaobaaaacaaaaaabbaaaaaiecaabaaa
aeaaaaaaegiocaaaabaaaaaaclaaaaaaegaobaaaacaaaaaadcaaaaakhcaabaaa
aaaaaaaaegiccaaaabaaaaaacmaaaaaaagaabaaaaaaaaaaaegacbaaaaeaaaaaa
dgaaaaaficaabaaaabaaaaaaabeaaaaaaaaaiadpbbaaaaaibcaabaaaacaaaaaa
egiocaaaabaaaaaacgaaaaaaegaobaaaabaaaaaabbaaaaaiccaabaaaacaaaaaa
egiocaaaabaaaaaachaaaaaaegaobaaaabaaaaaabbaaaaaiecaabaaaacaaaaaa
egiocaaaabaaaaaaciaaaaaaegaobaaaabaaaaaaaaaaaaahhcaabaaaaaaaaaaa
egacbaaaaaaaaaaaegacbaaaacaaaaaaaaaaaaajpcaabaaaacaaaaaafgafbaia
ebaaaaaaadaaaaaaegiocaaaabaaaaaaadaaaaaadiaaaaahpcaabaaaaeaaaaaa
fgafbaaaabaaaaaaegaobaaaacaaaaaadiaaaaahpcaabaaaacaaaaaaegaobaaa
acaaaaaaegaobaaaacaaaaaaaaaaaaajpcaabaaaafaaaaaaagaabaiaebaaaaaa
adaaaaaaegiocaaaabaaaaaaacaaaaaaaaaaaaajpcaabaaaadaaaaaakgakbaia
ebaaaaaaadaaaaaaegiocaaaabaaaaaaaeaaaaaadcaaaaajpcaabaaaaeaaaaaa
egaobaaaafaaaaaaagaabaaaabaaaaaaegaobaaaaeaaaaaadcaaaaajpcaabaaa
abaaaaaaegaobaaaadaaaaaakgakbaaaabaaaaaaegaobaaaaeaaaaaadcaaaaaj
pcaabaaaacaaaaaaegaobaaaafaaaaaaegaobaaaafaaaaaaegaobaaaacaaaaaa
dcaaaaajpcaabaaaacaaaaaaegaobaaaadaaaaaaegaobaaaadaaaaaaegaobaaa
acaaaaaaeeaaaaafpcaabaaaadaaaaaaegaobaaaacaaaaaadcaaaaanpcaabaaa
acaaaaaaegaobaaaacaaaaaaegiocaaaabaaaaaaafaaaaaaaceaaaaaaaaaiadp
aaaaiadpaaaaiadpaaaaiadpaoaaaaakpcaabaaaacaaaaaaaceaaaaaaaaaiadp
aaaaiadpaaaaiadpaaaaiadpegaobaaaacaaaaaadiaaaaahpcaabaaaabaaaaaa
egaobaaaabaaaaaaegaobaaaadaaaaaadeaaaaakpcaabaaaabaaaaaaegaobaaa
abaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadiaaaaahpcaabaaa
abaaaaaaegaobaaaacaaaaaaegaobaaaabaaaaaadiaaaaaihcaabaaaacaaaaaa
fgafbaaaabaaaaaaegiccaaaabaaaaaaahaaaaaadcaaaaakhcaabaaaacaaaaaa
egiccaaaabaaaaaaagaaaaaaagaabaaaabaaaaaaegacbaaaacaaaaaadcaaaaak
hcaabaaaabaaaaaaegiccaaaabaaaaaaaiaaaaaakgakbaaaabaaaaaaegacbaaa
acaaaaaadcaaaaakhcaabaaaabaaaaaaegiccaaaabaaaaaaajaaaaaapgapbaaa
abaaaaaaegacbaaaabaaaaaaaaaaaaahhccabaaaafaaaaaaegacbaaaaaaaaaaa
egacbaaaabaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 64 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityLighting" 720
Vector 32 [unity_4LightPosX0]
Vector 48 [unity_4LightPosY0]
Vector 64 [unity_4LightPosZ0]
Vector 80 [unity_4LightAtten0]
Vector 96 [unity_LightColor0]
Vector 112 [unity_LightColor1]
Vector 128 [unity_LightColor2]
Vector 144 [unity_LightColor3]
Vector 160 [unity_LightColor4]
Vector 176 [unity_LightColor5]
Vector 192 [unity_LightColor6]
Vector 208 [unity_LightColor7]
Vector 608 [unity_SHAr]
Vector 624 [unity_SHAg]
Vector 640 [unity_SHAb]
Vector 656 [unity_SHBr]
Vector 672 [unity_SHBg]
Vector 688 [unity_SHBb]
Vector 704 [unity_SHC]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
BindCB  "UnityPerDraw" 2
"vs_4_0_level_9_1
eefiecedefpdnijkfkdgppajcjbiadpaamfoligaabaaaaaaaabcaaaaaeaaaaaa
daaaaaaacaagaaaafabaaaaaeibbaaaaebgpgodjoiafaaaaoiafaaaaaaacpopp
ieafaaaageaaaaaaafaaceaaaaaagaaaaaaagaaaaaaaceaaabaagaaaaaaaakaa
acaaabaaaaaaaaaaabaaacaaaiaaadaaaaaaaaaaabaacgaaahaaalaaaaaaaaaa
acaaaaaaaeaabcaaaaaaaaaaacaaamaaahaabgaaaaaaaaaaaaaaaaaaaaacpopp
fbaaaaafbnaaapkaaaaaiadpaaaaaaaaaaaaaaaaaaaaaaaabpaaaaacafaaaaia
aaaaapjabpaaaaacafaaabiaabaaapjabpaaaaacafaaaciaacaaapjabpaaaaac
afaaadiaadaaapjaaeaaaaaeaaaaadoaadaaoejaabaaoekaabaaookaaeaaaaae
aaaaamoaadaaeejaacaaeekaacaaoekaafaaaaadaaaaabiaacaaaajabkaaaaka
afaaaaadaaaaaciaacaaaajablaaaakaafaaaaadaaaaaeiaacaaaajabmaaaaka
afaaaaadabaaabiaacaaffjabkaaffkaafaaaaadabaaaciaacaaffjablaaffka
afaaaaadabaaaeiaacaaffjabmaaffkaacaaaaadaaaaahiaaaaaoeiaabaaoeia
afaaaaadabaaabiaacaakkjabkaakkkaafaaaaadabaaaciaacaakkjablaakkka
afaaaaadabaaaeiaacaakkjabmaakkkaacaaaaadaaaaahiaaaaaoeiaabaaoeia
ceaaaaacabaaahiaaaaaoeiaafaaaaadaaaaahiaaaaaffjabhaaoekaaeaaaaae
aaaaahiabgaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaahiabiaaoekaaaaakkja
aaaaoeiaaeaaaaaeaaaaahiabjaaoekaaaaappjaaaaaoeiaacaaaaadacaaapia
aaaaffibaeaaoekaafaaaaadadaaapiaabaaffiaacaaoeiaafaaaaadacaaapia
acaaoeiaacaaoeiaacaaaaadaeaaapiaaaaaaaibadaaoekaaeaaaaaeadaaapia
aeaaoeiaabaaaaiaadaaoeiaaeaaaaaeacaaapiaaeaaoeiaaeaaoeiaacaaoeia
acaaaaadaeaaapiaaaaakkibafaaoekaaeaaaaaeadaaapiaaeaaoeiaabaakkia
adaaoeiaaeaaaaaeacaaapiaaeaaoeiaaeaaoeiaacaaoeiaahaaaaacaeaaabia
acaaaaiaahaaaaacaeaaaciaacaaffiaahaaaaacaeaaaeiaacaakkiaahaaaaac
aeaaaiiaacaappiaabaaaaacafaaabiabnaaaakaaeaaaaaeacaaapiaacaaoeia
agaaoekaafaaaaiaafaaaaadadaaapiaadaaoeiaaeaaoeiaalaaaaadadaaapia
adaaoeiabnaaffkaagaaaaacaeaaabiaacaaaaiaagaaaaacaeaaaciaacaaffia
agaaaaacaeaaaeiaacaakkiaagaaaaacaeaaaiiaacaappiaafaaaaadacaaapia
adaaoeiaaeaaoeiaafaaaaadadaaahiaacaaffiaaiaaoekaaeaaaaaeadaaahia
ahaaoekaacaaaaiaadaaoeiaaeaaaaaeacaaahiaajaaoekaacaakkiaadaaoeia
aeaaaaaeacaaahiaakaaoekaacaappiaacaaoeiaafaaaaadaaaaaiiaabaaffia
abaaffiaaeaaaaaeaaaaaiiaabaaaaiaabaaaaiaaaaappibafaaaaadadaaapia
abaacjiaabaakeiaajaaaaadaeaaabiaaoaaoekaadaaoeiaajaaaaadaeaaacia
apaaoekaadaaoeiaajaaaaadaeaaaeiabaaaoekaadaaoeiaaeaaaaaeadaaahia
bbaaoekaaaaappiaaeaaoeiaabaaaaacabaaaiiabnaaaakaajaaaaadaeaaabia
alaaoekaabaaoeiaajaaaaadaeaaaciaamaaoekaabaaoeiaajaaaaadaeaaaeia
anaaoekaabaaoeiaacaaaaadadaaahiaadaaoeiaaeaaoeiaacaaaaadaeaaahoa
acaaoeiaadaaoeiaafaaaaadacaaapiaaaaaffjabdaaoekaaeaaaaaeacaaapia
bcaaoekaaaaaaajaacaaoeiaaeaaaaaeacaaapiabeaaoekaaaaakkjaacaaoeia
aeaaaaaeacaaapiabfaaoekaaaaappjaacaaoeiaaeaaaaaeaaaaadmaacaappia
aaaaoekaacaaoeiaabaaaaacaaaaammaacaaoeiaafaaaaadacaaahiaabaaffja
bhaamjkaaeaaaaaeacaaahiabgaamjkaabaaaajaacaaoeiaaeaaaaaeacaaahia
biaamjkaabaakkjaacaaoeiaaiaaaaadaaaaaiiaacaaoeiaacaaoeiaahaaaaac
aaaaaiiaaaaappiaafaaaaadacaaahiaaaaappiaacaaoeiaabaaaaacabaaaboa
acaakkiaafaaaaadadaaahiaabaanciaacaaoeiaaeaaaaaeadaaahiaabaamjia
acaamjiaadaaoeibafaaaaadadaaahiaadaaoeiaabaappjaabaaaaacabaaacoa
adaaaaiaabaaaaacabaaaeoaabaaaaiaabaaaaacabaaaioaaaaaaaiaabaaaaac
acaaaboaacaaaaiaabaaaaacadaaaboaacaaffiaabaaaaacacaaacoaadaaffia
abaaaaacadaaacoaadaakkiaabaaaaacacaaaeoaabaaffiaabaaaaacadaaaeoa
abaakkiaabaaaaacacaaaioaaaaaffiaabaaaaacadaaaioaaaaakkiappppaaaa
fdeieefcciakaaaaeaaaabaaikacaaaafjaaaaaeegiocaaaaaaaaaaaamaaaaaa
fjaaaaaeegiocaaaabaaaaaacnaaaaaafjaaaaaeegiocaaaacaaaaaabdaaaaaa
fpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaaabaaaaaafpaaaaadhcbabaaa
acaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaaepccabaaaaaaaaaaaabaaaaaa
gfaaaaadpccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaagfaaaaadpccabaaa
adaaaaaagfaaaaadpccabaaaaeaaaaaagfaaaaadhccabaaaafaaaaaagiaaaaac
agaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaaegiocaaaacaaaaaa
abaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaacaaaaaaaaaaaaaaagbabaaa
aaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaacaaaaaa
acaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpccabaaaaaaaaaaa
egiocaaaacaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaal
dccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaaaaaaaaaaakaaaaaaogikcaaa
aaaaaaaaakaaaaaadcaaaaalmccabaaaabaaaaaaagbebaaaadaaaaaaagiecaaa
aaaaaaaaalaaaaaakgiocaaaaaaaaaaaalaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaabaaaaaajgiecaaaacaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
jgiecaaaacaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaajgiecaaaacaaaaaaaoaaaaaakgbkbaaaabaaaaaaegacbaaa
aaaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaa
eeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaa
pgapbaaaaaaaaaaaegacbaaaaaaaaaaadgaaaaafbccabaaaacaaaaaackaabaaa
aaaaaaaadiaaaaaibcaabaaaabaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaa
baaaaaaadiaaaaaiccaabaaaabaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaa
bbaaaaaadiaaaaaiecaabaaaabaaaaaaakbabaaaacaaaaaaakiacaaaacaaaaaa
bcaaaaaadiaaaaaibcaabaaaacaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaa
baaaaaaadiaaaaaiccaabaaaacaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaa
bbaaaaaadiaaaaaiecaabaaaacaaaaaabkbabaaaacaaaaaabkiacaaaacaaaaaa
bcaaaaaaaaaaaaahhcaabaaaabaaaaaaegacbaaaabaaaaaaegacbaaaacaaaaaa
diaaaaaibcaabaaaacaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabaaaaaaa
diaaaaaiccaabaaaacaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabbaaaaaa
diaaaaaiecaabaaaacaaaaaackbabaaaacaaaaaackiacaaaacaaaaaabcaaaaaa
aaaaaaahhcaabaaaabaaaaaaegacbaaaabaaaaaaegacbaaaacaaaaaabaaaaaah
icaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaaeeaaaaaficaabaaa
aaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaabaaaaaapgapbaaaaaaaaaaa
egacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaaegacbaaaaaaaaaaacgajbaaa
abaaaaaadcaaaaakhcaabaaaacaaaaaajgaebaaaabaaaaaajgaebaaaaaaaaaaa
egacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaaacaaaaaaegacbaaaacaaaaaa
pgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaaakaabaaaacaaaaaadgaaaaaf
eccabaaaacaaaaaaakaabaaaabaaaaaadiaaaaaihcaabaaaadaaaaaafgbfbaaa
aaaaaaaaegiccaaaacaaaaaaanaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaa
acaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaa
adaaaaaaegiccaaaacaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaaadaaaaaa
dcaaaaakhcaabaaaadaaaaaaegiccaaaacaaaaaaapaaaaaapgbpbaaaaaaaaaaa
egacbaaaadaaaaaadgaaaaaficcabaaaacaaaaaaakaabaaaadaaaaaadgaaaaaf
bccabaaaadaaaaaaakaabaaaaaaaaaaadgaaaaafbccabaaaaeaaaaaabkaabaaa
aaaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaadgaaaaafcccabaaa
aeaaaaaackaabaaaacaaaaaadgaaaaafeccabaaaadaaaaaabkaabaaaabaaaaaa
dgaaaaaficcabaaaadaaaaaabkaabaaaadaaaaaadgaaaaaficcabaaaaeaaaaaa
ckaabaaaadaaaaaadgaaaaafeccabaaaaeaaaaaackaabaaaabaaaaaadiaaaaah
bcaabaaaaaaaaaaabkaabaaaabaaaaaabkaabaaaabaaaaaadcaaaaakbcaabaaa
aaaaaaaaakaabaaaabaaaaaaakaabaaaabaaaaaaakaabaiaebaaaaaaaaaaaaaa
diaaaaahpcaabaaaacaaaaaajgacbaaaabaaaaaaegakbaaaabaaaaaabbaaaaai
bcaabaaaaeaaaaaaegiocaaaabaaaaaacjaaaaaaegaobaaaacaaaaaabbaaaaai
ccaabaaaaeaaaaaaegiocaaaabaaaaaackaaaaaaegaobaaaacaaaaaabbaaaaai
ecaabaaaaeaaaaaaegiocaaaabaaaaaaclaaaaaaegaobaaaacaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaacmaaaaaaagaabaaaaaaaaaaaegacbaaa
aeaaaaaadgaaaaaficaabaaaabaaaaaaabeaaaaaaaaaiadpbbaaaaaibcaabaaa
acaaaaaaegiocaaaabaaaaaacgaaaaaaegaobaaaabaaaaaabbaaaaaiccaabaaa
acaaaaaaegiocaaaabaaaaaachaaaaaaegaobaaaabaaaaaabbaaaaaiecaabaaa
acaaaaaaegiocaaaabaaaaaaciaaaaaaegaobaaaabaaaaaaaaaaaaahhcaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaacaaaaaaaaaaaaajpcaabaaaacaaaaaa
fgafbaiaebaaaaaaadaaaaaaegiocaaaabaaaaaaadaaaaaadiaaaaahpcaabaaa
aeaaaaaafgafbaaaabaaaaaaegaobaaaacaaaaaadiaaaaahpcaabaaaacaaaaaa
egaobaaaacaaaaaaegaobaaaacaaaaaaaaaaaaajpcaabaaaafaaaaaaagaabaia
ebaaaaaaadaaaaaaegiocaaaabaaaaaaacaaaaaaaaaaaaajpcaabaaaadaaaaaa
kgakbaiaebaaaaaaadaaaaaaegiocaaaabaaaaaaaeaaaaaadcaaaaajpcaabaaa
aeaaaaaaegaobaaaafaaaaaaagaabaaaabaaaaaaegaobaaaaeaaaaaadcaaaaaj
pcaabaaaabaaaaaaegaobaaaadaaaaaakgakbaaaabaaaaaaegaobaaaaeaaaaaa
dcaaaaajpcaabaaaacaaaaaaegaobaaaafaaaaaaegaobaaaafaaaaaaegaobaaa
acaaaaaadcaaaaajpcaabaaaacaaaaaaegaobaaaadaaaaaaegaobaaaadaaaaaa
egaobaaaacaaaaaaeeaaaaafpcaabaaaadaaaaaaegaobaaaacaaaaaadcaaaaan
pcaabaaaacaaaaaaegaobaaaacaaaaaaegiocaaaabaaaaaaafaaaaaaaceaaaaa
aaaaiadpaaaaiadpaaaaiadpaaaaiadpaoaaaaakpcaabaaaacaaaaaaaceaaaaa
aaaaiadpaaaaiadpaaaaiadpaaaaiadpegaobaaaacaaaaaadiaaaaahpcaabaaa
abaaaaaaegaobaaaabaaaaaaegaobaaaadaaaaaadeaaaaakpcaabaaaabaaaaaa
egaobaaaabaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadiaaaaah
pcaabaaaabaaaaaaegaobaaaacaaaaaaegaobaaaabaaaaaadiaaaaaihcaabaaa
acaaaaaafgafbaaaabaaaaaaegiccaaaabaaaaaaahaaaaaadcaaaaakhcaabaaa
acaaaaaaegiccaaaabaaaaaaagaaaaaaagaabaaaabaaaaaaegacbaaaacaaaaaa
dcaaaaakhcaabaaaabaaaaaaegiccaaaabaaaaaaaiaaaaaakgakbaaaabaaaaaa
egacbaaaacaaaaaadcaaaaakhcaabaaaabaaaaaaegiccaaaabaaaaaaajaaaaaa
pgapbaaaabaaaaaaegacbaaaabaaaaaaaaaaaaahhccabaaaafaaaaaaegacbaaa
aaaaaaaaegacbaaaabaaaaaadoaaaaabejfdeheopaaaaaaaaiaaaaaaaiaaaaaa
miaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
acaaaaaaahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaa
oaaaaaaaabaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaa
aaaaaaaaadaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaa
agaaaaaaapaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaa
faepfdejfeejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfcee
aaedepemepfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaapaaaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
"
}
SubProgram "opengl " {
// Stats: 19 math, 3 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "VERTEXLIGHT_ON" }
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 unity_DynamicLightmapST;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec4 xlv_TEXCOORD6;
void main ()
{
  vec4 tmpvar_1;
  vec4 tmpvar_2;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec3 tmpvar_3;
  tmpvar_3 = (_Object2World * gl_Vertex).xyz;
  vec4 v_4;
  v_4.x = _World2Object[0].x;
  v_4.y = _World2Object[1].x;
  v_4.z = _World2Object[2].x;
  v_4.w = _World2Object[3].x;
  vec4 v_5;
  v_5.x = _World2Object[0].y;
  v_5.y = _World2Object[1].y;
  v_5.z = _World2Object[2].y;
  v_5.w = _World2Object[3].y;
  vec4 v_6;
  v_6.x = _World2Object[0].z;
  v_6.y = _World2Object[1].z;
  v_6.z = _World2Object[2].z;
  v_6.w = _World2Object[3].z;
  vec3 tmpvar_7;
  tmpvar_7 = normalize(((
    (v_4.xyz * gl_Normal.x)
   + 
    (v_5.xyz * gl_Normal.y)
  ) + (v_6.xyz * gl_Normal.z)));
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  vec3 tmpvar_9;
  tmpvar_9 = normalize((tmpvar_8 * TANGENT.xyz));
  vec3 tmpvar_10;
  tmpvar_10 = (((tmpvar_7.yzx * tmpvar_9.zxy) - (tmpvar_7.zxy * tmpvar_9.yzx)) * TANGENT.w);
  vec4 tmpvar_11;
  tmpvar_11.x = tmpvar_9.x;
  tmpvar_11.y = tmpvar_10.x;
  tmpvar_11.z = tmpvar_7.x;
  tmpvar_11.w = tmpvar_3.x;
  vec4 tmpvar_12;
  tmpvar_12.x = tmpvar_9.y;
  tmpvar_12.y = tmpvar_10.y;
  tmpvar_12.z = tmpvar_7.y;
  tmpvar_12.w = tmpvar_3.y;
  vec4 tmpvar_13;
  tmpvar_13.x = tmpvar_9.z;
  tmpvar_13.y = tmpvar_10.z;
  tmpvar_13.z = tmpvar_7.z;
  tmpvar_13.w = tmpvar_3.z;
  tmpvar_2.zw = ((gl_MultiTexCoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_11;
  xlv_TEXCOORD2 = tmpvar_12;
  xlv_TEXCOORD3 = tmpvar_13;
  xlv_TEXCOORD6 = tmpvar_2;
}


#endif
#ifdef FRAGMENT
#extension GL_ARB_shader_texture_lod : enable
uniform vec4 _WorldSpaceLightPos0;
uniform sampler2D unity_DynamicLightmap;
uniform vec4 unity_DynamicLightmap_HDR;
uniform vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
varying vec4 xlv_TEXCOORD6;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  worldN_1.x = dot (xlv_TEXCOORD1.xyz, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2.xyz, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3.xyz, normal_3);
  vec4 tmpvar_4;
  tmpvar_4 = texture2D (unity_DynamicLightmap, xlv_TEXCOORD6.zw);
  vec4 c_5;
  vec4 c_6;
  c_6.xyz = ((tmpvar_2.xyz * _LightColor0.xyz) * max (0.0, dot (worldN_1, _WorldSpaceLightPos0.xyz)));
  c_6.w = tmpvar_2.w;
  c_5.w = c_6.w;
  c_5.xyz = (c_6.xyz + (tmpvar_2.xyz * pow (
    ((unity_DynamicLightmap_HDR.x * tmpvar_4.w) * tmpvar_4.xyz)
  , unity_DynamicLightmap_HDR.yyy)));
  gl_FragData[0] = c_5;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 35 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord2" TexCoord2
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 12 [_BumpMap_ST]
Vector 11 [_MainTex_ST]
Vector 10 [unity_DynamicLightmapST]
"vs_3_0
def c13, 0, 0, 0, 0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dcl_texcoord2 v4
dcl_position o0
dcl_texcoord o1
dcl_texcoord1 o2
dcl_texcoord2 o3
dcl_texcoord3 o4
dcl_texcoord6 o5
dp4 o0.x, c0, v0
dp4 o0.y, c1, v0
dp4 o0.z, c2, v0
dp4 o0.w, c3, v0
mad o1.xy, v3, c11, c11.zwzw
mad o1.zw, v3.xyxy, c12.xyxy, c12
dp4 o2.w, c4, v0
dp4 o3.w, c5, v0
dp4 o4.w, c6, v0
mad o5.zw, v4.xyxy, c10.xyxy, c10
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov o2.x, r0.z
mul r1.xyz, c8.zxyw, v2.y
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov o2.y, r2.x
mov o2.z, r1.y
mov o3.x, r0.x
mov o4.x, r0.y
mov o3.y, r2.y
mov o4.y, r2.z
mov o3.z, r1.z
mov o4.z, r1.x
mov o5.xy, c13.x

"
}
SubProgram "d3d11 " {
// Stats: 34 math
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" "VERTEXLIGHT_ON" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord2" TexCoord2
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
ConstBuffer "UnityLightmaps" 32
Vector 16 [unity_DynamicLightmapST]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
BindCB  "UnityLightmaps" 2
"vs_4_0
eefiecedbgefnhdphgolocnmpicpcgphbjcolpmeabaaaaaadeaiaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapadaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaapaaaaaakeaaaaaaagaaaaaaaaaaaaaaadaaaaaaafaaaaaa
apaaaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
faagaaaaeaaaabaajeabaaaafjaaaaaeegiocaaaaaaaaaaaamaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafjaaaaaeegiocaaaacaaaaaaacaaaaaafpaaaaad
pcbabaaaaaaaaaaafpaaaaadpcbabaaaabaaaaaafpaaaaadhcbabaaaacaaaaaa
fpaaaaaddcbabaaaadaaaaaafpaaaaaddcbabaaaafaaaaaaghaaaaaepccabaaa
aaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadpccabaaaacaaaaaa
gfaaaaadpccabaaaadaaaaaagfaaaaadpccabaaaaeaaaaaagfaaaaadpccabaaa
afaaaaaagiaaaaacaeaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaaaaaaaaaa
egiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaa
aaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaaaaaaaaaa
egiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaak
pccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaaegaobaaa
aaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaaaaaaaaaa
akaaaaaaogikcaaaaaaaaaaaakaaaaaadcaaaaalmccabaaaabaaaaaaagbebaaa
adaaaaaaagiecaaaaaaaaaaaalaaaaaakgiocaaaaaaaaaaaalaaaaaadiaaaaai
ccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaadiaaaaai
ccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaaiccaabaaa
abaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaaiecaabaaa
abaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaaibcaabaaa
abaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaahhcaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaa
egacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaa
aaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaaaaaaaaaa
dgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaaabaaaaaa
fgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaabaaaaaa
jgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaadcaaaaak
hcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaaegacbaaa
abaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaa
eeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaabaaaaaa
pgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaaegacbaaa
aaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaaaaaaaaaa
jgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaaakaabaaa
acaaaaaadiaaaaaihcaabaaaadaaaaaafgbfbaaaaaaaaaaaegiccaaaabaaaaaa
anaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaaabaaaaaaamaaaaaaagbabaaa
aaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaaabaaaaaa
aoaaaaaakgbkbaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaadaaaaaa
egiccaaaabaaaaaaapaaaaaapgbpbaaaaaaaaaaaegacbaaaadaaaaaadgaaaaaf
iccabaaaacaaaaaaakaabaaaadaaaaaadgaaaaafbccabaaaacaaaaaackaabaaa
abaaaaaadgaaaaafeccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaa
aeaaaaaaakaabaaaaaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaa
dgaaaaafbccabaaaaeaaaaaabkaabaaaabaaaaaadgaaaaaficcabaaaadaaaaaa
bkaabaaaadaaaaaadgaaaaaficcabaaaaeaaaaaackaabaaaadaaaaaadgaaaaaf
cccabaaaadaaaaaabkaabaaaacaaaaaadgaaaaafcccabaaaaeaaaaaackaabaaa
acaaaaaadcaaaaalmccabaaaafaaaaaaagbebaaaafaaaaaaagiecaaaacaaaaaa
abaaaaaakgiocaaaacaaaaaaabaaaaaadgaaaaaidccabaaaafaaaaaaaceaaaaa
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadoaaaaab"
}
}
Program "fp" {
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 19 math, 2 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
Vector 2 [_Color]
Vector 1 [_LightColor0]
Vector 0 [_WorldSpaceLightPos0]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BumpMap] 2D 1
"ps_2_0
def c3, 2, -1, 0, 1
dcl t0
dcl t1.xyz
dcl t2.xyz
dcl t3.xyz
dcl_pp t4.xyz
dcl_2d s0
dcl_2d s1
mov r0.x, t0.z
mov r0.y, t0.w
texld_pp r0, r0, s1
texld r1, t0, s0
mad_pp r2.x, r0.w, c3.x, c3.y
mad_pp r2.y, r0.y, c3.x, c3.y
dp2add_sat_pp r2.w, r2, r2, c3.z
add_pp r2.w, -r2.w, c3.w
rsq_pp r2.w, r2.w
rcp_pp r2.z, r2.w
dp3_pp r0.x, t1, r2
dp3_pp r0.y, t2, r2
dp3_pp r0.z, t3, r2
dp3_pp r0.x, r0, c0
max_pp r2.x, r0.x, c3.z
mul_pp r0, r1, c2
mul_pp r1.xyz, r0, c1
mul_pp r2.yzw, r0.wzyx, t4.wzyx
mad_pp r0.xyz, r1, r2.x, r2.wzyx
mov_pp oC0, r0

"
}
SubProgram "d3d11 " {
// Stats: 14 math, 2 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BumpMap] 2D 1
ConstBuffer "$Globals" 192
Vector 96 [_LightColor0]
Vector 144 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0
eefiecedppeamjgglkgeeploobhmhmnfhpmopmnfabaaaaaaoiadaaaaadaaaaaa
cmaaaaaaoeaaaaaabiabaaaaejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapahaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapahaaaakeaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcmiacaaaa
eaaaaaaalcaaaaaafjaaaaaeegiocaaaaaaaaaaaakaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaa
gcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaa
adaaaaaagcbaaaadhcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacacaaaaaaefaaaaajpcaabaaaaaaaaaaaogbkbaaa
abaaaaaaeghobaaaabaaaaaaaagabaaaabaaaaaadcaaaaapdcaabaaaaaaaaaaa
hgapbaaaaaaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaa
aaaaialpaaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaa
aaaaaaaaegaabaaaaaaaaaaaddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaa
abeaaaaaaaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaa
abeaaaaaaaaaiadpelaaaaafecaabaaaaaaaaaaadkaabaaaaaaaaaaabaaaaaah
bcaabaaaabaaaaaaegbcbaaaacaaaaaaegacbaaaaaaaaaaabaaaaaahccaabaaa
abaaaaaaegbcbaaaadaaaaaaegacbaaaaaaaaaaabaaaaaahecaabaaaabaaaaaa
egbcbaaaaeaaaaaaegacbaaaaaaaaaaabaaaaaaibcaabaaaaaaaaaaaegacbaaa
abaaaaaaegiccaaaabaaaaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaa
aaaaaaaaabeaaaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaa
abaaaaaaegiocaaaaaaaaaaaajaaaaaadiaaaaaiocaabaaaaaaaaaaaagajbaaa
abaaaaaaagijcaaaaaaaaaaaagaaaaaadiaaaaahhcaabaaaabaaaaaaegacbaaa
abaaaaaaegbcbaaaafaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaa
dcaaaaajhccabaaaaaaaaaaajgahbaaaaaaaaaaaagaabaaaaaaaaaaaegacbaaa
abaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 14 math, 2 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BumpMap] 2D 1
ConstBuffer "$Globals" 192
Vector 96 [_LightColor0]
Vector 144 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0_level_9_1
eefieceddjjagcpjbfiikhhhnckbakkempbgoggcabaaaaaapeafaaaaaeaaaaaa
daaaaaaadiacaaaaaiafaaaamaafaaaaebgpgodjaaacaaaaaaacaaaaaaacpppp
laabaaaafaaaaaaaadaacmaaaaaafaaaaaaafaaaacaaceaaaaaafaaaaaaaaaaa
abababaaaaaaagaaabaaaaaaaaaaaaaaaaaaajaaabaaabaaaaaaaaaaabaaaaaa
abaaacaaaaaaaaaaaaacppppfbaaaaafadaaapkaaaaaaaeaaaaaialpaaaaaaaa
aaaaiadpbpaaaaacaaaaaaiaaaaaaplabpaaaaacaaaaaaiaabaaaplabpaaaaac
aaaaaaiaacaaaplabpaaaaacaaaaaaiaadaaaplabpaaaaacaaaaaaiaaeaachla
bpaaaaacaaaaaajaaaaiapkabpaaaaacaaaaaajaabaiapkaabaaaaacaaaaabia
aaaakklaabaaaaacaaaaaciaaaaapplaecaaaaadaaaacpiaaaaaoeiaabaioeka
ecaaaaadabaaapiaaaaaoelaaaaioekaaeaaaaaeacaacbiaaaaappiaadaaaaka
adaaffkaaeaaaaaeacaacciaaaaaffiaadaaaakaadaaffkafkaaaaaeacaadiia
acaaoeiaacaaoeiaadaakkkaacaaaaadacaaciiaacaappibadaappkaahaaaaac
acaaciiaacaappiaagaaaaacacaaceiaacaappiaaiaaaaadaaaacbiaabaaoela
acaaoeiaaiaaaaadaaaacciaacaaoelaacaaoeiaaiaaaaadaaaaceiaadaaoela
acaaoeiaaiaaaaadaaaacbiaaaaaoeiaacaaoekaalaaaaadacaacbiaaaaaaaia
adaakkkaafaaaaadaaaacpiaabaaoeiaabaaoekaafaaaaadabaachiaaaaaoeia
aaaaoekaafaaaaadacaacoiaaaaabliaaeaabllaaeaaaaaeaaaachiaabaaoeia
acaaaaiaacaabliaabaaaaacaaaicpiaaaaaoeiappppaaaafdeieefcmiacaaaa
eaaaaaaalcaaaaaafjaaaaaeegiocaaaaaaaaaaaakaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaa
gcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaa
adaaaaaagcbaaaadhcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacacaaaaaaefaaaaajpcaabaaaaaaaaaaaogbkbaaa
abaaaaaaeghobaaaabaaaaaaaagabaaaabaaaaaadcaaaaapdcaabaaaaaaaaaaa
hgapbaaaaaaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaa
aaaaialpaaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaa
aaaaaaaaegaabaaaaaaaaaaaddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaa
abeaaaaaaaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaa
abeaaaaaaaaaiadpelaaaaafecaabaaaaaaaaaaadkaabaaaaaaaaaaabaaaaaah
bcaabaaaabaaaaaaegbcbaaaacaaaaaaegacbaaaaaaaaaaabaaaaaahccaabaaa
abaaaaaaegbcbaaaadaaaaaaegacbaaaaaaaaaaabaaaaaahecaabaaaabaaaaaa
egbcbaaaaeaaaaaaegacbaaaaaaaaaaabaaaaaaibcaabaaaaaaaaaaaegacbaaa
abaaaaaaegiccaaaabaaaaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaa
aaaaaaaaabeaaaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaa
abaaaaaaegiocaaaaaaaaaaaajaaaaaadiaaaaaiocaabaaaaaaaaaaaagajbaaa
abaaaaaaagijcaaaaaaaaaaaagaaaaaadiaaaaahhcaabaaaabaaaaaaegacbaaa
abaaaaaaegbcbaaaafaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaa
dcaaaaajhccabaaaaaaaaaaajgahbaaaaaaaaaaaagaabaaaaaaaaaaaegacbaaa
abaaaaaadoaaaaabejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapahaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapahaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaapahaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
epfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
aaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 25 math, 3 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
Vector 3 [_Color]
Vector 2 [_LightColor0]
Vector 0 [_WorldSpaceLightPos0]
Vector 1 [unity_DynamicLightmap_HDR]
SetTexture 0 [unity_DynamicLightmap] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_BumpMap] 2D 2
"ps_3_0
def c4, 2, -1, 0, 1
dcl_texcoord v0
dcl_texcoord1 v1.xyz
dcl_texcoord2 v2.xyz
dcl_texcoord3 v3.xyz
dcl_texcoord6 v4.zw
dcl_2d s0
dcl_2d s1
dcl_2d s2
texld_pp r0, v0.zwzw, s2
mad_pp r0.xy, r0.wyzw, c4.x, c4.y
dp2add_sat_pp r0.w, r0, r0, c4.z
add_pp r0.w, -r0.w, c4.w
rsq_pp r0.w, r0.w
rcp_pp r0.z, r0.w
dp3_pp r1.x, v1, r0
dp3_pp r1.y, v2, r0
dp3_pp r1.z, v3, r0
dp3_pp r0.x, r1, c0
max_pp r1.x, r0.x, c4.z
texld_pp r0, v4.zwzw, s0
mul_pp r0.w, r0.w, c1.x
mul_pp r0.xyz, r0, r0.w
log_pp r2.x, r0.x
log_pp r2.y, r0.y
log_pp r2.z, r0.z
mul_pp r0.xyz, r2, c1.y
exp_pp r2.x, r0.x
exp_pp r2.y, r0.y
exp_pp r2.z, r0.z
texld r0, v0, s1
mul_pp r0, r0, c3
mul_pp r1.yzw, r2.xxyz, r0.xxyz
mul_pp r0.xyz, r0, c2
mov_pp oC0.w, r0.w
mad_pp oC0.xyz, r0, r1.x, r1.yzww

"
}
SubProgram "d3d11 " {
// Stats: 19 math, 3 textures
Keywords { "DIRECTIONAL" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_ON" }
SetTexture 0 [_MainTex] 2D 1
SetTexture 1 [_BumpMap] 2D 2
SetTexture 2 [unity_DynamicLightmap] 2D 0
ConstBuffer "$Globals" 192
Vector 80 [unity_DynamicLightmap_HDR]
Vector 96 [_LightColor0]
Vector 144 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0
eefiecedkehebhiknnldmhepfeagoknenbilplaoabaaaaaakmaeaaaaadaaaaaa
cmaaaaaaoeaaaaaabiabaaaaejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapahaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapahaaaakeaaaaaaagaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapamaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcimadaaaa
eaaaaaaaodaaaaaafjaaaaaeegiocaaaaaaaaaaaakaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fkaaaaadaagabaaaacaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaae
aahabaaaabaaaaaaffffaaaafibiaaaeaahabaaaacaaaaaaffffaaaagcbaaaad
pcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaaadaaaaaa
gcbaaaadhcbabaaaaeaaaaaagcbaaaadmcbabaaaafaaaaaagfaaaaadpccabaaa
aaaaaaaagiaaaaacacaaaaaaefaaaaajpcaabaaaaaaaaaaaogbkbaaaabaaaaaa
eghobaaaabaaaaaaaagabaaaacaaaaaadcaaaaapdcaabaaaaaaaaaaahgapbaaa
aaaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaaaaaaialp
aaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaaaaaaaaa
egaabaaaaaaaaaaaddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaa
aaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaaabeaaaaa
aaaaiadpelaaaaafecaabaaaaaaaaaaadkaabaaaaaaaaaaabaaaaaahbcaabaaa
abaaaaaaegbcbaaaacaaaaaaegacbaaaaaaaaaaabaaaaaahccaabaaaabaaaaaa
egbcbaaaadaaaaaaegacbaaaaaaaaaaabaaaaaahecaabaaaabaaaaaaegbcbaaa
aeaaaaaaegacbaaaaaaaaaaabaaaaaaibcaabaaaaaaaaaaaegacbaaaabaaaaaa
egiccaaaabaaaaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaa
abeaaaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaogbkbaaaafaaaaaaeghobaaa
acaaaaaaaagabaaaaaaaaaaadiaaaaaiccaabaaaaaaaaaaadkaabaaaabaaaaaa
akiacaaaaaaaaaaaafaaaaaadiaaaaahocaabaaaaaaaaaaaagajbaaaabaaaaaa
fgafbaaaaaaaaaaacpaaaaafocaabaaaaaaaaaaafgaobaaaaaaaaaaadiaaaaai
ocaabaaaaaaaaaaafgaobaaaaaaaaaaafgifcaaaaaaaaaaaafaaaaaabjaaaaaf
ocaabaaaaaaaaaaafgaobaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaa
abaaaaaaeghobaaaaaaaaaaaaagabaaaabaaaaaadiaaaaaipcaabaaaabaaaaaa
egaobaaaabaaaaaaegiocaaaaaaaaaaaajaaaaaadiaaaaahocaabaaaaaaaaaaa
fgaobaaaaaaaaaaaagajbaaaabaaaaaadiaaaaaihcaabaaaabaaaaaaegacbaaa
abaaaaaaegiccaaaaaaaaaaaagaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaa
abaaaaaadcaaaaajhccabaaaaaaaaaaaegacbaaaabaaaaaaagaabaaaaaaaaaaa
jgahbaaaaaaaaaaadoaaaaab"
}
}
 }


 // Stats for Vertex shader:
 //       d3d11 : 33 math
 //    d3d11_9x : 33 math
 //        d3d9 : 33 math
 //      opengl : 19 avg math (14..26), 3 avg texture (2..4)
 // Stats for Fragment shader:
 //       d3d11 : 21 avg math (13..29), 3 avg texture (2..4)
 //    d3d11_9x : 21 avg math (13..29), 3 avg texture (2..4)
 //        d3d9 : 26 avg math (18..34), 3 avg texture (2..4)
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Blend SrcAlpha One
  ColorMask RGB
  GpuProgramID 95184
Program "vp" {
SubProgram "opengl " {
// Stats: 20 math, 3 textures
Keywords { "POINT" }
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec4 v_2;
  v_2.x = _World2Object[0].x;
  v_2.y = _World2Object[1].x;
  v_2.z = _World2Object[2].x;
  v_2.w = _World2Object[3].x;
  vec4 v_3;
  v_3.x = _World2Object[0].y;
  v_3.y = _World2Object[1].y;
  v_3.z = _World2Object[2].y;
  v_3.w = _World2Object[3].y;
  vec4 v_4;
  v_4.x = _World2Object[0].z;
  v_4.y = _World2Object[1].z;
  v_4.z = _World2Object[2].z;
  v_4.w = _World2Object[3].z;
  vec3 tmpvar_5;
  tmpvar_5 = normalize(((
    (v_2.xyz * gl_Normal.x)
   + 
    (v_3.xyz * gl_Normal.y)
  ) + (v_4.xyz * gl_Normal.z)));
  mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  vec3 tmpvar_7;
  tmpvar_7 = normalize((tmpvar_6 * TANGENT.xyz));
  vec3 tmpvar_8;
  tmpvar_8 = (((tmpvar_5.yzx * tmpvar_7.zxy) - (tmpvar_5.zxy * tmpvar_7.yzx)) * TANGENT.w);
  vec3 tmpvar_9;
  tmpvar_9.x = tmpvar_7.x;
  tmpvar_9.y = tmpvar_8.x;
  tmpvar_9.z = tmpvar_5.x;
  vec3 tmpvar_10;
  tmpvar_10.x = tmpvar_7.y;
  tmpvar_10.y = tmpvar_8.y;
  tmpvar_10.z = tmpvar_5.y;
  vec3 tmpvar_11;
  tmpvar_11.x = tmpvar_7.z;
  tmpvar_11.y = tmpvar_8.z;
  tmpvar_11.z = tmpvar_5.z;
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_9;
  xlv_TEXCOORD2 = tmpvar_10;
  xlv_TEXCOORD3 = tmpvar_11;
  xlv_TEXCOORD4 = (_Object2World * gl_Vertex).xyz;
}


#endif
#ifdef FRAGMENT
uniform vec4 _WorldSpaceLightPos0;
uniform vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform mat4 _LightMatrix0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = xlv_TEXCOORD4;
  vec3 tmpvar_5;
  tmpvar_5 = (_LightMatrix0 * tmpvar_4).xyz;
  worldN_1.x = dot (xlv_TEXCOORD1, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3, normal_3);
  vec4 c_6;
  vec4 c_7;
  c_7.xyz = ((tmpvar_2.xyz * (_LightColor0.xyz * texture2D (_LightTexture0, vec2(
    dot (tmpvar_5, tmpvar_5)
  )).w)) * max (0.0, dot (worldN_1, 
    normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4))
  )));
  c_7.w = tmpvar_2.w;
  c_6.w = c_7.w;
  c_6.xyz = c_7.xyz;
  gl_FragData[0] = c_6;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 33 math
Keywords { "POINT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 11 [_BumpMap_ST]
Vector 10 [_MainTex_ST]
"vs_2_0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v3, c10, c10.zwzw
mad oT0.zw, v3.xyxy, c11.xyxy, c11
dp4 oT4.x, c4, v0
dp4 oT4.y, c5, v0
dp4 oT4.z, c6, v0
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r1.xyz, v2.y, c8.zxyw
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov oT1.y, r2.x
mov oT1.z, r1.y
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r2.y
mov oT3.y, r2.z
mov oT2.z, r1.z
mov oT3.z, r1.x

"
}
SubProgram "d3d11 " {
// Stats: 33 math
Keywords { "POINT" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefieceddhghnbmamjojmnfpekgacngjhkdldebiabaaaaaajaahaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaa
ahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 33 math
Keywords { "POINT" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0_level_9_1
eefiecedlaaiejlfkbgmijemhnpfeoggoifjdhjbabaaaaaaoaakaaaaaeaaaaaa
daaaaaaahmadaaaadaajaaaaciakaaaaebgpgodjeeadaaaaeeadaaaaaaacpopp
piacaaaaemaaaaaaadaaceaaaaaaeiaaaaaaeiaaaaaaceaaabaaeiaaaaaaaoaa
acaaabaaaaaaaaaaabaaaaaaaeaaadaaaaaaaaaaabaaamaaahaaahaaaaaaaaaa
aaaaaaaaaaacpoppbpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabiaabaaapja
bpaaaaacafaaaciaacaaapjabpaaaaacafaaadiaadaaapjaaeaaaaaeaaaaadoa
adaaoejaabaaoekaabaaookaaeaaaaaeaaaaamoaadaaeejaacaaeekaacaaoeka
afaaaaadaaaaahiaaaaaffjaaiaaoekaaeaaaaaeaaaaahiaahaaoekaaaaaaaja
aaaaoeiaaeaaaaaeaaaaahiaajaaoekaaaaakkjaaaaaoeiaaeaaaaaeaeaaahoa
akaaoekaaaaappjaaaaaoeiaafaaaaadaaaaapiaaaaaffjaaeaaoekaaeaaaaae
aaaaapiaadaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapiaafaaoekaaaaakkja
aaaaoeiaaeaaaaaeaaaaapiaagaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadma
aaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeiaafaaaaadaaaaahia
abaaffjaaiaamjkaaeaaaaaeaaaaahiaahaamjkaabaaaajaaaaaoeiaaeaaaaae
aaaaahiaajaamjkaabaakkjaaaaaoeiaaiaaaaadaaaaaiiaaaaaoeiaaaaaoeia
ahaaaaacaaaaaiiaaaaappiaafaaaaadaaaaahiaaaaappiaaaaaoeiaabaaaaac
abaaaboaaaaakkiaafaaaaadabaaaciaacaaaajaalaaaakaafaaaaadabaaaeia
acaaaajaamaaaakaafaaaaadabaaabiaacaaaajaanaaaakaafaaaaadacaaacia
acaaffjaalaaffkaafaaaaadacaaaeiaacaaffjaamaaffkaafaaaaadacaaabia
acaaffjaanaaffkaacaaaaadabaaahiaabaaoeiaacaaoeiaafaaaaadacaaacia
acaakkjaalaakkkaafaaaaadacaaaeiaacaakkjaamaakkkaafaaaaadacaaabia
acaakkjaanaakkkaacaaaaadabaaahiaabaaoeiaacaaoeiaaiaaaaadaaaaaiia
abaaoeiaabaaoeiaahaaaaacaaaaaiiaaaaappiaafaaaaadabaaahiaaaaappia
abaaoeiaafaaaaadacaaahiaaaaaoeiaabaaoeiaaeaaaaaeacaaahiaabaancia
aaaamjiaacaaoeibafaaaaadacaaahiaacaaoeiaabaappjaabaaaaacabaaacoa
acaaaaiaabaaaaacabaaaeoaabaaffiaabaaaaacacaaaboaaaaaaaiaabaaaaac
adaaaboaaaaaffiaabaaaaacacaaacoaacaaffiaabaaaaacadaaacoaacaakkia
abaaaaacacaaaeoaabaakkiaabaaaaacadaaaeoaabaaaaiappppaaaafdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaabejfdeheopaaaaaaaaiaaaaaaaiaaaaaa
miaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
acaaaaaaahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaa
oaaaaaaaabaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaa
aaaaaaaaadaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaa
agaaaaaaapaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaa
faepfdejfeejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfcee
aaedepemepfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
"
}
SubProgram "opengl " {
// Stats: 14 math, 2 textures
Keywords { "DIRECTIONAL" }
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec4 v_2;
  v_2.x = _World2Object[0].x;
  v_2.y = _World2Object[1].x;
  v_2.z = _World2Object[2].x;
  v_2.w = _World2Object[3].x;
  vec4 v_3;
  v_3.x = _World2Object[0].y;
  v_3.y = _World2Object[1].y;
  v_3.z = _World2Object[2].y;
  v_3.w = _World2Object[3].y;
  vec4 v_4;
  v_4.x = _World2Object[0].z;
  v_4.y = _World2Object[1].z;
  v_4.z = _World2Object[2].z;
  v_4.w = _World2Object[3].z;
  vec3 tmpvar_5;
  tmpvar_5 = normalize(((
    (v_2.xyz * gl_Normal.x)
   + 
    (v_3.xyz * gl_Normal.y)
  ) + (v_4.xyz * gl_Normal.z)));
  mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  vec3 tmpvar_7;
  tmpvar_7 = normalize((tmpvar_6 * TANGENT.xyz));
  vec3 tmpvar_8;
  tmpvar_8 = (((tmpvar_5.yzx * tmpvar_7.zxy) - (tmpvar_5.zxy * tmpvar_7.yzx)) * TANGENT.w);
  vec3 tmpvar_9;
  tmpvar_9.x = tmpvar_7.x;
  tmpvar_9.y = tmpvar_8.x;
  tmpvar_9.z = tmpvar_5.x;
  vec3 tmpvar_10;
  tmpvar_10.x = tmpvar_7.y;
  tmpvar_10.y = tmpvar_8.y;
  tmpvar_10.z = tmpvar_5.y;
  vec3 tmpvar_11;
  tmpvar_11.x = tmpvar_7.z;
  tmpvar_11.y = tmpvar_8.z;
  tmpvar_11.z = tmpvar_5.z;
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_9;
  xlv_TEXCOORD2 = tmpvar_10;
  xlv_TEXCOORD3 = tmpvar_11;
  xlv_TEXCOORD4 = (_Object2World * gl_Vertex).xyz;
}


#endif
#ifdef FRAGMENT
uniform vec4 _WorldSpaceLightPos0;
uniform vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  worldN_1.x = dot (xlv_TEXCOORD1, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3, normal_3);
  vec4 c_4;
  vec4 c_5;
  c_5.xyz = ((tmpvar_2.xyz * _LightColor0.xyz) * max (0.0, dot (worldN_1, _WorldSpaceLightPos0.xyz)));
  c_5.w = tmpvar_2.w;
  c_4.w = c_5.w;
  c_4.xyz = c_5.xyz;
  gl_FragData[0] = c_4;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 33 math
Keywords { "DIRECTIONAL" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 11 [_BumpMap_ST]
Vector 10 [_MainTex_ST]
"vs_2_0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v3, c10, c10.zwzw
mad oT0.zw, v3.xyxy, c11.xyxy, c11
dp4 oT4.x, c4, v0
dp4 oT4.y, c5, v0
dp4 oT4.z, c6, v0
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r1.xyz, v2.y, c8.zxyw
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov oT1.y, r2.x
mov oT1.z, r1.y
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r2.y
mov oT3.y, r2.z
mov oT2.z, r1.z
mov oT3.z, r1.x

"
}
SubProgram "d3d11 " {
// Stats: 33 math
Keywords { "DIRECTIONAL" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefiecedacndjggefnfciclbfgefdjbeimojoaigabaaaaaajaahaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaa
ahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaaamaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaakaaaaaaogikcaaaaaaaaaaaakaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaalaaaaaakgiocaaaaaaaaaaaalaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 33 math
Keywords { "DIRECTIONAL" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 160 [_MainTex_ST]
Vector 176 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0_level_9_1
eefiecedajlcejdcfaplecdiflgkbjikochajcnnabaaaaaaoaakaaaaaeaaaaaa
daaaaaaahmadaaaadaajaaaaciakaaaaebgpgodjeeadaaaaeeadaaaaaaacpopp
piacaaaaemaaaaaaadaaceaaaaaaeiaaaaaaeiaaaaaaceaaabaaeiaaaaaaakaa
acaaabaaaaaaaaaaabaaaaaaaeaaadaaaaaaaaaaabaaamaaahaaahaaaaaaaaaa
aaaaaaaaaaacpoppbpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabiaabaaapja
bpaaaaacafaaaciaacaaapjabpaaaaacafaaadiaadaaapjaaeaaaaaeaaaaadoa
adaaoejaabaaoekaabaaookaaeaaaaaeaaaaamoaadaaeejaacaaeekaacaaoeka
afaaaaadaaaaahiaaaaaffjaaiaaoekaaeaaaaaeaaaaahiaahaaoekaaaaaaaja
aaaaoeiaaeaaaaaeaaaaahiaajaaoekaaaaakkjaaaaaoeiaaeaaaaaeaeaaahoa
akaaoekaaaaappjaaaaaoeiaafaaaaadaaaaapiaaaaaffjaaeaaoekaaeaaaaae
aaaaapiaadaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapiaafaaoekaaaaakkja
aaaaoeiaaeaaaaaeaaaaapiaagaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadma
aaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeiaafaaaaadaaaaahia
abaaffjaaiaamjkaaeaaaaaeaaaaahiaahaamjkaabaaaajaaaaaoeiaaeaaaaae
aaaaahiaajaamjkaabaakkjaaaaaoeiaaiaaaaadaaaaaiiaaaaaoeiaaaaaoeia
ahaaaaacaaaaaiiaaaaappiaafaaaaadaaaaahiaaaaappiaaaaaoeiaabaaaaac
abaaaboaaaaakkiaafaaaaadabaaaciaacaaaajaalaaaakaafaaaaadabaaaeia
acaaaajaamaaaakaafaaaaadabaaabiaacaaaajaanaaaakaafaaaaadacaaacia
acaaffjaalaaffkaafaaaaadacaaaeiaacaaffjaamaaffkaafaaaaadacaaabia
acaaffjaanaaffkaacaaaaadabaaahiaabaaoeiaacaaoeiaafaaaaadacaaacia
acaakkjaalaakkkaafaaaaadacaaaeiaacaakkjaamaakkkaafaaaaadacaaabia
acaakkjaanaakkkaacaaaaadabaaahiaabaaoeiaacaaoeiaaiaaaaadaaaaaiia
abaaoeiaabaaoeiaahaaaaacaaaaaiiaaaaappiaafaaaaadabaaahiaaaaappia
abaaoeiaafaaaaadacaaahiaaaaaoeiaabaaoeiaaeaaaaaeacaaahiaabaancia
aaaamjiaacaaoeibafaaaaadacaaahiaacaaoeiaabaappjaabaaaaacabaaacoa
acaaaaiaabaaaaacabaaaeoaabaaffiaabaaaaacacaaaboaaaaaaaiaabaaaaac
adaaaboaaaaaffiaabaaaaacacaaacoaacaaffiaabaaaaacadaaacoaacaakkia
abaaaaacacaaaeoaabaakkiaabaaaaacadaaaeoaabaaaaiappppaaaafdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaaamaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaakaaaaaaogikcaaaaaaaaaaaakaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaalaaaaaakgiocaaaaaaaaaaaalaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaabejfdeheopaaaaaaaaiaaaaaaaiaaaaaa
miaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
acaaaaaaahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaa
oaaaaaaaabaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaa
aaaaaaaaadaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaa
agaaaaaaapaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaa
faepfdejfeejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfcee
aaedepemepfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
"
}
SubProgram "opengl " {
// Stats: 26 math, 4 textures
Keywords { "SPOT" }
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec4 v_2;
  v_2.x = _World2Object[0].x;
  v_2.y = _World2Object[1].x;
  v_2.z = _World2Object[2].x;
  v_2.w = _World2Object[3].x;
  vec4 v_3;
  v_3.x = _World2Object[0].y;
  v_3.y = _World2Object[1].y;
  v_3.z = _World2Object[2].y;
  v_3.w = _World2Object[3].y;
  vec4 v_4;
  v_4.x = _World2Object[0].z;
  v_4.y = _World2Object[1].z;
  v_4.z = _World2Object[2].z;
  v_4.w = _World2Object[3].z;
  vec3 tmpvar_5;
  tmpvar_5 = normalize(((
    (v_2.xyz * gl_Normal.x)
   + 
    (v_3.xyz * gl_Normal.y)
  ) + (v_4.xyz * gl_Normal.z)));
  mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  vec3 tmpvar_7;
  tmpvar_7 = normalize((tmpvar_6 * TANGENT.xyz));
  vec3 tmpvar_8;
  tmpvar_8 = (((tmpvar_5.yzx * tmpvar_7.zxy) - (tmpvar_5.zxy * tmpvar_7.yzx)) * TANGENT.w);
  vec3 tmpvar_9;
  tmpvar_9.x = tmpvar_7.x;
  tmpvar_9.y = tmpvar_8.x;
  tmpvar_9.z = tmpvar_5.x;
  vec3 tmpvar_10;
  tmpvar_10.x = tmpvar_7.y;
  tmpvar_10.y = tmpvar_8.y;
  tmpvar_10.z = tmpvar_5.y;
  vec3 tmpvar_11;
  tmpvar_11.x = tmpvar_7.z;
  tmpvar_11.y = tmpvar_8.z;
  tmpvar_11.z = tmpvar_5.z;
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_9;
  xlv_TEXCOORD2 = tmpvar_10;
  xlv_TEXCOORD3 = tmpvar_11;
  xlv_TEXCOORD4 = (_Object2World * gl_Vertex).xyz;
}


#endif
#ifdef FRAGMENT
uniform vec4 _WorldSpaceLightPos0;
uniform vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform mat4 _LightMatrix0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = xlv_TEXCOORD4;
  vec4 tmpvar_5;
  tmpvar_5 = (_LightMatrix0 * tmpvar_4);
  worldN_1.x = dot (xlv_TEXCOORD1, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3, normal_3);
  vec4 c_6;
  vec4 c_7;
  c_7.xyz = ((tmpvar_2.xyz * (_LightColor0.xyz * 
    ((float((tmpvar_5.z > 0.0)) * texture2D (_LightTexture0, ((tmpvar_5.xy / tmpvar_5.w) + 0.5)).w) * texture2D (_LightTextureB0, vec2(dot (tmpvar_5.xyz, tmpvar_5.xyz))).w)
  )) * max (0.0, dot (worldN_1, 
    normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4))
  )));
  c_7.w = tmpvar_2.w;
  c_6.w = c_7.w;
  c_6.xyz = c_7.xyz;
  gl_FragData[0] = c_6;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 33 math
Keywords { "SPOT" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 11 [_BumpMap_ST]
Vector 10 [_MainTex_ST]
"vs_2_0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v3, c10, c10.zwzw
mad oT0.zw, v3.xyxy, c11.xyxy, c11
dp4 oT4.x, c4, v0
dp4 oT4.y, c5, v0
dp4 oT4.z, c6, v0
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r1.xyz, v2.y, c8.zxyw
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov oT1.y, r2.x
mov oT1.z, r1.y
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r2.y
mov oT3.y, r2.z
mov oT2.z, r1.z
mov oT3.z, r1.x

"
}
SubProgram "d3d11 " {
// Stats: 33 math
Keywords { "SPOT" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefieceddhghnbmamjojmnfpekgacngjhkdldebiabaaaaaajaahaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaa
ahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 33 math
Keywords { "SPOT" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0_level_9_1
eefiecedlaaiejlfkbgmijemhnpfeoggoifjdhjbabaaaaaaoaakaaaaaeaaaaaa
daaaaaaahmadaaaadaajaaaaciakaaaaebgpgodjeeadaaaaeeadaaaaaaacpopp
piacaaaaemaaaaaaadaaceaaaaaaeiaaaaaaeiaaaaaaceaaabaaeiaaaaaaaoaa
acaaabaaaaaaaaaaabaaaaaaaeaaadaaaaaaaaaaabaaamaaahaaahaaaaaaaaaa
aaaaaaaaaaacpoppbpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabiaabaaapja
bpaaaaacafaaaciaacaaapjabpaaaaacafaaadiaadaaapjaaeaaaaaeaaaaadoa
adaaoejaabaaoekaabaaookaaeaaaaaeaaaaamoaadaaeejaacaaeekaacaaoeka
afaaaaadaaaaahiaaaaaffjaaiaaoekaaeaaaaaeaaaaahiaahaaoekaaaaaaaja
aaaaoeiaaeaaaaaeaaaaahiaajaaoekaaaaakkjaaaaaoeiaaeaaaaaeaeaaahoa
akaaoekaaaaappjaaaaaoeiaafaaaaadaaaaapiaaaaaffjaaeaaoekaaeaaaaae
aaaaapiaadaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapiaafaaoekaaaaakkja
aaaaoeiaaeaaaaaeaaaaapiaagaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadma
aaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeiaafaaaaadaaaaahia
abaaffjaaiaamjkaaeaaaaaeaaaaahiaahaamjkaabaaaajaaaaaoeiaaeaaaaae
aaaaahiaajaamjkaabaakkjaaaaaoeiaaiaaaaadaaaaaiiaaaaaoeiaaaaaoeia
ahaaaaacaaaaaiiaaaaappiaafaaaaadaaaaahiaaaaappiaaaaaoeiaabaaaaac
abaaaboaaaaakkiaafaaaaadabaaaciaacaaaajaalaaaakaafaaaaadabaaaeia
acaaaajaamaaaakaafaaaaadabaaabiaacaaaajaanaaaakaafaaaaadacaaacia
acaaffjaalaaffkaafaaaaadacaaaeiaacaaffjaamaaffkaafaaaaadacaaabia
acaaffjaanaaffkaacaaaaadabaaahiaabaaoeiaacaaoeiaafaaaaadacaaacia
acaakkjaalaakkkaafaaaaadacaaaeiaacaakkjaamaakkkaafaaaaadacaaabia
acaakkjaanaakkkaacaaaaadabaaahiaabaaoeiaacaaoeiaaiaaaaadaaaaaiia
abaaoeiaabaaoeiaahaaaaacaaaaaiiaaaaappiaafaaaaadabaaahiaaaaappia
abaaoeiaafaaaaadacaaahiaaaaaoeiaabaaoeiaaeaaaaaeacaaahiaabaancia
aaaamjiaacaaoeibafaaaaadacaaahiaacaaoeiaabaappjaabaaaaacabaaacoa
acaaaaiaabaaaaacabaaaeoaabaaffiaabaaaaacacaaaboaaaaaaaiaabaaaaac
adaaaboaaaaaffiaabaaaaacacaaacoaacaaffiaabaaaaacadaaacoaacaakkia
abaaaaacacaaaeoaabaakkiaabaaaaacadaaaeoaabaaaaiappppaaaafdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaabejfdeheopaaaaaaaaiaaaaaaaiaaaaaa
miaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
acaaaaaaahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaa
oaaaaaaaabaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaa
aaaaaaaaadaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaa
agaaaaaaapaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaa
faepfdejfeejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfcee
aaedepemepfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
"
}
SubProgram "opengl " {
// Stats: 21 math, 4 textures
Keywords { "POINT_COOKIE" }
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec4 v_2;
  v_2.x = _World2Object[0].x;
  v_2.y = _World2Object[1].x;
  v_2.z = _World2Object[2].x;
  v_2.w = _World2Object[3].x;
  vec4 v_3;
  v_3.x = _World2Object[0].y;
  v_3.y = _World2Object[1].y;
  v_3.z = _World2Object[2].y;
  v_3.w = _World2Object[3].y;
  vec4 v_4;
  v_4.x = _World2Object[0].z;
  v_4.y = _World2Object[1].z;
  v_4.z = _World2Object[2].z;
  v_4.w = _World2Object[3].z;
  vec3 tmpvar_5;
  tmpvar_5 = normalize(((
    (v_2.xyz * gl_Normal.x)
   + 
    (v_3.xyz * gl_Normal.y)
  ) + (v_4.xyz * gl_Normal.z)));
  mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  vec3 tmpvar_7;
  tmpvar_7 = normalize((tmpvar_6 * TANGENT.xyz));
  vec3 tmpvar_8;
  tmpvar_8 = (((tmpvar_5.yzx * tmpvar_7.zxy) - (tmpvar_5.zxy * tmpvar_7.yzx)) * TANGENT.w);
  vec3 tmpvar_9;
  tmpvar_9.x = tmpvar_7.x;
  tmpvar_9.y = tmpvar_8.x;
  tmpvar_9.z = tmpvar_5.x;
  vec3 tmpvar_10;
  tmpvar_10.x = tmpvar_7.y;
  tmpvar_10.y = tmpvar_8.y;
  tmpvar_10.z = tmpvar_5.y;
  vec3 tmpvar_11;
  tmpvar_11.x = tmpvar_7.z;
  tmpvar_11.y = tmpvar_8.z;
  tmpvar_11.z = tmpvar_5.z;
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_9;
  xlv_TEXCOORD2 = tmpvar_10;
  xlv_TEXCOORD3 = tmpvar_11;
  xlv_TEXCOORD4 = (_Object2World * gl_Vertex).xyz;
}


#endif
#ifdef FRAGMENT
uniform vec4 _WorldSpaceLightPos0;
uniform vec4 _LightColor0;
uniform samplerCube _LightTexture0;
uniform mat4 _LightMatrix0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = xlv_TEXCOORD4;
  vec3 tmpvar_5;
  tmpvar_5 = (_LightMatrix0 * tmpvar_4).xyz;
  worldN_1.x = dot (xlv_TEXCOORD1, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3, normal_3);
  vec4 c_6;
  vec4 c_7;
  c_7.xyz = ((tmpvar_2.xyz * (_LightColor0.xyz * 
    (texture2D (_LightTextureB0, vec2(dot (tmpvar_5, tmpvar_5))).w * textureCube (_LightTexture0, tmpvar_5).w)
  )) * max (0.0, dot (worldN_1, 
    normalize((_WorldSpaceLightPos0.xyz - xlv_TEXCOORD4))
  )));
  c_7.w = tmpvar_2.w;
  c_6.w = c_7.w;
  c_6.xyz = c_7.xyz;
  gl_FragData[0] = c_6;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 33 math
Keywords { "POINT_COOKIE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 11 [_BumpMap_ST]
Vector 10 [_MainTex_ST]
"vs_2_0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v3, c10, c10.zwzw
mad oT0.zw, v3.xyxy, c11.xyxy, c11
dp4 oT4.x, c4, v0
dp4 oT4.y, c5, v0
dp4 oT4.z, c6, v0
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r1.xyz, v2.y, c8.zxyw
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov oT1.y, r2.x
mov oT1.z, r1.y
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r2.y
mov oT3.y, r2.z
mov oT2.z, r1.z
mov oT3.z, r1.x

"
}
SubProgram "d3d11 " {
// Stats: 33 math
Keywords { "POINT_COOKIE" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefieceddhghnbmamjojmnfpekgacngjhkdldebiabaaaaaajaahaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaa
ahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 33 math
Keywords { "POINT_COOKIE" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0_level_9_1
eefiecedlaaiejlfkbgmijemhnpfeoggoifjdhjbabaaaaaaoaakaaaaaeaaaaaa
daaaaaaahmadaaaadaajaaaaciakaaaaebgpgodjeeadaaaaeeadaaaaaaacpopp
piacaaaaemaaaaaaadaaceaaaaaaeiaaaaaaeiaaaaaaceaaabaaeiaaaaaaaoaa
acaaabaaaaaaaaaaabaaaaaaaeaaadaaaaaaaaaaabaaamaaahaaahaaaaaaaaaa
aaaaaaaaaaacpoppbpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabiaabaaapja
bpaaaaacafaaaciaacaaapjabpaaaaacafaaadiaadaaapjaaeaaaaaeaaaaadoa
adaaoejaabaaoekaabaaookaaeaaaaaeaaaaamoaadaaeejaacaaeekaacaaoeka
afaaaaadaaaaahiaaaaaffjaaiaaoekaaeaaaaaeaaaaahiaahaaoekaaaaaaaja
aaaaoeiaaeaaaaaeaaaaahiaajaaoekaaaaakkjaaaaaoeiaaeaaaaaeaeaaahoa
akaaoekaaaaappjaaaaaoeiaafaaaaadaaaaapiaaaaaffjaaeaaoekaaeaaaaae
aaaaapiaadaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapiaafaaoekaaaaakkja
aaaaoeiaaeaaaaaeaaaaapiaagaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadma
aaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeiaafaaaaadaaaaahia
abaaffjaaiaamjkaaeaaaaaeaaaaahiaahaamjkaabaaaajaaaaaoeiaaeaaaaae
aaaaahiaajaamjkaabaakkjaaaaaoeiaaiaaaaadaaaaaiiaaaaaoeiaaaaaoeia
ahaaaaacaaaaaiiaaaaappiaafaaaaadaaaaahiaaaaappiaaaaaoeiaabaaaaac
abaaaboaaaaakkiaafaaaaadabaaaciaacaaaajaalaaaakaafaaaaadabaaaeia
acaaaajaamaaaakaafaaaaadabaaabiaacaaaajaanaaaakaafaaaaadacaaacia
acaaffjaalaaffkaafaaaaadacaaaeiaacaaffjaamaaffkaafaaaaadacaaabia
acaaffjaanaaffkaacaaaaadabaaahiaabaaoeiaacaaoeiaafaaaaadacaaacia
acaakkjaalaakkkaafaaaaadacaaaeiaacaakkjaamaakkkaafaaaaadacaaabia
acaakkjaanaakkkaacaaaaadabaaahiaabaaoeiaacaaoeiaaiaaaaadaaaaaiia
abaaoeiaabaaoeiaahaaaaacaaaaaiiaaaaappiaafaaaaadabaaahiaaaaappia
abaaoeiaafaaaaadacaaahiaaaaaoeiaabaaoeiaaeaaaaaeacaaahiaabaancia
aaaamjiaacaaoeibafaaaaadacaaahiaacaaoeiaabaappjaabaaaaacabaaacoa
acaaaaiaabaaaaacabaaaeoaabaaffiaabaaaaacacaaaboaaaaaaaiaabaaaaac
adaaaboaaaaaffiaabaaaaacacaaacoaacaaffiaabaaaaacadaaacoaacaakkia
abaaaaacacaaaeoaabaakkiaabaaaaacadaaaeoaabaaaaiappppaaaafdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaabejfdeheopaaaaaaaaiaaaaaaaiaaaaaa
miaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
acaaaaaaahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaa
oaaaaaaaabaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaa
aaaaaaaaadaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaa
agaaaaaaapaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaa
faepfdejfeejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfcee
aaedepemepfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
"
}
SubProgram "opengl " {
// Stats: 17 math, 3 textures
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 _MainTex_ST;
uniform vec4 _BumpMap_ST;
attribute vec4 TANGENT;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xy = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.zw = ((gl_MultiTexCoord0.xy * _BumpMap_ST.xy) + _BumpMap_ST.zw);
  vec4 v_2;
  v_2.x = _World2Object[0].x;
  v_2.y = _World2Object[1].x;
  v_2.z = _World2Object[2].x;
  v_2.w = _World2Object[3].x;
  vec4 v_3;
  v_3.x = _World2Object[0].y;
  v_3.y = _World2Object[1].y;
  v_3.z = _World2Object[2].y;
  v_3.w = _World2Object[3].y;
  vec4 v_4;
  v_4.x = _World2Object[0].z;
  v_4.y = _World2Object[1].z;
  v_4.z = _World2Object[2].z;
  v_4.w = _World2Object[3].z;
  vec3 tmpvar_5;
  tmpvar_5 = normalize(((
    (v_2.xyz * gl_Normal.x)
   + 
    (v_3.xyz * gl_Normal.y)
  ) + (v_4.xyz * gl_Normal.z)));
  mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  vec3 tmpvar_7;
  tmpvar_7 = normalize((tmpvar_6 * TANGENT.xyz));
  vec3 tmpvar_8;
  tmpvar_8 = (((tmpvar_5.yzx * tmpvar_7.zxy) - (tmpvar_5.zxy * tmpvar_7.yzx)) * TANGENT.w);
  vec3 tmpvar_9;
  tmpvar_9.x = tmpvar_7.x;
  tmpvar_9.y = tmpvar_8.x;
  tmpvar_9.z = tmpvar_5.x;
  vec3 tmpvar_10;
  tmpvar_10.x = tmpvar_7.y;
  tmpvar_10.y = tmpvar_8.y;
  tmpvar_10.z = tmpvar_5.y;
  vec3 tmpvar_11;
  tmpvar_11.x = tmpvar_7.z;
  tmpvar_11.y = tmpvar_8.z;
  tmpvar_11.z = tmpvar_5.z;
  gl_Position = (gl_ModelViewProjectionMatrix * gl_Vertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_9;
  xlv_TEXCOORD2 = tmpvar_10;
  xlv_TEXCOORD3 = tmpvar_11;
  xlv_TEXCOORD4 = (_Object2World * gl_Vertex).xyz;
}


#endif
#ifdef FRAGMENT
uniform vec4 _WorldSpaceLightPos0;
uniform vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform mat4 _LightMatrix0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
uniform vec4 _Color;
varying vec4 xlv_TEXCOORD0;
varying vec3 xlv_TEXCOORD1;
varying vec3 xlv_TEXCOORD2;
varying vec3 xlv_TEXCOORD3;
varying vec3 xlv_TEXCOORD4;
void main ()
{
  vec3 worldN_1;
  vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0.xy) * _Color);
  vec3 normal_3;
  normal_3.xy = ((texture2D (_BumpMap, xlv_TEXCOORD0.zw).wy * 2.0) - 1.0);
  normal_3.z = sqrt((1.0 - clamp (
    dot (normal_3.xy, normal_3.xy)
  , 0.0, 1.0)));
  vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = xlv_TEXCOORD4;
  worldN_1.x = dot (xlv_TEXCOORD1, normal_3);
  worldN_1.y = dot (xlv_TEXCOORD2, normal_3);
  worldN_1.z = dot (xlv_TEXCOORD3, normal_3);
  vec4 c_5;
  vec4 c_6;
  c_6.xyz = ((tmpvar_2.xyz * (_LightColor0.xyz * texture2D (_LightTexture0, 
    (_LightMatrix0 * tmpvar_4)
  .xy).w)) * max (0.0, dot (worldN_1, _WorldSpaceLightPos0.xyz)));
  c_6.w = tmpvar_2.w;
  c_5.w = c_6.w;
  c_5.xyz = c_6.xyz;
  gl_FragData[0] = c_5;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 33 math
Keywords { "DIRECTIONAL_COOKIE" }
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 11 [_BumpMap_ST]
Vector 10 [_MainTex_ST]
"vs_2_0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dp4 oPos.x, c0, v0
dp4 oPos.y, c1, v0
dp4 oPos.z, c2, v0
dp4 oPos.w, c3, v0
mad oT0.xy, v3, c10, c10.zwzw
mad oT0.zw, v3.xyxy, c11.xyxy, c11
dp4 oT4.x, c4, v0
dp4 oT4.y, c5, v0
dp4 oT4.z, c6, v0
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r1.xyz, v2.y, c8.zxyw
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov oT1.y, r2.x
mov oT1.z, r1.y
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r2.y
mov oT3.y, r2.z
mov oT2.z, r1.z
mov oT3.z, r1.x

"
}
SubProgram "d3d11 " {
// Stats: 33 math
Keywords { "DIRECTIONAL_COOKIE" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0
eefieceddhghnbmamjojmnfpekgacngjhkdldebiabaaaaaajaahaaaaadaaaaaa
cmaaaaaaceabaaaanmabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
apaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaakeaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaa
ahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklfdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 33 math
Keywords { "DIRECTIONAL_COOKIE" }
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 256
Vector 224 [_MainTex_ST]
Vector 240 [_BumpMap_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
"vs_4_0_level_9_1
eefiecedlaaiejlfkbgmijemhnpfeoggoifjdhjbabaaaaaaoaakaaaaaeaaaaaa
daaaaaaahmadaaaadaajaaaaciakaaaaebgpgodjeeadaaaaeeadaaaaaaacpopp
piacaaaaemaaaaaaadaaceaaaaaaeiaaaaaaeiaaaaaaceaaabaaeiaaaaaaaoaa
acaaabaaaaaaaaaaabaaaaaaaeaaadaaaaaaaaaaabaaamaaahaaahaaaaaaaaaa
aaaaaaaaaaacpoppbpaaaaacafaaaaiaaaaaapjabpaaaaacafaaabiaabaaapja
bpaaaaacafaaaciaacaaapjabpaaaaacafaaadiaadaaapjaaeaaaaaeaaaaadoa
adaaoejaabaaoekaabaaookaaeaaaaaeaaaaamoaadaaeejaacaaeekaacaaoeka
afaaaaadaaaaahiaaaaaffjaaiaaoekaaeaaaaaeaaaaahiaahaaoekaaaaaaaja
aaaaoeiaaeaaaaaeaaaaahiaajaaoekaaaaakkjaaaaaoeiaaeaaaaaeaeaaahoa
akaaoekaaaaappjaaaaaoeiaafaaaaadaaaaapiaaaaaffjaaeaaoekaaeaaaaae
aaaaapiaadaaoekaaaaaaajaaaaaoeiaaeaaaaaeaaaaapiaafaaoekaaaaakkja
aaaaoeiaaeaaaaaeaaaaapiaagaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadma
aaaappiaaaaaoekaaaaaoeiaabaaaaacaaaaammaaaaaoeiaafaaaaadaaaaahia
abaaffjaaiaamjkaaeaaaaaeaaaaahiaahaamjkaabaaaajaaaaaoeiaaeaaaaae
aaaaahiaajaamjkaabaakkjaaaaaoeiaaiaaaaadaaaaaiiaaaaaoeiaaaaaoeia
ahaaaaacaaaaaiiaaaaappiaafaaaaadaaaaahiaaaaappiaaaaaoeiaabaaaaac
abaaaboaaaaakkiaafaaaaadabaaaciaacaaaajaalaaaakaafaaaaadabaaaeia
acaaaajaamaaaakaafaaaaadabaaabiaacaaaajaanaaaakaafaaaaadacaaacia
acaaffjaalaaffkaafaaaaadacaaaeiaacaaffjaamaaffkaafaaaaadacaaabia
acaaffjaanaaffkaacaaaaadabaaahiaabaaoeiaacaaoeiaafaaaaadacaaacia
acaakkjaalaakkkaafaaaaadacaaaeiaacaakkjaamaakkkaafaaaaadacaaabia
acaakkjaanaakkkaacaaaaadabaaahiaabaaoeiaacaaoeiaaiaaaaadaaaaaiia
abaaoeiaabaaoeiaahaaaaacaaaaaiiaaaaappiaafaaaaadabaaahiaaaaappia
abaaoeiaafaaaaadacaaahiaaaaaoeiaabaaoeiaaeaaaaaeacaaahiaabaancia
aaaamjiaacaaoeibafaaaaadacaaahiaacaaoeiaabaappjaabaaaaacabaaacoa
acaaaaiaabaaaaacabaaaeoaabaaffiaabaaaaacacaaaboaaaaaaaiaabaaaaac
adaaaboaaaaaffiaabaaaaacacaaacoaacaaffiaabaaaaacadaaacoaacaakkia
abaaaaacacaaaeoaabaakkiaabaaaaacadaaaeoaabaaaaiappppaaaafdeieefc
kmafaaaaeaaaabaaglabaaaafjaaaaaeegiocaaaaaaaaaaabaaaaaaafjaaaaae
egiocaaaabaaaaaabdaaaaaafpaaaaadpcbabaaaaaaaaaaafpaaaaadpcbabaaa
abaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaaadaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaadpccabaaaabaaaaaagfaaaaadhccabaaa
acaaaaaagfaaaaadhccabaaaadaaaaaagfaaaaadhccabaaaaeaaaaaagfaaaaad
hccabaaaafaaaaaagiaaaaacadaaaaaadiaaaaaipcaabaaaaaaaaaaafgbfbaaa
aaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaa
abaaaaaaaaaaaaaaagbabaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaakpcaabaaa
aaaaaaaaegiocaaaabaaaaaaacaaaaaakgbkbaaaaaaaaaaaegaobaaaaaaaaaaa
dcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaaadaaaaaapgbpbaaaaaaaaaaa
egaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaaegbabaaaadaaaaaaegiacaaa
aaaaaaaaaoaaaaaaogikcaaaaaaaaaaaaoaaaaaadcaaaaalmccabaaaabaaaaaa
agbebaaaadaaaaaaagiecaaaaaaaaaaaapaaaaaakgiocaaaaaaaaaaaapaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadgaaaaafbccabaaaacaaaaaackaabaaaabaaaaaadgaaaaaf
eccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaafeccabaaaaeaaaaaaakaabaaa
aaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaaabaaaaaadgaaaaafbccabaaa
aeaaaaaabkaabaaaabaaaaaadgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaa
dgaaaaafcccabaaaaeaaaaaackaabaaaacaaaaaadiaaaaaihcaabaaaaaaaaaaa
fgbfbaaaaaaaaaaaegiccaaaabaaaaaaanaaaaaadcaaaaakhcaabaaaaaaaaaaa
egiccaaaabaaaaaaamaaaaaaagbabaaaaaaaaaaaegacbaaaaaaaaaaadcaaaaak
hcaabaaaaaaaaaaaegiccaaaabaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaa
aaaaaaaadcaaaaakhccabaaaafaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaa
aaaaaaaaegacbaaaaaaaaaaadoaaaaabejfdeheopaaaaaaaaiaaaaaaaiaaaaaa
miaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaa
aaaaaaaaadaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
acaaaaaaahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaa
oaaaaaaaabaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaaoaaaaaaaacaaaaaa
aaaaaaaaadaaaaaaafaaaaaaapaaaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaa
agaaaaaaapaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaa
faepfdejfeejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfcee
aaedepemepfcaaklepfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapaaaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahaiaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahaiaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaahaiaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahaiaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
"
}
}
Program "fp" {
SubProgram "opengl " {
Keywords { "POINT" }
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 29 math, 3 textures
Keywords { "POINT" }
Matrix 0 [_LightMatrix0] 3
Vector 5 [_Color]
Vector 4 [_LightColor0]
Vector 3 [_WorldSpaceLightPos0]
SetTexture 0 [_LightTexture0] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_BumpMap] 2D 2
"ps_2_0
def c6, 2, -1, 0, 1
dcl t0
dcl_pp t1.xyz
dcl_pp t2.xyz
dcl_pp t3.xyz
dcl t4.xyz
dcl_2d s0
dcl_2d s1
dcl_2d s2
add r0.xyz, -t4, c3
nrm_pp r1.xyz, r0
mov r0.x, t0.z
mov r0.y, t0.w
mov r2.xyz, t4
mov_pp r2.w, c6.w
dp4_pp r3.x, c0, r2
dp4_pp r3.y, c1, r2
dp4_pp r3.z, c2, r2
dp3_pp r2.xy, r3, r3
texld_pp r0, r0, s2
texld_pp r2, r2, s0
texld r3, t0, s1
mad_pp r4.x, r0.w, c6.x, c6.y
mad_pp r4.y, r0.y, c6.x, c6.y
dp2add_sat_pp r1.w, r4, r4, c6.z
add_pp r1.w, -r1.w, c6.w
rsq_pp r1.w, r1.w
rcp_pp r4.z, r1.w
dp3_pp r0.x, t1, r4
dp3_pp r0.y, t2, r4
dp3_pp r0.z, t3, r4
dp3_pp r0.x, r0, r1
max_pp r1.x, r0.x, c6.z
mul_pp r0.xyz, r2.x, c4
mul_pp r2, r3, c5
mul_pp r0.xyz, r0, r2
mul_pp r2.xyz, r1.x, r0
mov_pp oC0, r2

"
}
SubProgram "d3d11 " {
// Stats: 23 math, 3 textures
Keywords { "POINT" }
SetTexture 0 [_MainTex] 2D 1
SetTexture 1 [_BumpMap] 2D 2
SetTexture 2 [_LightTexture0] 2D 0
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0
eefiecedeagcphmobnekgidbblmhhjbclggijdgdabaaaaaadiafaaaaadaaaaaa
cmaaaaaaoeaaaaaabiabaaaaejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaakeaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcbiaeaaaa
eaaaaaaaagabaaaafjaaaaaeegiocaaaaaaaaaaaaoaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fkaaaaadaagabaaaacaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaae
aahabaaaabaaaaaaffffaaaafibiaaaeaahabaaaacaaaaaaffffaaaagcbaaaad
pcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaaadaaaaaa
gcbaaaadhcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaagfaaaaadpccabaaa
aaaaaaaagiaaaaacadaaaaaaaaaaaaajhcaabaaaaaaaaaaaegbcbaiaebaaaaaa
afaaaaaaegiccaaaabaaaaaaaaaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaa
aaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaa
diaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaaaaaaaaaaefaaaaaj
pcaabaaaabaaaaaaogbkbaaaabaaaaaaeghobaaaabaaaaaaaagabaaaacaaaaaa
dcaaaaapdcaabaaaabaaaaaahgapbaaaabaaaaaaaceaaaaaaaaaaaeaaaaaaaea
aaaaaaaaaaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaaaaaaaaaaaaaapaaaaah
icaabaaaaaaaaaaaegaabaaaabaaaaaaegaabaaaabaaaaaaddaaaaahicaabaaa
aaaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaiadpaaaaaaaiicaabaaaaaaaaaaa
dkaabaiaebaaaaaaaaaaaaaaabeaaaaaaaaaiadpelaaaaafecaabaaaabaaaaaa
dkaabaaaaaaaaaaabaaaaaahbcaabaaaacaaaaaaegbcbaaaacaaaaaaegacbaaa
abaaaaaabaaaaaahccaabaaaacaaaaaaegbcbaaaadaaaaaaegacbaaaabaaaaaa
baaaaaahecaabaaaacaaaaaaegbcbaaaaeaaaaaaegacbaaaabaaaaaabaaaaaah
bcaabaaaaaaaaaaaegacbaaaacaaaaaaegacbaaaaaaaaaaadeaaaaahbcaabaaa
aaaaaaaaakaabaaaaaaaaaaaabeaaaaaaaaaaaaadiaaaaaiocaabaaaaaaaaaaa
fgbfbaaaafaaaaaaagijcaaaaaaaaaaaakaaaaaadcaaaaakocaabaaaaaaaaaaa
agijcaaaaaaaaaaaajaaaaaaagbabaaaafaaaaaafgaobaaaaaaaaaaadcaaaaak
ocaabaaaaaaaaaaaagijcaaaaaaaaaaaalaaaaaakgbkbaaaafaaaaaafgaobaaa
aaaaaaaaaaaaaaaiocaabaaaaaaaaaaafgaobaaaaaaaaaaaagijcaaaaaaaaaaa
amaaaaaabaaaaaahccaabaaaaaaaaaaajgahbaaaaaaaaaaajgahbaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaafgafbaaaaaaaaaaaeghobaaaacaaaaaaaagabaaa
aaaaaaaadiaaaaaiocaabaaaaaaaaaaaagaabaaaabaaaaaaagijcaaaaaaaaaaa
agaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaa
aagabaaaabaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaa
aaaaaaaaanaaaaaadiaaaaahocaabaaaaaaaaaaafgaobaaaaaaaaaaaagajbaaa
abaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaa
aaaaaaaaagaabaaaaaaaaaaajgahbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 23 math, 3 textures
Keywords { "POINT" }
SetTexture 0 [_MainTex] 2D 1
SetTexture 1 [_BumpMap] 2D 2
SetTexture 2 [_LightTexture0] 2D 0
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0_level_9_1
eefiecedikfciejbphefdgbpkokfmpgiaapkhdonabaaaaaaneahaaaaaeaaaaaa
daaaaaaamiacaaaaoiagaaaakaahaaaaebgpgodjjaacaaaajaacaaaaaaacpppp
dmacaaaafeaaaaaaadaadaaaaaaafeaaaaaafeaaadaaceaaaaaafeaaacaaaaaa
aaababaaabacacaaaaaaagaaabaaaaaaaaaaaaaaaaaaajaaafaaabaaaaaaaaaa
abaaaaaaabaaagaaaaaaaaaaaaacppppfbaaaaafahaaapkaaaaaaaeaaaaaialp
aaaaaaaaaaaaiadpbpaaaaacaaaaaaiaaaaaaplabpaaaaacaaaaaaiaabaachla
bpaaaaacaaaaaaiaacaachlabpaaaaacaaaaaaiaadaachlabpaaaaacaaaaaaia
aeaaahlabpaaaaacaaaaaajaaaaiapkabpaaaaacaaaaaajaabaiapkabpaaaaac
aaaaaajaacaiapkaacaaaaadaaaaahiaaeaaoelbagaaoekaceaaaaacabaachia
aaaaoeiaabaaaaacaaaaabiaaaaakklaabaaaaacaaaaaciaaaaapplaafaaaaad
acaachiaaeaafflaacaaoekaaeaaaaaeacaachiaabaaoekaaeaaaalaacaaoeia
aeaaaaaeacaachiaadaaoekaaeaakklaacaaoeiaacaaaaadacaachiaacaaoeia
aeaaoekaaiaaaaadacaacdiaacaaoeiaacaaoeiaecaaaaadaaaacpiaaaaaoeia
acaioekaecaaaaadacaacpiaacaaoeiaaaaioekaecaaaaadadaaapiaaaaaoela
abaioekaaeaaaaaeaeaacbiaaaaappiaahaaaakaahaaffkaaeaaaaaeaeaaccia
aaaaffiaahaaaakaahaaffkafkaaaaaeabaadiiaaeaaoeiaaeaaoeiaahaakkka
acaaaaadabaaciiaabaappibahaappkaahaaaaacabaaciiaabaappiaagaaaaac
aeaaceiaabaappiaaiaaaaadaaaacbiaabaaoelaaeaaoeiaaiaaaaadaaaaccia
acaaoelaaeaaoeiaaiaaaaadaaaaceiaadaaoelaaeaaoeiaaiaaaaadaaaacbia
aaaaoeiaabaaoeiaalaaaaadabaacbiaaaaaaaiaahaakkkaafaaaaadaaaachia
acaaaaiaaaaaoekaafaaaaadacaacpiaadaaoeiaafaaoekaafaaaaadaaaachia
aaaaoeiaacaaoeiaafaaaaadacaachiaabaaaaiaaaaaoeiaabaaaaacaaaicpia
acaaoeiappppaaaafdeieefcbiaeaaaaeaaaaaaaagabaaaafjaaaaaeegiocaaa
aaaaaaaaaoaaaaaafjaaaaaeegiocaaaabaaaaaaabaaaaaafkaaaaadaagabaaa
aaaaaaaafkaaaaadaagabaaaabaaaaaafkaaaaadaagabaaaacaaaaaafibiaaae
aahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaafibiaaae
aahabaaaacaaaaaaffffaaaagcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaa
acaaaaaagcbaaaadhcbabaaaadaaaaaagcbaaaadhcbabaaaaeaaaaaagcbaaaad
hcbabaaaafaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacadaaaaaaaaaaaaaj
hcaabaaaaaaaaaaaegbcbaiaebaaaaaaafaaaaaaegiccaaaabaaaaaaaaaaaaaa
baaaaaahicaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaf
icaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaa
aaaaaaaaegacbaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaogbkbaaaabaaaaaa
eghobaaaabaaaaaaaagabaaaacaaaaaadcaaaaapdcaabaaaabaaaaaahgapbaaa
abaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaaaaaaialp
aaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaabaaaaaa
egaabaaaabaaaaaaddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaa
aaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaaabeaaaaa
aaaaiadpelaaaaafecaabaaaabaaaaaadkaabaaaaaaaaaaabaaaaaahbcaabaaa
acaaaaaaegbcbaaaacaaaaaaegacbaaaabaaaaaabaaaaaahccaabaaaacaaaaaa
egbcbaaaadaaaaaaegacbaaaabaaaaaabaaaaaahecaabaaaacaaaaaaegbcbaaa
aeaaaaaaegacbaaaabaaaaaabaaaaaahbcaabaaaaaaaaaaaegacbaaaacaaaaaa
egacbaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaa
aaaaaaaadiaaaaaiocaabaaaaaaaaaaafgbfbaaaafaaaaaaagijcaaaaaaaaaaa
akaaaaaadcaaaaakocaabaaaaaaaaaaaagijcaaaaaaaaaaaajaaaaaaagbabaaa
afaaaaaafgaobaaaaaaaaaaadcaaaaakocaabaaaaaaaaaaaagijcaaaaaaaaaaa
alaaaaaakgbkbaaaafaaaaaafgaobaaaaaaaaaaaaaaaaaaiocaabaaaaaaaaaaa
fgaobaaaaaaaaaaaagijcaaaaaaaaaaaamaaaaaabaaaaaahccaabaaaaaaaaaaa
jgahbaaaaaaaaaaajgahbaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaafgafbaaa
aaaaaaaaeghobaaaacaaaaaaaagabaaaaaaaaaaadiaaaaaiocaabaaaaaaaaaaa
agaabaaaabaaaaaaagijcaaaaaaaaaaaagaaaaaaefaaaaajpcaabaaaabaaaaaa
egbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaabaaaaaadiaaaaaipcaabaaa
abaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaaanaaaaaadiaaaaahocaabaaa
aaaaaaaafgaobaaaaaaaaaaaagajbaaaabaaaaaadgaaaaaficcabaaaaaaaaaaa
dkaabaaaabaaaaaadiaaaaahhccabaaaaaaaaaaaagaabaaaaaaaaaaajgahbaaa
aaaaaaaadoaaaaabejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaa
abaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
abaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahahaaaa
keaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaakeaaaaaaadaaaaaa
aaaaaaaaadaaaaaaaeaaaaaaahahaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaa
afaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklkl
epfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaa
aaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL" }
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 18 math, 2 textures
Keywords { "DIRECTIONAL" }
Vector 2 [_Color]
Vector 1 [_LightColor0]
Vector 0 [_WorldSpaceLightPos0]
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BumpMap] 2D 1
"ps_2_0
def c3, 2, -1, 0, 1
dcl t0
dcl_pp t1.xyz
dcl_pp t2.xyz
dcl_pp t3.xyz
dcl_2d s0
dcl_2d s1
mov r0.x, t0.z
mov r0.y, t0.w
texld_pp r0, r0, s1
texld r1, t0, s0
mad_pp r2.x, r0.w, c3.x, c3.y
mad_pp r2.y, r0.y, c3.x, c3.y
dp2add_sat_pp r2.w, r2, r2, c3.z
add_pp r2.w, -r2.w, c3.w
rsq_pp r2.w, r2.w
rcp_pp r2.z, r2.w
dp3_pp r0.x, t1, r2
dp3_pp r0.y, t2, r2
dp3_pp r0.z, t3, r2
dp3_pp r0.x, r0, c0
max_pp r2.x, r0.x, c3.z
mul_pp r0, r1, c2
mul_pp r1.xyz, r0, c1
mul_pp r0.xyz, r2.x, r1
mov_pp oC0, r0

"
}
SubProgram "d3d11 " {
// Stats: 13 math, 2 textures
Keywords { "DIRECTIONAL" }
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BumpMap] 2D 1
ConstBuffer "$Globals" 192
Vector 96 [_LightColor0]
Vector 144 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0
eefiecedamdgbcanofgnmnflkjjccakcilecdijeabaaaaaaliadaaaaadaaaaaa
cmaaaaaaoeaaaaaabiabaaaaejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaakeaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaahaaaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcjiacaaaa
eaaaaaaakgaaaaaafjaaaaaeegiocaaaaaaaaaaaakaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaa
gcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaa
adaaaaaagcbaaaadhcbabaaaaeaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaac
acaaaaaaefaaaaajpcaabaaaaaaaaaaaogbkbaaaabaaaaaaeghobaaaabaaaaaa
aagabaaaabaaaaaadcaaaaapdcaabaaaaaaaaaaahgapbaaaaaaaaaaaaceaaaaa
aaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaaaaa
aaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaaaaaaaaa
ddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaiadpaaaaaaai
icaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaaabeaaaaaaaaaiadpelaaaaaf
ecaabaaaaaaaaaaadkaabaaaaaaaaaaabaaaaaahbcaabaaaabaaaaaaegbcbaaa
acaaaaaaegacbaaaaaaaaaaabaaaaaahccaabaaaabaaaaaaegbcbaaaadaaaaaa
egacbaaaaaaaaaaabaaaaaahecaabaaaabaaaaaaegbcbaaaaeaaaaaaegacbaaa
aaaaaaaabaaaaaaibcaabaaaaaaaaaaaegacbaaaabaaaaaaegiccaaaabaaaaaa
aaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaa
ajaaaaaadiaaaaaiocaabaaaaaaaaaaaagajbaaaabaaaaaaagijcaaaaaaaaaaa
agaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaa
aaaaaaaaagaabaaaaaaaaaaajgahbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 13 math, 2 textures
Keywords { "DIRECTIONAL" }
SetTexture 0 [_MainTex] 2D 0
SetTexture 1 [_BumpMap] 2D 1
ConstBuffer "$Globals" 192
Vector 96 [_LightColor0]
Vector 144 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0_level_9_1
eefieceddeienhcddcfefonpoepkoddigicaocleabaaaaaakeafaaaaaeaaaaaa
daaaaaaabiacaaaaliaeaaaahaafaaaaebgpgodjoaabaaaaoaabaaaaaaacpppp
jaabaaaafaaaaaaaadaacmaaaaaafaaaaaaafaaaacaaceaaaaaafaaaaaaaaaaa
abababaaaaaaagaaabaaaaaaaaaaaaaaaaaaajaaabaaabaaaaaaaaaaabaaaaaa
abaaacaaaaaaaaaaaaacppppfbaaaaafadaaapkaaaaaaaeaaaaaialpaaaaaaaa
aaaaiadpbpaaaaacaaaaaaiaaaaaaplabpaaaaacaaaaaaiaabaachlabpaaaaac
aaaaaaiaacaachlabpaaaaacaaaaaaiaadaachlabpaaaaacaaaaaajaaaaiapka
bpaaaaacaaaaaajaabaiapkaabaaaaacaaaaabiaaaaakklaabaaaaacaaaaacia
aaaapplaecaaaaadaaaacpiaaaaaoeiaabaioekaecaaaaadabaaapiaaaaaoela
aaaioekaaeaaaaaeacaacbiaaaaappiaadaaaakaadaaffkaaeaaaaaeacaaccia
aaaaffiaadaaaakaadaaffkafkaaaaaeacaadiiaacaaoeiaacaaoeiaadaakkka
acaaaaadacaaciiaacaappibadaappkaahaaaaacacaaciiaacaappiaagaaaaac
acaaceiaacaappiaaiaaaaadaaaacbiaabaaoelaacaaoeiaaiaaaaadaaaaccia
acaaoelaacaaoeiaaiaaaaadaaaaceiaadaaoelaacaaoeiaaiaaaaadaaaacbia
aaaaoeiaacaaoekaalaaaaadacaacbiaaaaaaaiaadaakkkaafaaaaadaaaacpia
abaaoeiaabaaoekaafaaaaadabaachiaaaaaoeiaaaaaoekaafaaaaadaaaachia
acaaaaiaabaaoeiaabaaaaacaaaicpiaaaaaoeiappppaaaafdeieefcjiacaaaa
eaaaaaaakgaaaaaafjaaaaaeegiocaaaaaaaaaaaakaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaa
gcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaa
adaaaaaagcbaaaadhcbabaaaaeaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaac
acaaaaaaefaaaaajpcaabaaaaaaaaaaaogbkbaaaabaaaaaaeghobaaaabaaaaaa
aagabaaaabaaaaaadcaaaaapdcaabaaaaaaaaaaahgapbaaaaaaaaaaaaceaaaaa
aaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaaaaa
aaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaaaaaaaaaegaabaaaaaaaaaaa
ddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaiadpaaaaaaai
icaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaaabeaaaaaaaaaiadpelaaaaaf
ecaabaaaaaaaaaaadkaabaaaaaaaaaaabaaaaaahbcaabaaaabaaaaaaegbcbaaa
acaaaaaaegacbaaaaaaaaaaabaaaaaahccaabaaaabaaaaaaegbcbaaaadaaaaaa
egacbaaaaaaaaaaabaaaaaahecaabaaaabaaaaaaegbcbaaaaeaaaaaaegacbaaa
aaaaaaaabaaaaaaibcaabaaaaaaaaaaaegacbaaaabaaaaaaegiccaaaabaaaaaa
aaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
aaaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaa
ajaaaaaadiaaaaaiocaabaaaaaaaaaaaagajbaaaabaaaaaaagijcaaaaaaaaaaa
agaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaa
aaaaaaaaagaabaaaaaaaaaaajgahbaaaaaaaaaaadoaaaaabejfdeheolaaaaaaa
agaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaa
keaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaa
aaaaaaaaadaaaaaaacaaaaaaahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaa
adaaaaaaahahaaaakeaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaa
keaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaaahaaaaaafdfgfpfaepfdejfe
ejepeoaafeeffiedepepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaa
caaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgf
heaaklkl"
}
SubProgram "opengl " {
Keywords { "SPOT" }
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 34 math, 4 textures
Keywords { "SPOT" }
Matrix 0 [_LightMatrix0]
Vector 6 [_Color]
Vector 5 [_LightColor0]
Vector 4 [_WorldSpaceLightPos0]
SetTexture 0 [_LightTexture0] 2D 0
SetTexture 1 [_LightTextureB0] 2D 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_BumpMap] 2D 3
"ps_2_0
def c7, 2, -1, 0, 1
def c8, 0.5, 0, 0, 0
dcl t0
dcl_pp t1.xyz
dcl_pp t2.xyz
dcl_pp t3.xyz
dcl t4.xyz
dcl_2d s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
add r0.xyz, -t4, c4
nrm_pp r1.xyz, r0
mov r0.x, t0.z
mov r0.y, t0.w
mov r2.xyz, t4
mov r2.w, c7.w
dp4_pp r3.x, c0, r2
dp4_pp r3.y, c1, r2
dp4_pp r3.z, c2, r2
dp4_pp r1.w, c3, r2
rcp r1.w, r1.w
mad_pp r2.xy, r3, r1.w, c8.x
dp3_pp r3.xy, r3, r3
texld_pp r0, r0, s3
texld_pp r2, r2, s0
texld_pp r4, r3, s1
texld r5, t0, s2
mad_pp r2.x, r0.w, c7.x, c7.y
mad_pp r2.y, r0.y, c7.x, c7.y
dp2add_sat_pp r1.w, r2, r2, c7.z
add_pp r1.w, -r1.w, c7.w
rsq_pp r1.w, r1.w
rcp_pp r2.z, r1.w
dp3_pp r0.x, t1, r2
dp3_pp r0.y, t2, r2
dp3_pp r0.z, t3, r2
dp3_pp r0.x, r0, r1
max_pp r1.x, r0.x, c7.z
mul r0.x, r2.w, r4.x
mul_pp r0.xyz, r0.x, c5
cmp_pp r0.xyz, -r3.z, c7.z, r0
mul_pp r2, r5, c6
mul_pp r0.xyz, r0, r2
mul_pp r2.xyz, r1.x, r0
mov_pp oC0, r2

"
}
SubProgram "d3d11 " {
// Stats: 29 math, 4 textures
Keywords { "SPOT" }
SetTexture 0 [_MainTex] 2D 2
SetTexture 1 [_BumpMap] 2D 3
SetTexture 2 [_LightTexture0] 2D 0
SetTexture 3 [_LightTextureB0] 2D 1
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0
eefiecedifbolekeghfnajbanajkljfffgfhnnelabaaaaaacmagaaaaadaaaaaa
cmaaaaaaoeaaaaaabiabaaaaejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaakeaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcamafaaaa
eaaaaaaaedabaaaafjaaaaaeegiocaaaaaaaaaaaaoaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fkaaaaadaagabaaaacaaaaaafkaaaaadaagabaaaadaaaaaafibiaaaeaahabaaa
aaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaafibiaaaeaahabaaa
acaaaaaaffffaaaafibiaaaeaahabaaaadaaaaaaffffaaaagcbaaaadpcbabaaa
abaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaaadaaaaaagcbaaaad
hcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaagfaaaaadpccabaaaaaaaaaaa
giaaaaacadaaaaaaaaaaaaajhcaabaaaaaaaaaaaegbcbaiaebaaaaaaafaaaaaa
egiccaaaabaaaaaaaaaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaaaaaaaaa
egacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaah
hcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaaaaaaaaaaefaaaaajpcaabaaa
abaaaaaaogbkbaaaabaaaaaaeghobaaaabaaaaaaaagabaaaadaaaaaadcaaaaap
dcaabaaaabaaaaaahgapbaaaabaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaa
aaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaa
aaaaaaaaegaabaaaabaaaaaaegaabaaaabaaaaaaddaaaaahicaabaaaaaaaaaaa
dkaabaaaaaaaaaaaabeaaaaaaaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaia
ebaaaaaaaaaaaaaaabeaaaaaaaaaiadpelaaaaafecaabaaaabaaaaaadkaabaaa
aaaaaaaabaaaaaahbcaabaaaacaaaaaaegbcbaaaacaaaaaaegacbaaaabaaaaaa
baaaaaahccaabaaaacaaaaaaegbcbaaaadaaaaaaegacbaaaabaaaaaabaaaaaah
ecaabaaaacaaaaaaegbcbaaaaeaaaaaaegacbaaaabaaaaaabaaaaaahbcaabaaa
aaaaaaaaegacbaaaacaaaaaaegacbaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaa
akaabaaaaaaaaaaaabeaaaaaaaaaaaaadiaaaaaipcaabaaaabaaaaaafgbfbaaa
afaaaaaaegiocaaaaaaaaaaaakaaaaaadcaaaaakpcaabaaaabaaaaaaegiocaaa
aaaaaaaaajaaaaaaagbabaaaafaaaaaaegaobaaaabaaaaaadcaaaaakpcaabaaa
abaaaaaaegiocaaaaaaaaaaaalaaaaaakgbkbaaaafaaaaaaegaobaaaabaaaaaa
aaaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaaamaaaaaa
aoaaaaahgcaabaaaaaaaaaaaagabbaaaabaaaaaapgapbaaaabaaaaaaaaaaaaak
gcaabaaaaaaaaaaafgagbaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaadpaaaaaadp
aaaaaaaaefaaaaajpcaabaaaacaaaaaajgafbaaaaaaaaaaaeghobaaaacaaaaaa
aagabaaaaaaaaaaadbaaaaahccaabaaaaaaaaaaaabeaaaaaaaaaaaaackaabaaa
abaaaaaabaaaaaahecaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaabaaaaaa
efaaaaajpcaabaaaabaaaaaakgakbaaaaaaaaaaaeghobaaaadaaaaaaaagabaaa
abaaaaaaabaaaaahccaabaaaaaaaaaaabkaabaaaaaaaaaaaabeaaaaaaaaaiadp
diaaaaahccaabaaaaaaaaaaadkaabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaah
ccaabaaaaaaaaaaaakaabaaaabaaaaaabkaabaaaaaaaaaaadiaaaaaiocaabaaa
aaaaaaaafgafbaaaaaaaaaaaagijcaaaaaaaaaaaagaaaaaaefaaaaajpcaabaaa
abaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaacaaaaaadiaaaaai
pcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaaanaaaaaadiaaaaah
ocaabaaaaaaaaaaafgaobaaaaaaaaaaaagajbaaaabaaaaaadgaaaaaficcabaaa
aaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaaaaaaaaaaagaabaaaaaaaaaaa
jgahbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 29 math, 4 textures
Keywords { "SPOT" }
SetTexture 0 [_MainTex] 2D 2
SetTexture 1 [_BumpMap] 2D 3
SetTexture 2 [_LightTexture0] 2D 0
SetTexture 3 [_LightTextureB0] 2D 1
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0_level_9_1
eefiecedlbigcnafhcdjlifojmhokckcgleejghkabaaaaaaeeajaaaaaeaaaaaa
daaaaaaaeeadaaaafiaiaaaabaajaaaaebgpgodjamadaaaaamadaaaaaaacpppp
leacaaaafiaaaaaaadaadeaaaaaafiaaaaaafiaaaeaaceaaaaaafiaaacaaaaaa
adababaaaaacacaaabadadaaaaaaagaaabaaaaaaaaaaaaaaaaaaajaaafaaabaa
aaaaaaaaabaaaaaaabaaagaaaaaaaaaaaaacppppfbaaaaafahaaapkaaaaaaaea
aaaaialpaaaaaaaaaaaaiadpfbaaaaafaiaaapkaaaaaaadpaaaaaaaaaaaaaaaa
aaaaaaaabpaaaaacaaaaaaiaaaaaaplabpaaaaacaaaaaaiaabaachlabpaaaaac
aaaaaaiaacaachlabpaaaaacaaaaaaiaadaachlabpaaaaacaaaaaaiaaeaaahla
bpaaaaacaaaaaajaaaaiapkabpaaaaacaaaaaajaabaiapkabpaaaaacaaaaaaja
acaiapkabpaaaaacaaaaaajaadaiapkaacaaaaadaaaaahiaaeaaoelbagaaoeka
ceaaaaacabaachiaaaaaoeiaabaaaaacaaaaabiaaaaakklaabaaaaacaaaaacia
aaaapplaafaaaaadacaacpiaaeaafflaacaaoekaaeaaaaaeacaacpiaabaaoeka
aeaaaalaacaaoeiaaeaaaaaeacaacpiaadaaoekaaeaakklaacaaoeiaacaaaaad
acaacpiaacaaoeiaaeaaoekaagaaaaacabaaaiiaacaappiaaeaaaaaeadaacdia
acaaoeiaabaappiaaiaaaakaaiaaaaadacaacdiaacaaoeiaacaaoeiaecaaaaad
aaaacpiaaaaaoeiaadaioekaecaaaaadadaacpiaadaaoeiaaaaioekaecaaaaad
aeaacpiaacaaoeiaabaioekaecaaaaadafaaapiaaaaaoelaacaioekaaeaaaaae
adaacbiaaaaappiaahaaaakaahaaffkaaeaaaaaeadaacciaaaaaffiaahaaaaka
ahaaffkafkaaaaaeabaadiiaadaaoeiaadaaoeiaahaakkkaacaaaaadabaaciia
abaappibahaappkaahaaaaacabaaciiaabaappiaagaaaaacadaaceiaabaappia
aiaaaaadaaaacbiaabaaoelaadaaoeiaaiaaaaadaaaacciaacaaoelaadaaoeia
aiaaaaadaaaaceiaadaaoelaadaaoeiaaiaaaaadaaaacbiaaaaaoeiaabaaoeia
alaaaaadabaacbiaaaaaaaiaahaakkkaafaaaaadaaaaabiaadaappiaaeaaaaia
afaaaaadaaaachiaaaaaaaiaaaaaoekafiaaaaaeaaaachiaacaakkibahaakkka
aaaaoeiaafaaaaadacaacpiaafaaoeiaafaaoekaafaaaaadaaaachiaaaaaoeia
acaaoeiaafaaaaadacaachiaabaaaaiaaaaaoeiaabaaaaacaaaicpiaacaaoeia
ppppaaaafdeieefcamafaaaaeaaaaaaaedabaaaafjaaaaaeegiocaaaaaaaaaaa
aoaaaaaafjaaaaaeegiocaaaabaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaa
fkaaaaadaagabaaaabaaaaaafkaaaaadaagabaaaacaaaaaafkaaaaadaagabaaa
adaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaa
ffffaaaafibiaaaeaahabaaaacaaaaaaffffaaaafibiaaaeaahabaaaadaaaaaa
ffffaaaagcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaad
hcbabaaaadaaaaaagcbaaaadhcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacadaaaaaaaaaaaaajhcaabaaaaaaaaaaa
egbcbaiaebaaaaaaafaaaaaaegiccaaaabaaaaaaaaaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaaefaaaaajpcaabaaaabaaaaaaogbkbaaaabaaaaaaeghobaaaabaaaaaa
aagabaaaadaaaaaadcaaaaapdcaabaaaabaaaaaahgapbaaaabaaaaaaaceaaaaa
aaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaaaaa
aaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaabaaaaaaegaabaaaabaaaaaa
ddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaiadpaaaaaaai
icaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaaabeaaaaaaaaaiadpelaaaaaf
ecaabaaaabaaaaaadkaabaaaaaaaaaaabaaaaaahbcaabaaaacaaaaaaegbcbaaa
acaaaaaaegacbaaaabaaaaaabaaaaaahccaabaaaacaaaaaaegbcbaaaadaaaaaa
egacbaaaabaaaaaabaaaaaahecaabaaaacaaaaaaegbcbaaaaeaaaaaaegacbaaa
abaaaaaabaaaaaahbcaabaaaaaaaaaaaegacbaaaacaaaaaaegacbaaaaaaaaaaa
deaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaaaaaaaaaadiaaaaai
pcaabaaaabaaaaaafgbfbaaaafaaaaaaegiocaaaaaaaaaaaakaaaaaadcaaaaak
pcaabaaaabaaaaaaegiocaaaaaaaaaaaajaaaaaaagbabaaaafaaaaaaegaobaaa
abaaaaaadcaaaaakpcaabaaaabaaaaaaegiocaaaaaaaaaaaalaaaaaakgbkbaaa
afaaaaaaegaobaaaabaaaaaaaaaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaa
egiocaaaaaaaaaaaamaaaaaaaoaaaaahgcaabaaaaaaaaaaaagabbaaaabaaaaaa
pgapbaaaabaaaaaaaaaaaaakgcaabaaaaaaaaaaafgagbaaaaaaaaaaaaceaaaaa
aaaaaaaaaaaaaadpaaaaaadpaaaaaaaaefaaaaajpcaabaaaacaaaaaajgafbaaa
aaaaaaaaeghobaaaacaaaaaaaagabaaaaaaaaaaadbaaaaahccaabaaaaaaaaaaa
abeaaaaaaaaaaaaackaabaaaabaaaaaabaaaaaahecaabaaaaaaaaaaaegacbaaa
abaaaaaaegacbaaaabaaaaaaefaaaaajpcaabaaaabaaaaaakgakbaaaaaaaaaaa
eghobaaaadaaaaaaaagabaaaabaaaaaaabaaaaahccaabaaaaaaaaaaabkaabaaa
aaaaaaaaabeaaaaaaaaaiadpdiaaaaahccaabaaaaaaaaaaadkaabaaaacaaaaaa
bkaabaaaaaaaaaaadiaaaaahccaabaaaaaaaaaaaakaabaaaabaaaaaabkaabaaa
aaaaaaaadiaaaaaiocaabaaaaaaaaaaafgafbaaaaaaaaaaaagijcaaaaaaaaaaa
agaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaa
aagabaaaacaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaa
aaaaaaaaanaaaaaadiaaaaahocaabaaaaaaaaaaafgaobaaaaaaaaaaaagajbaaa
abaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaa
aaaaaaaaagaabaaaaaaaaaaajgahbaaaaaaaaaaadoaaaaabejfdeheolaaaaaaa
agaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaa
keaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaa
aaaaaaaaadaaaaaaacaaaaaaahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaa
adaaaaaaahahaaaakeaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaa
keaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaaahahaaaafdfgfpfaepfdejfe
ejepeoaafeeffiedepepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaa
caaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgf
heaaklkl"
}
SubProgram "opengl " {
Keywords { "POINT_COOKIE" }
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 30 math, 4 textures
Keywords { "POINT_COOKIE" }
Matrix 0 [_LightMatrix0] 3
Vector 5 [_Color]
Vector 4 [_LightColor0]
Vector 3 [_WorldSpaceLightPos0]
SetTexture 0 [_LightTexture0] CUBE 0
SetTexture 1 [_LightTextureB0] 2D 1
SetTexture 2 [_MainTex] 2D 2
SetTexture 3 [_BumpMap] 2D 3
"ps_2_0
def c6, 2, -1, 0, 1
dcl t0
dcl_pp t1.xyz
dcl_pp t2.xyz
dcl_pp t3.xyz
dcl t4.xyz
dcl_cube s0
dcl_2d s1
dcl_2d s2
dcl_2d s3
add r0.xyz, -t4, c3
nrm_pp r1.xyz, r0
mov r0.x, t0.z
mov r0.y, t0.w
mov r2.xyz, t4
mov_pp r2.w, c6.w
dp4_pp r3.x, c0, r2
dp4_pp r3.y, c1, r2
dp4_pp r3.z, c2, r2
dp3_pp r2.xy, r3, r3
texld_pp r0, r0, s3
texld r3, r3, s0
texld r2, r2, s1
texld r4, t0, s2
mad_pp r3.x, r0.w, c6.x, c6.y
mad_pp r3.y, r0.y, c6.x, c6.y
dp2add_sat_pp r1.w, r3, r3, c6.z
add_pp r1.w, -r1.w, c6.w
rsq_pp r1.w, r1.w
rcp_pp r3.z, r1.w
dp3_pp r0.x, t1, r3
dp3_pp r0.y, t2, r3
dp3_pp r0.z, t3, r3
dp3_pp r0.x, r0, r1
max_pp r1.x, r0.x, c6.z
mul_pp r0.x, r3.w, r2.x
mul_pp r0.xyz, r0.x, c4
mul_pp r2, r4, c5
mul_pp r0.xyz, r0, r2
mul_pp r2.xyz, r1.x, r0
mov_pp oC0, r2

"
}
SubProgram "d3d11 " {
// Stats: 24 math, 4 textures
Keywords { "POINT_COOKIE" }
SetTexture 0 [_MainTex] 2D 2
SetTexture 1 [_BumpMap] 2D 3
SetTexture 2 [_LightTextureB0] 2D 1
SetTexture 3 [_LightTexture0] CUBE 0
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0
eefiecedppicplcimkmglccacbddabimhedfepglabaaaaaajeafaaaaadaaaaaa
cmaaaaaaoeaaaaaabiabaaaaejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaakeaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcheaeaaaa
eaaaaaaabnabaaaafjaaaaaeegiocaaaaaaaaaaaaoaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fkaaaaadaagabaaaacaaaaaafkaaaaadaagabaaaadaaaaaafibiaaaeaahabaaa
aaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaaffffaaaafibiaaaeaahabaaa
acaaaaaaffffaaaafidaaaaeaahabaaaadaaaaaaffffaaaagcbaaaadpcbabaaa
abaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaaadaaaaaagcbaaaad
hcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaagfaaaaadpccabaaaaaaaaaaa
giaaaaacadaaaaaaaaaaaaajhcaabaaaaaaaaaaaegbcbaiaebaaaaaaafaaaaaa
egiccaaaabaaaaaaaaaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaaaaaaaaa
egacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaah
hcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaaaaaaaaaaefaaaaajpcaabaaa
abaaaaaaogbkbaaaabaaaaaaeghobaaaabaaaaaaaagabaaaadaaaaaadcaaaaap
dcaabaaaabaaaaaahgapbaaaabaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaa
aaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaa
aaaaaaaaegaabaaaabaaaaaaegaabaaaabaaaaaaddaaaaahicaabaaaaaaaaaaa
dkaabaaaaaaaaaaaabeaaaaaaaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaia
ebaaaaaaaaaaaaaaabeaaaaaaaaaiadpelaaaaafecaabaaaabaaaaaadkaabaaa
aaaaaaaabaaaaaahbcaabaaaacaaaaaaegbcbaaaacaaaaaaegacbaaaabaaaaaa
baaaaaahccaabaaaacaaaaaaegbcbaaaadaaaaaaegacbaaaabaaaaaabaaaaaah
ecaabaaaacaaaaaaegbcbaaaaeaaaaaaegacbaaaabaaaaaabaaaaaahbcaabaaa
aaaaaaaaegacbaaaacaaaaaaegacbaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaa
akaabaaaaaaaaaaaabeaaaaaaaaaaaaadiaaaaaiocaabaaaaaaaaaaafgbfbaaa
afaaaaaaagijcaaaaaaaaaaaakaaaaaadcaaaaakocaabaaaaaaaaaaaagijcaaa
aaaaaaaaajaaaaaaagbabaaaafaaaaaafgaobaaaaaaaaaaadcaaaaakocaabaaa
aaaaaaaaagijcaaaaaaaaaaaalaaaaaakgbkbaaaafaaaaaafgaobaaaaaaaaaaa
aaaaaaaiocaabaaaaaaaaaaafgaobaaaaaaaaaaaagijcaaaaaaaaaaaamaaaaaa
baaaaaahbcaabaaaabaaaaaajgahbaaaaaaaaaaajgahbaaaaaaaaaaaefaaaaaj
pcaabaaaacaaaaaajgahbaaaaaaaaaaaeghobaaaadaaaaaaaagabaaaaaaaaaaa
efaaaaajpcaabaaaabaaaaaaagaabaaaabaaaaaaeghobaaaacaaaaaaaagabaaa
abaaaaaadiaaaaahccaabaaaaaaaaaaadkaabaaaacaaaaaaakaabaaaabaaaaaa
diaaaaaiocaabaaaaaaaaaaafgafbaaaaaaaaaaaagijcaaaaaaaaaaaagaaaaaa
efaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
acaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaa
anaaaaaadiaaaaahocaabaaaaaaaaaaafgaobaaaaaaaaaaaagajbaaaabaaaaaa
dgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaaaaaaaaaa
agaabaaaaaaaaaaajgahbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 24 math, 4 textures
Keywords { "POINT_COOKIE" }
SetTexture 0 [_MainTex] 2D 2
SetTexture 1 [_BumpMap] 2D 3
SetTexture 2 [_LightTextureB0] 2D 1
SetTexture 3 [_LightTexture0] CUBE 0
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0_level_9_1
eefiecedkbigclajifjdcojdchdologmdnehbihhabaaaaaagmaiaaaaaeaaaaaa
daaaaaaaaeadaaaaiaahaaaadiaiaaaaebgpgodjmmacaaaammacaaaaaaacpppp
heacaaaafiaaaaaaadaadeaaaaaafiaaaaaafiaaaeaaceaaaaaafiaaadaaaaaa
acababaaaaacacaaabadadaaaaaaagaaabaaaaaaaaaaaaaaaaaaajaaafaaabaa
aaaaaaaaabaaaaaaabaaagaaaaaaaaaaaaacppppfbaaaaafahaaapkaaaaaaaea
aaaaialpaaaaaaaaaaaaiadpbpaaaaacaaaaaaiaaaaaaplabpaaaaacaaaaaaia
abaachlabpaaaaacaaaaaaiaacaachlabpaaaaacaaaaaaiaadaachlabpaaaaac
aaaaaaiaaeaaahlabpaaaaacaaaaaajiaaaiapkabpaaaaacaaaaaajaabaiapka
bpaaaaacaaaaaajaacaiapkabpaaaaacaaaaaajaadaiapkaacaaaaadaaaaahia
aeaaoelbagaaoekaceaaaaacabaachiaaaaaoeiaabaaaaacaaaaabiaaaaakkla
abaaaaacaaaaaciaaaaapplaafaaaaadacaachiaaeaafflaacaaoekaaeaaaaae
acaachiaabaaoekaaeaaaalaacaaoeiaaeaaaaaeacaachiaadaaoekaaeaakkla
acaaoeiaacaaaaadacaachiaacaaoeiaaeaaoekaaiaaaaadabaaciiaacaaoeia
acaaoeiaabaaaaacadaacdiaabaappiaecaaaaadaaaacpiaaaaaoeiaadaioeka
ecaaaaadacaaapiaacaaoeiaaaaioekaecaaaaadadaaapiaadaaoeiaabaioeka
ecaaaaadaeaaapiaaaaaoelaacaioekaaeaaaaaeacaacbiaaaaappiaahaaaaka
ahaaffkaaeaaaaaeacaacciaaaaaffiaahaaaakaahaaffkafkaaaaaeabaadiia
acaaoeiaacaaoeiaahaakkkaacaaaaadabaaciiaabaappibahaappkaahaaaaac
abaaciiaabaappiaagaaaaacacaaceiaabaappiaaiaaaaadaaaacbiaabaaoela
acaaoeiaaiaaaaadaaaacciaacaaoelaacaaoeiaaiaaaaadaaaaceiaadaaoela
acaaoeiaaiaaaaadaaaacbiaaaaaoeiaabaaoeiaalaaaaadabaacbiaaaaaaaia
ahaakkkaafaaaaadaaaacbiaacaappiaadaaaaiaafaaaaadaaaachiaaaaaaaia
aaaaoekaafaaaaadacaacpiaaeaaoeiaafaaoekaafaaaaadaaaachiaaaaaoeia
acaaoeiaafaaaaadacaachiaabaaaaiaaaaaoeiaabaaaaacaaaicpiaacaaoeia
ppppaaaafdeieefcheaeaaaaeaaaaaaabnabaaaafjaaaaaeegiocaaaaaaaaaaa
aoaaaaaafjaaaaaeegiocaaaabaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaa
fkaaaaadaagabaaaabaaaaaafkaaaaadaagabaaaacaaaaaafkaaaaadaagabaaa
adaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaaeaahabaaaabaaaaaa
ffffaaaafibiaaaeaahabaaaacaaaaaaffffaaaafidaaaaeaahabaaaadaaaaaa
ffffaaaagcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaad
hcbabaaaadaaaaaagcbaaaadhcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaa
gfaaaaadpccabaaaaaaaaaaagiaaaaacadaaaaaaaaaaaaajhcaabaaaaaaaaaaa
egbcbaiaebaaaaaaafaaaaaaegiccaaaabaaaaaaaaaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaaefaaaaajpcaabaaaabaaaaaaogbkbaaaabaaaaaaeghobaaaabaaaaaa
aagabaaaadaaaaaadcaaaaapdcaabaaaabaaaaaahgapbaaaabaaaaaaaceaaaaa
aaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaaaaaaialpaaaaialpaaaaaaaa
aaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaabaaaaaaegaabaaaabaaaaaa
ddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaaaaaaiadpaaaaaaai
icaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaaabeaaaaaaaaaiadpelaaaaaf
ecaabaaaabaaaaaadkaabaaaaaaaaaaabaaaaaahbcaabaaaacaaaaaaegbcbaaa
acaaaaaaegacbaaaabaaaaaabaaaaaahccaabaaaacaaaaaaegbcbaaaadaaaaaa
egacbaaaabaaaaaabaaaaaahecaabaaaacaaaaaaegbcbaaaaeaaaaaaegacbaaa
abaaaaaabaaaaaahbcaabaaaaaaaaaaaegacbaaaacaaaaaaegacbaaaaaaaaaaa
deaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaaaaaaaaaadiaaaaai
ocaabaaaaaaaaaaafgbfbaaaafaaaaaaagijcaaaaaaaaaaaakaaaaaadcaaaaak
ocaabaaaaaaaaaaaagijcaaaaaaaaaaaajaaaaaaagbabaaaafaaaaaafgaobaaa
aaaaaaaadcaaaaakocaabaaaaaaaaaaaagijcaaaaaaaaaaaalaaaaaakgbkbaaa
afaaaaaafgaobaaaaaaaaaaaaaaaaaaiocaabaaaaaaaaaaafgaobaaaaaaaaaaa
agijcaaaaaaaaaaaamaaaaaabaaaaaahbcaabaaaabaaaaaajgahbaaaaaaaaaaa
jgahbaaaaaaaaaaaefaaaaajpcaabaaaacaaaaaajgahbaaaaaaaaaaaeghobaaa
adaaaaaaaagabaaaaaaaaaaaefaaaaajpcaabaaaabaaaaaaagaabaaaabaaaaaa
eghobaaaacaaaaaaaagabaaaabaaaaaadiaaaaahccaabaaaaaaaaaaadkaabaaa
acaaaaaaakaabaaaabaaaaaadiaaaaaiocaabaaaaaaaaaaafgafbaaaaaaaaaaa
agijcaaaaaaaaaaaagaaaaaaefaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaacaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaa
abaaaaaaegiocaaaaaaaaaaaanaaaaaadiaaaaahocaabaaaaaaaaaaafgaobaaa
aaaaaaaaagajbaaaabaaaaaadgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaa
diaaaaahhccabaaaaaaaaaaaagaabaaaaaaaaaaajgahbaaaaaaaaaaadoaaaaab
ejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaaaaaaaaaaabaaaaaaadaaaaaa
aaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapapaaaa
keaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaahahaaaakeaaaaaaacaaaaaa
aaaaaaaaadaaaaaaadaaaaaaahahaaaakeaaaaaaadaaaaaaaaaaaaaaadaaaaaa
aeaaaaaaahahaaaakeaaaaaaaeaaaaaaaaaaaaaaadaaaaaaafaaaaaaahahaaaa
fdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklepfdeheocmaaaaaa
abaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaa
fdfgfpfegbhcghgfheaaklkl"
}
SubProgram "opengl " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 23 math, 3 textures
Keywords { "DIRECTIONAL_COOKIE" }
Matrix 0 [_LightMatrix0] 2
Vector 4 [_Color]
Vector 3 [_LightColor0]
Vector 2 [_WorldSpaceLightPos0]
SetTexture 0 [_LightTexture0] 2D 0
SetTexture 1 [_MainTex] 2D 1
SetTexture 2 [_BumpMap] 2D 2
"ps_2_0
def c5, 2, -1, 0, 1
dcl t0
dcl_pp t1.xyz
dcl_pp t2.xyz
dcl_pp t3.xyz
dcl t4.xyz
dcl_2d s0
dcl_2d s1
dcl_2d s2
mov r0.x, t0.z
mov r0.y, t0.w
mov r1.xyz, t4
mov_pp r1.w, c5.w
dp4_pp r2.x, c0, r1
dp4_pp r2.y, c1, r1
texld_pp r0, r0, s2
texld_pp r1, r2, s0
texld r2, t0, s1
mad_pp r1.x, r0.w, c5.x, c5.y
mad_pp r1.y, r0.y, c5.x, c5.y
dp2add_sat_pp r0.x, r1, r1, c5.z
add_pp r0.x, -r0.x, c5.w
rsq_pp r0.x, r0.x
rcp_pp r1.z, r0.x
dp3_pp r0.x, t1, r1
dp3_pp r0.y, t2, r1
dp3_pp r0.z, t3, r1
dp3_pp r0.x, r0, c2
max_pp r1.x, r0.x, c5.z
mul_pp r0.xyz, r1.w, c3
mul_pp r2, r2, c4
mul_pp r0.xyz, r0, r2
mul_pp r2.xyz, r1.x, r0
mov_pp oC0, r2

"
}
SubProgram "d3d11 " {
// Stats: 18 math, 3 textures
Keywords { "DIRECTIONAL_COOKIE" }
SetTexture 0 [_MainTex] 2D 1
SetTexture 1 [_BumpMap] 2D 2
SetTexture 2 [_LightTexture0] 2D 0
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0
eefiecedekabhhpmihfoiifchoglkndfolpfdpkcabaaaaaalaaeaaaaadaaaaaa
cmaaaaaaoeaaaaaabiabaaaaejfdeheolaaaaaaaagaaaaaaaiaaaaaajiaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaahahaaaakeaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaakeaaaaaaaeaaaaaaaaaaaaaa
adaaaaaaafaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklklfdeieefcjaadaaaa
eaaaaaaaoeaaaaaafjaaaaaeegiocaaaaaaaaaaaaoaaaaaafjaaaaaeegiocaaa
abaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaaabaaaaaa
fkaaaaadaagabaaaacaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaafibiaaae
aahabaaaabaaaaaaffffaaaafibiaaaeaahabaaaacaaaaaaffffaaaagcbaaaad
pcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaaadaaaaaa
gcbaaaadhcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaagfaaaaadpccabaaa
aaaaaaaagiaaaaacacaaaaaaefaaaaajpcaabaaaaaaaaaaaogbkbaaaabaaaaaa
eghobaaaabaaaaaaaagabaaaacaaaaaadcaaaaapdcaabaaaaaaaaaaahgapbaaa
aaaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaaaaaaialp
aaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaaaaaaaaaa
egaabaaaaaaaaaaaddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaaabeaaaaa
aaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaaabeaaaaa
aaaaiadpelaaaaafecaabaaaaaaaaaaadkaabaaaaaaaaaaabaaaaaahbcaabaaa
abaaaaaaegbcbaaaacaaaaaaegacbaaaaaaaaaaabaaaaaahccaabaaaabaaaaaa
egbcbaaaadaaaaaaegacbaaaaaaaaaaabaaaaaahecaabaaaabaaaaaaegbcbaaa
aeaaaaaaegacbaaaaaaaaaaabaaaaaaibcaabaaaaaaaaaaaegacbaaaabaaaaaa
egiccaaaabaaaaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaaaaaaaaaa
abeaaaaaaaaaaaaadiaaaaaigcaabaaaaaaaaaaafgbfbaaaafaaaaaaagibcaaa
aaaaaaaaakaaaaaadcaaaaakgcaabaaaaaaaaaaaagibcaaaaaaaaaaaajaaaaaa
agbabaaaafaaaaaafgagbaaaaaaaaaaadcaaaaakgcaabaaaaaaaaaaaagibcaaa
aaaaaaaaalaaaaaakgbkbaaaafaaaaaafgagbaaaaaaaaaaaaaaaaaaigcaabaaa
aaaaaaaafgagbaaaaaaaaaaaagibcaaaaaaaaaaaamaaaaaaefaaaaajpcaabaaa
abaaaaaajgafbaaaaaaaaaaaeghobaaaacaaaaaaaagabaaaaaaaaaaadiaaaaai
ocaabaaaaaaaaaaapgapbaaaabaaaaaaagijcaaaaaaaaaaaagaaaaaaefaaaaaj
pcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaabaaaaaa
diaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaaanaaaaaa
diaaaaahocaabaaaaaaaaaaafgaobaaaaaaaaaaaagajbaaaabaaaaaadgaaaaaf
iccabaaaaaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaaaaaaaaaaagaabaaa
aaaaaaaajgahbaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 18 math, 3 textures
Keywords { "DIRECTIONAL_COOKIE" }
SetTexture 0 [_MainTex] 2D 1
SetTexture 1 [_BumpMap] 2D 2
SetTexture 2 [_LightTexture0] 2D 0
ConstBuffer "$Globals" 256
Matrix 144 [_LightMatrix0]
Vector 96 [_LightColor0]
Vector 208 [_Color]
ConstBuffer "UnityLighting" 720
Vector 0 [_WorldSpaceLightPos0]
BindCB  "$Globals" 0
BindCB  "UnityLighting" 1
"ps_4_0_level_9_1
eefiecednkdmdhelkanfphmlgincggkhfhdlldmmabaaaaaacaahaaaaaeaaaaaa
daaaaaaajmacaaaadeagaaaaomagaaaaebgpgodjgeacaaaageacaaaaaaacpppp
baacaaaafeaaaaaaadaadaaaaaaafeaaaaaafeaaadaaceaaaaaafeaaacaaaaaa
aaababaaabacacaaaaaaagaaabaaaaaaaaaaaaaaaaaaajaaafaaabaaaaaaaaaa
abaaaaaaabaaagaaaaaaaaaaaaacppppfbaaaaafahaaapkaaaaaaaeaaaaaialp
aaaaaaaaaaaaiadpbpaaaaacaaaaaaiaaaaaaplabpaaaaacaaaaaaiaabaachla
bpaaaaacaaaaaaiaacaachlabpaaaaacaaaaaaiaadaachlabpaaaaacaaaaaaia
aeaaahlabpaaaaacaaaaaajaaaaiapkabpaaaaacaaaaaajaabaiapkabpaaaaac
aaaaaajaacaiapkaabaaaaacaaaaabiaaaaakklaabaaaaacaaaaaciaaaaappla
afaaaaadaaaacmiaaeaafflaacaablkaaeaaaaaeaaaacmiaabaablkaaeaaaala
aaaaoeiaaeaaaaaeaaaacmiaadaablkaaeaakklaaaaaoeiaacaaaaadabaacdia
aaaabliaaeaaoekaecaaaaadaaaacpiaaaaaoeiaacaioekaecaaaaadabaacpia
abaaoeiaaaaioekaecaaaaadacaaapiaaaaaoelaabaioekaaeaaaaaeabaacbia
aaaappiaahaaaakaahaaffkaaeaaaaaeabaacciaaaaaffiaahaaaakaahaaffka
fkaaaaaeaaaadbiaabaaoeiaabaaoeiaahaakkkaacaaaaadaaaacbiaaaaaaaib
ahaappkaahaaaaacaaaacbiaaaaaaaiaagaaaaacabaaceiaaaaaaaiaaiaaaaad
aaaacbiaabaaoelaabaaoeiaaiaaaaadaaaacciaacaaoelaabaaoeiaaiaaaaad
aaaaceiaadaaoelaabaaoeiaaiaaaaadaaaacbiaaaaaoeiaagaaoekaalaaaaad
abaacbiaaaaaaaiaahaakkkaafaaaaadaaaachiaabaappiaaaaaoekaafaaaaad
acaacpiaacaaoeiaafaaoekaafaaaaadaaaachiaaaaaoeiaacaaoeiaafaaaaad
acaachiaabaaaaiaaaaaoeiaabaaaaacaaaicpiaacaaoeiappppaaaafdeieefc
jaadaaaaeaaaaaaaoeaaaaaafjaaaaaeegiocaaaaaaaaaaaaoaaaaaafjaaaaae
egiocaaaabaaaaaaabaaaaaafkaaaaadaagabaaaaaaaaaaafkaaaaadaagabaaa
abaaaaaafkaaaaadaagabaaaacaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaa
fibiaaaeaahabaaaabaaaaaaffffaaaafibiaaaeaahabaaaacaaaaaaffffaaaa
gcbaaaadpcbabaaaabaaaaaagcbaaaadhcbabaaaacaaaaaagcbaaaadhcbabaaa
adaaaaaagcbaaaadhcbabaaaaeaaaaaagcbaaaadhcbabaaaafaaaaaagfaaaaad
pccabaaaaaaaaaaagiaaaaacacaaaaaaefaaaaajpcaabaaaaaaaaaaaogbkbaaa
abaaaaaaeghobaaaabaaaaaaaagabaaaacaaaaaadcaaaaapdcaabaaaaaaaaaaa
hgapbaaaaaaaaaaaaceaaaaaaaaaaaeaaaaaaaeaaaaaaaaaaaaaaaaaaceaaaaa
aaaaialpaaaaialpaaaaaaaaaaaaaaaaapaaaaahicaabaaaaaaaaaaaegaabaaa
aaaaaaaaegaabaaaaaaaaaaaddaaaaahicaabaaaaaaaaaaadkaabaaaaaaaaaaa
abeaaaaaaaaaiadpaaaaaaaiicaabaaaaaaaaaaadkaabaiaebaaaaaaaaaaaaaa
abeaaaaaaaaaiadpelaaaaafecaabaaaaaaaaaaadkaabaaaaaaaaaaabaaaaaah
bcaabaaaabaaaaaaegbcbaaaacaaaaaaegacbaaaaaaaaaaabaaaaaahccaabaaa
abaaaaaaegbcbaaaadaaaaaaegacbaaaaaaaaaaabaaaaaahecaabaaaabaaaaaa
egbcbaaaaeaaaaaaegacbaaaaaaaaaaabaaaaaaibcaabaaaaaaaaaaaegacbaaa
abaaaaaaegiccaaaabaaaaaaaaaaaaaadeaaaaahbcaabaaaaaaaaaaaakaabaaa
aaaaaaaaabeaaaaaaaaaaaaadiaaaaaigcaabaaaaaaaaaaafgbfbaaaafaaaaaa
agibcaaaaaaaaaaaakaaaaaadcaaaaakgcaabaaaaaaaaaaaagibcaaaaaaaaaaa
ajaaaaaaagbabaaaafaaaaaafgagbaaaaaaaaaaadcaaaaakgcaabaaaaaaaaaaa
agibcaaaaaaaaaaaalaaaaaakgbkbaaaafaaaaaafgagbaaaaaaaaaaaaaaaaaai
gcaabaaaaaaaaaaafgagbaaaaaaaaaaaagibcaaaaaaaaaaaamaaaaaaefaaaaaj
pcaabaaaabaaaaaajgafbaaaaaaaaaaaeghobaaaacaaaaaaaagabaaaaaaaaaaa
diaaaaaiocaabaaaaaaaaaaapgapbaaaabaaaaaaagijcaaaaaaaaaaaagaaaaaa
efaaaaajpcaabaaaabaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaa
abaaaaaadiaaaaaipcaabaaaabaaaaaaegaobaaaabaaaaaaegiocaaaaaaaaaaa
anaaaaaadiaaaaahocaabaaaaaaaaaaafgaobaaaaaaaaaaaagajbaaaabaaaaaa
dgaaaaaficcabaaaaaaaaaaadkaabaaaabaaaaaadiaaaaahhccabaaaaaaaaaaa
agaabaaaaaaaaaaajgahbaaaaaaaaaaadoaaaaabejfdeheolaaaaaaaagaaaaaa
aiaaaaaajiaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaakeaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaabaaaaaaapapaaaakeaaaaaaabaaaaaaaaaaaaaa
adaaaaaaacaaaaaaahahaaaakeaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaa
ahahaaaakeaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaaahahaaaakeaaaaaa
aeaaaaaaaaaaaaaaadaaaaaaafaaaaaaahahaaaafdfgfpfaepfdejfeejepeoaa
feeffiedepepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl
"
}
}
 }


 // Stats for Vertex shader:
 //       d3d11 : 38 math
 //    d3d11_9x : 38 math
 //        d3d9 : 43 math
 //      opengl : 19 math, 1 texture, 3 branch
 // Stats for Fragment shader:
 //       d3d11 : 5 math, 1 texture
 //    d3d11_9x : 5 math, 1 texture
 //        d3d9 : 15 math, 1 texture
 Pass {
  Name "META"
  Tags { "LIGHTMODE"="Meta" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  ColorMask RGB
  GpuProgramID 131530
Program "vp" {
SubProgram "opengl " {
// Stats: 19 math, 1 textures, 3 branches
"!!GLSL
#ifdef VERTEX

uniform mat4 _Object2World;
uniform mat4 _World2Object;
uniform vec4 unity_LightmapST;
uniform vec4 unity_DynamicLightmapST;
uniform bvec4 unity_MetaVertexControl;
uniform vec4 _MainTex_ST;
attribute vec4 TANGENT;
varying vec2 xlv_TEXCOORD0;
varying vec4 xlv_TEXCOORD1;
varying vec4 xlv_TEXCOORD2;
varying vec4 xlv_TEXCOORD3;
void main ()
{
  vec4 vertex_1;
  vertex_1 = gl_Vertex;
  if (unity_MetaVertexControl.x) {
    vertex_1.xy = ((gl_MultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
    float tmpvar_2;
    if ((gl_Vertex.z > 0.0)) {
      tmpvar_2 = 0.0001;
    } else {
      tmpvar_2 = 0.0;
    };
    vertex_1.z = tmpvar_2;
  };
  if (unity_MetaVertexControl.y) {
    vertex_1.xy = ((gl_MultiTexCoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
    float tmpvar_3;
    if ((vertex_1.z > 0.0)) {
      tmpvar_3 = 0.0001;
    } else {
      tmpvar_3 = 0.0;
    };
    vertex_1.z = tmpvar_3;
  };
  vec3 tmpvar_4;
  tmpvar_4 = (_Object2World * gl_Vertex).xyz;
  vec4 v_5;
  v_5.x = _World2Object[0].x;
  v_5.y = _World2Object[1].x;
  v_5.z = _World2Object[2].x;
  v_5.w = _World2Object[3].x;
  vec4 v_6;
  v_6.x = _World2Object[0].y;
  v_6.y = _World2Object[1].y;
  v_6.z = _World2Object[2].y;
  v_6.w = _World2Object[3].y;
  vec4 v_7;
  v_7.x = _World2Object[0].z;
  v_7.y = _World2Object[1].z;
  v_7.z = _World2Object[2].z;
  v_7.w = _World2Object[3].z;
  vec3 tmpvar_8;
  tmpvar_8 = normalize(((
    (v_5.xyz * gl_Normal.x)
   + 
    (v_6.xyz * gl_Normal.y)
  ) + (v_7.xyz * gl_Normal.z)));
  mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  vec3 tmpvar_10;
  tmpvar_10 = normalize((tmpvar_9 * TANGENT.xyz));
  vec3 tmpvar_11;
  tmpvar_11 = (((tmpvar_8.yzx * tmpvar_10.zxy) - (tmpvar_8.zxy * tmpvar_10.yzx)) * TANGENT.w);
  vec4 tmpvar_12;
  tmpvar_12.x = tmpvar_10.x;
  tmpvar_12.y = tmpvar_11.x;
  tmpvar_12.z = tmpvar_8.x;
  tmpvar_12.w = tmpvar_4.x;
  vec4 tmpvar_13;
  tmpvar_13.x = tmpvar_10.y;
  tmpvar_13.y = tmpvar_11.y;
  tmpvar_13.z = tmpvar_8.y;
  tmpvar_13.w = tmpvar_4.y;
  vec4 tmpvar_14;
  tmpvar_14.x = tmpvar_10.z;
  tmpvar_14.y = tmpvar_11.z;
  tmpvar_14.z = tmpvar_8.z;
  tmpvar_14.w = tmpvar_4.z;
  gl_Position = (gl_ModelViewProjectionMatrix * vertex_1);
  xlv_TEXCOORD0 = ((gl_MultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_12;
  xlv_TEXCOORD2 = tmpvar_13;
  xlv_TEXCOORD3 = tmpvar_14;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform vec4 _Color;
uniform bvec4 unity_MetaFragmentControl;
uniform float unity_OneOverOutputBoost;
uniform float unity_MaxOutputValue;
uniform float unity_UseLinearSpace;
varying vec2 xlv_TEXCOORD0;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1 = (texture2D (_MainTex, xlv_TEXCOORD0) * _Color);
  vec3 tmpvar_2;
  tmpvar_2 = tmpvar_1.xyz;
  vec4 res_3;
  res_3 = vec4(0.0, 0.0, 0.0, 0.0);
  if (unity_MetaFragmentControl.x) {
    vec4 tmpvar_4;
    tmpvar_4.w = 1.0;
    tmpvar_4.xyz = tmpvar_2;
    res_3.w = tmpvar_4.w;
    res_3.xyz = clamp (pow (tmpvar_1.xyz, vec3(clamp (unity_OneOverOutputBoost, 0.0, 1.0))), vec3(0.0, 0.0, 0.0), vec3(unity_MaxOutputValue));
  };
  if (unity_MetaFragmentControl.y) {
    vec3 emission_5;
    if (bool(unity_UseLinearSpace)) {
      emission_5 = vec3(0.0, 0.0, 0.0);
    } else {
      emission_5 = vec3(0.0, 0.0, 0.0);
    };
    vec4 rgbm_6;
    vec4 tmpvar_7;
    tmpvar_7.w = 1.0;
    tmpvar_7.xyz = (emission_5 * 0.01030928);
    rgbm_6.xyz = tmpvar_7.xyz;
    rgbm_6.w = max (max (tmpvar_7.x, tmpvar_7.y), max (tmpvar_7.z, 0.02));
    rgbm_6.w = (ceil((rgbm_6.w * 255.0)) / 255.0);
    rgbm_6.w = max (rgbm_6.w, 0.02);
    rgbm_6.xyz = (tmpvar_7.xyz / rgbm_6.w);
    res_3 = rgbm_6;
  };
  gl_FragData[0] = res_3;
}


#endif
"
}
SubProgram "d3d9 " {
// Stats: 43 math
Bind "vertex" Vertex
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Bind "tangent" TexCoord4
Matrix 4 [_Object2World] 3
Matrix 7 [_World2Object] 3
Matrix 0 [glstate_matrix_mvp]
Vector 13 [_MainTex_ST]
Vector 11 [unity_DynamicLightmapST]
Vector 10 [unity_LightmapST]
Vector 12 [unity_MetaVertexControl]
"vs_2_0
def c14, 0, 9.99999975e-005, 0, 0
dcl_position v0
dcl_tangent v1
dcl_normal v2
dcl_texcoord v3
dcl_texcoord1 v4
dcl_texcoord2 v5
slt r0.x, c14.x, v0.z
mul r0.z, r0.x, c14.y
mad r0.xy, v4, c10, c10.zwzw
lrp r1.xyz, c12.x, r0, v0
slt r0.x, c14.x, r1.z
mul r0.z, r0.x, c14.y
mad r0.xy, v5, c11, c11.zwzw
lrp r2.xyz, c12.y, r0, r1
mov r2.w, v0.w
dp4 oPos.x, c0, r2
dp4 oPos.y, c1, r2
dp4 oPos.z, c2, r2
dp4 oPos.w, c3, r2
mad oT0.xy, v3, c13, c13.zwzw
dp4 oT1.w, c4, v0
dp4 oT2.w, c5, v0
dp4 oT3.w, c6, v0
dp3 r0.z, c4, v1
dp3 r0.x, c5, v1
dp3 r0.y, c6, v1
dp3 r0.w, r0, r0
rsq r0.w, r0.w
mul r0.xyz, r0.w, r0
mov oT1.x, r0.z
mul r1.xyz, v2.y, c8.zxyw
mad r1.xyz, c7.zxyw, v2.x, r1
mad r1.xyz, c9.zxyw, v2.z, r1
dp3 r0.w, r1, r1
rsq r0.w, r0.w
mul r1.xyz, r0.w, r1
mul r2.xyz, r0, r1
mad r2.xyz, r1.zxyw, r0.yzxw, -r2
mul r2.xyz, r2, v1.w
mov oT1.y, r2.x
mov oT1.z, r1.y
mov oT2.x, r0.x
mov oT3.x, r0.y
mov oT2.y, r2.y
mov oT3.y, r2.z
mov oT2.z, r1.z
mov oT3.z, r1.x

"
}
SubProgram "d3d11 " {
// Stats: 38 math
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 176 [_MainTex_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
ConstBuffer "UnityLightmaps" 32
Vector 0 [unity_LightmapST]
Vector 16 [unity_DynamicLightmapST]
ConstBuffer "UnityMetaPass" 32
VectorBool 0 [unity_MetaVertexControl] 4
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
BindCB  "UnityLightmaps" 2
BindCB  "UnityMetaPass" 3
"vs_4_0
eefiecedfbpbodbojnihkddpkaejgpopefjlgdbhabaaaaaammaiaaaaadaaaaaa
cmaaaaaaceabaaaameabaaaaejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapadaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapadaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheojiaaaaaaafaaaaaaaiaaaaaaiaaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaaimaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
adamaaaaimaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaaimaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaaimaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaapaaaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklklfdeieefcaaahaaaaeaaaabaamaabaaaafjaaaaaeegiocaaaaaaaaaaa
amaaaaaafjaaaaaeegiocaaaabaaaaaabdaaaaaafjaaaaaeegiocaaaacaaaaaa
acaaaaaafjaaaaaeegiocaaaadaaaaaaabaaaaaafpaaaaadpcbabaaaaaaaaaaa
fpaaaaadpcbabaaaabaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaa
adaaaaaafpaaaaaddcbabaaaaeaaaaaafpaaaaaddcbabaaaafaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaadpccabaaa
acaaaaaagfaaaaadpccabaaaadaaaaaagfaaaaadpccabaaaaeaaaaaagiaaaaac
aeaaaaaadbaaaaahbcaabaaaaaaaaaaaabeaaaaaaaaaaaaackbabaaaaaaaaaaa
abaaaaahecaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaabhlhnbdidcaaaaal
dcaabaaaaaaaaaaaegbabaaaaeaaaaaaegiacaaaacaaaaaaaaaaaaaaogikcaaa
acaaaaaaaaaaaaaadhaaaaakhcaabaaaaaaaaaaaagiacaaaadaaaaaaaaaaaaaa
egacbaaaaaaaaaaaegbcbaaaaaaaaaaadbaaaaahicaabaaaaaaaaaaaabeaaaaa
aaaaaaaackaabaaaaaaaaaaaabaaaaahecaabaaaabaaaaaadkaabaaaaaaaaaaa
abeaaaaabhlhnbdidcaaaaaldcaabaaaabaaaaaaegbabaaaafaaaaaaegiacaaa
acaaaaaaabaaaaaaogikcaaaacaaaaaaabaaaaaadhaaaaakhcaabaaaaaaaaaaa
fgifcaaaadaaaaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaaaaaaaaadiaaaaai
pcaabaaaabaaaaaafgafbaaaaaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaak
pcaabaaaabaaaaaaegiocaaaabaaaaaaaaaaaaaaagaabaaaaaaaaaaaegaobaaa
abaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaaacaaaaaakgakbaaa
aaaaaaaaegaobaaaabaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaa
adaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaa
egbabaaaadaaaaaaegiacaaaaaaaaaaaalaaaaaaogikcaaaaaaaaaaaalaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadiaaaaaihcaabaaaadaaaaaafgbfbaaaaaaaaaaaegiccaaa
abaaaaaaanaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaaabaaaaaaamaaaaaa
agbabaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaa
abaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaa
adaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaaaaaaaaaaegacbaaaadaaaaaa
dgaaaaaficcabaaaacaaaaaaakaabaaaadaaaaaadgaaaaafbccabaaaacaaaaaa
ckaabaaaabaaaaaadgaaaaafeccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaaf
eccabaaaaeaaaaaaakaabaaaaaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaa
abaaaaaadgaaaaafbccabaaaaeaaaaaabkaabaaaabaaaaaadgaaaaaficcabaaa
adaaaaaabkaabaaaadaaaaaadgaaaaaficcabaaaaeaaaaaackaabaaaadaaaaaa
dgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaadgaaaaafcccabaaaaeaaaaaa
ckaabaaaacaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 38 math
Bind "vertex" Vertex
Bind "color" Color
Bind "normal" Normal
Bind "texcoord" TexCoord0
Bind "texcoord1" TexCoord1
Bind "texcoord2" TexCoord2
Bind "tangent" TexCoord4
ConstBuffer "$Globals" 192
Vector 176 [_MainTex_ST]
ConstBuffer "UnityPerDraw" 336
Matrix 0 [glstate_matrix_mvp]
Matrix 192 [_Object2World]
Matrix 256 [_World2Object]
ConstBuffer "UnityLightmaps" 32
Vector 0 [unity_LightmapST]
Vector 16 [unity_DynamicLightmapST]
ConstBuffer "UnityMetaPass" 32
VectorBool 0 [unity_MetaVertexControl] 4
BindCB  "$Globals" 0
BindCB  "UnityPerDraw" 1
BindCB  "UnityLightmaps" 2
BindCB  "UnityMetaPass" 3
"vs_4_0_level_9_1
eefiecedegnekcibefepkjonilgoalaahlmklonnabaaaaaaaeanaaaaaeaaaaaa
daaaaaaageaeaaaagmalaaaageamaaaaebgpgodjcmaeaaaacmaeaaaaaaacpopp
miadaaaageaaaaaaafaaceaaaaaagaaaaaaagaaaaaaaceaaabaagaaaaaaaalaa
abaaabaaaaaaaaaaabaaaaaaaeaaacaaaaaaaaaaabaaamaaahaaagaaaaaaaaaa
acaaaaaaacaaanaaaaaaaaaaadaaaaaaabaaapaaababababaaaaaaaaaaacpopp
fbaaaaafbaaaapkaaaaaaaaabhlhnbdiaaaaaaaaaaaaaaaabpaaaaacafaaaaia
aaaaapjabpaaaaacafaaabiaabaaapjabpaaaaacafaaaciaacaaapjabpaaaaac
afaaadiaadaaapjabpaaaaacafaaaeiaaeaaapjabpaaaaacafaaafiaafaaapja
aeaaaaaeaaaaadoaadaaoejaabaaoekaabaaookaamaaaaadaaaaabiabaaaaaka
aaaakkjaafaaaaadaaaaaeiaaaaaaaiabaaaffkaaeaaaaaeaaaaadiaaeaaoeja
anaaoekaanaaookabcaaaaaeabaaahiaapaaaakaaaaaoeiaaaaaoejaamaaaaad
aaaaabiabaaaaakaabaakkiaafaaaaadaaaaaeiaaaaaaaiabaaaffkaaeaaaaae
aaaaadiaafaaoejaaoaaoekaaoaaookabcaaaaaeacaaahiaapaaffkaaaaaoeia
abaaoeiaafaaaaadaaaaapiaacaaffiaadaaoekaaeaaaaaeaaaaapiaacaaoeka
acaaaaiaaaaaoeiaaeaaaaaeaaaaapiaaeaaoekaacaakkiaaaaaoeiaaeaaaaae
aaaaapiaafaaoekaaaaappjaaaaaoeiaaeaaaaaeaaaaadmaaaaappiaaaaaoeka
aaaaoeiaabaaaaacaaaaammaaaaaoeiaafaaaaadaaaaahiaabaaffjaahaamjka
aeaaaaaeaaaaahiaagaamjkaabaaaajaaaaaoeiaaeaaaaaeaaaaahiaaiaamjka
abaakkjaaaaaoeiaaiaaaaadaaaaaiiaaaaaoeiaaaaaoeiaahaaaaacaaaaaiia
aaaappiaafaaaaadaaaaahiaaaaappiaaaaaoeiaabaaaaacabaaaboaaaaakkia
afaaaaadabaaaciaacaaaajaakaaaakaafaaaaadabaaaeiaacaaaajaalaaaaka
afaaaaadabaaabiaacaaaajaamaaaakaafaaaaadacaaaciaacaaffjaakaaffka
afaaaaadacaaaeiaacaaffjaalaaffkaafaaaaadacaaabiaacaaffjaamaaffka
acaaaaadabaaahiaabaaoeiaacaaoeiaafaaaaadacaaaciaacaakkjaakaakkka
afaaaaadacaaaeiaacaakkjaalaakkkaafaaaaadacaaabiaacaakkjaamaakkka
acaaaaadabaaahiaabaaoeiaacaaoeiaaiaaaaadaaaaaiiaabaaoeiaabaaoeia
ahaaaaacaaaaaiiaaaaappiaafaaaaadabaaahiaaaaappiaabaaoeiaafaaaaad
acaaahiaaaaaoeiaabaaoeiaaeaaaaaeacaaahiaabaanciaaaaamjiaacaaoeib
afaaaaadacaaahiaacaaoeiaabaappjaabaaaaacabaaacoaacaaaaiaabaaaaac
abaaaeoaabaaffiaafaaaaadadaaahiaaaaaffjaahaaoekaaeaaaaaeadaaahia
agaaoekaaaaaaajaadaaoeiaaeaaaaaeadaaahiaaiaaoekaaaaakkjaadaaoeia
aeaaaaaeadaaahiaajaaoekaaaaappjaadaaoeiaabaaaaacabaaaioaadaaaaia
abaaaaacacaaaboaaaaaaaiaabaaaaacadaaaboaaaaaffiaabaaaaacacaaacoa
acaaffiaabaaaaacadaaacoaacaakkiaabaaaaacacaaaeoaabaakkiaabaaaaac
adaaaeoaabaaaaiaabaaaaacacaaaioaadaaffiaabaaaaacadaaaioaadaakkia
ppppaaaafdeieefcaaahaaaaeaaaabaamaabaaaafjaaaaaeegiocaaaaaaaaaaa
amaaaaaafjaaaaaeegiocaaaabaaaaaabdaaaaaafjaaaaaeegiocaaaacaaaaaa
acaaaaaafjaaaaaeegiocaaaadaaaaaaabaaaaaafpaaaaadpcbabaaaaaaaaaaa
fpaaaaadpcbabaaaabaaaaaafpaaaaadhcbabaaaacaaaaaafpaaaaaddcbabaaa
adaaaaaafpaaaaaddcbabaaaaeaaaaaafpaaaaaddcbabaaaafaaaaaaghaaaaae
pccabaaaaaaaaaaaabaaaaaagfaaaaaddccabaaaabaaaaaagfaaaaadpccabaaa
acaaaaaagfaaaaadpccabaaaadaaaaaagfaaaaadpccabaaaaeaaaaaagiaaaaac
aeaaaaaadbaaaaahbcaabaaaaaaaaaaaabeaaaaaaaaaaaaackbabaaaaaaaaaaa
abaaaaahecaabaaaaaaaaaaaakaabaaaaaaaaaaaabeaaaaabhlhnbdidcaaaaal
dcaabaaaaaaaaaaaegbabaaaaeaaaaaaegiacaaaacaaaaaaaaaaaaaaogikcaaa
acaaaaaaaaaaaaaadhaaaaakhcaabaaaaaaaaaaaagiacaaaadaaaaaaaaaaaaaa
egacbaaaaaaaaaaaegbcbaaaaaaaaaaadbaaaaahicaabaaaaaaaaaaaabeaaaaa
aaaaaaaackaabaaaaaaaaaaaabaaaaahecaabaaaabaaaaaadkaabaaaaaaaaaaa
abeaaaaabhlhnbdidcaaaaaldcaabaaaabaaaaaaegbabaaaafaaaaaaegiacaaa
acaaaaaaabaaaaaaogikcaaaacaaaaaaabaaaaaadhaaaaakhcaabaaaaaaaaaaa
fgifcaaaadaaaaaaaaaaaaaaegacbaaaabaaaaaaegacbaaaaaaaaaaadiaaaaai
pcaabaaaabaaaaaafgafbaaaaaaaaaaaegiocaaaabaaaaaaabaaaaaadcaaaaak
pcaabaaaabaaaaaaegiocaaaabaaaaaaaaaaaaaaagaabaaaaaaaaaaaegaobaaa
abaaaaaadcaaaaakpcaabaaaaaaaaaaaegiocaaaabaaaaaaacaaaaaakgakbaaa
aaaaaaaaegaobaaaabaaaaaadcaaaaakpccabaaaaaaaaaaaegiocaaaabaaaaaa
adaaaaaapgbpbaaaaaaaaaaaegaobaaaaaaaaaaadcaaaaaldccabaaaabaaaaaa
egbabaaaadaaaaaaegiacaaaaaaaaaaaalaaaaaaogikcaaaaaaaaaaaalaaaaaa
diaaaaaiccaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaaaaaaaaaakbabaaaacaaaaaaakiacaaaabaaaaaabcaaaaaa
diaaaaaiccaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabaaaaaaa
diaaaaaiecaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabbaaaaaa
diaaaaaibcaabaaaabaaaaaabkbabaaaacaaaaaabkiacaaaabaaaaaabcaaaaaa
aaaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaai
ccaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabaaaaaaadiaaaaai
ecaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabbaaaaaadiaaaaai
bcaabaaaabaaaaaackbabaaaacaaaaaackiacaaaabaaaaaabcaaaaaaaaaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaaegacbaaaabaaaaaabaaaaaahicaabaaa
aaaaaaaaegacbaaaaaaaaaaaegacbaaaaaaaaaaaeeaaaaaficaabaaaaaaaaaaa
dkaabaaaaaaaaaaadiaaaaahhcaabaaaaaaaaaaapgapbaaaaaaaaaaaegacbaaa
aaaaaaaadgaaaaafeccabaaaacaaaaaabkaabaaaaaaaaaaadiaaaaaihcaabaaa
abaaaaaafgbfbaaaabaaaaaajgiecaaaabaaaaaaanaaaaaadcaaaaakhcaabaaa
abaaaaaajgiecaaaabaaaaaaamaaaaaaagbabaaaabaaaaaaegacbaaaabaaaaaa
dcaaaaakhcaabaaaabaaaaaajgiecaaaabaaaaaaaoaaaaaakgbkbaaaabaaaaaa
egacbaaaabaaaaaabaaaaaahicaabaaaaaaaaaaaegacbaaaabaaaaaaegacbaaa
abaaaaaaeeaaaaaficaabaaaaaaaaaaadkaabaaaaaaaaaaadiaaaaahhcaabaaa
abaaaaaapgapbaaaaaaaaaaaegacbaaaabaaaaaadiaaaaahhcaabaaaacaaaaaa
egacbaaaaaaaaaaaegacbaaaabaaaaaadcaaaaakhcaabaaaacaaaaaacgajbaaa
aaaaaaaajgaebaaaabaaaaaaegacbaiaebaaaaaaacaaaaaadiaaaaahhcaabaaa
acaaaaaaegacbaaaacaaaaaapgbpbaaaabaaaaaadgaaaaafcccabaaaacaaaaaa
akaabaaaacaaaaaadiaaaaaihcaabaaaadaaaaaafgbfbaaaaaaaaaaaegiccaaa
abaaaaaaanaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaaabaaaaaaamaaaaaa
agbabaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaaadaaaaaaegiccaaa
abaaaaaaaoaaaaaakgbkbaaaaaaaaaaaegacbaaaadaaaaaadcaaaaakhcaabaaa
adaaaaaaegiccaaaabaaaaaaapaaaaaapgbpbaaaaaaaaaaaegacbaaaadaaaaaa
dgaaaaaficcabaaaacaaaaaaakaabaaaadaaaaaadgaaaaafbccabaaaacaaaaaa
ckaabaaaabaaaaaadgaaaaafeccabaaaadaaaaaackaabaaaaaaaaaaadgaaaaaf
eccabaaaaeaaaaaaakaabaaaaaaaaaaadgaaaaafbccabaaaadaaaaaaakaabaaa
abaaaaaadgaaaaafbccabaaaaeaaaaaabkaabaaaabaaaaaadgaaaaaficcabaaa
adaaaaaabkaabaaaadaaaaaadgaaaaaficcabaaaaeaaaaaackaabaaaadaaaaaa
dgaaaaafcccabaaaadaaaaaabkaabaaaacaaaaaadgaaaaafcccabaaaaeaaaaaa
ckaabaaaacaaaaaadoaaaaabejfdeheopaaaaaaaaiaaaaaaaiaaaaaamiaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapapaaaanbaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaapapaaaanjaaaaaaaaaaaaaaaaaaaaaaadaaaaaaacaaaaaa
ahahaaaaoaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaadaaaaaaapadaaaaoaaaaaaa
abaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapadaaaaoaaaaaaaacaaaaaaaaaaaaaa
adaaaaaaafaaaaaaapadaaaaoaaaaaaaadaaaaaaaaaaaaaaadaaaaaaagaaaaaa
apaaaaaaojaaaaaaaaaaaaaaaaaaaaaaadaaaaaaahaaaaaaapaaaaaafaepfdej
feejepeoaafeebeoehefeofeaaeoepfcenebemaafeeffiedepepfceeaaedepem
epfcaaklepfdeheojiaaaaaaafaaaaaaaiaaaaaaiaaaaaaaaaaaaaaaabaaaaaa
adaaaaaaaaaaaaaaapaaaaaaimaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaa
adamaaaaimaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaaimaaaaaa
acaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaaimaaaaaaadaaaaaaaaaaaaaa
adaaaaaaaeaaaaaaapaaaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfcee
aaklklkl"
}
}
Program "fp" {
SubProgram "opengl " {
"!!GLSL"
}
SubProgram "d3d9 " {
// Stats: 15 math, 1 textures
Vector 0 [_Color]
Float 3 [unity_MaxOutputValue]
Vector 1 [unity_MetaFragmentControl]
Float 2 [unity_OneOverOutputBoost]
SetTexture 0 [_MainTex] 2D 0
"ps_2_0
def c4, 0, 0, 0, 0.0199999996
dcl t0.xy
dcl_2d s0
texld r0, t0, s0
mov_sat r0.w, c2.x
mul_pp r0.xyz, r0, c0
log r1.x, r0.x
log r1.y, r0.y
log r1.z, r0.z
mul r0.xyz, r0.w, r1
exp_pp r1.x, r0.x
exp_pp r1.y, r0.y
exp_pp r1.z, r0.z
min_pp r0.xyz, c3.x, r1
mov r1.xy, c1
cmp_pp r0.xyz, -r1.x, c4.x, r0
mov r0.w, c1.x
cmp_pp r0, -r1.y, r0, c4
mov_pp oC0, r0

"
}
SubProgram "d3d11 " {
// Stats: 5 math, 1 textures
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 192
Vector 144 [_Color]
Float 160 [unity_OneOverOutputBoost]
Float 164 [unity_MaxOutputValue]
ConstBuffer "UnityMetaPass" 32
VectorBool 16 [unity_MetaFragmentControl] 4
BindCB  "$Globals" 0
BindCB  "UnityMetaPass" 1
"ps_4_0
eefiecedpgddlbjahmdgikbgjkdhmnlchfecaplkabaaaaaakmacaaaaadaaaaaa
cmaaaaaammaaaaaaaaabaaaaejfdeheojiaaaaaaafaaaaaaaiaaaaaaiaaaaaaa
aaaaaaaaabaaaaaaadaaaaaaaaaaaaaaapaaaaaaimaaaaaaaaaaaaaaaaaaaaaa
adaaaaaaabaaaaaaadadaaaaimaaaaaaabaaaaaaaaaaaaaaadaaaaaaacaaaaaa
apaaaaaaimaaaaaaacaaaaaaaaaaaaaaadaaaaaaadaaaaaaapaaaaaaimaaaaaa
adaaaaaaaaaaaaaaadaaaaaaaeaaaaaaapaaaaaafdfgfpfaepfdejfeejepeoaa
feeffiedepepfceeaaklklklepfdeheocmaaaaaaabaaaaaaaiaaaaaacaaaaaaa
aaaaaaaaaaaaaaaaadaaaaaaaaaaaaaaapaaaaaafdfgfpfegbhcghgfheaaklkl
fdeieefckeabaaaaeaaaaaaagjaaaaaafjaaaaaeegiocaaaaaaaaaaaalaaaaaa
fjaaaaaeegiocaaaabaaaaaaacaaaaaafkaaaaadaagabaaaaaaaaaaafibiaaae
aahabaaaaaaaaaaaffffaaaagcbaaaaddcbabaaaabaaaaaagfaaaaadpccabaaa
aaaaaaaagiaaaaacabaaaaaaefaaaaajpcaabaaaaaaaaaaaegbabaaaabaaaaaa
eghobaaaaaaaaaaaaagabaaaaaaaaaaadiaaaaaihcaabaaaaaaaaaaaegacbaaa
aaaaaaaaegiccaaaaaaaaaaaajaaaaaacpaaaaafhcaabaaaaaaaaaaaegacbaaa
aaaaaaaadgcaaaagicaabaaaaaaaaaaaakiacaaaaaaaaaaaakaaaaaadiaaaaah
hcaabaaaaaaaaaaaegacbaaaaaaaaaaapgapbaaaaaaaaaaabjaaaaafhcaabaaa
aaaaaaaaegacbaaaaaaaaaaaddaaaaaihcaabaaaaaaaaaaaegacbaaaaaaaaaaa
fgifcaaaaaaaaaaaakaaaaaadgaaaaaficaabaaaaaaaaaaaabeaaaaaaaaaiadp
dhaaaaanpcaabaaaaaaaaaaaagiacaaaabaaaaaaabaaaaaaegaobaaaaaaaaaaa
aceaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaadhaaaaanpccabaaaaaaaaaaa
fgifcaaaabaaaaaaabaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaambmamadm
egaobaaaaaaaaaaadoaaaaab"
}
SubProgram "d3d11_9x " {
// Stats: 5 math, 1 textures
SetTexture 0 [_MainTex] 2D 0
ConstBuffer "$Globals" 192
Vector 144 [_Color]
Float 160 [unity_OneOverOutputBoost]
Float 164 [unity_MaxOutputValue]
ConstBuffer "UnityMetaPass" 32
VectorBool 16 [unity_MetaFragmentControl] 4
BindCB  "$Globals" 0
BindCB  "UnityMetaPass" 1
"ps_4_0_level_9_1
eefiecedgbjdcnhcfnminlepjbbnlihofoeogagpabaaaaaabaaeaaaaaeaaaaaa
daaaaaaajaabaaaadmadaaaanmadaaaaebgpgodjfiabaaaafiabaaaaaaacpppp
biabaaaaeaaaaaaaacaaciaaaaaaeaaaaaaaeaaaabaaceaaaaaaeaaaaaaaaaaa
aaaaajaaacaaaaaaaaaaaaaaabaaabaaabaaacaaababababaaacppppfbaaaaaf
adaaapkaaaaaaaaaaaaaaaaaaaaaaaaaaknhkddmbpaaaaacaaaaaaiaaaaaadla
bpaaaaacaaaaaajaaaaiapkaecaaaaadaaaaapiaaaaaoelaaaaioekaabaaaaac
aaaabiiaabaaaakaafaaaaadaaaachiaaaaaoeiaaaaaoekaapaaaaacabaaabia
aaaaaaiaapaaaaacabaaaciaaaaaffiaapaaaaacabaaaeiaaaaakkiaafaaaaad
aaaaahiaaaaappiaabaaoeiaaoaaaaacabaacbiaaaaaaaiaaoaaaaacabaaccia
aaaaffiaaoaaaaacabaaceiaaaaakkiaakaaaaadaaaachiaabaaffkaabaaoeia
abaaaaacabaaadiaacaaoekafiaaaaaeaaaachiaabaaaaibadaaaakaaaaaoeia
abaaaaacaaaaaiiaacaaaakafiaaaaaeaaaacpiaabaaffibaaaaoeiaadaaoeka
abaaaaacaaaicpiaaaaaoeiappppaaaafdeieefckeabaaaaeaaaaaaagjaaaaaa
fjaaaaaeegiocaaaaaaaaaaaalaaaaaafjaaaaaeegiocaaaabaaaaaaacaaaaaa
fkaaaaadaagabaaaaaaaaaaafibiaaaeaahabaaaaaaaaaaaffffaaaagcbaaaad
dcbabaaaabaaaaaagfaaaaadpccabaaaaaaaaaaagiaaaaacabaaaaaaefaaaaaj
pcaabaaaaaaaaaaaegbabaaaabaaaaaaeghobaaaaaaaaaaaaagabaaaaaaaaaaa
diaaaaaihcaabaaaaaaaaaaaegacbaaaaaaaaaaaegiccaaaaaaaaaaaajaaaaaa
cpaaaaafhcaabaaaaaaaaaaaegacbaaaaaaaaaaadgcaaaagicaabaaaaaaaaaaa
akiacaaaaaaaaaaaakaaaaaadiaaaaahhcaabaaaaaaaaaaaegacbaaaaaaaaaaa
pgapbaaaaaaaaaaabjaaaaafhcaabaaaaaaaaaaaegacbaaaaaaaaaaaddaaaaai
hcaabaaaaaaaaaaaegacbaaaaaaaaaaafgifcaaaaaaaaaaaakaaaaaadgaaaaaf
icaabaaaaaaaaaaaabeaaaaaaaaaiadpdhaaaaanpcaabaaaaaaaaaaaagiacaaa
abaaaaaaabaaaaaaegaobaaaaaaaaaaaaceaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
aaaaaaaadhaaaaanpccabaaaaaaaaaaafgifcaaaabaaaaaaabaaaaaaaceaaaaa
aaaaaaaaaaaaaaaaaaaaaaaambmamadmegaobaaaaaaaaaaadoaaaaabejfdeheo
jiaaaaaaafaaaaaaaiaaaaaaiaaaaaaaaaaaaaaaabaaaaaaadaaaaaaaaaaaaaa
apaaaaaaimaaaaaaaaaaaaaaaaaaaaaaadaaaaaaabaaaaaaadadaaaaimaaaaaa
abaaaaaaaaaaaaaaadaaaaaaacaaaaaaapaaaaaaimaaaaaaacaaaaaaaaaaaaaa
adaaaaaaadaaaaaaapaaaaaaimaaaaaaadaaaaaaaaaaaaaaadaaaaaaaeaaaaaa
apaaaaaafdfgfpfaepfdejfeejepeoaafeeffiedepepfceeaaklklklepfdeheo
cmaaaaaaabaaaaaaaiaaaaaacaaaaaaaaaaaaaaaaaaaaaaaadaaaaaaaaaaaaaa
apaaaaaafdfgfpfegbhcghgfheaaklkl"
}
}
 }
}
Fallback "Legacy Shaders/Transparent/Diffuse"
}