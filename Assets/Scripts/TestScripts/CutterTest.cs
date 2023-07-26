using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class CutterTest : MonoBehaviour, IMixedRealityHandJointHandler
{
    public TextMeshProUGUI IndexText = null, ThumbText = null;
    public TextMeshPro HandText = null;
    void Start(){
        HandText.text = "Cum";
        IndexText.text = "Cum";
        ThumbText.text = "Cum";
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityHandJointHandler>(this);
    }
    string Hand(Handedness Hand) => Hand == Handedness.Left ? "Left" : "Right";

    public void OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        string hand = Hand(eventData.Handedness);
        HandText.text = $"Current Hand: {hand}";
        if (eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out MixedRealityPose indexPose))
        {
            IndexText.text = $"{indexPose.Position.ToString("F4")} \n\n {indexPose.Rotation.ToString("F4")}";
        }
        if (eventData.InputData.TryGetValue(TrackedHandJoint.ThumbTip, out MixedRealityPose thumbPose))
        {
            ThumbText.text = $"{thumbPose.Position.ToString("F4")} \n\n {thumbPose.Rotation.ToString("F4")}";
        }
    }

    // public void OnSourceDetected(SourceStateEventData eventData)
    // {
    //     IMixedRealityHand hand = eventData.Controller as IMixedRealityHand;
    //     if (hand != null)
    //     {
    //         if (hand.TryGetJoint(TrackedHandJoint.IndexTip, out MixedRealityPose indexPose))
    //         {
    //             IndexText.text = $"{indexPose.Position.ToString("F4")} \n {indexPose.Rotation.ToString("F4")}";
    //         }
    //         if (hand.TryGetJoint(TrackedHandJoint.ThumbTip, out MixedRealityPose thumbPose))
    //         {
    //             ThumbText.text = $"{thumbPose.Position.ToString("F4")} \n {thumbPose.Rotation.ToString("F4")}";
    //         }
    //     }
    // }
}
