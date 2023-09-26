using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterTest : MonoBehaviour
{
   public GameObject ASide;
   public GameObject BSide;
   float CurAngle;

   void FixedUpdate(){
        CurAngle = Quaternion.Angle(ASide.transform.rotation, BSide.transform.rotation);
        print(System.Math.Round(CurAngle));
   }
}
