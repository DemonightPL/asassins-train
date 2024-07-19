using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public float fireRate = 0.1f;
    public float maxSpreadAngle = 10f;
    public float spreadIncreasePerShot = 1f;
    public float spreadDecreasePerSecond = 5f;
    private float fireCooldown = 0f;
    private float currentSpreadAngle = 0f;
    private bool isFiring = false;
    private bool isTouching;

    void Update()
    {
        

        if (Input.GetButton("Fire1") && fireCooldown <= 0f & isTouching)
        {
            Fire();
            fireCooldown = fireRate;
            currentSpreadAngle += spreadIncreasePerShot;
            if (currentSpreadAngle > maxSpreadAngle)
            {
                currentSpreadAngle = maxSpreadAngle;
            }
        }
         if (Input.GetButton("Fire1"))
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }
       

        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (!isFiring && currentSpreadAngle > 0f)
        {
            currentSpreadAngle -= spreadDecreasePerSecond * Time.deltaTime;
            if (currentSpreadAngle < 0f)
            {
                currentSpreadAngle = 0f;
            }
        }
    }

    void Fire()
    {
        float angle = Random.Range(-currentSpreadAngle, currentSpreadAngle);
        Quaternion spread = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation * spread);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        bullet.GetComponent<Bullet>().firedByPlayer = true;
        
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Hand"))
        {
        isTouching = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hand"))
        {
        isTouching = false;
        }
    }
}
