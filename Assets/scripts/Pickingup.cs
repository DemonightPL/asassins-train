using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickingup : MonoBehaviour
{
    private bool canpickup;
    private GameObject currentPickup;
    private GameObject possiblePickup;
    private bool handsfull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) & canpickup)
        {
           
            take();
            
        }
        else if(Input.GetKey(KeyCode.E) & handsfull)
        {
            drop();
        }
        
        if(handsfull)
        {
            currentPickup.transform.position = transform.position;
        }
    }

    void drop()
    {
        handsfull = false;
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
