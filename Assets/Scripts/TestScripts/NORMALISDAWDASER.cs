using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NORMALISDAWDASER : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject A;
     public GameObject B;
 public GameObject C;
    // Update is called once per frame
    void Update()
    {
        print(Utils.NormalisedVectorCheck(A.transform.position, B.transform.position, C.transform.position));
    }
}
