using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(HandControllerNew))]
public class PalmBoxController : MonoBehaviour
{
    HandControllerNew controller;
    public List<GameObject> TempToolSet = new();

    void Start()
    {
        controller = GetComponent<HandControllerNew>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (TempToolSet.Contains(other.gameObject))
        {
            controller.enabled = true;
            controller.constraints = other.GetComponent<NewToolBase>().constraints;
        };
    }

}