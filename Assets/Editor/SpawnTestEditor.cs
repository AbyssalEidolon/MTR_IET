using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnTest))]
public class SpawnTestEditor : Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        SpawnTest sTest = (SpawnTest)target;
        if(GUILayout.Button("Spawn Ind")){
            sTest.SpawnIndicators();
        }
    }
}