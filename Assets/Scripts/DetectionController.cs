using UnityEngine;

public class DetectionController : MonoBehaviour
{
    CircleCollider2D _CircleCollider2D;
    void Start()
    {
        _CircleCollider2D = GetComponent<CircleCollider2D>();
        if (_CircleCollider2D == null)
        {
            Debug.LogError("CircleCollider2D component not found on " + gameObject.name);
        }
    }

    private void Update()
    {
        // Make simple and quick movement zqsd with the attached rigidbody2D
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY);
        GetComponent<Rigidbody2D>().linearVelocity = movement * 5f;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BuildingController bc = collision.GetComponent<BuildingController>();
        if (bc != null)
        {
            bc.OnCloseEnough();
        }
    }
}
