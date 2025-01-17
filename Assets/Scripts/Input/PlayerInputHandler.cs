using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Iput Action Asset")]
    [SerializeField] private InputActionAsset playerInput;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name Reference")]
    [SerializeField] private string moveActionName = "Move";
    [SerializeField] private string lookActionName = "Look";
    [SerializeField] private string dashActionName = "Dash";
    [SerializeField] private string fireActionName = "Fire";
    [SerializeField] private string reloadActionName = "Reload";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction dashAction;
    public InputAction fireAction;
    public InputAction reloadAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool FireInput { get; private set; }
    public bool FireInputPressed { get; private set; }
    public bool ReloadInput { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        moveAction = playerInput.FindActionMap(actionMapName).FindAction(moveActionName);
        lookAction = playerInput.FindActionMap(actionMapName).FindAction(lookActionName);
        dashAction = playerInput.FindActionMap(actionMapName).FindAction(dashActionName);
        fireAction = playerInput.FindActionMap(actionMapName).FindAction(fireActionName);
        reloadAction = playerInput.FindActionMap(actionMapName).FindAction(reloadActionName);
        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        dashAction.performed += context => DashInput = true;
        dashAction.canceled += context => DashInput = false;
        
        fireAction.started += context => FireInputPressed = true;
        fireAction.performed += context => FireInputPressed = false;
        fireAction.canceled += context => FireInputPressed = false;

        fireAction.performed += context => FireInput = true;
        fireAction.canceled += context => FireInput = false;

        reloadAction.started += context => ReloadInput = true;
        reloadAction.performed += context => ReloadInput = false;
        reloadAction.canceled += context => ReloadInput = false;
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        dashAction.Enable();
        fireAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        dashAction.Disable();
        fireAction.Disable();
    }
}
