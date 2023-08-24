using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Pulley : MonoBehaviour
{
    public static Pulley i = null;
    MeshCollider Collider;
    public Transform Start = null;
    public Transform End = null;
    public LineRenderer Line = null;
    bool Tick = false;

    // public Material mat;
    void Awake()
    {
        i = this;
        Line = GetComponent<LineRenderer>();
    }
    public void UpdateLine()
    {
        Line.SetPosition(0, Start.localPosition);
        Line.SetPosition(1, SceneController.i.SliderThumb.transform.localPosition);
    }
    public void Pickup(){
        Tick = true;
    }
    public void Drop(){
        Tick = false;
    }
    void FixedUpdate(){
        if(Tick) UpdateLine();
    }
    public void BakeCollider()
    {
        if (!Line.GetComponent<MeshCollider>())
        {
            Collider = Line.gameObject.AddComponent<MeshCollider>();
        }
        else { Collider = Line.GetComponent<MeshCollider>(); }
        Mesh mesh = new();
        Line.GetComponent<LineRenderer>().BakeMesh(mesh, true);
        Collider.sharedMesh = mesh;
    }
    public void DestroyCollider(){
        Destroy(Line.GetComponent<MeshCollider>());
    }
}
