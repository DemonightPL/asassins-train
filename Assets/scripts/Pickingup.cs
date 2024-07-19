using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickingup : MonoBehaviour
{
    public bool canpickup;
    public GameObject currentPickup;
    public GameObject possiblePickup;
    public GameObject scroll;
    public GameObject secondary;
    public bool handsfull;
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (handsfull)
            {
                drop();
            }
            else if (canpickup)
            {
                take();
            }
        }
        
        if (handsfull)
        {
            currentPickup.transform.position = transform.position;
            currentPickup.transform.rotation = transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            swap();
        }

        if (secondary != null)
        {
            secondary.transform.position = player.transform.position;
            secondary.transform.rotation = player.transform.rotation;
        }
    }

    void drop()
    {
        currentPickup.GetComponent<Gun>().isheld = false;
        handsfull = false;
        currentPickup.GetComponent<Collider2D>().enabled = true;
        currentPickup = null;
    }

    void take()
    {
        if (handsfull && secondary == null)
        {
            secondary = possiblePickup.gameObject;
            secondary.GetComponent<Gun>().isheld = true;
            secondary.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            handsfull = true;
            currentPickup = possiblePickup.gameObject;
            currentPickup.GetComponent<Gun>().isheld = true;
            currentPickup.GetComponent<Collider2D>().enabled = false;
        }
        canpickup = false;
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
        if (other.CompareTag("Weapon"))
        {
            canpickup = false;
            possiblePickup = null;
        }
    }

    void swap()
    {
        if (currentPickup != null && secondary != null)
        {
            currentPickup.GetComponent<Gun>().isheld = false;
            scroll = currentPickup;
            currentPickup = secondary;
            secondary = scroll;
            scroll = null;

            if (currentPickup == null)
            {
                handsfull = false;
            }
            else
            {
                currentPickup.GetComponent<Gun>().isheld = true;
                currentPickup.GetComponent<Collider2D>().enabled = false;
            }

            if (secondary != null)
            {
                secondary.GetComponent<Collider2D>().enabled = true;
            }
        }
        else if (currentPickup != null && secondary == null)
        {
            secondary = currentPickup;
            secondary.GetComponent<Gun>().isheld = false;
            currentPickup = null;
            handsfull = false;
        }
        else if (currentPickup == null && secondary != null)
        {
            currentPickup = secondary;
            currentPickup.GetComponent<Gun>().isheld = true;
            secondary = null;
            handsfull = true;
        }
    }
}
