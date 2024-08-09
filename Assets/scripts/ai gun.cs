using UnityEngine;
using System.Collections;

public class aigun : MonoBehaviour
{
    private Renderer objectRenderer;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public float maxAmmo = 30f; 
    public float fireRate = 0.1f;
    public float maxSpreadAngle = 10f;
    public float spreadIncreasePerShot = 1f;
    public float spreadDecreasePerSecond = 5f;
    private float fireCooldown = 0f;
    private float currentSpreadAngle = 0f;
    private bool isFiring = false;
    private bool reloading = false;
    private float ammo;
    
    public bool seePlayer;

    void Start()
    {
        ammo = maxAmmo;
        objectRenderer = GetComponent<Renderer>();

        
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color randomColor = new Color(r, g, b);
        objectRenderer.material.color = randomColor;
    }

    void Update()
    {
        if (seePlayer && !reloading)
        {
            HandleNPCShooting();
        }
    }

    void HandleNPCShooting()
    {
        if (fireCooldown <= 0f && ammo > 0f)
        {
            Fire();
            ammo -= 1f;
            fireCooldown = fireRate;
            currentSpreadAngle += spreadIncreasePerShot;
            if (currentSpreadAngle > maxSpreadAngle)
            {
                currentSpreadAngle = maxSpreadAngle;
            }
        }

        if (ammo <= 0f && !reloading)
        {
            reload();
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

    void reload()
    {
        reloading = true;
        StartCoroutine(ReloadWait());
    }

    IEnumerator ReloadWait()
    {
        yield return new WaitForSeconds(3); 
        ammo = maxAmmo;
        reloading = false;
    }

    void Fire()
    {
        float angle = Random.Range(-currentSpreadAngle, currentSpreadAngle);
        Quaternion spread = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation * spread);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        bullet.GetComponent<Bullet>().firedByPlayer = false;
    }
}
