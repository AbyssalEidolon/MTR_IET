using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    void OnCollisionEnter(Collision other){
        print(other.contacts[0]);
        print(other.gameObject.name);
    }
}
