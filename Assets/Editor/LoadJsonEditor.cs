using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(PacificJsonLoader))]
public class LoadJsonEditor : Editor { 
    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        PacificJsonLoader loader = (PacificJsonLoader)target;
        if(GUILayout.Button("Read Jsons")){
            loader.LoadPoses();
        }
    }
    
}