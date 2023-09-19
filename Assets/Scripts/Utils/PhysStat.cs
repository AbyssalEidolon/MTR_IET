using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]
public class PhysStat : MonoBehaviour
{
    public StatFlagas stats;
    public GameObject target = null;
    TextMeshProUGUI text;
    void Start() => text = gameObject.GetComponent<TextMeshProUGUI>();
    void FixedUpdate(){
        if(!target) return;
        string temp = "";
        if(stats.HasFlag(StatFlagas.Position)) temp = $"Position: \n {target.transform.position.ToString("F4")} \n";
        if(stats.HasFlag(StatFlagas.Rotation)) temp += $"Rotation: \n{target.transform.rotation.ToString("F4")} \n";
        if(stats.HasFlag(StatFlagas.Scale)) temp += $"Scale: \n{target.transform.localScale.ToString("F4")} \n";
        text.text = temp;
    }
}
[Flags]
    public enum StatFlagas{
        Position = 1,
        Rotation = 2,
        Scale = 3,
    }
