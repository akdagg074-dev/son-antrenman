using UnityEngine;

// Cukurun dibindeki trigger: icine dusen kureyi sayar.
public class Hole : MonoBehaviour
{
    public GameObject goalEffect;   // gol olunca cikan konfeti efekti
    public AudioClip goalSound;     // gol sesi

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            GameManager.instance.BallScored();

            // efekt kalenin ortasinda, ses kameranin yaninda (tam ses duyulsun)
            if (goalEffect != null) Instantiate(goalEffect, transform.parent.position + Vector3.up * 0.4f, Quaternion.identity);
            if (goalSound != null) AudioSource.PlayClipAtPoint(goalSound, Camera.main.transform.position);
        }
    }
}
