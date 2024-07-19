using UnityEngine;

public class Sound : MonoBehaviour
{
    
    public GameObject referenceObject;  
    public float scaleMultiplier = 1.0f;  

    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private float speed;

    void Start()
    {
        

        previousPosition = referenceObject.transform.position;
    }

    void FixedUpdate()
    {
        currentPosition = referenceObject.transform.position;
        speed = (currentPosition - previousPosition).magnitude / Time.deltaTime;

        float scalingFactor = speed * scaleMultiplier;
        gameObject.transform.localScale = new Vector3(scalingFactor, scalingFactor, scalingFactor);

        previousPosition = currentPosition;
    }
}
