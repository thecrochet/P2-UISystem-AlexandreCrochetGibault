using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Holds player states (health, ammo)
// exposes C# events for UI to subscribe
// Keeps gameplay logic decoupled from UI

public class GameManager : MonoBehaviour
{
    //Singleton pattern for easy access
    public static GameManager Instance { get; private set; }

    [Header("Player stats")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float startingAmmo = 10f;


    [Header("Player reference")]
    [SerializeField] private Transform playerTransform;

    private float m_CurrentHealth;
    private int m_AmmoCount;

    //size effect states
    private Vector3 m_originalScale;
    private Coroutine m_sizeRoutine;
    private Coroutine m_effectTimerRoutine;


    
    // Events used by UI/HUD
    public event Action<float> OnHealthChanged;  // normalized 0–1
    public event Action<int> OnAmmoChanged;
    public event Action<int> OnEffectTimerChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        
       {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //initialize player states
        m_CurrentHealth = maxHealth;
        m_AmmoCount = Mathf.RoundToInt(startingAmmo);
        // cache original player scale for size modifications
        m_originalScale = playerTransform != null ? playerTransform.localScale : Vector3.one;
    }

    private void Start()
    {
        // Broadcast initial values
        OnHealthChanged?.Invoke(m_CurrentHealth / maxHealth);
        OnAmmoChanged?.Invoke(m_AmmoCount);
    }

    // substracts health and boradcast update
    public void TakeDamage(float amount)
    {
        if (amount == 0) return;
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth - amount, 0f, maxHealth);
        OnHealthChanged?.Invoke(m_CurrentHealth / maxHealth);
    }

    // adds health and broadcasts update
    public void Heal(float amount)
    {
        if (amount == 0) return;
        m_CurrentHealth = Mathf.Clamp(m_CurrentHealth + amount, 0f, maxHealth);
        OnHealthChanged?.Invoke(m_CurrentHealth / maxHealth);
    }

    // sets health to max and broadcast update
    public void SetToMaxHealth()
    {
        m_CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(1f);
    }

    // Addsammo, fire OnAmmoChanged
    public void AddAmmo(int amount)
    {
        if (amount == 0) return;
        m_AmmoCount += amount;
        OnAmmoChanged?.Invoke(m_AmmoCount);
        Debug.Log($"Ammo count: {m_AmmoCount}");
    }

    // temporarly modify player size by multiplier for duration seconds.
    public void ModifySize(float multiplier, float duration)
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("ModifySize: playerTransform is null. Assign a Player transform in GameManager or tag your player 'Player'.");
            return;
        }
        // Stop existing size routine to prevent overlap
        if (m_sizeRoutine != null) 
            StopCoroutine(m_sizeRoutine);

        if (duration <= 0f)
        {
            playerTransform.localScale = m_originalScale * multiplier;
            m_sizeRoutine = null;
            return;
        }

        m_sizeRoutine = StartCoroutine(SizeCoroutine(multiplier, duration));
    }

    private IEnumerator SizeCoroutine(float multiplier, float duration)
    {
        playerTransform.localScale = m_originalScale * multiplier;
        yield return new WaitForSeconds(duration);
        playerTransform.localScale = m_originalScale;
        m_sizeRoutine = null;
    }

    // Starts visible countdown timer for active effects
    // UI subscribe to OnEffectTimerChanged to update display
    public void StartEffectTimer(float duration)
    {

        Debug.Log($"StartEffectTimer CALLED with duration: {duration}");

        if (m_effectTimerRoutine != null)
            StopCoroutine(m_effectTimerRoutine);

        // start the coroutine that raises the event each second
        m_effectTimerRoutine = StartCoroutine(EffectTimerCoroutine(duration));
    }

    private IEnumerator EffectTimerCoroutine(float duration)
    {
        float remaining = Mathf.Max(0f, duration);

        while (remaining > 0f)
        {

            Debug.Log($"Timer tick: {remaining}");
            int seconds = Mathf.CeilToInt(remaining);
            OnEffectTimerChanged?.Invoke(seconds);
            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }
        Debug.Log("Timer finished");
        // final update and hide
        OnEffectTimerChanged?.Invoke(0);
        m_effectTimerRoutine = null;
    }


    // for quick testing in editor
    [ContextMenu("Test: Take 10 Damage")]
    public void TestTakeDamage() => TakeDamage(10f);

    [ContextMenu("Test: Heal 20")]
    public void TestHeal() => Heal(20f);

    [ContextMenu("Test: Add Ammo")]
    public void TestAddAmmo() => AddAmmo(1);
    
    [ContextMenu("Test Timer")]
    
    public void TestTimer()
    {
        StartEffectTimer(5f);
    }

}