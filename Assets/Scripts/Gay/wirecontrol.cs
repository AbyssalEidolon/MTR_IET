using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wirecontrol : MonoBehaviour
{
    private void Start()
    {
        CheckParent();
    }
    void CheckParent()
    {
        if (transform.parent)
        {
            if (GetComponentInParent<wirecontrol>())
            {
                for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
                Destroy(this);
            };
        }
    }
}
