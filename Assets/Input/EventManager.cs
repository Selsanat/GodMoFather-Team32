using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("Input Actions")]
    public InputAction moveRight;
    public InputAction MoveLeft;
    public InputAction MoveUp;
    public InputAction MoveDown;
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
        moveRight.Enable();
        MoveLeft.Enable();

        radioFrequencyRight.Enable();
        radioFrequencyLeft.Enable();

        moveRight.performed += OnMoveRight;
        moveRight.canceled += OnMoveRight;

        MoveLeft.performed += OnMoveLeft;
        MoveLeft.canceled += OnMoveLeft;

        radioFrequencyRight.performed += OnRadioFrequencyRight;
        radioFrequencyLeft.performed += OnRadioFrequencyLeft;
    }

    private void OnDisable()
    {
        moveRight.Disable();
        MoveLeft.Disable();

        radioFrequencyRight.Disable();
        radioFrequencyLeft.Disable();

        moveRight.performed -= OnMoveRight;
        moveRight.canceled -= OnMoveRight;

        MoveLeft.performed -= OnMoveLeft;
        MoveLeft.canceled -= OnMoveLeft;

        radioFrequencyRight.performed -= OnRadioFrequencyRight;
        radioFrequencyLeft.performed -= OnRadioFrequencyLeft;
    }

    #region input Callbacks

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();

            if (input.x > 0.1f)
            {
                onMove?.Invoke(Vector2.right);
                Debug.Log(input + "valeur");
                Debug.Log("Move Right performed");            }
        }
        else if (context.canceled)
        {
            Debug.Log("Move Right canceled");
            onMove?.Invoke(Vector2.zero);
        }

    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();

            if (input.x < -0.1f)
            {
                onMove?.Invoke(Vector2.left);
                Debug.Log(input + "valeur");
                Debug.Log("Move Left performed");
            }    
        }
        else if (context.canceled)
        {
            Debug.Log("Move Left canceled");
            onMove?.Invoke(Vector2.zero);
        }
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();

            if (input.y > 0.1f)
            {
                onMove?.Invoke(Vector2.up);
                Debug.Log(input + "valeur");
                Debug.Log("Move Up performed");
            }
        }
        else if (context.canceled)
        {
            Debug.Log("Move Up canceled");
            onMove?.Invoke(Vector2.zero);
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();

            if (input.y < -0.1f)
            {
                onMove?.Invoke(Vector2.down);
                Debug.Log(input + "valeur");
                Debug.Log("Move Down performed");
            }
        }
        else if (context.canceled)
        {
            Debug.Log("Move Down canceled");
            onMove?.Invoke(Vector2.zero);
        }
    }

    public void OnRadioFrequencyRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Radio Frequency Right performed");
            onRadioFrequencyRight?.Invoke();
        }
        else if (context.canceled)
        {
            Debug.Log("Radio Frequency Right canceled");
        }   
    }

    public void OnRadioFrequencyLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Radio Frequency Left performed");
            onRadioFrequencyLeft?.Invoke();
        }
        else if (context.canceled)
        {
            Debug.Log("Radio Frequency Left canceled");
        }
    }
    #endregion
}
