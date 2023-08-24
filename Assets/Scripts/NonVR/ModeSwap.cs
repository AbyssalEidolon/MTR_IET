using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModeSwap : MonoBehaviour {
    [SerializeField]
    List<Sprite> ImageList = new();
    public static int curMode = 0;
    public delegate void ModeUpdate();
    static public ModeUpdate[] ModeChange = new ModeUpdate[Enum.GetValues(typeof(Mode)).Length];
    static public ModeUpdate ModeChanged;
    private void Start() {
        GetComponent<Image>().sprite = ImageList[0];
    }
    public void Next(){
        if(curMode >= 0 || curMode < ModeChange.Length -1){
            curMode++;
            ModeChange[curMode].Invoke();
            GetComponent<Image>().sprite = ImageList[curMode];
            ModeChanged.Invoke();
        }
    }   
    public void Previous(){
        if(curMode > 0 || curMode < ModeChange.Length){
            curMode--;
            ModeChange[curMode].Invoke();
            GetComponent<Image>().sprite = ImageList[curMode];
            ModeChanged.Invoke();
        }
    } 
}
public enum Mode{
    PULL = 0,
    CUT,
    MOVE,
    MOULD
}