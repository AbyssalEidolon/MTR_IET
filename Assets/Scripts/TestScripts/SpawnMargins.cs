using System;
using UnityEngine;
public class SpawnMargins : MonoBehaviour {
    bool temp = false;
    public WireController controller;
    LineRenderer[] StrippedSegs => controller.StrippedSegs;
    LineRenderer Self => controller.Self;
    float[] Margins => controller.Margins;
    int BoolToInt(bool ballin) => ballin ? 1 : 0;
    public void Init(){
        Vector3[] newVecs = new Vector3[2];
        for (int i = 0; i < StrippedSegs.Length; i++)
        {
            LineRenderer tempLine = Instantiate(gameObject, transform).GetComponent<LineRenderer>();
            Vector3 TargetVec = Vector3.MoveTowards(Self.GetPosition(BoolToInt(temp)), gameObject.transform.position, Margins[BoolToInt(temp)] * MRWireHandler.Scale);
            tempLine.SetPositions(new Vector3[]{
                Self.GetPosition(BoolToInt(temp)),
                TargetVec
            });
            tempLine.material = controller.marginMat;
            newVecs[i] = TargetVec;
            temp = !temp;
            StrippedSegs[i] = tempLine;
        }
        Self.SetPositions(newVecs);
        Destroy(this);
    }
}