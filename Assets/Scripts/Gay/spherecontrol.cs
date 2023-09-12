using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spherecontrol : MonoBehaviour
{
    public WireController wireController;
    public GameObject wiresphere;

    private void Start()
    {
        wireController = GameObject.Find("Wire").GetComponent<WireController>();
    }

    public void grab()
    {
        wireController.grab.Add(this.gameObject);
    }

    public void grabend(){
        wireController.grab.Remove(this.gameObject);
    }
}
