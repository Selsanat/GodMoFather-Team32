using UnityEngine;
using UnityEngine.Splines;

public class PoliceController : MonoBehaviour
{
    [HideInInspector] public SplineAnimate SplineAnimate;

    private void Start()
    {
        SplineAnimate = GetComponent<SplineAnimate>();
        RoadManager.Instance.PoliceCar = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Intersection>() != null)
        {
            collision.GetComponent<Intersection>().OnCollidedWithPolice(gameObject);
        }
    }
}
