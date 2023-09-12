using System;
using UnityEngine;
public class JointVisualiser : MonoBehaviour {
    public Poller poller;
    public int ind = 0;
    Vector3 Pos => poller.FingerPos[ind];
    void FixedUpdate(){
        if(Pos != null)
        transform.position = Pos;
    }
}
public class PalmRotVisualiser : MonoBehaviour {
    public Poller poller;
    void FixedUpdate(){
        if(poller.PalmRot != null)
        transform.rotation = poller.PalmRot;
    }
}