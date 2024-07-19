using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickingup : MonoBehaviour
{
    public bool canpickup;
    public GameObject currentPickup;
    public GameObject possiblePickup;
    public bool handsfull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) & handsfull)
        {
           
            drop();
            
        }
        else if(Input.GetKeyDown(KeyCode.E) & canpickup)
        {
            take();
        }
        
        if(handsfull)
        {
            currentPickup.transform.position = transform.position;
            currentPickup.transform.rotation = transform.rotation;
        }
    }

    void drop()
    {
        handsfull = false;
        currentPickup.GetComponent<Collider2D>().enabled = true;
    }

    void take()
    {
        handsfull = true;
        currentPickup = possiblePickup.gameObject;
        
    }

      void OnTriggerEnter2D(Collider2D other)
      {
        if (other.CompareTag("Weapon"))
        {
            canpickup = true;
            possiblePickup = other.gameObject;  
        }
      }

      void OnTriggerExit2D(Collider2D other)
      {
        canpickup = false;
      }
}
