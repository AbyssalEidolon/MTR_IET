using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttononclick : MonoBehaviour
{
    public void onstart()
    {
        SceneManager.LoadScene("step1", LoadSceneMode.Single);
    }
}