using System;
using System.Collections.Generic;
using System.Runtime;
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

    //0 is the old sphere, 1 is the new sphere
    public List<GameObject> theCircle = new List<GameObject>();
    public List<GameObject> grap = new List<GameObject>();
    public GameObject thePoint;

    public float distanceSet;

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

        //length between each sphere
        Vector3 length = (Vertices[^1] - Vertices[^2]) / 10f;
        //Vertices.Insert(1, Mid(Vertices[0], Vertices[^1]));
        print(length);
        //spawn sphere by that length
        for(int i = 1; i < 10; i++){
            Vertices.Insert(i, Vertices[0]+(length*i));
        }

        foreach (Vector3 i in Vertices)
        {
            createsphere(i);
        }
        distanceSet = Vector3.Distance(Vertices[0], Vertices[1]);
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
        updatesphereVector(sphere, Vertices);
        spheres.Add(sphere);
    }

    public void updatesphereVector(GameObject sphere, Vector3 Vertices)
    {
        sphere.transform.position = Vertices;
    }

    public void Update()
    {
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vertices[i] = spheres[i].transform.position;
        }

        if (grap.Count == 2)
        {
            //find there only one sphere between of that two grap sphere
            int a = spheres.IndexOf(theCircle[1].gameObject);
            int b = spheres.IndexOf(theCircle[3].gameObject);
            int slength = a - b;
            Debug.Log(a);
            Debug.Log(b);
            if (slength == 2 || slength == -2)
            {
                int c = (a + b) / 2;
                print(c);
                thePoint = spheres[c];
            }
        }

        if (theCircle.Count == 4){
            Vector3 dir = theCircle[2].transform.position - thePoint.transform.position;
            Vector3 projectdir = dir - Vector3.Project(dir, Vector3.forward);
            //projectdir = projectdir.normalized * 0.2475f;
            projectdir = projectdir.normalized * distanceSet;
            theCircle[3].transform.position = projectdir;
        }
    }

    public Vector3 Mid(Vector3 a, Vector3 b)
    {
        Vector3 c = (a + b)/2;
        return c;
    }
}