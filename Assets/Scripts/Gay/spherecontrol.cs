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
        if(wireController.grap.Count <= 1)
        {
            wireController.grap.Add(this.gameObject);
        }
        wiresphere = Instantiate(this.gameObject);
        replace(this.gameObject, wiresphere, wireController.spheres);
        wireController.theCircle.Add(this.gameObject);
        wireController.theCircle.Add(wiresphere);
    }
    public void grabend()
    {
        wireController.theCircle.Remove(this.gameObject);
        wireController.theCircle.Remove(wiresphere);
        wireController.grap.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    void replace(GameObject gameobject, GameObject newgameobject, List<GameObject> list)
    {
        int listindex = list.IndexOf(gameobject);
        list.Remove(gameobject);
        list.Insert(listindex, newgameobject);
    }
}
