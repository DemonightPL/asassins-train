using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float friction = 0.9f;
    private float maxSpeed = 10f;
    private float rotationSpeed = 5f;
    public float dashForce = 20f;
    public float dashCooldown = 1f;
    private float dashCooldownTimer = 0f;
    public float maxStamina = 100f;
    private float currentStamina;
    public float staminaDrainRate = 10f;
    public float staminaRecoveryRate = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    bool isDashing = false;
    
    public float staminaThresholdSpeeddep = 5f;
    public float staminaThresholdSpeedreg = 5f;
    public Slider staminaSlider;
    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        staminaSlider.value = currentStamina / maxStamina;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = 4f;
            rotationSpeed = 2f;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && currentStamina > 0)
        {
            maxSpeed = 12f;
            rotationSpeed = 7f;
        }
        else
        {
            maxSpeed = 8f;
            rotationSpeed = 5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0 && currentStamina >= 20f)
        {
            StartCoroutine(Dash());
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
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

            if (rb.velocity.magnitude > staminaThresholdSpeeddep)
            {
                DrainStamina();
            }

            if (rb.velocity.magnitude < staminaThresholdSpeedreg)
            {
               RecoverStamina();
            }
            
            
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Vector2 dashDirection = rb.velocity.normalized;
        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
        dashCooldownTimer = dashCooldown;
        currentStamina -= 20f;
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
    }

    void DrainStamina()
    {
        if (currentStamina > 0)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
        }
    }

    void RecoverStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }
}
