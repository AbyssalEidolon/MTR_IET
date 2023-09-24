using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class LineCollider : MonoBehaviour{
    MeshCollider collider;
    void GenerateCollider(){
         collider = !collider? gameObject.GetComponent<MeshCollider>(); 
    }
}