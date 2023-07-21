using UnityEngine;
public class Rotation : MonoBehaviour{
    public GameObject JointPositive = null;
    public GameObject JointNegative = null;
    float JointDistance => JointPositive.transform.eulerAngles.z - JointNegative.transform.rotation.z;
    void FixedUpdate(){
        print(Quaternion.Angle(JointPositive.transform.rotation, JointNegative.transform.rotation));
    }
    void Start(){
        Vector3 Scale = transform.localScale;
        Joint[] joints = GetComponentsInChildren<Joint>();
        foreach(Joint joint in joints){
            joint.anchor = Vector3.Scale(joint.anchor, Scale);
            joint.connectedAnchor = Vector3.Scale(joint.connectedAnchor, Scale);
        }
    }
}