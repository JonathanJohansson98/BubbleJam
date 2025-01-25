Shader "Custom/SinusWaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveFrequency ("Wave Frequency", Float) = 1.0
        _WaveAmplitude ("Wave Amplitude", Float) = 0.1
        _WaveSpeed ("Wave Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        float _WaveFrequency;
        float _WaveAmplitude;
        float _WaveSpeed;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Calculate the wave effect
            float wave = sin(_WaveFrequency * IN.uv_MainTex.x + _WaveSpeed * _Time.y) * _WaveAmplitude;
            float2 uv = IN.uv_MainTex;
            uv.y += wave;

            // Sample the texture with the modified UV coordinates
            fixed4 c = tex2D(_MainTex, uv);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
