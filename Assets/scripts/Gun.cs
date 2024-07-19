using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
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
    void Start()
    {
        ammo = maxammo;
        pl = GameObject.Find("Player");
        inventory = pl.GetComponent<inventory>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) & isheld )
        {

            
            switch(ammotype)
            {
            case "small":
            if(maxammo - ammo < inventory.smallammo)
            {
                inventory.decreasesmallammo(maxammo - ammo);
                reload();
               
            }
            
            
            break;

            case "medium":
             if(maxammo - ammo < inventory.mediumammo)
            {
            inventory.decreasemediumammo(maxammo - ammo);
            reload();
            }
            break;

            case "big":
             if(maxammo - ammo < inventory.bigammo)
            {
            inventory.decreasebigammo(maxammo - ammo);
            reload();
            
            }
            break;

            

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

        if (Input.GetMouseButton(0) && fireCooldown <= 0f & isheld & ammo > 0 & !reloading & type =="auto" || type=="semi" & Input.GetMouseButtonDown(0) && fireCooldown <= 0f & isheld & ammo > 0 & !reloading )
        {
            Fire();
            ammo -=1f;
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

    void Fire()
    {
        float angle = Random.Range(-currentSpreadAngle, currentSpreadAngle);
        Quaternion spread = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation * spread);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        bullet.GetComponent<Bullet>().firedByPlayer = true;
        
    }

    
}
