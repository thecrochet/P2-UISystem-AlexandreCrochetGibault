using UnityEngine;
using UnityEngine.UI;

// Builds slot-based inventory UI from runtime data
// UI is decoupled from gameplay via  C# event on PlayerInventory (OnInventoryChanged)


public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotsParent;

    private void OnEnable()
    {
        // Subscribe to inventory change event
        _playerInventory.OnInventoryChanged += Refresh;

        //Ensure UI is populated when enabled
        Refresh();
    }

    private void OnDisable()
    {
        //Unsubscribe to avoir memory leaks
        _playerInventory.OnInventoryChanged -= Refresh;
    }

    private void Start()
    {

    }
    // Rebuilds Visual slots
    public void Refresh()
    {
        if (_playerInventory == null)
            return;

        // Clear previous slots (destroy removes GameObjects & listeners)
        foreach (Transform child in _slotsParent)
            Destroy(child.gameObject);

        var items = _playerInventory.HeldItems;
        if (items == null)
            return;

        for (int i = 0; i < items.Count; i++)
        {
            int idx = i;
            ItemData item = items[i];

            //Instantiate slot prefab unvder the parent
            GameObject slot = Instantiate(_slotPrefab, _slotsParent);

            //Populate visuals with InventorySlotUI
            InventorySlotUI slotUI = slot.GetComponent<InventorySlotUI>();
            if (slotUI != null)
            {
                slotUI.SetData(item);
                slotUI.Initialize(idx, OnSlotClicked);
            }
            else
            {
                Debug.LogError("InventoryUI: Slot prefab does not have an InventorySlotUI component.", this);
            }
        }
    }

            private void OnSlotClicked(int index)
    {
        Debug.Log($"InventoryUI: Slot clicked {index}");

        if (_playerInventory != null)
        {
            _playerInventory.UseAtIndex(index);
            
        }
    }
}










//OLD CODE KEEPING IN CASE I WANT TO REVERT BACK TO THIS APPROACH
// Wire the Button so clicks call UseAtIndex with the correct slot index
/*Button btn = slot.GetComponent<Button>() ?? slot.GetComponentInChildren<Button>();
if (btn != null)
{
    btn.onClick.RemoveAllListeners();
    btn.onClick.AddListener(() =>
    {        
            // Use the item and immediately refresh UI so the slot is removed
            _playerInventory.UseAtIndex(idx);
            Refresh();
    });
}
else
{
    Debug.LogWarning("InventoryUI: Slot prefab has no Button component (root or child). Click will do nothing.", slot);
}*/
