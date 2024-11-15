using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    public float smoothSpeed = 3f;

    public float xOffset = 2f;
    public float yOffset = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("Can't find player");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float targetX = player.transform.position.x + xOffset;
        float targetY = player.transform.position.y + yOffset;

        float currentX = transform.position.x;
        float currentY = transform.position.y;

        float newX = Mathf.Lerp(currentX, targetX, smoothSpeed * Time.deltaTime);
        float newY = Mathf.Lerp(currentY, targetY, smoothSpeed * Time.deltaTime);

        transform.position = new Vector3(newX, newY, -10);
    }
}