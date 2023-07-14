using UnityEngine;

public class CTestSubset : MonoBehaviour{
    void OnCollisionEnter(Collision other){
        print(other.gameObject.name);
    }
}