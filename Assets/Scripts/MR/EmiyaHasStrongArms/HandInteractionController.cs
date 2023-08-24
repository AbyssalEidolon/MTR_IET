using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.MixedReality.Toolkit.Utilities;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HandInteractionController : MonoBehaviour
{
    //Assigned on palm.
    //Config will be per tool.
    //Loads trigger spots when tool is by palm(this.gameObject) via its own trigger, picks up target when tool-configured targets are reached.
    //This will abuse trigger and physics mechanics so shut the fuck up.
    //Palm will have its own layer, only detects if tool is in range + which type(OnTriggerEnter) [Palm/Tools]{layers}
    //Targeted joints will have their own layer, only checks if targets are met(OnTriggerEnter + Exit)[Joints/JointsTargets]{layers}
    public static HashSet<ToolBase> ToolTypes = new();
    public static HandInteractionController i;
    public GameObject JointTrackingPrefab = null;
    public GameObject TargetAreaPrefab = null;
    Joint[] joints = new Joint[5];
    string[] fingerNames = {"Thumb", "Pointer", "Middle", "Ring", "Pinky"};
    public bool DeployBuild = true;
    public bool PrintDebugMessages = true;
    JointCheckController jointCheckController = null;
    public Color AreaColourActive = Color.white;
    public readonly TrackedHandJoint[] targetJoints = {
        TrackedHandJoint.ThumbDistalJoint, TrackedHandJoint.IndexMiddleJoint, TrackedHandJoint.MiddleMiddleJoint, TrackedHandJoint.RingMiddleJoint, TrackedHandJoint.PinkyMiddleJoint
    };
    void Awake()
    {

        if(!JointTrackingPrefab.GetComponent<Collider>())Debug.LogError("Joints Are Missing Colliders!");
        Collider collider = TargetAreaPrefab.GetComponent<Collider>();
        if(!collider)Debug.LogError("Target Areas Are Missing Colliders!");
        if(!collider.isTrigger)Debug.LogError("Target Areas Have Not Been Set To Triggers!");

        jointCheckController = gameObject.AddComponent<JointCheckController>();
        jointCheckController.targetAreaPrefab = TargetAreaPrefab;
        i = this;
        for (int i = 0; i < joints.Length; i ++)
        {
            GameObject joint = Instantiate(JointTrackingPrefab, transform);
            joints[i] = joint.AddComponent<Joint>();
            joints[i].targetJoint = targetJoints[i];
            joints[i].controller = this;
            joints[i].name = fingerNames[i];
            if(DeployBuild)joint.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other){
        ToolBase toolBase = other.GetComponent<ToolBase>();
        if(toolBase == null || !ToolTypes.Contains(toolBase)) return;
        print(toolBase.gameObject.name);
        jointCheckController.LoadConfig(toolBase.config);
    }
}