using System;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using static Utils;

[RequireComponent(typeof(WireController))]
public class WireBender : MonoBehaviour {
    WireController controller;
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

    }
}