using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableController : MonoBehaviour {
    public bool MR = false;
    public static CableController i = null;
    public List<GameObject> FreeLines = new();
    private void Awake() {
        i = this;
    }
}