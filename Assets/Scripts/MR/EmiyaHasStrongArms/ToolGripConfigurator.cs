using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolGripConfig", menuName = "MRConfigurables/ToolGripConfig", order = 0)]
public class ToolGripConfigurator : ScriptableObject{
    public bool LeftHand = false;
    public float Scale = 1;
    [LabelledArray(new string[]{
        "Thumb", "Pointer", "Middle", "Ring", "Pinky"
    })]
    public FingerConfig[] FingerConfigs = {
        new(), 
        new(),
        new(),
        new(),
        new(),
        };
    [LabelledArray(new string[]{"BeginRot", "EndRot"})]
    public Quaternion[] palmRots = new Quaternion[2]{
        Quaternion.identity,
        Quaternion.identity
    };
}
[Serializable]
public struct FingerConfig{
    public bool Active;
    public Vector3 startPosition;
    public Vector3 endPosition;
}