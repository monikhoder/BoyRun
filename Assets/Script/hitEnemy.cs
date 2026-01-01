using UnityEngine;

public class hitEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            FindAnyObjectByType<PlayerController>()?.TakeDamage();
            AudioManager.Instance.PlaySound("Hit");
            // Destroy coin
            Destroy(gameObject);
        }
    }



}
