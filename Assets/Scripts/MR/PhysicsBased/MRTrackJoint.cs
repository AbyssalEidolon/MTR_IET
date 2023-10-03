using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using UnityEngine.Animations;

public class MRTrackJoint : MonoBehaviour, IMixedRealitySourceStateHandler{
    public bool OnPhysics = false;
    public TrackedHandJoint TargetJoint; 
    public IMixedRealityHand Hand = null;
    public PalmBoxController palmController;
    public Vector3 ConstOffset = new();
    public Vector3 ConstAngOffset = new();
    void Start(){
        if (!palmController.handController.FakeUpdate) CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(this);
    }
    void FixedUpdate(){if(OnPhysics) Follow();}
    void Update(){if(!OnPhysics) Follow();}
    void Follow(){
        if(Hand != null){
            if(Hand.TryGetJoint(TargetJoint, out MixedRealityPose palm)){
            gameObject.transform.position = palm.Position;
            gameObject.transform.eulerAngles = palm.Rotation.eulerAngles;
            // gameObject.transform.localPosition += ConstOffset;
            // gameObject.transform.localEulerAngles += ConstAngOffset;
            }
        }
    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
        palmController.handController.UnloadTB();
        Hand = eventData.Controller as IMixedRealityHand;
        palmController.handController.UpdateHand(Hand);
        if(Hand != null)
        palmController.UpdateHand(Hand.ControllerHandedness);
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        palmController.handController.UnloadTB();
        // throw new System.NotImplementedException();
    }
}