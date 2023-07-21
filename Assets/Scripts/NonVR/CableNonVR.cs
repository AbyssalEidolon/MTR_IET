using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CableNonVR : MonoBehaviour
{
    public LineRenderer lineRenderer = null;
    public GameObject Wheel = null;
    Vector3 WheelPos => Wheel.transform.position;
    public GameObject Handle = null;
    float HandleFill => Handle.GetComponent<Slider>().value;
    public GameObject Indicator = null;
    public GameObject End = null;
    public float IndOffset => SuggestedWireLength * -10;
    [Tooltip("Scaled 1:5 as in 1cm to 5 units")]
    public float SuggestedWireLength = 10;
    // Start is called before the first frame update
    int offset = -100;//Literally Line Offset
    float LineMultiplier(float val = -1, float mul = -1){
        if(val == -1) val = HandleFill;
        mul = mul == -1 ? End.transform.localPosition.x : -1;
        return val * mul + offset;
        }
    private void Start() {
        if(lineRenderer == null || Wheel == null || Handle == null){
            Debug.Log("Please set every variable up I beg of you");
            Application.Quit();
        }
        lineRenderer.SetPosition(0, WheelPos);
        lineRenderer.SetPosition(1, new(LineMultiplier(), WheelPos.y, WheelPos.z));
    }
    public void UpdateLine(){
        lineRenderer.SetPosition(1, new(LineMultiplier(), WheelPos.y, WheelPos.z));
        // print(LineMultiplier());
        if(LineMultiplier() - offset >= -IndOffset)
        {
            Indicator.SetActive(true);
            Indicator.transform.position = new(LineMultiplier() + IndOffset, WheelPos.y, WheelPos.z);
        }
        else Indicator.SetActive(false);
    } 
        
    
}
