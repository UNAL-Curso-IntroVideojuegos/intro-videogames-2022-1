Shader "Custom/Custom Bar"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "" {}

        _Color ("Primary Color", Color) = (1, 1, 1, 1)
        _ShadowColor ("Shadow Color", Color) = (0.9, 0.9, 0.9, 1)
        _DamagedColor ("Damaged Color", Color) = (0.2, 0.2, 0.2, 1)
        _ShieldColor ("Shield Color", Color) = (0, 0, 1, 1)

        _TickEvery ("Tick Every", Float) = 100
        _TickWidth ("Tick Width", Float) = 0.012
        _Value ("Current Value", Float) = 100
        _MaxValue ("Max Value", Float) = 100
        _DamagedValue ("Damaged Value", Float) = 0
        _ShieldValue ("Shield Value", Float) = 0
        _Alpha ("Current Alpha", Float) = 1
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
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 color : COLOR;
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            uniform half4 _Color;
            uniform sampler2D _MainTex;
            uniform half4 _ShadowColor;
            uniform half4 _DamagedColor;
            uniform half4 _ShieldColor;
            uniform half _TickEvery;
            uniform half _TickWidth;
            uniform half _Value;
            uniform half _MaxValue;
            uniform half _DamagedValue;
            uniform half _ShieldValue;
            uniform half _Alpha;

            v2f vert(appdata i)
            {
                v2f o;

                o.color = i.color * _Color;
                o.vertex = UnityObjectToClipPos(i.vertex);
                o.texcoord = i.texcoord;

                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 t = tex2D(_MainTex, i.texcoord);
                half4 c = t * i.color;
                half totalTicks = _MaxValue / _TickEvery;
                half isShadow = 1 - saturate((abs(t.r - 0.902) + abs(t.g - 0.902) + abs(t.b - 0.902)) * 5);
                half healthRatio = _Value / (_MaxValue + _ShieldValue);
                half shieldStartRatio = _MaxValue / (_MaxValue + _ShieldValue);

                if (i.texcoord.x > shieldStartRatio) {
                    c = _ShieldColor;
                } else if (i.texcoord.x > healthRatio) {
                    half damagedRatio = _DamagedValue / (_MaxValue + _ShieldValue);

                    if (i.texcoord.x > healthRatio + damagedRatio) {
                        c = half4(0, 0, 0, 1);
                    } else {
                        c = _DamagedColor;
                    }
                } else {
                    c.rgb = lerp(c.rgb, _ShadowColor, isShadow);
                }

                if (i.texcoord.x > _TickWidth / 100 && i.texcoord.x % (1 / totalTicks) < _TickWidth / 100) {
                    c.rgb = lerp(c.rgb, half3(0, 0, 0), 0.2);
                }

                c.a = _Alpha;

                return c;
            }
            ENDCG
        }
    }
}
