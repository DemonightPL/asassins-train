using UnityEngine;

public class Handscript : MonoBehaviour
{
    public Transform referenceObject;
    
    public float maxDistance;
    public float minDistance;
     

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - referenceObject.position;
        float distance = direction.magnitude;

        if (distance > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }
        else if (minDistance > distance)
        {
            direction = direction.normalized * minDistance;
        }   

        transform.position = referenceObject.position + direction;
        
        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (distance <= 1f)
        {
            transform.rotation = referenceObject.rotation;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
