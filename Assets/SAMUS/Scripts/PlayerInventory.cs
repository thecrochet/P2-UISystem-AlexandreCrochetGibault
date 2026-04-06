using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

// Stores ItemData instances at runtime
// Raises event OnInventoryChanged for subscribers
public class PlayerInventory : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private int _maxCapacity = 10;

    public event Action OnInventoryChanged;

    private List<ItemData> m_heldItems;

    // Pulic read-only accessors
    public int Count => m_heldItems.Count;
    public bool IsFull => m_heldItems.Count >= _maxCapacity;

    public List<ItemData> HeldItems => m_heldItems;

    private void Awake()
    {
        //cache reference to the list
        m_heldItems = new List<ItemData>();
    }

    // store given item to inventory
    // true when added, false if full
    // invoke Events so UI updates
    public bool PickUp(ItemData type)
    {
        if (IsFull)
        {
            Debug.Log("[Inventory] Full — cannot pick up more items.");
            return false;
        }

        m_heldItems.Add(type);
        OnInventoryChanged?.Invoke();   
        return true;
    }

    // Use and remove the first item of a given type
    public bool UseItem(ItemData type)
    {
        if (type == null)
        {
            Debug.LogWarning("[Inventory] UseItem called with null ItemData.");
            return false;
        }

        if (!m_heldItems.Contains(type))
        {
            Debug.Log($"[Inventory] {type.name} not in inventory.");
            return false;
        }

        m_heldItems.Remove(type);
        OnInventoryChanged?.Invoke();

        if (GameManager.Instance != null)
        {
            type.Apply(GameManager.Instance);
            Debug.Log($"[Inventory] Used and applied: {type.name}");
        }
        else
        {
            Debug.LogWarning("[Inventory] Used item but GameManager.Instance is null — effects not applied.");
        }

        return true;
    }

    // use and remove item at given index
    public bool UseAtIndex(int index)
    {
        if (index < 0 || index >= m_heldItems.Count)
            return false;

        ItemData item = m_heldItems[index];
        m_heldItems.RemoveAt(index);
        // notify listeners before applying effects
        OnInventoryChanged?.Invoke(); 

        if (GameManager.Instance != null)
            item.Apply(GameManager.Instance);

        return true;
    }

    // Use 1st item in the inventory 
    [ContextMenu("Use first item")]
    public bool UseFirst()
    {
    
        if (m_heldItems.Count == 0)
        {
            Debug.Log("[Inventory] Nothing to use.");
            return false;
        }

        return UseAtIndex(0);
    }

    // returns true if inventory contains the ItemData reference
    public bool Has(ItemData type) => m_heldItems.Contains(type);

    // debug helper : logs items in console
    public void LogContents()
    {
        if (m_heldItems.Count == 0)
        {
            Debug.Log("[Inventory] Empty.");
            return;
        }

        foreach (ItemData item in m_heldItems)
            Debug.Log($"  - {item}");
    }
}