using System.Linq;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using UnityEngine.Animations;

public abstract class HandBase
{
    internal virtual Poller poller() => null;
    internal GameObject[] Fingers;
    internal virtual Vector3[] FingerLocations() => new Vector3[5];
    public virtual Vector3 PalmLocation() => new();
    internal NewToolBase tb;
    internal GameObject Target = null;
    internal GameObject Locked = null;
    float[] RefMags = null;
    public virtual void LateInit() { }
    //LOADING
    internal virtual void LoadConstraints(NewToolBase tbt, GameObject tg) { tb = tbt; Target = tg; }
    internal virtual void UnloadConstraints() { tb.Locked = false; tb = null; Target = null; Detach(); }
    //CHECKINGMAGS && ATTACH
    void CheckMags()
    {
        if (tb && Target && Target == tb.gameObject)
        {
            float[] MagLimits = new float[]{0.3f, 0, 0, 0, 0};
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
                if (mag > MagLimits[i]) if (i > 0) Thresehold = true; else Thumb = true;
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
        float[] MagLimits = new float[]{0.3f, 0, 0, 0, 0};
        bool[] Valid = new bool[tb.constraints.Length];
        for (int i = 0; i < tb.constraints.Length; i++)
        {
            if (!tb.constraints[i].Active) return;
            float mag = Utils.NormalisedVectorCheck(
                tb.constraints[i].Start.transform.position,
                tb.constraints[i].End.transform.position,
                FingerLocations()[i]
            );
            if (mag < MagLimits[i]) { Valid[i] = false; continue; }
            else if (mag > 1) { Valid[i] = true; continue; }
            else if(Vector3.Distance(tb.constraints[i].End.transform.position, poller().FingerPos[i]) > Vector3.Distance(tb.constraints[i].Start.transform.position, tb.constraints[i].End.transform.position)) Valid[i] = false;
            else { Valid[i] = true; Fingers[i].transform.position = Utils.Clamp(FingerLocations()[i], tb.constraints[i].Start.transform.position, tb.constraints[i].End.transform.position); }
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
        if(Target) Target = null;
    }
    public virtual void FixedUpdate()
    {
        if (!Locked) CheckMags();
        else if(tb) UpdatePhysics();
        if (tb){
            RefMags = new float[tb.constraints.Length];
            for (int i = 0; i < tb.constraints.Length; i++)
            {
                RefMags[i] = Utils.NormalisedVectorCheck(
                        tb.constraints[i].Start.transform.position,
                        tb.constraints[i].End.transform.position,
                        FingerLocations()[i]);
                Debug.Log(RefMags[i]);

            }}
            else RefMags = null;
    }
    public void UpdateMags(out float[] Mags){Mags = RefMags;}
}

public class RealHand : HandBase
{
    Poller intPoller = new(Thumb: TrackedHandJoint.ThumbTip);
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