using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WireController : MonoBehaviour
{
    /*
    Wires should be treated as vertices instead of lines for ease of access and management.
    */
    float Scale => MRWireHandler.Scale;
    public bool Init = false;
    LineRenderer[] StrippedSegs = new LineRenderer[2];
    public List<Vector3> Vertices = new(){};
    public float[] Margins = new float[2]{
        0.5f, 0.5f
    };
    int boolToInt(bool ballin) => ballin ? 1 : 0;
    public LineRenderer Self;
    void Awake()
    {
        print("WHAT");
        CheckParent();
        Self = this.GetComponent<LineRenderer>();
    }
    void Start()
    {
        if (Init) TestInit();
        for (int i = 0; i < StrippedSegs.Length; i++)
        {
            Vertices.Add(StrippedSegs[i].GetPosition(0));
        }
    }
    public float GetDistance(int V1, int V2)
    {
        return Vector3.Distance(Vertices[V1], Vertices[V2]);
    }
    void TestInit()
    {
        bool temp = false;
        Vector3[] newVecs = new Vector3[2];
        for (int i = 0; i < StrippedSegs.Length; i++)
        {
            LineRenderer tempLine = Instantiate(gameObject, transform).GetComponent<LineRenderer>();
            Vector3 TargetVec = Vector3.MoveTowards(Self.GetPosition(boolToInt(temp)), gameObject.transform.position, Margins[boolToInt(temp)] * Scale);
            tempLine.SetPositions(new Vector3[]{
                Self.GetPosition(boolToInt(temp)),
                TargetVec
            });
            tempLine.material.color = Color.black;
            newVecs[i] = TargetVec;
            temp = !temp;
            StrippedSegs[i] = tempLine;
        }
        Self.SetPositions(newVecs);
    }
    void CheckParent()
    {
        if (transform.parent)
            if (GetComponentInParent<WireController>())
            {
                for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
                Destroy(this);
            };
    }
    public void UpdateLine()
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            if(i == 0){
                StrippedSegs[0].SetPosition(i, Vertices[0]);
            }
        }
    }
}