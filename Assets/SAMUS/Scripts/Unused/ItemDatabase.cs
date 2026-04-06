/*using System.Collections.Generic;
using UnityEngine;

// lookup for ScriptableObjects used
// Runtime Dictionary for fast lookups
// Singleton so other systems can access the database via Instance.
public class ItemDatabase : MonoBehaviour
{
    [Header("Assign ItemData assets here")]
    [SerializeField] private ItemData HealthPotion;
    [SerializeField] private ItemData MaxiPotion;
    [SerializeField] private ItemData PoisonPotion;
    [SerializeField] private ItemData Ammo;
    [SerializeField] private ItemData Elixir;

    private Dictionary<ItemType, ItemData> m_items;

    //Singleton instance for access
    public static ItemDatabase Instance { get; private set; }

    private void Awake()
    {
        //enforce single instance
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of ItemDatabase found! Destroying the new one.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Build runtime lookup
        BuildDatabase();
    }

    // Build dictionary from the inspector assigned ScriptableObjects
    private void BuildDatabase()
    {
        m_items = new Dictionary<ItemType, ItemData>
        {
            { ItemType.HealthPotion, HealthPotion },   
            { ItemType.Ammo, Ammo   },
            { ItemType.MaxiPotion, MaxiPotion },
            { ItemType.PoisonPotion, PoisonPotion },
            { ItemType.ELIXIR, Elixir   }
        };
    }

    // Try to get the ItemData for the given ItemType
    public bool TryGetItemData(ItemType type, out ItemData itemData)
    {
        return m_items.TryGetValue(type, out itemData);
    }

    // Returns true if database contains the given ItemType
    public bool ContainsItemType(ItemType type)
    {
        return m_items.ContainsKey(type);
    }
}*/
