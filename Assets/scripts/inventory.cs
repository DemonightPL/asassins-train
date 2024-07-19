using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public float bigammo;
    public float smallammo;
    public float mediumammo;
   
   public void decreasesmallammo(float amount)
   {
    smallammo -= amount;

   }
    public void decreasebigammo(float amount)
   {
    bigammo -= amount;

   }
 public  void decreasemediumammo(float amount)
   {
    mediumammo -= amount;

   }
}
