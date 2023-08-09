using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttononclick : MonoBehaviour
{
    public Button btn;

    public void CurrentScene()
    {
        switch (btn.name)
        {
            case "nextbtn":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                break;
            case "Returnbtn":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
                break;
            case "Homebtn":
                SceneManager.LoadScene(0);
                break;
            case "creditbtn":
                SceneManager.LoadScene("Credit", LoadSceneMode.Single);
                break;
        }
    }
}