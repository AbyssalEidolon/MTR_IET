using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{
    public GameObject[] Target;
    Vector3[] InitPos;
    Quaternion[] InitRot;
    void Awake(){
        InitPos = new Vector3[Target.Length];
        InitRot = new Quaternion[Target.Length];
        for(int i = 0; i < Target.Length; i++){
            InitPos[i] = Target[i].transform.position;
            InitRot[i] = Target[i].transform.rotation;
        }
    }
    public void Reset(){
        for(int i = 0; i < Target.Length; i++){
            Target[i].transform.position = InitPos[i];
            Target[i].transform.rotation = InitRot[i];
            Target[i].GetComponent<Rigidbody>().velocity = new(0, 0, 0);
        }
    }
}
