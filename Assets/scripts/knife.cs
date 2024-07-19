using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public Transform player;
    public PlayerMovement pMov;
    public float rotationSpeed = 500f;
    public float slashAngle = 90f;

    private bool isSlashing = false;
    private float startRotation;
    private float endRotation;
    private float t;

    void Update()
    {
        FollowPlayer();

        if (Input.GetKeyDown(KeyCode.Z) && !isSlashing & pMov.currentStamina >= 20)
        {
            pMov.currentStamina -= 20;
            isSlashing = true;
            startRotation = transform.eulerAngles.z;
            endRotation = startRotation + slashAngle;
            t = 0f;
        }

        if (isSlashing)
        {
            t += Time.deltaTime * rotationSpeed / slashAngle;
            float angle = Mathf.Lerp(startRotation, endRotation, t);
            transform.eulerAngles = new Vector3(0, 0, angle);

            if (t >= 1f)
            {
                isSlashing = false;
                transform.eulerAngles = new Vector3(0, 0, startRotation);
            }
        }
    }

    void FollowPlayer()
    {
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}
