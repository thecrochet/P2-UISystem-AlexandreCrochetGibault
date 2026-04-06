
using UnityEngine;
using UnityEngine.InputSystem;

// Toggles Inventory UI
// Routes  Input System maps for UI navigation
public class UIInputManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;

    private PlayerInputActions inputActions;

    private bool isOpen = false;

    private void Awake()
    {
        // create generated input actions instance
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        //enables the input system
        // Subscribe to the ToggleMenu action
        inputActions.Enable();
        inputActions.Player.ToggleMenu.performed += OnOpenMenu;
        Debug.Log("UIInputManager: subscribed to OpenMenu");
    }

    private void OnDisable()
    {
        //unsubscribe and disable input system
        inputActions.Player.ToggleMenu.performed -= OnOpenMenu;
        inputActions.Disable();
        Debug.Log("UIInputManager: unsubscribed and disabled inputActions");
    }

    // called when ToggleMenu action is performed
    private void OnOpenMenu(InputAction.CallbackContext ctx)
    {
        Debug.Log($"UIInputManager: OnOpenMenu called. isOpen={isOpen}");
        ToggleInventory();
    }

    // Toggles inventory but also  cursor visibility so the player can click UI
    private void ToggleInventory()
    {
        isOpen = !isOpen;

        if (inventoryUI != null)
            inventoryUI.SetActive(isOpen);

        if (isOpen)
        {
            // Keep OpenMenu enabled so subsequent presses still fire
            inputActions.Player.ToggleMenu.Enable();

            // Enable UI map for navigation but do not disable Player map
            inputActions.UI.Enable();

            // Refresh inventory UI after enabling it so it shows the most recent items
            var invUI = inventoryUI != null ? inventoryUI.GetComponent<InventoryUI>() : null;
            invUI?.Refresh();
            // show cursor and pause time
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;

            Debug.Log("UIInputManager: Inventory opened");
        }
        else
        {
            // Back to gameplay: disable UI map
            inputActions.UI.Disable();
            // hide cursor and unpause
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;

            Debug.Log("UIInputManager: Inventory closed");
        }
    }
}