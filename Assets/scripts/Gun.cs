using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
      private Renderer objectRenderer;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public float maxammo;
    public float ammo;
    public string ammotype;
    public string type;
    public float fireRate = 0.1f;
    public float maxSpreadAngle = 10f;
    public float spreadIncreasePerShot = 1f;
    public float spreadDecreasePerSecond = 5f;
    private float fireCooldown = 0f;
    private float currentSpreadAngle = 0f;
    private bool isFiring = false;
    private bool reloading;
    public inventory inventory;
    public bool isheld;
    public bool isheldai;

    private GameObject pl;
    private GameObject enemy;

    void Start()
    {
        ammo = maxammo;
        pl = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy"); 
         objectRenderer = GetComponent<Renderer>();
        inventory = pl.GetComponent<inventory>();
         float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

       
        Color randomColor = new Color(r, g, b);

        objectRenderer.material.color = randomColor;
    }

    void Update()
    {
        if (!isheldai)
        {
            HandlePlayerInput();
        }
       
    }

    void HandlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.R) & isheld)
        {
            switch (ammotype)
            {
                case "small":
                    if (maxammo - ammo < inventory.smallammo)
                    {
                        inventory.decreasesmallammo(maxammo - ammo);
                        reload();
                    }
                    break;

                case "medium":
                    if (maxammo - ammo < inventory.mediumammo)
                    {
                        inventory.decreasemediumammo(maxammo - ammo);
                        reload();
                    }
                    break;

                case "big":
                    if (maxammo - ammo < inventory.bigammo)
                    {
                        inventory.decreasebigammo(maxammo - ammo);
                        reload();
                    }
                    break;
            }
        }

        if (Input.GetMouseButton(0) && fireCooldown <= 0f & isheld & ammo > 0 & !reloading & type == "auto" || type == "semi" & Input.GetMouseButtonDown(0) && fireCooldown <= 0f & isheld & ammo > 0 & !reloading)
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

        if (Input.GetMouseButton(0) & isheld)
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

    void reload()
    {
        ammo = maxammo;
        reloading = true;
        StartCoroutine(reloadwait());
    }

    IEnumerator reloadwait()
    {
        yield return new WaitForSeconds(3);
        reloading = false;
    }

    

    public void Shoot()
    {
        if (fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = fireRate;
            currentSpreadAngle += spreadIncreasePerShot;
            if (currentSpreadAngle > maxSpreadAngle)
            {
                currentSpreadAngle = maxSpreadAngle;
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
}
