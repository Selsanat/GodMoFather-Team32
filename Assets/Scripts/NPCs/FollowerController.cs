using UnityEngine;
using UnityEngine.AI;
public class FollowerController : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;

    GameObject _player;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        // C'est dégueulasse mais on a pas trop de temps. On verra si ca tiens pour les besoins du projet.
        if (_player != null)
        {
            float distance = Vector3.Distance(transform.position, _player.transform.position);
            if (distance > 1f)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_player.transform.position);
                _navMeshAgent.speed = Mathf.Clamp(distance, 1f, 30f);
            }
            else
            {
                _navMeshAgent.isStopped = true;
            }
        }
    }
}
