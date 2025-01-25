Shader "Custom/VibratingBubbleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VibrationFrequency ("Vibration Frequency", Float) = 10.0
        _VibrationAmplitude ("Vibration Amplitude", Float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade vertex:vert

        sampler2D _MainTex;
        float _VibrationFrequency;
        float _VibrationAmplitude;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert (inout appdata_full v)
        {
            // Calculate the vibration effect
            float vibration = sin(_VibrationFrequency * _Time.y + v.vertex.x * 10.0) * _VibrationAmplitude;
            v.vertex.x += vibration;
            v.vertex.y += vibration;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Sample the texture
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
