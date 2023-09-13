using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using Microsoft.MixedReality.Toolkit.Examples.Demos.EyeTracking;
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
    //the sphere that grap rn
    public List<GameObject> grab = new List<GameObject>();

    public bool updatesphere;

    public float distanceSet;

    void Awake()
    {
        updatesphere = true;
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

        float num = 14;

        for (int i = 1; i < num; i++)
        {
            float t = i*(1/num);
            print(t);
            Vector3 spawnPos = Vector3.Lerp(Vertices[0], Vertices[^1], t);
            Vertices.Insert(i, spawnPos);
        }

        distanceSet = Vector3.Distance(Vertices[0], Vertices[1]);

        foreach (Vector3 i in Vertices)
        {
            createsphere(i, distanceSet, Vertices.IndexOf(i));
        }
    }
    void FixedUpdate()
    {
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
        else
        {
            Self.positionCount = 2;
            Self.SetPositions(marginVertices);
            foreach (Vector3 margVec in marginVertices) print(margVec.ToString());
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

    public void createsphere(Vector3 Vertices, float size, int where)
    {
        GameObject sphere = Instantiate(Gsphere);
        sphere.transform.localScale = new Vector3(size, size, size);
        sphere.AddComponent<BoxCollider>();
        sphere.transform.position = Vertices;
        spheres.Insert(where, sphere);
    }

    public void Update()
    {
        if (updatesphere)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i] = spheres[i].transform.position;
            }
        }

        if(grab.Count == 1){
            for(int i = 0; i < spheres.Count;i++){
                spheres[i].transform.SetParent(grab[0].transform,true);
            }
        }else if (grab.Count == 2){
            int firstGrap = spheres.IndexOf(grab[0]);
            int secondGrap = spheres.IndexOf(grab[1]);
            Debug.Log(firstGrap+" "+secondGrap);
            if(secondGrap > firstGrap)
            {
                for(int i = firstGrap+1; i < spheres.Count ;i++)
                {
                    spheres[i].transform.SetParent(GameObject.Find("empty").transform);
                }
                for(int i = firstGrap-1; i >= 0 ;i--)
                {
                    spheres[i].transform.SetParent(grab[0].transform);
                }
            }else{
                for(int i = firstGrap-1; i >= 0 ;i--)
                {
                    spheres[i].transform.SetParent(GameObject.Find("empty").transform);
                }
                for(int i = firstGrap+1; i < spheres.Count ;i++)
                {
                    spheres[i].transform.SetParent(grab[0].transform);
                }
            }
        }
    }
}