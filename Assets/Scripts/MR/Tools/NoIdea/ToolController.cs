using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public Transform origin;
    //0 Pos, 1 Neg
    public Transform[] joint = new Transform[2];
    [Range(0, 1)]
    public float Range = 0;
    public float Angle = 15;
    void FixedUpdate()
    {
        if (!origin || !joint[0] || !joint[1])
            return;
        else
            for (int i = 0; i < joint.Length; i++)
            {
                float newAngle = Angle * -(Range - 1);
                newAngle *= i == 1 ? -1 : 1;
                joint[i].eulerAngles = new(joint[i].eulerAngles.x, joint[i].eulerAngles.y, newAngle);
            }
    }
}
