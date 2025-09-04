using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("Input Actions")]
    public InputAction moveAction;
    public InputAction interactAction;
    public InputAction radioFrequencyRight;
    public InputAction radioFrequencyLeft;

    [Header("Unity Events")]
    public UnityEvent<Vector2> onMove;
    public UnityEvent onInteract;
    public UnityEvent onRadioFrequencyRight;
    public UnityEvent onRadioFrequencyLeft;
    public UnityEvent onChosingPlayerPath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        interactAction.Enable();
        radioFrequencyRight.Enable();
        radioFrequencyLeft.Enable();

        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        interactAction.performed += OnInteractPerformed;
        radioFrequencyRight.performed += OnRadioFrequencyRight;
        radioFrequencyLeft.performed += OnRadioFrequencyLeft;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        interactAction.Disable();
        radioFrequencyRight.Disable();
        radioFrequencyLeft.Disable();

        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;
        interactAction.performed -= OnInteractPerformed;
        radioFrequencyRight.performed -= OnRadioFrequencyRight;
        radioFrequencyLeft.performed -= OnRadioFrequencyLeft;
    }

    #region input Callbacks

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        onMove?.Invoke(direction); 
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        onMove?.Invoke(Vector2.zero); 
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        onInteract?.Invoke(); 
    }

    private void OnRadioFrequencyRight(InputAction.CallbackContext context)
    {
        onRadioFrequencyRight?.Invoke(); 
    }

    private void OnRadioFrequencyLeft(InputAction.CallbackContext context)
    {
        onRadioFrequencyLeft?.Invoke(); 
    }
    #endregion
}
