using System.Collections.Generic;
using UnityEngine;
public class AnchorSpawner : MonoBehaviour{
    public List<GameObject> Targets = new();
    public void SpawnAnchor(){
        if(Targets.Count < 2) return;
        Vector3 temp = new();
        foreach(GameObject gameObject in Targets){
            temp += gameObject.transform.position;
        }
        temp /= Targets.Count;
        GameObject temptemp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        temptemp.transform.localScale = new(0.01f, 0.01f, 0.01f);
        temptemp.transform.position = temp;
    }
}