using System.Collections.Generic;
using UnityEngine;

public class NewToolBase : MonoBehaviour{
    public FingerConstraints[] constraints = new FingerConstraints[5];
    public Vector3 DistFromPalm;
    public GameObject ASide;
   public GameObject BSide;
   public bool Locked = false;
   float CurAngle;
   bool Activated = false;

   void FixedUpdate(){
        CurAngle = Quaternion.Angle(ASide.transform.rotation, BSide.transform.rotation);
        if(!Locked) return;
        if(CurAngle <= 6){if(!Activated)Activate();}
        else Activated = false;
    }
    void Activate(){
        Activated = true;
        Debug.Log("AAAAAAAAAAAA");
    }
}