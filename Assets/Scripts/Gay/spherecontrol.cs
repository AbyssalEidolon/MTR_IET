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
        wiresphere = Instantiate(this.gameObject);
        replace(this.gameObject, wiresphere, wireController.spheres);
        wireController.gay.Add(this.gameObject);
        wireController.gay.Add(wiresphere);
    }
    public void grabend()
    {
        wireController.gay.Remove(this.gameObject);
        wireController.gay.Remove(wiresphere);
        Destroy(this.gameObject);
    }

    void replace(GameObject gameobject, GameObject newgameobject, List<GameObject> list)
    {
        int listindex = list.IndexOf(gameobject);
        list.Remove(gameobject);
        list.Insert(listindex, newgameobject);
    }
}
