using UnityEngine;

public class Camerafollower : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float minDistance = 2f;
    public float minDistanceX = 2f;
    public float sizeChangeSpeed = 2f;
    public float maxSize = 10f;
    public float minSize = 5f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Follow player up and down
        if (Mathf.Abs(player.position.y - transform.position.y) > minDistance)
        {
            Vector3 newPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, speed * Time.deltaTime);
        }

        // Adjust camera size when player moves left or right
        if (Mathf.Abs(player.position.x - transform.position.x) > minDistanceX)
        {
            float targetSize = Mathf.Clamp(cam.orthographicSize + sizeChangeSpeed * Time.deltaTime, minSize, maxSize);
            cam.orthographicSize = targetSize;
        }
        else
        {
            float targetSize = Mathf.Clamp(cam.orthographicSize - sizeChangeSpeed * Time.deltaTime, minSize, maxSize);
            cam.orthographicSize = targetSize;
        }
    }
}
