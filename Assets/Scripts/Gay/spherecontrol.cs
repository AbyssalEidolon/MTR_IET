using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spherecontrol : MonoBehaviour
{
    public WireController wireController;
    public GameObject empty;

    private void Start()
    {
        wireController = GameObject.Find("Wire").GetComponent<WireController>();
    }

    public void grab()
    {
        wireController.grab.Add(this.gameObject);
        if(wireController.grab.Count == 1){
            empty = new GameObject("empty");
            empty.transform.SetParent(wireController.grab[0].transform);
        }
    }

    public void grabend(){
        wireController.grab.Remove(this.gameObject);
        for(int i = 0; i < wireController.spheres.Count;i++)
        {
            wireController.spheres[i].transform.parent = null;
        }
    }
}
