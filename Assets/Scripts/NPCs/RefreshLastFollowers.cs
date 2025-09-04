using Unity.Cinemachine;
using UnityEngine;

public class RefreshLastFollowers : MonoBehaviour
{
    private GameObject lastFollower;
    [SerializeField] Transform player;
    [SerializeField] CinemachineTargetGroup targetGroup;
    void Start()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
        InvokeRepeating("FindLastFollower", 0f, 5f);
    }

    

    void FindLastFollower()
    {
        Vector2 lastPostiton = Vector2.zero;
        if (FollowersManager.Instance.followers.Count != 0)
        {
            foreach (GameObject follower in FollowersManager.Instance.followers)
            {
                if (Vector2.Distance(player.position, follower.transform.position) > Vector2.Distance(player.position, lastPostiton))
                {
                    lastPostiton = follower.transform.position;
                    lastFollower = follower;
                    if(targetGroup != null)
                    {
                        SetTargetGroupList(1, lastFollower);

                    }
                    
                }
            }

        }

    }

    void SetTargetGroupList(int i,  GameObject target)
    {
        targetGroup.Targets[i].Object = target.transform;
        targetGroup.Targets[i].Radius = targetGroup.Targets[0].Radius;
        targetGroup.Targets[i].Weight = targetGroup.Targets[0].Weight;
    }
}
