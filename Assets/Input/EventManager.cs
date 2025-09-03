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

    [Header("Unity Event")]
    public UnityEvent<Vector2> onMove;
    public UnityEvent onInteract;
    public UnityEvent onRadioFrequencyRight;
    public UnityEvent onRadioFrequencyLeft;

    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 direction;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        rb.linearVelocity = direction * moveSpeed;
    }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void OnEnable()
    {
        moveAction.Enable();
        interactAction.Enable();
        radioFrequencyRight.Enable();
        radioFrequencyLeft.Enable();

        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;

        radioFrequencyRight.performed += OnRadioFrequencyRight;
        radioFrequencyLeft.performed += OnRadioFrequencyLeft;

        interactAction.performed += OnInteractPerformed;
    }


    public void OnDisable()
    {
        moveAction.Disable();
        interactAction.Disable();
        radioFrequencyRight.Disable();
        radioFrequencyLeft.Disable();

        moveAction.performed -= OnMovePerformed;
        moveAction.canceled -= OnMoveCanceled;

        radioFrequencyRight.performed -= OnRadioFrequencyRight;
        radioFrequencyLeft.performed -= OnRadioFrequencyLeft;

        interactAction.performed -= OnInteractPerformed;
    }

    #region Input Callbacks
    public void OnMovePerformed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log(direction);
            direction = context.ReadValue<Vector2>();
        }
        else
        {
            OnMoveCanceled(context);
        }
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            direction = Vector2.zero;
        }
    }

    public void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            
        }
    }

    public void OnRadioFrequencyRight(InputAction.CallbackContext context)
    {

    }

    public void OnRadioFrequencyLeft(InputAction.CallbackContext context)
    {

    }

    public void Pause()
    {

    }
    #endregion

}
