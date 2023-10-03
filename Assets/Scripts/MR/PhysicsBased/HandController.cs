using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
public class HandController : MonoBehaviour
{
    public TextMeshProUGUI target;
    public bool FakeUpdate = true;
    public GameObject FingerPrefab;
    [LabelledArray(new string[] { "Thumb", "Pointer", "Middle", "Ring", "Pinky" })]
    public GameObject[] FakeTrackers = new GameObject[5];
    public GameObject[] constraintParents = new GameObject[2];
    HandBase handInternal;
    TextManager tm;
    NewToolBase toolBase;
    public float[] PullOutGame = null;
    public void LoadTB(NewToolBase tb, GameObject tg) { handInternal.LoadConstraints(tb, tg); toolBase = tb; }
    public void UnloadTB(NewToolBase tb = null) { if (toolBase == tb && tb != null) handInternal.UnloadConstraints(); }
    public void UpdateHand(IMixedRealityHand hand){if(hand != null)foreach(GameObject gameObject in constraintParents) gameObject.transform.localEulerAngles = hand.ControllerHandedness == Handedness.Left ? new(0, 180, 0) : new();}
    void Start()
    {
#if !UNITY_EDITOR
        FakeUpdate = false;
#endif
        handInternal = FakeUpdate ? new FakeHand(FakeTrackers) : new RealHand();
        if (!FakeUpdate) CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(handInternal.poller());
        if(target != null) tm = new(this);
        handInternal.Fingers = new GameObject[FakeTrackers.Length];
        for (int i = 0; i < handInternal.Fingers.Length; i++)
        {
            handInternal.Fingers[i] = Instantiate(FingerPrefab);
            handInternal.Fingers[i].SetActive(false);
        };
    }
    void FixedUpdate(){
        handInternal.FixedUpdate();
        handInternal.UpdateMags(out PullOutGame);
        tm.UpdateText();
    }
}
public class TextManager{
    TextMeshProUGUI target => cn.target;
    public HandController cn;
    virtual public float[] objects() => cn.PullOutGame;

    public TextManager(HandController cn){
        this.cn = cn;
    }
    public void UpdateText(){
        if(objects() == null) return;
        string temp = "";
        foreach(float obj in objects()){
            temp += obj.ToString();
            temp += "\n";
        }
        target.text = temp;
    }
}
[Serializable]
public struct FingerConstraints
{
    public GameObject Start;
    public GameObject End;
    public bool Active;
}
