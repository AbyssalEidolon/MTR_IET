using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class autoscoll : MonoBehaviour
{
    public float scrollSpeed = 10.0f;
    public List<GameObject> childs = new List<GameObject>();
    public List<GameObject> able = new List<GameObject>();
    public List<Vector3> childspos = new List<Vector3>();
    public Vector3 maxpos;
    public Vector3 minpos;
    public GameObject btn;
    public float runTime;
    public float endTime;

    private void OnEnable()
    {
        runTime = 0;
        if (childs.Count != 0)
        {
            for (int i = 0; i < childspos.Count; i++)
            {
                childs[i].GetComponent<RectTransform>().localPosition = childspos[i];
            }
        }
    }
    void Start()
    {
        foreach (Transform childTrans in transform)
        {
            if (childTrans.gameObject != btn)
            {
                childs.Add(childTrans.gameObject);
            }
        }
    }


    void Update()
    {
        runTime += 1 * Time.deltaTime;
        if (runTime < endTime)
        {
            foreach (GameObject child in childs)
            {
                RectTransform rect = child.GetComponent<RectTransform>();
                rect.localPosition += new Vector3(0, -10f, 0) * Time.deltaTime;
                if (rect.localPosition.y <= maxpos.y || rect.localPosition.y >= minpos.y)
                {
                    child.SetActive(false);
                    if (able.Count != 0)
                    {
                        able.Remove(child);
                    }
                }
                else
                {
                    child.SetActive(true);
                    if (able.Count == 0)
                    {
                        able.Add(child);
                    }
                }
            }
        }

        if (able.Count == 0)
        {
            btn.SetActive(true);
        }
        else
        {
            btn.SetActive(false);
        }
    }
}