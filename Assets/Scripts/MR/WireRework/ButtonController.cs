using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour{
    //This component can be safely disabled since it's made as an addon.
    public GameObject[] Buttons;
    Interactable[] interactables;
    void Awake(){
        interactables = new Interactable[Buttons.Length];
        for(int i = 0; i < Buttons.Length; i++)
        {
            interactables[i] = Buttons[i].GetComponent<Interactable>();
        }
        foreach(Interactable button in interactables){
            button.IsEnabled = false;
        }
        WireController.WireUISync += UpdateButtons;
    }
    void UpdateButtons(bool[] buttonStates){
        for(int i = 0; i < buttonStates.Length; i++){
            interactables[i].IsEnabled = buttonStates[i];
        }
    }
}