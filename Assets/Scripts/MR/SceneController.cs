using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour{
    public static SceneController i;
    public GameObject SliderThumb = null;
    public BoxCollider[] DisabledOnToolPickup;
    public LineManipulator lineManipulator = null;
    public TextMeshProUGUI TestText = null;
    void Awake(){
        i = this;
    }
    public static Vector3 Clamp(Vector3 original, Vector3 Max, Vector3 Min){
        Vector3 Target = new();
        for(int i = 0; i < 3; i++){
            Target[i] = Mathf.Clamp(original[i], Min[i], Max[i]);
        }
        return Target;
    }
}