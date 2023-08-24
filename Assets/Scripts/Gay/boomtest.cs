using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomtest : MonoBehaviour
{
    public GameObject stripper;
    public float mid;
    public bool left;

    public void strip(GameObject stripper, GameObject wire, Color left, Color right)
    {
        Vector3 pos = wire.transform.position;
        GameObject lwire = Instantiate(wire);
        lwire.name = "leftwire";
        lwire.transform.position = pos;
        float stripperValueX = stripper.transform.position.x - pos.x;
        LineRenderer wireLine = wire.GetComponent<LineRenderer>();
        LineRenderer lwireline = lwire.GetComponent<LineRenderer>();
        lwireline.SetPosition(1, new Vector3(wireLine.GetPosition(1).x, wireLine.GetPosition(1).y, stripperValueX));
        BoxCollider lwirebox = lwire.GetComponent<BoxCollider>();
        lwirebox.size = new Vector3(lwirebox.size.x, lwirebox.size.y, stripperValueX);
        lwirebox.center = new Vector3(0, 0, lwirebox.size.z / 2);
        lwire.GetComponent<LineRenderer>().material.color = left;

        GameObject rwire = Instantiate(wire);
        rwire.name = "rightwire";
        rwire.transform.position = new Vector3(stripperValueX, pos.y, pos.z);
        LineRenderer rwireline = rwire.GetComponent<LineRenderer>();
        rwireline.SetPosition(1, new Vector3(wireLine.GetPosition(1).x, wireLine.GetPosition(1).y, wireLine.GetPosition(1).z - stripperValueX));
        BoxCollider rwirebox = rwire.GetComponent<BoxCollider>();
        rwirebox.size = new Vector3(rwirebox.size.x, rwirebox.size.y, rwirebox.size.z - stripperValueX) ;
        rwirebox.center = new Vector3(0, 0, rwirebox.size.z / 2);
        rwire.GetComponent<LineRenderer>().material.color = right;

        GameObject empty = new GameObject();
        empty.transform.position = pos;
        empty.name = "dump";
        Destroy(wire);
        lwire.transform.SetParent(empty.transform);
        rwire.transform.SetParent(empty.transform);
    }

    void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (other.gameObject.name == "wire")
        {
            GameObject wire = other.gameObject;
            LineRenderer wireLine = wire.GetComponent<LineRenderer>();
            float mid = (wireLine.GetPosition(1).z - wireLine.GetPosition(0).z) / 2;
            if (stripper.transform.position.x < mid)
            {
                Debug.Log("left");
                strip(stripper, wire, Color.white, Color.red);
            }
            else
            {
                Debug.Log("right");
                strip(stripper, wire, Color.red, Color.white);
            }
        }
    }
}