
using TMPro;
using UnityEngine;

// Subscribe to GameManger C# event
public class AmmoCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private string prefix = "Ammo: ";

    private void Awake()
    {
        if (ammoText == null)
        {
            if (TryGetComponent(out TMP_Text found))
            {
                ammoText = found;
            }
            else
            {
                Debug.LogWarning($"AmmoCounterUI on {gameObject.name} has no TMP_Text reference. Assign one in the inspector.", this);
            }
        }
    }

    private void OnEnable()
    {
        // Subscribe to GameManager's initial broadcast
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnAmmoChanged += UpdateAmmoText;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        if (GameManager.Instance != null)
            GameManager.Instance.OnAmmoChanged -= UpdateAmmoText;
    }

    // Update ammo text UI
    private void UpdateAmmoText(int newAmmoCount)
    {
        if (ammoText == null) return;
        ammoText.text = prefix + newAmmoCount;
    }
}