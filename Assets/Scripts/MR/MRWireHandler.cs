using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.OpenXR;

public class MRWireHandler : MonoBehaviour
{
    public const float Scale = 0.033f;
    public PinchSlider Slider = null;
    float SliderValue = 0;
    public float WireLength => SliderValue * 15 * 0.033f;
    public float WireLengthCM => SliderValue * 15;
    int CurrentHand = 0;
    public void UpdateSliderValue(){
        SliderValue = Slider.SliderValue;
    }
    public void EnableCutters(HandTrackingInputEventData eventData){
        CurrentHand = (int)eventData.Handedness;
    }
}
