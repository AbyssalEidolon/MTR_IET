// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class CutToggle : MonoBehaviour
// {
//     public GameObject CutIndicator = null;
//     Toggle cutToggle = null;
//     bool toggle => cutToggle.isOn;
//     GameObject Line = null;
//     Slider slider => GameObject.Find("Slider").GetComponent<Slider>();
//     private void Awake() {
//         ModeSwap.ModeChange[(int)Mode.PULL] += PullMode;
//         ModeSwap.ModeChange[(int)Mode.CUT] += CutMode;
//         ModeSwap.ModeChanged += DisableCut;
        
//     }
//     private void Start() {
//         Line = GameObject.FindGameObjectWithTag("CablePulled");
//         cutToggle = gameObject.GetComponent<Toggle>();
//     }
//     void PullMode(){
//         slider.enabled = true;
//     }
//     void CutMode(){
//         MeshCollider collider;
//         Debug.Log("Cut Mode Enabled.");
//             CutIndicator.GetComponent<CutIndicator>().SetLine();
//             if(!Line.GetComponent<MeshCollider>()){
//                     collider = Line.AddComponent<MeshCollider>();
//             }
//             else{collider = Line.GetComponent<MeshCollider>();}
//             Mesh mesh = new();
//             Line.GetComponent<LineRenderer>().BakeMesh(mesh, true);
//             collider.sharedMesh = mesh;
//             slider.enabled = false;
//             CutIndicator.GetComponent<CutIndicator>().LineTarget = Line.GetComponent<MeshCollider>();
//             CutIndicator.SetActive(true);
//     }
//     void DisableCut(){
//         if(ModeSwap.curMode != (int)Mode.CUT){
//             CutIndicator.SetActive(false);
//         }
//     }
// }
