using System;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using static Utils;

[RequireComponent(typeof(WireController))]
public class WireBender : MonoBehaviour {
    WireController controller;
    Vector3 GripA = new();
    Vector3 GripB = new();
    void Awake(){
        controller = GetComponent<WireController>();
    }
    public void StartBend(){
        GetComponent<BoxCollider>().enabled = false;
    }
    public void EndBend(){
        GetComponent<BoxCollider>().enabled = true;
    }
    public void OnFirstGrip(){
        //Grab hand pos
        GripA = Clamp(new Vector3(), controller.Vertices[0], controller.Vertices[1]);
    }
}