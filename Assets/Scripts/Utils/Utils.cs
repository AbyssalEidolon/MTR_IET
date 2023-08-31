using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils{
    public delegate void ArglessEvent();
    public delegate void LineEvent(LineRenderer line);
    public delegate void SingleEvent(bool arg);
    public delegate void ArrayEvent(bool[] arg);
    public static Vector3 Clamp(Vector3 original, Vector3 Max, Vector3 Min){
        Vector3 Target = new();
        for(int i = 0; i < 3; i++){
            Target[i] = Mathf.Clamp(original[i], Min[i], Max[i]);
        }
        return Target;
    }
    Vector3 GetNearest(Vector3[] vectors, Vector3 Target, out int index){
        float curDist = 100;
        index = 0;
        for(int i = 0; i < vectors.Length; i++){
            float cur = Vector3.Distance(Target, vectors[i]);
            if(curDist > cur){
                curDist = cur;
                index = i;
            }
        }
        return vectors[index];
    }
    public static float NormalisedVectorCheck(Vector3 Start, Vector3 End, Vector3 Target){
        Vector3 Dist = Target - Start;
        Vector3 Start_End = End - Start;
        float distSqr = Start_End.sqrMagnitude;
        float d = Vector3.Dot(Dist, Start_End) / distSqr;
        return d;
    }
}