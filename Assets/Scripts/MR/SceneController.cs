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
    public Pulley Pulley = null;
    public TextMeshProUGUI TestText = null;
    public bool ToolBarsLocked = false;
    void Awake(){
        i = this;
    }
}