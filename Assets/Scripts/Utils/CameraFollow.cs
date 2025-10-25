using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Smooth Settings")]
    public float smoothTime = 0.2f;

    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(0, 0, -10);

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
