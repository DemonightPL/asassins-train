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

    void Start()
    {
        StartCoroutine(MoveInRandomDirection());
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, detectionDistance, obstacleMask);
        Debug.DrawRay(transform.position, transform.up * detectionDistance, Color.red);

        if (hit.collider != null)
        {
            if (!isAvoidingObstacle)
            {
                isAvoidingObstacle = true;
                StartCoroutine(RotateUntilClear());
            }
        }
        else
        {
            if (!isAvoidingObstacle)
            {
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
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
                StartCoroutine(wait());
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
            float randomAngle = Random.Range(-25f, 25f);
            transform.Rotate(Vector3.forward * randomAngle);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        isAvoidingObstacle = false;
    }
}
