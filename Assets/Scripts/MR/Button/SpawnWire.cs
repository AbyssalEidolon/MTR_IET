using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using static Utils;

public class SpawnWire : MonoBehaviour{
    public Transform parent = null;
    List<LineRenderer> FreeWires => WireValidation.i.FreeWires;
    public static event LineEvent NewLine;
    Material defaultMat;
    void Awake(){
        NewLine += Example;
    }
    void Start()
    {
        defaultMat = Pulley.i.Line.material;
    }
    public void SWire(int LengthCM){
        GameObject Hell = new();
        Hell.transform.parent = parent;
        Hell.transform.localPosition = new();
        InitLineInfo(Hell, LengthCM);
    }
    void InitLineInfo(GameObject target, int LengthCM){
        LineRenderer line = target.AddComponent<LineRenderer>();
        line.name = $"Wire {FreeWires.Count}";
        line.useWorldSpace = false;
        line.tag = "FreeWire";
        line.material = defaultMat;
        line.widthMultiplier = MRWireHandler.Scale / 10;
        line.SetPositions(new Vector3[]{new Vector3(-LengthCM/2*MRWireHandler.Scale, 0, 0), new(LengthCM/2*MRWireHandler.Scale, 0, 0)});
        line.transform.Translate(new(0, FreeWires.Count * 0.1f, 0));
        addObjectManip(line.gameObject);
        FreeWires.Add(line);
        NewLine.Invoke(line);
    }
    void addObjectManip(GameObject target){
        ObjectManipulator objManip = target.AddComponent<ObjectManipulator>();
        objManip.ManipulationType = ManipulationHandFlags.OneHanded;
        target.AddComponent<BoxCollider>().size = new(0.5f, 0.01f, 0.01f);
        target.AddComponent<NearInteractionGrabbable>();
    }
    void Example(LineRenderer l){}
}