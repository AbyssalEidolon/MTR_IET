using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomtest2 : MonoBehaviour
{
    public GameObject stripper;

    void resetBox(GameObject box)
    {
        Destroy(box.GetComponent<BoxCollider>());
        box.AddComponent<BoxCollider>();
    }

    void strip(GameObject stripper, GameObject wire)
    {
        GameObject newWire = Instantiate(wire);
        newWire.name = "New Wire";
        newWire.transform.SetParent(wire.transform);
        LineRenderer newWireRen = newWire.GetComponent<LineRenderer>();
        LineRenderer wireRen = wire.GetComponent<LineRenderer>();
        Vector3 point0 = wireRen.GetPosition(0);
        Vector3 point1 = wireRen.GetPosition(1);
        Vector3 pos = stripper.transform.position - wire.transform.position;

        float mid = (point1.x - point0.x)/ 2;
        print(mid);
        if (pos.x < mid)
        {
            wireRen.SetPosition(0, new Vector3(pos.x, point0.y, point0.z));
            newWireRen.SetPosition(1, new Vector3(pos.x, point0.y, point0.z));
            newWireRen.material.color = Color.white;
        }
        else
        {
            wireRen.SetPosition(1, new Vector3(pos.x, point1.y, point1.z));
            newWireRen.SetPosition(0, new Vector3(pos.x, point1.y, point1.z));
            newWireRen.material.color = Color.white;
        }
        resetBox(newWire);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "wire")
        {
            if (collision.gameObject.transform.childCount < 2)
            {
                strip(stripper, collision.gameObject);
            }
        }
    }
}
