using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KILLME : MonoBehaviour
{
    public float A;
    public float B;
    public float C;
    public float D;
    Quaternion AAAAAAA => new Quaternion(A, B, C, D);
    void Start(){
        transform.rotation = AAAAAAA;
    }
}
