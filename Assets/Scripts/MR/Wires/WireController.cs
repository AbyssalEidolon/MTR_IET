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
    public bool VertBasedUpdate = false;
    public LineRenderer[] StrippedSegs = new LineRenderer[2];
    public List<Vector3> Vertices = new() { };
    public float[] Margins = new float[2]{
        0.5f, 0.5f
    };
    public LineRenderer Self;
    public Material marginMat = null;
    public List<GameObject> spheres = new List<GameObject>();
    public GameObject Gsphere;
    public GameObject projectSphere;
    public GameObject cursor;
    public GameObject cube;

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
        Vector3 midpoint = (Vertices[0] + Vertices[^1]) / 2f;
        Vertices.Insert(1, midpoint);
        foreach (Vector3 i in Vertices)
        {
            createsphere(i);
        }
        cursor = GameObject.Find("DefaultGazeCursor(Clone)");
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

    public void createsphere(Vector3 Vertices)
    {
        GameObject sphere = Instantiate(Gsphere);
        sphere.transform.localScale = new Vector3(0.033f, 0.033f, 0.033f);
        sphere.AddComponent<BoxCollider>();
        updateCubeVector(sphere, Vertices);
        spheres.Add(sphere);
    }

    public void updateCubeVector(GameObject sphere, Vector3 Vertices)
    {
        sphere.transform.position = Vertices;
    }

    public void Update()
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i] = spheres[i].transform.position;
        }
        Vector3 dir = cursor.transform.position - spheres[1].transform.position;
        projectSphere.transform.position = dir - Vector3.Project(dir, Vector3.forward);
        Vector3 projectdir = dir - Vector3.Project(dir, Vector3.forward);
        projectdir = projectdir.normalized * 0.2475f;
        cube.transform.position = projectdir;
        //Debug.Log(Vector3.Project(Vector3.up, cursor.transform.position - spheres[1].transform.position));
    }
}