
//used by ItemData and gameplay logic
//Values are asset data and used at runtime
// stored on ScriptableObjects
public enum ItemType
{
    //no item
    None,
    // Restores a fixed amount of health when applied
    HealthPotion,
    // Represents ammo pickups
    Ammo,
    //replenishes health and may apply size bonus
    MaxiPotion,
    //damages player and may apply a negative size
    PoisonPotion,
    // test for ALL powerful potion
    ELIXIR
}