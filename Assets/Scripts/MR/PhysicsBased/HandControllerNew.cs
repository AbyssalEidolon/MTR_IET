using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

// public class HandControllerNew : MonoBehaviour
// {
//     public bool FakeUpdate = true;
//     //Non Array For Testing
//     public GameObject FingerPrefab;
//     [LabelledArray(new string[]{"Thumb", "Pointer", "Middle", "Ring", "Pinky"})]
//     public GameObject[] FakeTrackers = new GameObject[5];
//     public FingerConstraints[] constraints = new FingerConstraints[5];
//     HandBase handInternal;
//     public bool Locked = false;
//     void Start()
//     {
//         handInternal = FakeUpdate? new FakeHand(FakeTrackers, constraints) : new RealHand(constraints);
//         if(!FakeUpdate) CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(handInternal.poller());
//         handInternal.Fingers = new GameObject[FakeTrackers.Length]; 
//         for (int i = 0; i < handInternal.Fingers.Length; i++)handInternal.Fingers[i] = Instantiate(FingerPrefab);
//     }
//     void FixedUpdate() {if(Locked) handInternal.FixedUpdate();}
//     void OnLock(){
//         if(FakeUpdate)foreach(GameObject gameObject in FakeTrackers) gameObject.SetActive(true);
//     }
//     void OnUnlock(){
//         if(FakeUpdate)foreach(GameObject gameObject in FakeTrackers) gameObject.SetActive(false);
//     }
//     public void UpdateConstraints() => handInternal.UpdateConstraints(constraints);
// }

// public class RealHand : HandBase{
//     Poller intPoller = new();

//     public RealHand(FingerConstraints[] constraints) => UpdateConstraints(constraints);

//     override internal Poller poller() => intPoller;
//     override internal Vector3[] FingerLocations() => poller().FingerPos;
//     override public void FixedUpdate(){
//         poller().PollFingers();
//         base.FixedUpdate();
//     }
// }
// public class FakeHand : HandBase{
//     public GameObject[] FakeJoints = new GameObject[5];
//     override internal Vector3[] FingerLocations(){return FakeJoints.Select(a => a.transform.position).ToArray();}
//     public FakeHand(GameObject[] b, FingerConstraints[] constraints){
//             FakeJoints = b;
//             UpdateConstraints(constraints);
//     }
// }
// public abstract class HandBase{
//     public virtual void LateInit(){
//         Valid = new bool[Constraints.Length];
//     }
//     bool[] Valid;
//     public virtual void FixedUpdate(){
//         for(int i = 0; i < Constraints.Length; i++){
//             if(!Constraints[i].Active) return;
//             float mag = Utils.NormalisedVectorCheck(
//                 Constraints[i].Start.transform.position, 
//                 Constraints[i].End.transform.position,
//                 FingerLocations()[i]
//             );
//             if(mag < 0) {Valid[i] = false; continue;}
//             else if(mag > 1){Valid[i] = true; continue;}
//             else{Valid[i] = true; Fingers[i].transform.position = FingerLocations()[i];}
//         }
//         if(!Valid.Contains(true)) OutOfBounds();
//     }
//     internal virtual void OutOfBounds(){}
//     internal GameObject[] Fingers;
//     internal virtual Vector3[] FingerLocations() => new Vector3[5];
//     internal FingerConstraints[] Constraints;
//     internal virtual Poller poller() => null;
//     public void UpdateConstraints(FingerConstraints[] constraints){
//         Constraints = constraints;
//     }

// }