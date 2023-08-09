using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;

[RequireComponent(typeof(AudioSource))]
public class Cutter : ToolBase
{
    public float SkinMargins = 0;
    public GameObject SkinIndPrefab = null;
    public LineRenderer newLine = null;
    GameObject oldLineObject => LineManipulator.i.Line.gameObject;
    LineRenderer oldLine => oldLineObject.GetComponent<LineRenderer>();
    public override string ToolType() => "cutter";
    public bool DisableOldLine = false;
    public static Cutter i = null;

    private void Awake()
    {
        i = this;
    }

    public override void Duplicate(ManipulationEventData eventData)
    {
        base.Duplicate(eventData);
        LineManipulator.i.BakeCollider();
        foreach(BoxCollider collider in SceneController.i.DisabledOnToolPickup){
            if(!SceneController.i.ToolBarsLocked)collider.enabled = false;
        };
    }
    public override void Delete(ManipulationEventData eventData)
    {
        base.Delete(eventData);
        LineManipulator.i.DestroyCollider();
        foreach(BoxCollider collider in SceneController.i.DisabledOnToolPickup){
            if(!SceneController.i.ToolBarsLocked)collider.enabled = true;
        };
    }
    protected override void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (other.gameObject.name == "PinchSlider")
        {
            if(SceneController.i.TestText) SceneController.i.TestText.text += "<br>" + other.gameObject.name;
            // LineRenderer newLine = Instantiate(new GameObject(), oldLineObject.transform.position, oldLine.transform.rotation, oldLine.transform.parent).AddComponent<LineRenderer>(); 
            // newLine.name = "Wire";
            // newLine.useWorldSpace = false;
            // newLine.tag = "FreeWire";
            // DupeLineInfo(newLine, Utils.Clamp(other.GetContact(0).point, oldLine.GetPosition(1), oldLine.GetPosition(0)));
            // addObjectManip(newLine.gameObject);
            // newLine.transform.Translate(0, 0, -0.1f);
            // newLine.gameObject.AddComponent<BoxCollider>().size = new(0.5f, 0.1f, 0.1f);
            // newLine.gameObject.layer = 3;
            // WireController.i.ValidateWire(newLine);
            // oldLine.gameObject.SetActive(!DisableOldLine);
            // // print("AAAAAAAAAAAAA");
            // gameObject.GetComponent<AudioSource>().Play();
            LineRenderer newLine = new GameObject().AddComponent<LineRenderer>();
            newLine.gameObject.SetActive(false);
            DupeLineInfo(newLine, Utils.Clamp(other.GetContact(0).point, oldLine.GetPosition(1), oldLine.GetPosition(0)));
            WireController.i.ValidateWire(newLine);
            Delete(new ManipulationEventData());
        }
    }
    void DupeLineInfo(LineRenderer line, Vector3 PosOnLine){
        line.SetPosition(0, PosOnLine);
        line.SetPosition(1, SceneController.i.SliderThumb.transform.localPosition);
        line.startWidth = oldLine.startWidth;
        line.endWidth = oldLine.endWidth;
        line.material = oldLine.material;
        // FreeWires.Add(line);
    }
    MeshCollider BakeCollider(LineRenderer line){
        MeshCollider collider = null;
        if (!line.GetComponent<MeshCollider>())
        {
            collider = line.gameObject.AddComponent<MeshCollider>();
        }
        else { collider = line.GetComponent<MeshCollider>(); }
        Mesh mesh = new();
        line.GetComponent<LineRenderer>().BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
        return collider;
    }
    void addObjectManip(GameObject target){
        ObjectManipulator objManip = target.AddComponent<ObjectManipulator>();
        objManip.ManipulationType = ManipulationHandFlags.OneHanded;
        // objManip.AllowFarManipulation = false;
    }

    MeshCollider BakeCollider(LineRenderer line)
    {
        MeshCollider collider = null;
        if (!line.GetComponent<MeshCollider>())
        {
            collider = line.gameObject.AddComponent<MeshCollider>();
        }
        else { collider = line.GetComponent<MeshCollider>(); }
        Mesh mesh = new();
        line.GetComponent<LineRenderer>().BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
        return collider;
    }
}