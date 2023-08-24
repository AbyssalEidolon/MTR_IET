using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject Cube = null;
    Vector3 StartPos;
    Quaternion StartRot;
    void Start(){
        StartPos = Cube.transform.position;
        StartRot = Cube.transform.rotation;
    }
    public void SummonCube(){
        Instantiate(Cube, StartPos, StartRot);
    }
}
