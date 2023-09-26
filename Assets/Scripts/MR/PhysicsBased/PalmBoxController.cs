using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(HandController))]
public class PalmBoxController : MonoBehaviour
{
    HandController controller;
    public List<GameObject> TempToolSet = new();
    void Start()
    {
        controller = GetComponent<HandController>();
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject that = other.transform.root.gameObject;
        if (TempToolSet.Contains(that))  controller.LoadTB(that.GetComponent<NewToolBase>(), that);
    }
    void OnTriggetExit(Collider other){
        GameObject that = other.transform.root.gameObject;
        if (TempToolSet.Contains(that))  controller.UnloadTB(that.GetComponent<NewToolBase>());
    } 
}