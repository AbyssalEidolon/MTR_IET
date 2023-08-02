using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttononclick : MonoBehaviour
{
    public void ReturnPage()
    {
        SceneManager.LoadScene("Start_page",LoadSceneMode.Single);
    }
    public void step1()
    {
        SceneManager.LoadScene("Step1", LoadSceneMode.Single);
    }
    public void step2()
    {
        SceneManager.LoadScene("Step2", LoadSceneMode.Single);
    }
    public void Credit()
    {
        SceneManager.LoadScene("Credit", LoadSceneMode.Single);
    }
}