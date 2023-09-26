using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.Animations;
public class HandController : MonoBehaviour
{
    bool FakeUpdate = true;
    public GameObject FingerPrefab;
    [LabelledArray(new string[] { "Thumb", "Pointer", "Middle", "Ring", "Pinky" })]
    public GameObject[] FakeTrackers = new GameObject[5];
    HandBase handInternal;
    NewToolBase toolBase;
    public void LoadTB(NewToolBase tb, GameObject tg) { handInternal.LoadConstraints(tb, tg); toolBase = tb; }
    public void UnloadTB(NewToolBase tb) { if (toolBase == tb) handInternal.UnloadConstraints(); }
    void Start()
    {
        handInternal = FakeUpdate ? new FakeHand(FakeTrackers) : new RealHand();
        if (!FakeUpdate) CoreServices.InputSystem?.RegisterHandler<IMixedRealitySourceStateHandler>(handInternal.poller());
        handInternal.Fingers = new GameObject[FakeTrackers.Length];
        for (int i = 0; i < handInternal.Fingers.Length; i++)
        {
            handInternal.Fingers[i] = Instantiate(FingerPrefab);
            handInternal.Fingers[i].SetActive(false);
        };
    }
    void FixedUpdate() => handInternal.FixedUpdate();
}
[Serializable]
public struct FingerConstraints
{
    public GameObject Start;
    public GameObject End;
    public bool Active;
}
public abstract class HandBase
{
    internal virtual Poller poller() => null;
    internal GameObject[] Fingers;
    internal virtual Vector3[] FingerLocations() => new Vector3[5];
    public virtual Vector3 PalmLocation() => new();
    internal NewToolBase tb;
    internal GameObject Target = null;
    internal GameObject Locked = null;
    public virtual void LateInit() { }
    //LOADING
    internal virtual void LoadConstraints(NewToolBase tbt, GameObject tg) { tb = tbt; Target = tg;}
    internal virtual void UnloadConstraints() { tb.Locked = false; tb = null; Target = null; Detach(); }
    //CHECKINGMAGS && ATTACH
    void CheckMags()
    {
        if (tb && Target && Target == tb.gameObject)
        {
            bool Thumb = false;
            bool Thresehold = false;
            for (int i = 0; i < tb.constraints.Length; i++)
            {
                if (!tb.constraints[i].Active) continue;
                float mag = Utils.NormalisedVectorCheck(
                tb.constraints[i].Start.transform.position,
                tb.constraints[i].End.transform.position,
                FingerLocations()[i]
            );
                if (mag > 0) if (i > 0) Thresehold = true; else Thumb = true;
            }
            if (Thumb && Thresehold)
            {
                // Target.transform.position = PalmLocation() + tb.DistFromPalm;
                Target.GetComponent<ParentConstraint>().constraintActive = true;
                foreach (GameObject finger in Fingers) finger.SetActive(true);
                Locked = Target;
                tb.Locked = true;
            }
        }
    }
    //POST-ATTACH
    void UpdatePhysics()
    {
        bool[] Valid = new bool[tb.constraints.Length];
        for (int i = 0; i < tb.constraints.Length; i++)
        {
            if (!tb.constraints[i].Active) return;
            float mag = Utils.NormalisedVectorCheck(
                tb.constraints[i].Start.transform.position,
                tb.constraints[i].End.transform.position,
                FingerLocations()[i]
            );
            if (mag < 0) { Valid[i] = false; continue; }
            else if (mag > 1) { Valid[i] = true; continue; }
            else { Valid[i] = true; Fingers[i].transform.position = Utils.Clamp(FingerLocations()[i], tb.constraints[i].Start.transform.position, tb.constraints[i].End.transform.position);}
        }
        if (!Valid[0] || !Valid.Contains(true)) Detach();
    }
    //DETACH
    void Detach()
    {
        Target.GetComponent<ParentConstraint>().constraintActive = false;
        foreach (GameObject finger in Fingers) finger.SetActive(false);
        Locked = null;
        tb.Locked = false;
    }
    public virtual void FixedUpdate()
    {
        if (!Locked) CheckMags();
        else UpdatePhysics();
        if(tb)for (int i = 0; i < tb.constraints.Length; i++)
            {
                Debug.Log(Utils.NormalisedVectorCheck(
                        tb.constraints[i].Start.transform.position,
                        tb.constraints[i].End.transform.position,
                        FingerLocations()[i]));
            }
    }
}

public class RealHand : HandBase
{
    Poller intPoller = new();
    override internal Poller poller() => intPoller;
    override internal Vector3[] FingerLocations() => poller().FingerPos;
    override public void FixedUpdate()
    {
        poller().PollFingers();
        base.FixedUpdate();
    }
}
public class FakeHand : HandBase
{
    public GameObject[] FakeJoints;
    override internal Vector3[] FingerLocations() { return FakeJoints.Select(a => a.transform.position).ToArray(); }
    public FakeHand(GameObject[] b) => FakeJoints = b;
}