using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
public class ToolBase : MonoBehaviour{
    public Transform Parent = null;
    public GameObject Dave = null;
    void Awake(){
        Parent = transform.parent;
        if(!gameObject.GetComponent<Rigidbody>())
        gameObject.AddComponent<Rigidbody>().useGravity = false;
    }
    public virtual void Duplicate(ManipulationEventData eventData)
    {
        if(SceneController.i.TestText) SceneController.i.TestText.text = SceneController.i.TestText.text.Replace("Waiting...","Picked Up");
        name = "New";
        Dave = Instantiate(gameObject, Parent);
        Dave.GetComponent<MeshRenderer>().enabled = false;
        Dave.name = "Root";
        Destroy(Dave.GetComponent<Rigidbody>());
        Dave.GetComponent<BoxCollider>().enabled = false;
    }
    public virtual void Delete(ManipulationEventData eventData){
        if(SceneController.i.TestText) SceneController.i.TestText.text = SceneController.i.TestText.text.Replace("Picked Up","Waiting...");
        Destroy(gameObject);
        Rigidbody rb = Dave.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Dave.GetComponent<BoxCollider>().enabled = true;
        Dave.GetComponent<MeshRenderer>().enabled = true;
    }
}