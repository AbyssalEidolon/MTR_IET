using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        print(other.name);
    }
}
