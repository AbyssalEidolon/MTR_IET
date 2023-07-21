Shader "MRTKPac/Object"
{
    Properties
    {
       _Color ("Main Color", Color) = (1.000000,1.000000,1.000000,1.000000)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Stencil{
            Ref 2
            Comp Equal
        }
        Pass
        {
            SetTexture [_MainTex] {
                constantColor [_Color]
                Combine texture * constant, texture * constant 
            }
        }
    }
}
