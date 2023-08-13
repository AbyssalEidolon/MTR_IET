using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolGripConfig", menuName = "MRConfigurables/ToolGripConfig", order = 0)]
public class ToolGripConfigurator : ScriptableObject{
    public bool LeftHand = false;
    public FingerConfig Thumb = new();
    public FingerConfig Pointer = new();
    public FingerConfig Middle = new();
    public FingerConfig Ring = new();
    public FingerConfig Pinky = new();
    public float Scale = 1;
}
[Serializable]
public struct FingerConfig{
    public bool Active;
    public Vector3 targetPosition;
}