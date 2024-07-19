using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float friction = 0.9f;
    private float maxSpeed = 10f;
    private float rotationSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
   
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            maxSpeed = 4f;
           rotationSpeed = 2f;
        }
        else
        {
            
            maxSpeed = 8f;
            rotationSpeed = 5f;
        }
    }

    void FixedUpdate()
    {
        if (movement.magnitude > 0)
        {
            rb.AddForce(movement.normalized * moveSpeed, ForceMode2D.Force);
        }
        else
        {
            rb.velocity *= friction;
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        Vector2 lookDir = mousePos - rb.position;
        float targetAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        float angle = Mathf.LerpAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.rotation = angle;
    }
}
