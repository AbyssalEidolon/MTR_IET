using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine.Animations;
[RequireComponent(typeof(HandController))]
public class PalmBoxController : MonoBehaviour
{
    public HandController handController;
    public List<GameObject> TempToolSet = new();
    public Vector3 Start;
    void Awake()
    {
        handController = GetComponent<HandController>();
        Start = gameObject.transform.localPosition;
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject that = other.transform.root.gameObject;
        if (TempToolSet.Contains(that))  handController.LoadTB(that.GetComponent<NewToolBase>(), that);
    }
    void OnTriggetExit(Collider other){
        GameObject that = other.transform.root.gameObject;
        if (TempToolSet.Contains(that))  handController.UnloadTB(that.GetComponent<NewToolBase>());
    } 
    public void UpdateHand(Handedness hand){
        foreach(GameObject gameObject in TempToolSet){
            gameObject.GetComponent<ParentConstraint>().SetRotationOffset(0, hand == Handedness.Right? new(180, 0, 30) : new(180, 0, -30));
        }
        gameObject.transform.localPosition = new(hand == Handedness.Left? Start.x : -Start.x, Start.y, Start.z);
    }
}