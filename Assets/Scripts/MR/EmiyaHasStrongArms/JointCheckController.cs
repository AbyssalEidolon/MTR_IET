using System;
using UnityEngine;

public class JointCheckController : MonoBehaviour {
    [HideInInspector]
    public GameObject targetAreaPrefab = null;
    bool[] jointChecks = new bool[5];
    GameObject[] CheckAreas = new GameObject[5];
    Vector3 checkAreaInitScale = new();
    BoxCollider[] boxColliders = new BoxCollider[5];
    public float[] NormalisedJointValues = new float[5]{
        -1, -1, -1, -1, -1
    };
    JointTriggerArea[] triggerAreas = new JointTriggerArea[5];
    void Start(){
        checkAreaInitScale = targetAreaPrefab.GetComponent<BoxCollider>().size;
        for(int i = 0; i < CheckAreas.Length; i++){
            CheckAreas[i] = Instantiate(targetAreaPrefab, transform);
            JointTriggerArea TriggerArea = CheckAreas[i].AddComponent<JointTriggerArea>();
            TriggerArea.targetJoint = HandInteractionController.i.targetJoints[i];
            TriggerArea.ActiveColour = HandInteractionController.i.AreaColourActive;
            TriggerArea.controller = this;
            TriggerArea.ID = i;
            TriggerArea.name = HandInteractionController.i.targetJoints[i].ToString();
            triggerAreas[i] = TriggerArea;
            CheckAreas[i].SetActive(false);
            boxColliders[i] = CheckAreas[i].GetComponent<BoxCollider>();
        }
    }
    public void LoadConfig(ToolGripConfigurator config){
        for(int i = 0; i < config.FingerConfigs.Length; i++){
            CheckAreas[i].SetActive(config.FingerConfigs[i].Active);
            triggerAreas[i].LoadConfig(config.FingerConfigs[i]);
        }
    }
}