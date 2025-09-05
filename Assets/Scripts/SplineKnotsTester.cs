using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Splines;
public class SplineKnotsTester : MonoBehaviour
{
    // make a button to reverse the order of knots in a spline container
    [Button("Reverse Knots Order")]
    public void ReverseKnotsOrder()
    {
        SplineContainer splineContainer = GetComponent<SplineContainer>();
        print(splineContainer);
        splineContainer.ReverseFlow(0);
    }
}
