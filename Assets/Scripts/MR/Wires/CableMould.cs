using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CableMould : MonoBehaviour
{
    public GameObject Target = null;
    public GameObject Current = null;
    public InputAction Pick;
    public InputAction Let;
    public bool FirstClick = true;
    public float clickCD = 0.1f;
    List<GameObject> Vertices = new();
    LineRenderer Line => Current.GetComponent<LineRenderer>();
    float[] SectionLength;
    private void Awake() {
        ModeSwap.ModeChange[(int)Mode.MOULD] += StartMould;
        ModeSwap.ModeChanged += StopMould;
        Pick.performed += ctx => PickCable();
        Pick.Enable();
        Let.performed += ctx => LetGo();
        Let.Enable();
    }
    void PickCable(){
        if(Current) return;
        if(FirstClick){
            FirstClick = false;
            StartCoroutine(CD());
            return;
        }
        Current = Target;
    }
    void LetGo(){
        foreach(GameObject vertex in Vertices){
            Destroy(vertex);
        }
    }
    void StartMould(){
        List<Vector3> preVectices = new();
        for(int i = 0; i < Line.positionCount; i++){
            preVectices.Add(Line.GetPosition(i));
        }
    }
    void MouldSync(){
        for(int i = 1; i < Vertices.Count - 2; i++){
            
        }
    }
    void StopMould(){
        if(ModeSwap.curMode != (int)Mode.MOULD){

        }
    }
    IEnumerator CD(){
        yield return new WaitForSeconds(clickCD);
        FirstClick = true;
        yield return null;
    }
}
