using UnityEngine;
using UnityEngine.UI;


// Subscribe to GameManger C# event
// C# Events are type-safe and efficient for runtime UI update

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider m_HealthSlider;
    

    private void Awake ()
    {
        // Cache Slider reference
        if (m_HealthSlider == null && !TryGetComponent(out m_HealthSlider))
        {
            Debug.LogError($"HealthBarUI on {gameObject.name} requires a Slider component.");
            enabled = false;
            return;
        }

        // Configure slider as normalized and non-interactable
        m_HealthSlider.value = 1f; // starting value
        m_HealthSlider.minValue = 0f;
        m_HealthSlider.maxValue = 1f; 
        m_HealthSlider.interactable = false;

        }

    private void OnEnable()
    {
        //Subscribe to health change events
        if (GameManager.Instance != null)
            GameManager.Instance.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        if (GameManager.Instance != null)
            GameManager.Instance.OnHealthChanged -= UpdateHealthBar;
    }

    // Receives normalized health (0-1) from GameManager and update slider
    void UpdateHealthBar(float normalizedHealth)
    {
        m_HealthSlider.value = normalizedHealth;
    }
}
