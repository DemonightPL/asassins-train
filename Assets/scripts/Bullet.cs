using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;
    public bool firedByPlayer;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("Weapon")  ||  other.gameObject.CompareTag("Knife") || firedByPlayer & other.gameObject.CompareTag("Player"))
        {
            return;
        }

        Destroy(gameObject);
    }
}
