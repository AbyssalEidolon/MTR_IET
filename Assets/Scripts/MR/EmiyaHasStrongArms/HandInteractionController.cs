using System;
using System.Collections.Generic;
using UnityEngine;

public class HandInteractionController : MonoBehaviour
{
    //Assigned on palm.
    //Config will be per tool.
    //Loads trigger spots when tool is by palm(this.gameObject) via its own trigger, picks up target when tool-configured targets are reached.
    //This will abuse trigger and physics mechanics so shut the fuck up.
    //Palm will have its own layer, only detects if tool is in range + which type(OnTriggerEnter) [Palm/Tools]{layers}
    //Targeted joints will have their own layer, only checks if targets are met(OnTriggerEnter + Exit)[Joints/JointsTargets]{layers}
    public static HashSet<string> ToolTypes = new();
    public static HandInteractionController i;
    public GameObject JointTrackingPrefab = null;
    JointController[] joints = new JointController[5];
    bool[] jointChecks = new bool[5];

    void Awake()
    {
        i = this;
        for (int i = 0; i < joints.Length; i ++)
        {
            GameObject joint = Instantiate(JointTrackingPrefab, this.transform);
            joints[i] = joint.AddComponent<JointController>();
            joints[i].controller = this;
            joint.SetActive(false);

        }
    }
    void OnTriggerEnter(){

    }
    //     void Start(){
    //         foreach(string V in ToolTypes){
    //             print(V);
    //         }
    //     }

}