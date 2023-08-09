using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WireController : MonoBehaviour {
    float Scale => MRWireHandler.Scale;
    public bool Init = false;
    LineRenderer[] StrippedSegs = new LineRenderer[2];
    public List<Vector3> Vertices = new();
    public float[] Margins = new float[2]{
        0.5f, 0.5f
    };
    void Awake(){
        CheckParent();
        if(Init) TestInit();
    }
    void Start(){
        Vector3[] temp = new Vector3[]{ };
        GetComponent<LineRenderer>().GetPositions(temp);
        Vertices.AddRange(temp);
    }
    public float GetDistance(int V1, int V2){
        return Vector3.Distance(Vertices[V1], Vertices[V2]);
    }
    void TestInit(){
        LineRenderer temp = Instantiate(this.gameObject, this.transform).AddComponent<LineRenderer>();
        
    }
    void CheckParent(){
        if(GetComponentInParent<WireController>()) Destroy(this);
    }
}