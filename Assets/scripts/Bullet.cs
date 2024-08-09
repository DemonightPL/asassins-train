using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;
    public bool firedByPlayer;
     public int damage = 10;
     private Health health;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("Weapon")  ||  other.gameObject.CompareTag("Knife") || firedByPlayer & other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Sound"))
        {
            return;
        }
        else
        {
            health = other.GetComponent<Collider2D>().gameObject.GetComponent<Health>();
        }

         if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
