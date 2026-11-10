using UnityEngine;
using UnityEngine.InputSystem;

// Space tusu ile karakterin onundeki topa sut ceker.
public class ShootBall : MonoBehaviour
{
    public float kickRange = 1.6f;    // karakterin onunde bu mesafedeki top vurulur
    public float kickPower = 5f;      // ileri itme kuvveti
    public float kickLift = 0.8f;     // topu hafifce havalandirir
    public float cooldown = 0.4f;
    public AudioClip kickSound;

    private float nextKickTime;

    void Update()
    {
        if (Time.timeScale == 0f) return; // menu / duraklatma ekraninda sut yok
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Time.time >= nextKickTime)
            Shoot();
    }

    public void Shoot()
    {
        // Karakterin onundeki en yakin topu bul
        Collider[] hits = Physics.OverlapSphere(transform.position, kickRange, LayerMask.GetMask("Ball"));
        Rigidbody ball = null;
        float bestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            Vector3 toBall = hit.transform.position - transform.position;
            toBall.y = 0f;
            if (Vector3.Dot(transform.forward, toBall.normalized) < 0.3f) continue; // arkadaki toplari vurma
            if (toBall.magnitude < bestDistance)
            {
                bestDistance = toBall.magnitude;
                ball = hit.attachedRigidbody;
            }
        }

        if (ball == null) return;

        ball.AddForce(transform.forward * kickPower + Vector3.up * kickLift, ForceMode.Impulse);
        nextKickTime = Time.time + cooldown;
        if (kickSound != null)
            AudioSource.PlayClipAtPoint(kickSound, Camera.main.transform.position, 0.8f);
    }
}
