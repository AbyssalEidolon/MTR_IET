using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmHandler : MonoBehaviour
{
    public float Distance = 0;
    GameObject target = null;
    CutterTest TestBlock => CutterTest.TestBlock;
    public void OnTriggerEnter(Collider other){
        ToolBase toolBase = other.GetComponent<ToolBase>();
        if(toolBase){
            if(toolBase.ToolType == "cutter"){
                target = other.gameObject;
                if(TestBlock) TestBlock.UpdateToolText("cutter");
            }
        }
    }
    public void OnTriggerExit(Collider other){
        if(target == other.gameObject) target = null;
        if(TestBlock) TestBlock.UpdateToolText();
    }
}