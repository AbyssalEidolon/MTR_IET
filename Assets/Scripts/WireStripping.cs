using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireStripping : ToolBase
{
    public GameObject stripper;
    public GameObject wire;

    public override void Duplicate(ManipulationEventData eventData)
    {
        base.Duplicate(eventData);
        LineManipulator.i.BakeCollider();
    }
    public override void Delete(ManipulationEventData eventData)
    {
        base.Delete(eventData);
        LineManipulator.i.DestroyCollider();
    }

    void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (other.gameObject.name == "Wire")
        {
            GameObject wire = other.gameObject;

            Material mat = wire.GetComponent<MeshRenderer>().material;
            Vector3 pos = stripper.transform.position;
            Vector3 wirescale = wire.transform.localScale;
            Vector3 leftPoint = wire.transform.position - Vector3.right * wirescale.x / 2;
            Vector3 rightPoint = wire.transform.position + Vector3.right * wirescale.x / 2;

            GameObject leftwire = Instantiate(other.gameObject);
            leftwire.transform.position = (leftPoint + pos) / 2;
            float leftWidth = Vector3.Distance(pos, leftPoint);
            leftwire.transform.localScale = new Vector3(leftWidth, wirescale.y, wirescale.z);
            leftwire.GetComponent<MeshRenderer>().material = mat;
            leftwire.GetComponent<MeshRenderer>().material.color = Color.red;

            GameObject rightwire = Instantiate(other.gameObject);
            rightwire.transform.position = (rightPoint + pos) / 2;
            float rightWidth = Vector3.Distance(pos, rightPoint);
            rightwire.transform.localScale = new Vector3(rightWidth, wirescale.y, wirescale.z);
            rightwire.GetComponent<MeshRenderer>().material = mat;
            rightwire.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
