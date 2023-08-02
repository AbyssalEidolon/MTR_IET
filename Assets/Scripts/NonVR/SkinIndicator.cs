using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class SkinIndicator : MonoBehaviour
{
    public GameObject IndicatorPrefab = null;
    [Range(0.8f,1)]
    public float SkinLength = 0.8f;
    float Scale = 0.033f;
    GameObject[] Indicators = new GameObject[2];
    LineRenderer Line;
    WireConstraints wireConstraints;
    void Start()
    {
        Line = gameObject.GetComponent<LineRenderer>();
        Indicators[0] = Instantiate(IndicatorPrefab, gameObject.transform.position + Vector3.MoveTowards(Line.GetPosition(0), Line.GetPosition(1), Scale * SkinLength),Quaternion.identity, gameObject.transform);
        Indicators[1] = Instantiate(IndicatorPrefab, gameObject.transform.position + Vector3.MoveTowards(Line.GetPosition(1), Line.GetPosition(0), Scale * SkinLength),Quaternion.identity, gameObject.transform);
        foreach(GameObject indicator in Indicators){
            indicator.AddComponent<FaceCamera>();
        }
        wireConstraints = new(Line);
    }
}
