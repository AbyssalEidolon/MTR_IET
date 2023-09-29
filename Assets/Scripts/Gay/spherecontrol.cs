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
        if (wireController.grab.Count == 2)
        {
            GameObject empty = new GameObject("empty");
            empty.transform.position = wireController.grab[1].transform.position;
            List<GameObject> spheres = wireController.spheres;
            List<GameObject> grab = wireController.grab;
            List<GameObject> ForChange = wireController.ForChange;
            if (spheres.IndexOf(grab[1].gameObject) > spheres.IndexOf(grab[0].gameObject))
            {
                for (int i = spheres.IndexOf(grab[0].gameObject) + 1; i < spheres.Count; i++)
                {
                    spheres[i].transform.SetParent(empty.transform);
                    ForChange.Add(spheres[i]);
                }
            }
            else
            {
                for (int i = 0; i < spheres.IndexOf(grab[0].gameObject) - 1; i++)
                {
                    spheres[i].transform.SetParent(empty.transform);
                    ForChange.Add(spheres[i]);
                }
            }
        }

    }

    public void grabend()
    {
        wireController.grab.Remove(this.gameObject);
        if (wireController.grab.Count == 0)
        {
            for (int i = 0; i < wireController.spheres.Count; i++)
            {
                wireController.spheres[i].transform.parent = null;
            }
        }else if (wireController.grab.Count == 1)
        {
            for (int i = 0; i < wireController.ForChange.Count; i++)
            {
                wireController.ForChange[i].transform.parent = null;
            }
        }
    }
    void replace(GameObject gameobject, GameObject newgameobject, List<GameObject> list)
    {
        int listindex = list.IndexOf(gameobject);
        list.Remove(gameobject);
        list.Insert(listindex, newgameobject);
    }
}
