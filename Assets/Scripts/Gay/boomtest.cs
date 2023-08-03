using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomtest : MonoBehaviour
{
    public GameObject stripper;

    void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (other.gameObject.name == "wire")
        {
            GameObject wire = other.gameObject;
            LineRenderer wireLine = wire.GetComponent<LineRenderer>();
            Material mat = wire.GetComponent<LineRenderer>().material;

            GameObject lwire = Instantiate(wire);
            LineRenderer lwireline = lwire.GetComponent<LineRenderer>();
            lwireline.SetPosition(1, new Vector3(wireLine.GetPosition(1).x, wireLine.GetPosition(1).y, stripper.transform.position.x));
            BoxCollider lwirebox = lwire.GetComponent<BoxCollider>();
            lwirebox.size = new Vector3(lwirebox.size.x, lwirebox.size.y, lwirebox.size.z - stripper.transform.position.x);
        }
    }
}