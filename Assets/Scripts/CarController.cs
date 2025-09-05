using System.Buffers;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [HideInInspector] public SplineAnimate SplineAnimate;
    private InputAction dpadAction;

    private void Start()
    {
        SplineAnimate = GetComponent<SplineAnimate>();
        RoadManager.Instance.PlayerCar = this;

        RoadManager.Instance.playerInput = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Intersection>() != null)
        {
            collision.GetComponent<Intersection>().OnCollidedWithPlayer(gameObject);
        }
    }
}
