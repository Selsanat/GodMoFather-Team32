using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    void Start()
    {

    }
    void Update()
    {

    }

    public void Move(Vector2 direction)
    {
        Debug.Log("Moving: " + direction);
    }

    public void Interact()
    {
        Debug.Log("Interacting");
    }
}
