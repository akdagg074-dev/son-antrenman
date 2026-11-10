using UnityEngine;

// Tepe acili kamera: karakteri yukaridan, yumusak sekilde takip eder.
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 14f, -9f);
    public float smoothTime = 0.25f;

    // Ana menude kamera sahanin etrafinda yavasca doner
    public bool orbit;
    public Vector3 orbitCenter = Vector3.zero;
    public float orbitRadius = 26f;
    public float orbitHeight = 16f;
    public float orbitSpeed = 8f;

    private Vector3 velocity;
    private float orbitAngle;

    void Start()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    public void StopOrbit()
    {
        orbit = false;
        velocity = Vector3.zero;
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        if (orbit)
        {
            orbitAngle += orbitSpeed * Time.unscaledDeltaTime; // menude Time.timeScale = 0
            transform.position = orbitCenter + Quaternion.Euler(0f, orbitAngle, 0f) * new Vector3(0f, orbitHeight, -orbitRadius);
            transform.LookAt(orbitCenter);
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}
