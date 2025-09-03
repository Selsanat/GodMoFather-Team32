using UnityEngine;
using UnityEngine.AI;
public class FollowerController : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;

    Rigidbody _rigidbody;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        // detect mouse click with new input system
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print(hit.point);
                _navMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
