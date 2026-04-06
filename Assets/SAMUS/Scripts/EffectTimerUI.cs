using TMPro;
using UnityEngine;


// UI  listens to GameManager via C# events 
// preferred because type-safe, easy to unsubscribe
public class EffectTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;


    private void Awake()
    {
        // Hide inspector text at start until timer is active
        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }
    //
    private void Start()
    {

        Debug.Log("EffectTimerUI subscribed");


        if (GameManager.Instance != null)
            GameManager.Instance.OnEffectTimerChanged += UpdateTimer;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnEffectTimerChanged -= UpdateTimer;
    }

    //called by game manager.OnEffectTimerChanged event
    private void UpdateTimer(int seconds)
    {
      
        if (timerText == null) return;

        if (seconds > 0)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = $"Time: {seconds}s";
        }
        else
        {
            timerText.text = "";
            timerText.gameObject.SetActive(false);
        }
    }
}