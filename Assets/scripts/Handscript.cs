using UnityEngine;

public class Handscript : MonoBehaviour
{
    public Transform referenceObject;
    public float maxDistance = 5f;

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

        transform.position = referenceObject.position + direction;
    }
}
