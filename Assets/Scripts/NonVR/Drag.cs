using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour{
    public GameObject Target = null;
    public GameObject PickedUp = null;
    Mouse mouse;
    bool Enabled = false;
    public InputAction Pick;
    public InputAction Let;
    public bool FirstClick = true;
    public float clickCD = 0.1f;
    public float Scale = 1;
    public GameObject CanvasShit;
    public Vector3[] ClampVecs = new Vector3[2];
    private void Awake() {
        mouse = Mouse.current;
        ModeSwap.ModeChange[(int)Mode.MOVE] += () => {
            Enabled = true;
        };
        ModeSwap.ModeChanged += () => {
            if(ModeSwap.curMode != (int)Mode.MOVE)
            Enabled = false;
            PickedUp = null;
        };
        Pick.performed += ctx => PickUp();
        Pick.Enable();
        Let.performed += ctx => LetGo();
        Let.Enable();
    }
    bool MouseToWorld(out Vector3 point, MeshCollider LineTarget){
        Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        RaycastHit hit;
        if(LineTarget.Raycast(ray, out hit, 10000)){
            point = hit.point;
            return true;
        }
        else{
            point = Vector3.zero;
            return false;
        }
    }
    private void FixedUpdate() {
        if(!Enabled) return;
        // Vector3 placeholder = new();
        // foreach(GameObject Cable in CableController.i.FreeLines){
        //     if(MouseToWorld(out placeholder, Cable.GetComponent<MeshCollider>())){
        //         Target = Cable;
        //     }
        // }
        if(PickedUp){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            if (CanvasShit.GetComponent<MeshCollider>().Raycast(ray, out hit, 10000))
            {
                print(hit);
                PickedUp.transform.position = Vector3.MoveTowards(PickedUp.transform.position, hit.point, Scale);
            }
            else print("shit");
        }
    }
    void PickUp(){
        if(PickedUp) return;
        if(FirstClick){
            FirstClick = false;
            StartCoroutine(CD());
            return;
        }
        PickedUp = Target;
    }
    void LetGo(){
        PickedUp = null;
    }
    IEnumerator CD(){
        yield return new WaitForSeconds(clickCD);
        FirstClick = true;
        yield return null;
    }
}