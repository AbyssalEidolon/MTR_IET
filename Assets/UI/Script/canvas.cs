using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class canvas : MonoBehaviour
{
    public Canvas[] CanVas = new Canvas[13];
    int index = 0;
    public void Next()
    {
        index ++;
        foreach (Canvas CanVAs in CanVas)
        {
            CanVAs.gameObject.SetActive(false);
        }
        CanVas[index].gameObject.SetActive(true);
    }
    public void Previous()
    {
        index --;
        foreach (Canvas CanVAs in CanVas)
        {
            CanVAs.gameObject.SetActive(false);
        }
        CanVas[index].gameObject.SetActive(true);
    }
    public void credit(int i)
    {
        foreach (Canvas CanVas in CanVas)
        {
            CanVas.gameObject.SetActive(false);
            index=12;
        }
        CanVas[12].gameObject.SetActive(true);
    }

    public void home(int i)
    {
        foreach (Canvas CanVas in CanVas)
        {
            CanVas.gameObject.SetActive(false);
            index = 0;
        }
        CanVas[0].gameObject.SetActive(true);
    }
    private void Update()
    {
        Debug.Log(index);
    }
}

