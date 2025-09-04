using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //private void OnEnable()
    //{
    //    EventManager.instance.onMove.AddListener(OnMove);
    //    EventManager.instance.onInteract.AddListener(OnInteract);
    //}

    //private void OnDisable()
    //{
    //    EventManager.instance.onMove.RemoveListener(OnMove);
    //    EventManager.instance.onInteract.RemoveListener(OnInteract);
    //}

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(Vector2 direction)
    {
        EventManager.instance.onMove.Invoke(direction);

        moveInput = direction;
    }

}
