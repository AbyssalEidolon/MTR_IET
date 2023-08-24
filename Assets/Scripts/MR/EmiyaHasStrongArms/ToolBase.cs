using System;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class ToolBase : MonoBehaviour {
    public ToolGripConfigurator config = null;
    // void Start(){
    //     if(!HandInteractionController.ToolTypes.Contains(this))HandInteractionController.ToolTypes.Add(this);
    //     if(HandInteractionController.i.PrintDebugMessages) print(this.GetType().Name);
    // }
}