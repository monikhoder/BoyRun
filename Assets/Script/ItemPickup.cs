using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           // Debug.Log("Item picked up by the player.");


            FindAnyObjectByType<UIManager>()?.RefreshCoin();
            AudioManager.Instance.PlaySound("Item");
           

            // Destroy coin
            Destroy(gameObject);
        }
    }
}