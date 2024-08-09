using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 100;
    public GameObject gun;
   

   
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("HP: " + hp);

        
        if (hp <= 0)
        {
            Instantiate(gun, transform.position, transform.rotation);
            Destroy(gameObject);
            
        }
    }
}
