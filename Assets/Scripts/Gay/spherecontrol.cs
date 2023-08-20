using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spherecontrol : MonoBehaviour
{
    public GameObject cursor;
    public WireController wireController;
    public GameObject wiresphere;

    private void Start()
    {
        cursor = GameObject.Find("DefaultGazeCursor(Clone)");
        wireController = GameObject.Find("Wire").GetComponent<WireController>();
    }

    public void grab()
    {
        wiresphere = Instantiate(this.gameObject);
        Vector3 dir = cursor.transform.position - wireController.spheres[1].transform.position;
        Vector3 projectdir = dir - Vector3.Project(dir, Vector3.forward);
        projectdir = projectdir.normalized * 0.2475f;
        wiresphere.transform.position = projectdir;
    }
}
