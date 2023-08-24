using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using static Utils;
public class Joint : MonoBehaviour {
    public HandInteractionController controller = null;
    public TrackedHandJoint targetJoint = TrackedHandJoint.None;
    Transform target = null;
    Handedness curHand = Handedness.None;
    IMixedRealityHandJointService jointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
    public void OnSourceDetected(SourceStateEventData eventData){
        IMixedRealityHand hand = eventData.Controller.Visualizer as IMixedRealityHand;
        if(hand != null){
            curHand = hand.ControllerHandedness;
            target = jointService.RequestJointTransform(targetJoint, curHand);
        }
        else curHand = Handedness.None;
    }
    void FixedUpdate(){
        if(curHand != Handedness.None){
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
    void OnTriggerEnter(Collider other){
        JointTriggerArea area = other.GetComponent<JointTriggerArea>();
        if(!area || area.targetJoint != targetJoint) return;
        area.Activate(true);
        area.joint = this;
    }
}