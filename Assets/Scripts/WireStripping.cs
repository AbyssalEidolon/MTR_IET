using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireStripping : ToolBase
{
    // Start is called before the first frame update
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
