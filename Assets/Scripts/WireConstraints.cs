using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireConstraints
{
    Vector3[] Vertices;
    Vector3[] Ends = new Vector3[2];
    Vector3[] SkinEnds = new Vector3[2];
    List<float> WireLengths = new();
    List<float> SegmentLengths = new();
    Vector3 WireCentre = new();
    LineRenderer Line;
    float Alpha = 1;
    Color DefColor;
    float WireLength;
    Gradient LineGradient => Line.colorGradient;
    public WireConstraints(LineRenderer line){
        Line = line;
        Line.GetPositions(Ends);
        WireCentre = (Ends[0] + Ends[1])/ 2;
        line.positionCount++;
        line.SetPositions(new[]{
            Ends[0], WireCentre, Ends[1]
        });
        Vertices = new[]{
            Ends[0], WireCentre, Ends[1]
        };
        DefColor = Line.startColor;
        WireLength = Vector3.Distance(Ends[0], Ends[1]);
        // RecalculateSegments();
        // Debug.Log(WireCentre.ToString("F4"));
    }
    // float GradientLocation(Vector3 Seg){
    //     float SegLength = Vector3.Distance(Ends[0], Seg);
    //     return SegLength / WireLength;
    // }
    // public void RecalculateSegments(){
    //     WireLengths.Clear();
    //     int VertexNum;
    //     Vector3[] Vertices = new Vector3[Line.positionCount];
    //     VertexNum = Line.GetPositions(Vertices);
    //     for(int i = 0; i < VertexNum - 1; i ++){
    //         if(Vertices[i + 1] != null){
    //             WireLengths.Add(Vector3.Distance(Vertices[i], Vertices[i + 1]));
    //             // Debug.Log(WireLengths[i].ToString("F4"));
    //         }
    //     }
    //     List<GradientColorKey> initColors = new();
    //     SegmentLengths.Clear();
    //     for(int i = 0; i < VertexNum; i++){
    //         float gradL = GradientLocation(Vertices[i]);
    //         initColors.Add(new(DefColor, gradL));
    //         SegmentLengths.Add(gradL);
    //     }
    //     LineGradient.SetKeys(initColors.ToArray(), LineGradient.alphaKeys);
    // }
    // public void SetSegmentColour(int segment, Color color){
    //     LineGradient.colorKeys[segment] = new(color, SegmentLengths[segment]);
    // }
}