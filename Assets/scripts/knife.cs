using UnityEngine;

public class Knife : MonoBehaviour
{
    public Transform player;
    public PlayerMovement pMov;
    public float rotationSpeed = 500f;
    public float slashAngle = 90f;
    public Health health;
    public int damage = 50;
    public bool isSlashing = false;
    private float startRotation;
    private float endRotation;
    private float t;

    void Update()
    {
        FollowPlayer();

        if (Input.GetKeyDown(KeyCode.Z) && !isSlashing & pMov.currentStamina >= 20 & health != null)
        {
            pMov.currentStamina -= 5;
            isSlashing = true;
            startRotation = transform.eulerAngles.z;
            endRotation = startRotation + slashAngle;
            health.TakeDamage(damage);
            t = 0f;
        }

        if (isSlashing)
        {
            t += Time.deltaTime * rotationSpeed / slashAngle;
            float angle = Mathf.Lerp(startRotation, endRotation, t);
            transform.eulerAngles = new Vector3(0, 0, angle);

            if (t >= 1f)
            {
                isSlashing = false;
                transform.eulerAngles = new Vector3(0, 0, startRotation);
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("Weapon")  ||  other.gameObject.CompareTag("Knife") || other.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            health = other.GetComponent<Collider2D>().gameObject.GetComponent<Health>();
        }

        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        health = null;

    }

    void FollowPlayer()
    {
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
     
}

