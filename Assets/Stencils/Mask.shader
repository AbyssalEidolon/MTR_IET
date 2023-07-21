Shader "MRTKPac/Mask"
{
    Properties
    {
        _Color ("Main Color", Color) = (1.000000,1.000000,1.000000,1.000000)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend Zero One
        Stencil{
            Ref 2
            Comp Always
            Pass Replace
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
