using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CableMould : MonoBehaviour
{
    GameObject[] Vertices = null;
    LineRenderer LineR = null;
    float Length = 0;
    private void Start() {
        LineR = gameObject.GetComponent<LineRenderer>();
        Length = Vector3.Distance(LineR.GetPosition(0), LineR.GetPosition(1));
    }   
    void UpdateLine(){
        
    } 
}
