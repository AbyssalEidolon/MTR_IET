using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnchorSpawner))]
public class AnchorSpawnerEditor : Editor{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        AnchorSpawner aSpawner = (AnchorSpawner)target;
        if(GUILayout.Button("Spawn Anchor.")){
            aSpawner.SpawnAnchor();
        }
    }
}