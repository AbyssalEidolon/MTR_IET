using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestFollow : MonoBehaviour
{
   private void FixedUpdate() {
    Vector2 MouseMacro = Mouse.current.position.ReadValue();
    Vector3 RayResult = Camera.main.ScreenToWorldPoint(new(MouseMacro.x, MouseMacro.y, 10));
   //  print(RayResult);
    gameObject.GetComponent<Rigidbody2D>().AddForce(new(RayResult.x, 0), ForceMode2D.Impulse);
   }
}
