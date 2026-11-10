using UnityEngine;

// Sahada top kalmayinca, karakterin etrafinda en yakin topu gosteren bir ok cikarir.
public class BallPointer : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer arrow;
    public Vector2 pitchHalfSize = new Vector2(18f, 12f);  // saha cizgilerinin yari boyutu
    public float radius = 1.6f;                            // okun karaktere uzakligi

    void LateUpdate()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        Transform nearest = null;
        float best = Mathf.Infinity;
        bool anyOnPitch = false;

        foreach (GameObject b in balls)
        {
            Vector3 p = b.transform.position;
            if (Mathf.Abs(p.x) < pitchHalfSize.x && Mathf.Abs(p.z) < pitchHalfSize.y)
                anyOnPitch = true;

            float d = Vector3.Distance(player.position, p);
            if (d < best)
            {
                best = d;
                nearest = b.transform;
            }
        }

        // Sahada hala top varsa ya da en yakin top zaten dibimizdeyse ok gizli
        bool show = !anyOnPitch && nearest != null && best > 3f;
        arrow.enabled = show;
        if (!show) return;

        Vector3 dir = nearest.position - player.position;
        dir.y = 0f;
        dir.Normalize();

        transform.position = player.position + dir * radius + Vector3.up * 0.15f;
        float yaw = Mathf.Atan2(-dir.z, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f) * Quaternion.Euler(90f, 0f, 0f);
    }
}
