using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireStripping : ToolBase
{
    public Material mat = new Material(Shader.Find("Standard"));

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
        if(other.gameObject.name == "Wire")
        {
            mat.color = Color.white;
            Renderer ren = other.gameObject.GetComponent<Renderer>();
            Material originalMat = ren.sharedMaterial;

            Material[] materials = new Material[2];

            materials[0] = originalMat;
            materials[1] = mat;

            ren.sharedMaterials = materials;
        }
    }
}
