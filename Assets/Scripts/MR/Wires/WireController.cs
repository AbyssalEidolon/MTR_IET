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
    public bool VertBasedUpdate = true;
    public LineRenderer[] StrippedSegs = new LineRenderer[2];
    public List<Vector3> Vertices = new() { };
    public float[] Margins = new float[2]{
        0.5f, 0.5f
    };
    public LineRenderer Self;
    public Material marginMat = null;
    void Awake()
    {
        print("WHAT");
        CheckParent();
        Self = this.GetComponent<LineRenderer>();
    }
    void Start()
    {
        if (Init)
        {
            SpawnMargins test = gameObject.AddComponent<SpawnMargins>();
            test.controller = this;
            test.Init();
        }
        for (int i = 0; i < StrippedSegs.Length; i++)
        {
            Vertices.Add(StrippedSegs[i].GetPosition(0));
        }
    }
    void FixedUpdate(){
        if (VertBasedUpdate) UpdateLine();
    }
    public float GetDistance(int V1, int V2)
    {
        return Vector3.Distance(Vertices[V1], Vertices[V2]);
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
        Vector3[] marginVertices = new Vector3[2];
        //Sets values for first disconnected segment of the wire(Stripped).
        StrippedSegs[0].SetPosition(0, Vertices[0]);
        marginVertices[0] = Vector3.MoveTowards(Vertices[0], Vertices[1], Margins[0] * Scale);
        StrippedSegs[0].SetPosition(1, marginVertices[0]);


        StrippedSegs[1].SetPosition(0, Vertices[^1]);
        marginVertices[1] = Vector3.MoveTowards(Vertices[^1], Vertices[^2], Margins[1] * Scale);
        StrippedSegs[1].SetPosition(1, marginVertices[1]);

        if (Vertices.Count > 2)
        {
            List<Vector3> tempVerts = new(Vertices);
            tempVerts[0] = marginVertices[0];
            tempVerts[^1] = marginVertices[1];
            Self.positionCount = tempVerts.Count;
            Self.SetPositions(tempVerts.ToArray());
        }
        else{
            Self.positionCount = 2;
            Self.SetPositions(marginVertices);
            foreach(Vector3 margVec in marginVertices) print(margVec.ToString());
        }
    }
    public void BakeCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();
        if (collider == null) collider = gameObject.AddComponent<MeshCollider>();
        Mesh mesh = new();
        Self.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }
}