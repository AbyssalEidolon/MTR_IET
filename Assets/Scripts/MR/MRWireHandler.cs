using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.OpenXR;
using System.Threading;

public class MRWireHandler : MonoBehaviour
{
    public const float Scale = 0.033f;
    public PinchSlider Slider = null;
    float SliderValue = 0;
    public float WireLength => SliderValue * 15 * 0.033f;
    public float WireLengthCM => SliderValue * 15;
    int CurrentHand = 0;
    float IndicatorProc = 10f/15f;
    public GameObject Indicator = null;
    public TMPro.TextMeshProUGUI LengthInd = null;
    GameObject Wheel = null;
    void Start(){
        Wheel = GameObject.Find("Wheel");

    }
    public void UpdateSliderValue(){
        SliderValue = Slider.SliderValue;
        new Thread(new ThreadStart(UpdateIndicators));
    }
    public void EnableCutters(HandTrackingInputEventData eventData){
        CurrentHand = (int)eventData.Handedness;
    }
    void UpdateIndicators(){
        while(true){
        float Change = Slider.SliderValue - SliderValue;
        float Angle = Change / Mathf.PI * 360;
        Wheel.transform.Rotate(new(0, 0, Angle));
        Indicator.SetActive(SliderValue >= IndicatorProc);
        LengthInd.text = WireLengthCM.ToString("F2") + " cm";
        Thread.Sleep(1000);
        }
    }   
}
