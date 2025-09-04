using UnityEngine;
using UnityEngine.Splines;
using NaughtyAttributes;

public class RoadManager : MonoBehaviour
{
    public static RoadManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [HideInInspector] public CarController PlayerCar;
    [HideInInspector] public bool WaitForInput = false;
    [HideInInspector] public Intersection CurrentIntersection;
    public void MakePlayerFollowSpline(SplineContainer splineContainer)
    {
        PlayerCar.SplineAnimate.Container = splineContainer;
        PlayerCar.SplineAnimate.Restart(false);
        PlayerCar.SplineAnimate.Play();
    }

    [Button("Go Up")]
    public void GoUp()
    {
        MakePlayerFollowSpline(CurrentIntersection.Up.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersection = null;
    }
    [Button("Go Left")]
    public void GoLeft()
    {
        MakePlayerFollowSpline(CurrentIntersection.Left.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersection = null;
    }
    [Button("Go Right")]
    public void GoRight()
    {
        MakePlayerFollowSpline(CurrentIntersection.Right.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersection = null;
    }
    [Button("Go Down")]
    public void GoDown()
    {
        MakePlayerFollowSpline(CurrentIntersection.Down.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersection = null;
    }
}
