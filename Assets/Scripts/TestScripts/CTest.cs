using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTest : MonoBehaviour
{
    public List<GameObject> WithCollider = new();
    void Start(){
        GameObject[] Gamering = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach(GameObject obj in Gamering){
            if(obj.GetComponent<Collider>()){
                WithCollider.Add(obj);
                obj.AddComponent<CTestSubset>();
            }
        }
    }
    
    public void CallColliders(){
        foreach(GameObject obj in WithCollider){
            if(obj)print(obj.name);
        }
    }
}
