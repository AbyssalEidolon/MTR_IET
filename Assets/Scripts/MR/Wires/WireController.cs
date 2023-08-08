using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WireController : MonoBehaviour {
    public float[] SkinMargins = new float[2];
    public List<Vector3> Vertices = new();
    void Start(){
        /*
        (ADD IN MARGIN CHECKS)
        */
        Vector3[] temp = new Vector3[]{ };
        GetComponent<LineRenderer>().GetPositions(temp);
        Vertices.AddRange(temp);
    }
    public float GetDistance(int V1, int V2){
        return Vector3.Distance(Vertices[V1], Vertices[V2]);
    }
}