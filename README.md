# P2-UISystem-AlexandreCrochetGibault

Dawson IVGD - Scripting 2 - Assignment 2 - 

Alexandre Crochet Gibault 

Id :2545874



DESCRIPTION

A small 3D demo scene that demonstrates a complete UI system:

\- Title/Menu screen with Start / Settings / Quit.

\- In‑game HUD with Health (slider), Ammo counter and a Count‑down Timer (only visible when potion used).

\- Slot-based collectible inventory using Scriptable Objects(slots fill when items are picked up; items can be used from the inventory).



CONTROLS

Keyboard/Mouse - Gamepad

Run : Shift - Left Trigger

Move : Arrows/WASD - Left Joystick

Jump : Spacebar - A button (south button)

Open Menu : Tab - Select

In-Menu Move : Mouse

Select : left click - A button



PLAY

Open SAMUS folder in Assets

Open Scenes Folder

Run MainMenu Scene and click Start (Opens Metroid Scene)

Pick up items (collide with pickup prefabs) — they appear in inventory slots.

Open inventory

Use slots with mouse



ITEM TYPES

Health Potion - RED CUBE : Gain life sometimes applied on pick up (red spheres)

Bullets - BLUE CUBE: Ammunition clipped to be used

MaxiPotion - Yellow Cube : Power up that makes you bigger and replenish life when used

Poison Potion - Green Cube : Reduce character size and health

Elixir - Samus Weapon : Test to implement a lot at the same time

* ItemData` is a ScriptableObject with \[CreateAssetMenu] so you can create item assets via right-click.



LIMITATIONS

* no shooting mechanic yet
* No game/level
* Some bugs ( like when you grow bigger sometimes you'll be under the ground)
* Wanted to implement also moving through menu with controller/keyboard but didn't have time

NB : This project is intentionally a UI-focused demo — gameplay is placeholder (pickups, simple player interactions) so the UI and event wiring are the focus.



SYSTEMS



* Collectible Inventory (slot-based). 

Pickups create ItemData ScriptableObjects that are stored in `PlayerInventory` and displayed in the Inventory UI. Some items may optionally apply immediately on pickup (configurable per pickup).

The Slots appear as you collect item (maxed to 10) but it's not truly a collectible inventory more a backpack inventory



* Events

Primary runtime signalling uses C# events (e.g. `GameManager.OnHealthChanged`, `GameManager.OnAmmoChanged`, `PlayerInventory.OnInventoryChanged`, `GameTimer.OnTimerChanged`).

Rationale: C# events are type-safe, fast, and easy to subscribe/unsubscribe in code which reduces coupling and prevents leaks in complex scenes.

UnityEvent is available in some inspector fields where designer-driven wiring is convenient

