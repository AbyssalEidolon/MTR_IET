using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class WireStrip : ToolBase
{
    public override string ToolType() => "wireStripper";
    protected override void OnCollisionEnter(Collision collision)
    {
        throw new System.NotImplementedException();
    }
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
}
