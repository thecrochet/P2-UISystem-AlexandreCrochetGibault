
using UnityEngine;
// For gameobjects picked-up on world 
// applyOnPickup = true : immediately apply item effects
// applyOnPickup = false :store the ItemData in inventory
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData m_type;
    [SerializeField] private bool applyOnPickup = false;

    // When player collides with  pickup
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (m_type == null)
        {
            Debug.LogWarning($"ItemPickup '{name}' has no ItemData assigned.");
            return;
        }

        if (applyOnPickup)
        {
            // immediately apply item effects to player (consume)
            if (GameManager.Instance == null)
            {
                Debug.LogWarning("ItemPickup: GameManager.Instance is null - cannot apply item effects.");
                return;
            }

            m_type.Apply(GameManager.Instance);
            Debug.Log($"ItemPickup: Applied '{m_type.name}' to player.");
            Destroy(gameObject);
            return;
        }

        // otherwise : store in inventory
        var inventory = other.GetComponent<PlayerInventory>()
                        ?? other.GetComponentInParent<PlayerInventory>()
                        ?? other.GetComponentInChildren<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogWarning("ItemPickup: PlayerInventory not found on the player (cannot store item).");
            return;
        }

        bool picked = inventory.PickUp(m_type);
        Debug.Log($"ItemPickup: PickUp returned {picked} for '{m_type.name}'");
        if (picked) Destroy(gameObject);
    }
}