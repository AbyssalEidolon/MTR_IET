using System.Security.Cryptography;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
public class JointTriggerArea : MonoBehaviour {
    public TrackedHandJoint targetJoint = TrackedHandJoint.None;
    Color areaInitColour = Color.white;
    public Color ActiveColour = Color.white;
    public JointCheckController controller = null;
    bool Active = false;
    public int ID = -1;
    Vector3 start = new();
    Vector3 end = new();
    public Joint joint = null;
    void Start(){
        areaInitColour = GetComponent<MeshRenderer>().material.color;
    }
    public void Activate(bool NewState){
        if(NewState){
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else{
            GetComponent<MeshRenderer>().material.color = areaInitColour;
            controller.NormalisedJointValues[ID] = -1;
        }
        Active = NewState;
        gameObject.SetActive(NewState);
    }
    void FixedUpdate(){
        if(Active){
            float Mag = Utils.NormalisedVectorCheck(start, end, joint.gameObject.transform.position);
            if(Mag >= 1) controller.NormalisedJointValues[ID] = 1;
            else if(Mag < 0) Activate(false);
            else controller.NormalisedJointValues[ID] = Mag;
        }
    }
    public void LoadConfig(FingerConfig config){
        start = config.startPosition;
        end = config.endPosition;
    }
}