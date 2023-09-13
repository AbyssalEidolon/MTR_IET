using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas_v2 : MonoBehaviour
{
    public GameObject[] wire_step = new GameObject[9];
    public Canvas[] CanVas = new Canvas[11];
    int index = 0;
    int wire_index = 0;


    public void Next()
    {
        index++;
        wire_index = index - 1;
        Debug.Log("wire: " + wire_index);
        Debug.Log(index);
        foreach (Canvas CanVAs in CanVas)
        {
            CanVAs.gameObject.SetActive(false);
            ;
        }
        CanVas[index].gameObject.SetActive(true);
        
        if (wire_index >= 0)
        {
            foreach (GameObject Wire_Step in wire_step)
            {
                Wire_Step.gameObject.SetActive(false);
            }
            if (wire_index > wire_step.Length -1)
                return;
            else
                wire_step[wire_index].SetActive(true);
        }
    }
    public void Previous()
    {
        index--;
        wire_index = index - 1;
        foreach (Canvas CanVAs in CanVas)
        {
            CanVAs.gameObject.SetActive(false);
        }
        CanVas[index].gameObject.SetActive(true);
        if (wire_index >= 0)
        {
            foreach (GameObject Wire_Step in wire_step)
            {
                Wire_Step.gameObject.SetActive(false);
            }
            wire_step[wire_index].SetActive(true);
        }
    }
    public void credit(int i)
    {
        foreach (Canvas CanVas in CanVas)
        {
            CanVas.gameObject.SetActive(false);
        }
        CanVas[CanVas.Length - 1].gameObject.SetActive(true);

    }

    public void home(int i)
    {
        foreach (Canvas CanVas in CanVas)
        {
            CanVas.gameObject.SetActive(false);
            index = 0;
        }
        CanVas[0].gameObject.SetActive(true);
        foreach (GameObject Wire_Step in wire_step)
        {
            Wire_Step.SetActive(false);
            wire_index = index - 1;
        }
    }
    private void Update()
    {
        //Debug.Log(index);
    }

    private void Start()
    {
        foreach (GameObject Wire_Step in wire_step)
        {
            Wire_Step.gameObject.SetActive(false);
        }
    }
}
