using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Mathematics;
using System.Runtime.ConstrainedExecution;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(BoxCollider2D))]
public class Intersection : MonoBehaviour
{
    public GameObject Up;
    public GameObject Left;
    public GameObject Right;
    public GameObject Down;

    private List<GameObject> canvasGroups = new List<GameObject>();

    public void Start()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        EventManager.instance.onChosingPlayerPath.AddListener(ClearDirections);
    }

    public void OnCollidedWithPlayer(GameObject colision)
    {
        RoadManager.Instance.WaitForInput = true;
        RoadManager.Instance.CurrentIntersections[colision] = this;
        CheckPathOrientation(colision.transform.position, colision.GetComponent<SplineAnimate>().splineContainer);
        SpawnDirections(colision.GetComponent<SplineAnimate>().splineContainer);
    }
    public void OnCollidedWithPolice(GameObject colision)
    {
        RoadManager.Instance.CurrentIntersections[colision] = this;
        CheckPathOrientation(colision.transform.position, colision.GetComponent<SplineAnimate>().splineContainer);
        RoadManager.Instance.MakePoliceTakeRandomTurn(colision);
    }

    public void CheckPathOrientation(Vector2 position, SplineContainer ignore)
    {
        if (Up != null && Up.GetComponent<SplineContainer>() != ignore)
        {
            RoadManager.Instance.DetermineIfReversingIsNeeded(Up.GetComponent<SplineContainer>(), position);
        }
        if (Down != null && Down.GetComponent<SplineContainer>() != ignore)
        {
            RoadManager.Instance.DetermineIfReversingIsNeeded(Down.GetComponent<SplineContainer>(), position);
        }
        if (Left != null && Left.GetComponent<SplineContainer>() != ignore)
        {
            RoadManager.Instance.DetermineIfReversingIsNeeded(Left.GetComponent<SplineContainer>(), position);
        }
        if (Right != null && Right.GetComponent<SplineContainer>() != ignore)
        {
            RoadManager.Instance.DetermineIfReversingIsNeeded(Right.GetComponent<SplineContainer>(), position);
        }
    }

    public void ClearDirections()
    {
        foreach (GameObject canvasGroup in canvasGroups)
        {
            CanvasGroup cg = canvasGroup.GetComponent<CanvasGroup>();
            cg.DOFade(0f, 0.5f).OnComplete(() => Destroy(canvasGroup));
        }
        canvasGroups.Clear();
    }

    public void SpawnDirections(SplineContainer ignore)
    {

        if (Up != null && Up.GetComponent<SplineContainer>() != ignore)
        {
            SpawnDirection(Up, Direction.Up);
        }
        if (Down != null && Down.GetComponent<SplineContainer>() != ignore)
        {
            SpawnDirection(Down, Direction.Down);
        }
        if (Left != null && Left.GetComponent<SplineContainer>() != ignore)
        {
            SpawnDirection(Left, Direction.Left);
        }
        if (Right != null && Right.GetComponent<SplineContainer>() != ignore)
        {
            SpawnDirection(Right, Direction.Right);
        }
    }

    private void SpawnDirection(GameObject obj, Direction dir)
    {
        TooltipManager tooltipManager = TooltipManager.Instance;
        GameObject instance = Instantiate(tooltipManager.imagePrefab, obj.transform);
        UnityEngine.UI.Image image = instance.GetComponentInChildren<UnityEngine.UI.Image>();
        CanvasGroup canvasGroup = instance.GetComponent<CanvasGroup>();
        SplineContainer splineContainer = obj.GetComponent<SplineContainer>();
        float3 Origin = splineContainer.EvaluatePosition(0.2f);
        canvasGroup.alpha = 0f;

        canvasGroup.DOFade(1f, 0.5f);
        canvasGroups.Add(instance);

        switch (dir)
        {
            case Direction.Up:
                image.sprite = TooltipManager.Instance.Up;
                instance.GetComponentInParent<RectTransform>().position = Origin;
                break;
            case Direction.Down:
                image.sprite = TooltipManager.Instance.Down;
                instance.GetComponentInParent<RectTransform>().position = Origin;
                break;
            case Direction.Left:
                image.sprite = TooltipManager.Instance.Left;
                instance.GetComponentInParent<RectTransform>().position = Origin;
                break;
            case Direction.Right:
                image.sprite = TooltipManager.Instance.Right;
                instance.GetComponentInParent<RectTransform>().position = Origin;
                break;
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
