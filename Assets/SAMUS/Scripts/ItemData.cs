using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{

    // ScriptableObject asset: stores item definition data only.
    // Runtime code reads this data, but never modifies the asset itself.

    [SerializeField] private ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int value;


    


    [Header("Effect values")]
    [SerializeField] private float healthAmount = 20f;    // for HealthPotion
    [SerializeField] private int ammoAmount = 5;          // for Ammo
    [SerializeField] private float sizeMultiplier = 1f;   // 0.75 = shrink, 1.5 = grow
    [SerializeField] private float effectDuration = 15f;  // seconds for size effects


    public ItemType Type() { return type; }
    public string ItemName() { return itemName; }
    public Sprite Icon() { return icon; }
    public int Value() { return value; }



    public void Apply(GameManager gm)
    {
        if (gm == null)
        {
            Debug.LogWarning($"ItemData.Apply called with null GameManager on item '{name}'");
            return;
        }

        switch (type)
        {
            case ItemType.HealthPotion:
                gm.Heal(healthAmount);
                Debug.Log($"Applied {name}: Heal {healthAmount}");
                break;

            case ItemType.Ammo:
                gm.AddAmmo(ammoAmount);
                Debug.Log($"Applied {name}: +{ammoAmount} ammo");
                break;

            case ItemType.PoisonPotion:
                // implement once ennemies for now negative health
                // gm.TakeDamage(30f);
                gm.Heal(healthAmount);
                gm.ModifySize(sizeMultiplier, effectDuration); // shrink to 25% for durationif (effectDuration > 0f)
                gm.StartEffectTimer(effectDuration);
                Debug.Log($"Applied {name}: Damage 30 + shrink to 25% for {effectDuration}s");
                break;

            case ItemType.MaxiPotion:
                gm.SetToMaxHealth();
                gm.ModifySize(sizeMultiplier, effectDuration); // grow to 150% for durationif (effectDuration > 0f)
                gm.StartEffectTimer(effectDuration);
                Debug.Log($"Applied {name}: Replenish health + grow to 150% for {effectDuration}s");
                break;

                case ItemType.ELIXIR:
                    gm.Heal(healthAmount);
                    gm.AddAmmo(ammoAmount);
                    gm.ModifySize(sizeMultiplier, effectDuration);
                    gm.StartEffectTimer(effectDuration);
                    Debug.Log($"Applied {name}: Heal {healthAmount} + +{ammoAmount} ammo + grow to 150% for {effectDuration}s");
                    break;

            default:
                Debug.Log($"ItemData.Apply: No behavior for {type} on '{name}'");
                break;
        }
    }
}



