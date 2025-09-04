using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(BoxCollider2D))]
public class Intersection : MonoBehaviour
{
    public GameObject Up;
    public GameObject Left;
    public GameObject Right;
    public GameObject Down;

    public void Start()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    public void OnCollidedWithPlayer()
    {
        print("Collided with player at intersection");
        print(this);
        RoadManager.Instance.WaitForInput = true;
        RoadManager.Instance.CurrentIntersection = this;
    }
}
