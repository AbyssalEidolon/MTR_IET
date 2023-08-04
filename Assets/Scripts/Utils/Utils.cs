using System.Collections;
using System.Collections.Generic;
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
}