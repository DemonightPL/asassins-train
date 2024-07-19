using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickingup : MonoBehaviour
{
    public bool canpickup;
    public GameObject currentPickup;
    public GameObject possiblePickup;
    private  GameObject scroll;
    public GameObject secondary;
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

         if (Input.GetKeyDown(KeyCode.Q) & handsfull)
        {
           swap();
            
            
        }

        if(secondary !=null)
        {
            secondary.transform.position = transform.position;
            secondary.transform.rotation = transform.rotation;
        }
    }

    void drop()
    {
        currentPickup.GetComponent<Gun>().isheld = false;
        handsfull = false;
        currentPickup.GetComponent<Collider2D>().enabled = true;
    }

    void take()
    {
        handsfull = true;
        currentPickup = possiblePickup.gameObject;
        currentPickup.GetComponent<Gun>().isheld = true;
        
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
      void swap()
      {
             currentPickup.GetComponent<Gun>().isheld = false;
           scroll = currentPickup;
            currentPickup = secondary;
            secondary = scroll;
            scroll = null;
            
            handsfull = false;
      }

      
}
