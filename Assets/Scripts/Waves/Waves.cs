using UnityEngine;

public class Waves : MonoBehaviour
{
    LineRenderer _lineRenderer;
    public int points;
    [Range(0f,1.85f)]public float amplitude = 1;
    public float frequency = 1;
    public Vector2 xLimits = new Vector2(0, 1);
    public float movementSpeed = 1;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Draw()
    {
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xEnd = xLimits.y;

        _lineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float t = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xEnd, t);
            float y = amplitude*Mathf.Sin((Tau*frequency*x) + (Time.timeSinceLevelLoad*movementSpeed));
            _lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    private void Update()
    {
        Draw();
    }
}
