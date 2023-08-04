using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour{
    //This component can be safely disabled since it's made as an addon.
    public GameObject[] Buttons = new GameObject[];
    void Awake(){
        foreach(GameObject button in Buttons){
            button.SetActive(false);
        }
        WireController.WireUISync += UpdateButtons;
    }
    void UpdateButtons(bool[] buttonStates){
        for(int i = 0; i < buttonStates.Length; i++){
            Buttons[i].SetActive(buttonStates[i]);
        }
    }
}