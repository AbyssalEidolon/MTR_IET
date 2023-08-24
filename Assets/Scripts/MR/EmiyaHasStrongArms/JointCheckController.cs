using System;
using UnityEngine;

public class JointCheckController : MonoBehaviour {
    [HideInInspector]
    public GameObject targetAreaPrefab = null;
    bool[] jointChecks = new bool[5];
    GameObject[] CheckAreas = new GameObject[5];
    Vector3 checkAreaInitScale = new();
    BoxCollider[] boxColliders = new BoxCollider[5];
    void Start(){
        checkAreaInitScale = targetAreaPrefab.GetComponent<BoxCollider>().size;
        for(int i = 0; i < CheckAreas.Length; i++){
            CheckAreas[i] = Instantiate(targetAreaPrefab, transform);
            CheckAreas[i].SetActive(false);
            boxColliders[i] = CheckAreas[i].GetComponent<BoxCollider>();
        }
    }
    public void LoadConfig(ToolGripConfigurator config){
        for(int i = 0; i < config.FingerConfigs.Length; i++){
            CheckAreas[i].SetActive(config.FingerConfigs[i].Active);
            boxColliders[i].size = new(config.Scale, config.Scale, config.Scale);
            CheckAreas[i].transform.localPosition = config.FingerConfigs[i].startPosition;
        }
    }
}