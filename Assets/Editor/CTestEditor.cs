using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CTest))]
public class CTestEditor : Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        CTest cTest = (CTest)target;
        if(GUILayout.Button("Call Colliders.")){
            cTest.CallColliders();
        }
    }
}