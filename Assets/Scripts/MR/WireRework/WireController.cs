using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    public static WireController i = null;
    int[] WireLengths = {8, 10, 12};
    bool[] WireChecks = {false, false, false};
    public float Margin = 0.5f;
    float Scale => MRWireHandler.Scale;
    public static event Utils.SingleEvent WireValid;
    public static event Utils.ArrayEvent WireUISync;
    float WireLength(LineRenderer wire) => Vector3.Distance(wire.GetPosition(0), wire.GetPosition(wire.positionCount - 1));
    public List<LineRenderer> FreeWires = new();
    void Awake(){
        i = this;
        if(Margin >= 1){
            Debug.LogError("Margins will introduce the wire to be counted towards multiple goals");
        }
        WireValid += Example;
        WireUISync += Example;
    }
    public void ValidateWire(LineRenderer wire){
        float wireLength = WireLength(wire) / Scale;
        // print(wireLength);
        bool valid = false;
        for(int i = 0; i < WireLengths.Length; i++){
            if(wireLength >= WireLengths[i] - Margin && wireLength <= WireLengths[i] + Margin && !WireChecks[i]){
                WireChecks[i] = true;
                valid = true;
                break;
            }
        }
        WireValid.Invoke(valid);
        WireUISync.Invoke(WireChecks);
    }
    void Example(bool b){
        print(b);
    }
    void Example(bool[] b){
        string newString = "";
        foreach(bool fuck in b) newString += fuck.ToString() + " ";
        print(newString);
    }
}
