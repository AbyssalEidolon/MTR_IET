using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttononclick : MonoBehaviour
{
    public Button btn;
    int btnstatement = 0;

    public void CurrentScene()
    {
        switch (btn.name)
        {
            case "nextbtn":
                btnstatement++;
                SceneManager.LoadScene(btnstatement);
                break;
            case "Returnbtn":
                btnstatement--;
                SceneManager.LoadScene(btnstatement);
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