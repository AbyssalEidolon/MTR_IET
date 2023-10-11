using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas_v2 : MonoBehaviour
{
    public bool grabbed;
    public GameObject Camera;
    public GameObject theBox;
    public GameObject[] wire_step = new GameObject[9];
    public Canvas[] CanVas = new Canvas[11];
    public int index = 0;
    public int wire_index = 0;


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
        CanVas[index].gameObject.transform.position = CanVas[index - 1].gameObject.transform.position;
        CanVas[index].gameObject.transform.localScale = CanVas[index - 1].gameObject.transform.localScale;


        if (wire_index >= 0 && wire_index != 10)
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
        if(index == 11)
        {
            foreach (GameObject wireStep in wire_step)
            {
                wireStep.SetActive(true);
            }
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
        CanVas[index].gameObject.transform.position = CanVas[index + 1].gameObject.transform.position;
        CanVas[index].gameObject.transform.localScale = CanVas[index + 1].gameObject.transform.localScale;

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
        CanVas[0].gameObject.transform.position = CanVas[index].gameObject.transform.position;
        Debug.Log("changed"+ CanVas[index].gameObject.name);
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
        foreach (Canvas CanVas in CanVas)
        {
            CanVas.transform.LookAt(Camera.transform);
            CanVas.transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
        }
        if(grabbed == true)
        {
            theBox.transform.LookAt(Camera.transform);
            theBox.transform.rotation *= Quaternion.Euler(-55f, 180f, 0f);
        }
    }

    private void Start()
    {
        foreach (GameObject Wire_Step in wire_step)
        {
            Wire_Step.gameObject.SetActive(false);
        }
    }

    public void grab()
    {
        grabbed = true;
    }

    public void grabend()
    {
        grabbed = false;
    }
}
