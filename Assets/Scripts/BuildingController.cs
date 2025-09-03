using UnityEngine;
using DG.Tweening;
public class BuildingController : MonoBehaviour
{
    public SpriteRenderer TargetSpriteRenderer;
    public int FollowerToSpawn = 3;

    void Start()
    {
        if (TargetSpriteRenderer == null)
        {
            Debug.LogError("TargetSpriteRenderer is not assigned in " + gameObject.name);
        }
    }

    public void OnCloseEnough()
    {
        if (TargetSpriteRenderer != null)
        {
            TargetSpriteRenderer.DOColor(Color.red, 1f);
            FollowersManager.Instance.SpawnFollower(transform.position);
            Destroy(this);
        }
    }
}
