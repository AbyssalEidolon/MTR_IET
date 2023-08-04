using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ToggleCollider : MonoBehaviour
{
    public Collider colieder = null;
    Interactable inter = null;
    void Start(){
        inter = GetComponent<Interactable>();
    }
    public void OnToggle(){
        colieder.enabled = !inter.IsToggled;
    }
}
