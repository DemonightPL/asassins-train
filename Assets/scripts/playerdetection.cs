using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public float detectionAngle = 45f;
    public float detectionDistance = 10f;
    public bool seeplayer;
    public bool canhear;
    

    void Update()
    {
        DetectPlayers();
       
    }

    void DetectPlayers()
    {
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionDistance);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Vector2 directionToPlayer = (hit.transform.position - transform.position).normalized;
                float angle = Vector2.Angle(transform.up, directionToPlayer);
                  
                if (angle < detectionAngle || canhear)
                {

                    
                    seeplayer = true;
                    
                    
                }
                else{
                    seeplayer = false;
                }
                
            }
            
           
        }
    }
    


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, detectionAngle / 2) * transform.up * detectionDistance;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -detectionAngle / 2) * transform.up * detectionDistance;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }

}
