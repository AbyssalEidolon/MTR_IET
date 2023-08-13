using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class CutterTest : MonoBehaviour, IMixedRealityHandJointHandler
{
    public TextMeshProUGUI IndexText = null; 
    public TextMeshProUGUI ThumbText = null;
    public TextMeshProUGUI DistText = null;
    public TextMeshProUGUI CurText = null;
    public TextMeshPro HandText = null;
    public static CutterTest TestBlock = null;
    MixedRealityPose indexPose, thumbPose;
    void Start(){
        HandText.text = "Cum";
        IndexText.text = "Cum";
        ThumbText.text = "Cum";
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityHandJointHandler>(this);
        TestBlock = this;
    }
    string Hand(Handedness Hand) => Hand == Handedness.Left ? "Left" : "Right";

    public void OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        // string hand = Hand(eventData.Handedness);
        // HandText.text = $"Current Hand: {hand}";
        // if (eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out indexPose))
        // {
        //     if(IndexText) IndexText.text = $"{indexPose.Rotation.ToString("F4")}";
        // }
        // if (eventData.InputData.TryGetValue(TrackedHandJoint.ThumbTip, out thumbPose))
        // {
        //     if(ThumbText) ThumbText.text = $"{thumbPose.Rotation.ToString("F4")}";
        // }
        // // if(DistText){
        // //     Quaternion Difference = Quaternion.Inverse(thumbPose.Rotation) * indexPose.Rotation;
        // //     string State = Difference.y <= 0? "Yes": "No";
        // //     DistText.text = $"{Difference.y.ToString("F4")} \n\n {State}";
        // // }
        string hand = Hand(eventData.Handedness);
        HandText.text = $"Current Hand: {hand}";

        float IndexCurl = HandPoseUtils.IndexFingerCurl(eventData.Handedness);
        if(IndexText) IndexText.text = $"{IndexCurl.ToString("F4")}";
        
        float ThumbCurl = HandPoseUtils.ThumbFingerCurl(eventData.Handedness);
        if(ThumbText) ThumbText.text = $"{ThumbCurl.ToString("F4")}";

        DistText.text = IndexCurl + ThumbCurl > 1? "Yes": "No";
    }
    public void UpdateToolText(string ToolType = "empty"){
        CurText.text = ToolType;
    }
}
