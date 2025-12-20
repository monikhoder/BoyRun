using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Settings")]
    public float offsetFromLeft = 3f;
    public float cameraHeight = 0f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate camera width and height
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

         // Calculate target position
        float targetCamX = player.position.x + (width / 2f) - offsetFromLeft;

        // Apply the new position
        transform.position = new Vector3(targetCamX, cameraHeight, -10f);
    }
}