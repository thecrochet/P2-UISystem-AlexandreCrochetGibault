using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Presents an ItemDama in a single slot
// Binds visuals and click input to callback
public class InventorySlotUI : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemText;
    [SerializeField] private GameObject highlight; // assign a

    // Runtime state
    private int _slotIndex = -1;
    private Action<int> _onUseCallback;

    private Button _button;

    // Called by InventoryUI when slot is instantiated or reused
    public void Initialize(int slotIndex, Action<int> onUseCallback)
    {
        _slotIndex = slotIndex;
        _onUseCallback = onUseCallback;

        // Wire the Button listener
        _button = GetComponent<Button>() ?? GetComponentInChildren<Button>();
        if (_button != null)
        {
            _button.onClick.RemoveAllListeners();
            int idx = slotIndex;
            _button.onClick.AddListener(() =>
            {
                Debug.Log($"InventorySlotUI: Button clicked for slot {idx}", this);
                _onUseCallback?.Invoke(idx);
            });
        }
        else
        {
            // No button still calls callback
            Debug.LogWarning("InventorySlotUI.Initialize: no Button found on slot prefab.", this);
        }
    }

    // Binds ItemData visuals to slot
    public void SetData(ItemData itemData)
    {
        if (itemData == null)
        {
            //Clear visuals for empty slot
            if (icon != null) icon.gameObject.SetActive(false);
            if (itemText != null) itemText.text = "";
            return;
        }

        if (icon != null)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = itemData.Icon();
        }

        if (itemText != null)
            itemText.text = itemData.ItemName();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (highlight != null) highlight.SetActive(true);

    }
    // Allow clicking via pointer if no Button is present
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_onUseCallback != null && eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log($"InventorySlotUI: Pointer clicked slot {_slotIndex}", this);
            _onUseCallback.Invoke(_slotIndex);
        }
    }
}