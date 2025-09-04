using UnityEngine;
using UnityEngine.Splines;
using NaughtyAttributes;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

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
    [HideInInspector] public PoliceController PoliceCar;
    [HideInInspector] public bool WaitForInput = false;
    [HideInInspector] public Dictionary<GameObject, Intersection> CurrentIntersections = new Dictionary<GameObject, Intersection>();
    public void MakePlayerFollowSpline(SplineContainer splineContainer)
    {
        EventManager.instance.onChosingPlayerPath?.Invoke();
        LerpCarToFirstPoint(PlayerCar.gameObject, splineContainer);
    }

    public void MakePoliceTakeRandomTurn(GameObject PoliceCar)
    {
        Intersection intersection = CurrentIntersections[PoliceCar];
        // select random turn from available turns
        List<SplineContainer> availableTurns = new List<SplineContainer>();
        if (intersection.Up != null) availableTurns.Add(intersection.Up.GetComponent<SplineContainer>());
        if (intersection.Down != null) availableTurns.Add(intersection.Down.GetComponent<SplineContainer>());
        if (intersection.Left != null) availableTurns.Add(intersection.Left.GetComponent<SplineContainer>());
        if (intersection.Right != null) availableTurns.Add(intersection.Right.GetComponent<SplineContainer>());
        int random = Random.Range(0, availableTurns.Count);
        SplineContainer randomTurn = availableTurns[random];
        LerpCarToFirstPoint(PoliceCar, randomTurn);
        CurrentIntersections[PoliceCar] = null;
    }

    public void LerpCarToFirstPoint(GameObject car, SplineContainer splineContainer)
    {
        Vector3 firstPoint = splineContainer.transform.TransformPoint(splineContainer.Spline.First().Position);
        car.transform.DOMove(firstPoint, 0.5f).OnComplete(() =>
        {
            car.GetComponent<SplineAnimate>().Container = splineContainer;
            car.GetComponent<SplineAnimate>().Restart(false);
            car.GetComponent<SplineAnimate>().Play();
        });
    }
    public void DetermineIfReversingIsNeeded(SplineContainer splineContainer, Vector3 carPosition)
    {
        float closestDistance = float.MaxValue;
        int closestKnotIndex = -1;
        for (int i = 0; i < splineContainer.Spline.Count; i++)
        {
            Vector3 knotPosition = splineContainer.transform.TransformPoint(splineContainer.Spline.Knots.ToArray()[i].Position);
            float distance = Vector3.Distance(carPosition, knotPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestKnotIndex = i;
            }
        }
        if (closestKnotIndex == -1) return;
        if (closestKnotIndex != 0)
        {
            ReverseKnotsOrder(splineContainer);
        }
    }
    public void ReverseKnotsOrder(SplineContainer splineContainer)
    {
        splineContainer.ReverseFlow(0);
    }

    [Button("Go Up")]
    public void GoUp()
    {
        MakePlayerFollowSpline(CurrentIntersections[PlayerCar.gameObject].Up.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersections[PlayerCar.gameObject] = null;
    }
    [Button("Go Left")]
    public void GoLeft()
    {
        MakePlayerFollowSpline(CurrentIntersections[PlayerCar.gameObject].Left.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersections[PlayerCar.gameObject] = null;
    }
    [Button("Go Right")]
    public void GoRight()
    {
        MakePlayerFollowSpline(CurrentIntersections[PlayerCar.gameObject].Right.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersections[PlayerCar.gameObject] = null;
    }
    [Button("Go Down")]
    public void GoDown()
    {
        MakePlayerFollowSpline(CurrentIntersections[PlayerCar.gameObject].Down.GetComponent<SplineContainer>());
        WaitForInput = false;
        CurrentIntersections[PlayerCar.gameObject] = null;
    }
}
