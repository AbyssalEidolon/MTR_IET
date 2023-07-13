using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Cutter : ToolBase
{
    public float SkinMargins = 0;
    public GameObject SkinIndPrefab = null;
    LineRenderer newLine = null;
    GameObject oldLineObject => LineManipulator.i.Line.gameObject;
    LineRenderer oldLine => oldLineObject.GetComponent<LineRenderer>();
    public bool DisableOldLine = false;
    
    public override void Duplicate(ManipulationEventData eventData)
    {
        base.Duplicate(eventData);
        LineManipulator.i.BakeCollider();
        foreach(BoxCollider collider in SceneController.i.DisabledOnToolPickup){
            collider.enabled = false;
        };

    }
    public override void Delete(ManipulationEventData eventData)
    {
        base.Delete(eventData);
        LineManipulator.i.DestroyCollider();
        foreach(BoxCollider collider in SceneController.i.DisabledOnToolPickup){
            collider.enabled = true;
        };
    }
    void OnCollisionEnter(Collision other)
    {
        // print(other.gameObject.name);
        if (other.gameObject.name == "PinchSlider" && !newLine)
        {
            if(SceneController.i.TestText) SceneController.i.TestText.text += "<br>" + other.gameObject.name;
            newLine = Instantiate(new GameObject(), oldLineObject.transform.position, oldLine.transform.rotation, oldLine.transform.parent).AddComponent<LineRenderer>(); 
            newLine.name = "Wire";
            newLine.useWorldSpace = false;
            DupeLineInfo(SceneController.Clamp(other.GetContact(0).point, oldLine.GetPosition(1), oldLine.GetPosition(0)));
            oldLine.gameObject.SetActive(!DisableOldLine);
            // print("AAAAAAAAAAAAA");
            gameObject.GetComponent<AudioSource>().Play();
            Delete(new ManipulationEventData());
        }
    }
    void DupeLineInfo(Vector3 PosOnLine){
        newLine.SetPosition(0, PosOnLine);
        newLine.SetPosition(1, SceneController.i.SliderThumb.transform.localPosition);
        newLine.startWidth = oldLine.startWidth;
        newLine.endWidth = oldLine.endWidth;
        newLine.material = oldLine.material;
    }
    void ShowSkinHints(){
        // Instantiate(SkinIndPrefab,)
    }
}