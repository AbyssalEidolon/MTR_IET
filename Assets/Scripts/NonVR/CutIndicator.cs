// using System.Collections;
// using System.Collections.Generic;
// using System.Threading;
// using UnityEngine;
// using UnityEngine.InputSystem;
// public class CutIndicator : MonoBehaviour {
//     public GameObject Coil;
//     // List<GameObject> freeLines => CableController.i.FreeLines;
//     public float Scale = 1;
//     Mouse mouse = null;
//     public bool cd = false;
//     public MeshCollider LineTarget = null;
//     LineRenderer Line = null;
//     [SerializeField]
//     Vector3[] ClampVecs = new Vector3[2];
//     public InputAction Wire;
//     private void Start() {
//         mouse = Mouse.current;
//         ClampVecs[0] = Coil.transform.position;
//         Wire.performed += ctx => NewWire();
//         Wire.Enable();
//     }
//     public void SetLine(){
//         Line = GameObject.FindGameObjectWithTag("CablePulled").GetComponent<LineRenderer>();
//         ClampVecs[1] = Line.GetPosition(1);
//     }
//     private void FixedUpdate() {
//         if(!CableController.i.MR) FollowMouse();
//     }
//     #region NonMR
//     void FollowMouse(){
//         bool Hit;
//         Vector3 MousePos = MouseToWorld(out Hit);
//         if(Hit)
//         gameObject.transform.position = Clamp(
//             Vector3.MoveTowards(transform.position, MousePos, Scale),
//             ClampVecs[1],
//             ClampVecs[0]
//         );
//     }
//     Vector3 MouseToWorld(out bool Hit){
//         Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
//         RaycastHit hit;
//         if(LineTarget.Raycast(ray, out hit, 10000)){
//             Hit = true;
//             return hit.point; 
//         }
//         else{
//             // Debug.Log("AAAAAAAAAAAAAAA");
//             Hit = false;
//             return Vector3.zero;
//         }
//     }
//     Vector3 Clamp(Vector3 original, Vector3 Max, Vector3 Min){
//         Vector3 Target = new();
//         for(int i = 0; i < 3; i++){
//             Target[i] = Mathf.Clamp(original[i], Min[i], Max[i]);
//         }
//         return Target;
//     }
//     #endregion
//     void NewWire(){
//         if(!cd){
//             cd = true;
//             StartCoroutine(Reset(2));
//             LineRenderer ren = Instantiate(Line.gameObject, Line.transform.position, Line.transform.rotation).GetComponent<LineRenderer>();
//             freeLines.Add(ren.gameObject);
//             ren.SetPosition(0, transform.position);
//             ren.transform.Translate(new(0,Vector3.Distance(ClampVecs[1], ClampVecs[2]) / 10 * freeLines.Count,0));
//             ren.gameObject.name = "Wire " + freeLines.Count;
//         }
//     }
//     IEnumerator Reset(float delay){
//         yield return new WaitForSeconds(delay);
//         cd = false;
//         yield return null;
//     }
//     private void OnEnable() {
//         cd = true;
//         StartCoroutine((Reset(2)));
//     }
// }