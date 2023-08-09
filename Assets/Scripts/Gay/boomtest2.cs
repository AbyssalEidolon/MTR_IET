using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomtest2 : MonoBehaviour
{
    public GameObject stripper;

    void strip(GameObject stripper, GameObject wire)
    {
        LineRenderer wireRen = wire.GetComponent<LineRenderer>();
        Vector3 point0 = wireRen.GetPosition(0);
        Vector3 point1 = wireRen.GetPosition(1);
        GameObject newWire = Instantiate(wire);
        newWire.transform.SetParent(wire.transform);
        newWire.name = "New Wire";
        LineRenderer newWireRen = newWire.GetComponent<LineRenderer>();

        float mid = (point1.z - point0.z) / 2;
        if(stripper.transform.position.x < mid)
        {
            Debug.Log("left");
            wireRen.SetPosition(0, new Vector3(point0.x, point0.y, stripper.transform.position.x));
            newWireRen.SetPosition(1, new Vector3(point0.x, point0.y, stripper.transform.position.x));
            newWireRen.material.color = Color.white;
        }
        else
        {
            Debug.Log("right");
            wireRen.SetPosition(1, new Vector3(point1.x, point1.y, stripper.transform.position.x));
            newWireRen.SetPosition(0, new Vector3(point1.x, point1.y, stripper.transform.position.x));
            newWireRen.material.color = Color.white;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "wire")
        {
            GameObject wire = collision.gameObject;
            strip(stripper, wire);
        }
    }
}
