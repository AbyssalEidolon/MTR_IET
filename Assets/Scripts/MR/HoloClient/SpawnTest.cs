using System;
using UnityEngine;

public class SpawnTest : MonoBehaviour {
    public ToolGripConfigurator config = null;
    public GameObject FignerIndicator = null;
    public GameObject WristIndicator = null;
    GameObject[] Wrists = new GameObject[2];
    public GameObject[,] Fingers = new GameObject[2,5];
    Vector3 ConfigPos(int FingerIndex, int start)=> start == 0? config.FingerConfigs[FingerIndex].startPosition : start == 1? config.FingerConfigs[FingerIndex].endPosition : new();
    string Name(int i) => i == 0? "Start" : "End";
    public void SpawnIndicators(){
        if(!config || !FignerIndicator || !WristIndicator){ Debug.LogError("WHY"); return;}
        for(int i = 0; i < 2; i ++){
            Wrists[i] = Instantiate(WristIndicator);
            Wrists[i].transform.rotation = config.palmRots[i];
            Wrists[i].name = Name(i);
            for(int f = 0; f < 5; f++){
                if(!config.FingerConfigs[f].Active) continue;
                Fingers[i, f] = Instantiate(FignerIndicator);
                Fingers[i, f].GetComponent<MeshRenderer>().material.color = i == 0? Color.green : Color.red;
                Fingers[i, f].transform.parent = Wrists[i].transform;
                Fingers[i, f].transform.position = ConfigPos(f, i);
            }
        }
    }
}