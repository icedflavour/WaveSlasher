using UnityEngine;


public class script : MonoBehaviour
{
 Rigidbody2D body;

 float horizontal;
 float vertical;

 public float runSpeed = 20.0f;

 void Start ()
 {
   body = GetComponent<Rigidbody2D>(); 
 }

 void Update ()
 {
   horizontal = Input.GetAxisRaw("Horizontal");
   vertical = Input.GetAxisRaw("Vertical"); 
 }

 private void FixedUpdate()
 {  
   body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
 }
}
