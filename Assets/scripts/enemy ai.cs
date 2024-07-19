using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAi : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float detectionDistance = 5f;
    public LayerMask obstacleMask;
    private float dir;
    private bool isAvoidingObstacle = false;
    public PlayerDetector detector;

    private bool lookforplayer;
    void Start()
    {
        StartCoroutine(MoveInRandomDirection());
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, detectionDistance, obstacleMask);
        Debug.DrawRay(transform.position, transform.up * detectionDistance, Color.red);

        if (hit.collider != null & lookforplayer)
        {
            if (!isAvoidingObstacle)
            {
                isAvoidingObstacle = true;
                StartCoroutine(RotateUntilClear());
            }
        }
        else
        {
            if (!isAvoidingObstacle & lookforplayer)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
        }

        if(detector.seeplayer)
        {
            lookforplayer = false;
        }
        else
        {
            lookforplayer = true;
        }
    }

    IEnumerator RotateUntilClear()
    {
        float randomDirection = Random.Range(0f, 2f);
        switch(randomDirection)
        {
            case > 1f:
            dir = 1f;
            break;
            case < 1f:
            dir = -1f;
            break;
        }
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, detectionDistance, obstacleMask);
            if (hit.collider == null)
            {
                transform.Rotate(Vector3.forward * Random.Range(15f, 150f) * dir); 
                yield return new WaitForSeconds(0.1f); 
                isAvoidingObstacle = false;
                break;
            }
            transform.Rotate(Vector3.forward * rotationSpeed * dir * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveInRandomDirection()
    {
        while (true)
        {
            float randomAngle = Random.Range(-90f, 90f);
            if(lookforplayer)
            {
            transform.Rotate(Vector3.forward * randomAngle);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
