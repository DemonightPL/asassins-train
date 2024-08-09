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
    public GameObject player;
    public Vector2 directionToPlayer;
    private bool lookforplayer;
    public float angle;

    void Start()
    {
        StartCoroutine(MoveInRandomDirection());
    }

    void Update()
    {
        if (!lookforplayer)
        {
            LookAtPlayer();
        }
        else
        {
            
            HandleMovement();
        }

        DetectPlayers();

        if (detector.seeplayer)
        {
            lookforplayer = false;
        }
        else
        {
            lookforplayer = true;
        }
    }

    void HandleMovement()
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

    void LookAtPlayer()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator RotateUntilClear()
    {
        float randomDirection = Random.Range(0f, 2f);
        dir = randomDirection > 1f ? 1f : -1f;

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
            if (lookforplayer)
            {
                float randomAngle = Random.Range(-90f, 90f);
                transform.Rotate(Vector3.forward * randomAngle);
            }

            yield return new WaitForSeconds(2f);
        }
    }

    void DetectPlayers()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 20f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                directionToPlayer = (hit.transform.position - transform.position).normalized;
                angle = Vector2.Angle(transform.up, directionToPlayer);
            }
        }
    }
}
