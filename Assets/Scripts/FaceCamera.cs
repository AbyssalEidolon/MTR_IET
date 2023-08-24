using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    GameObject Camera = null;
    void Awake(){
        Camera = GameObject.FindWithTag("MainCamera");
    }
    void FixedUpdate(){
        transform.LookAt(Camera.transform, Vector3.up);
    }
}
