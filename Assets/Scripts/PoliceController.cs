using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class PoliceController : MonoBehaviour
{
    [HideInInspector] public SplineAnimate SplineAnimate;
    Transform playerTransform;
    SpriteRenderer spriteRenderer;
    public float DistanceToAppear = 3f;

    private void Start()
    {
        SplineAnimate = GetComponent<SplineAnimate>();
        RoadManager.Instance.PoliceCar = this;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Intersection>() != null)
        {
            collision.GetComponent<Intersection>().OnCollidedWithPolice(gameObject);
        }

        if (collision.CompareTag("GameOver") || collision.CompareTag("Player"))
        {
            GameOverController.Instance.TriggerGameOver();
        }
    }

    private void Update()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        print(distanceToPlayer);
        float targetAlpha = distanceToPlayer < DistanceToAppear ? 1f : 0f;
        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * 2f);
        spriteRenderer.color = color;
    }
}
