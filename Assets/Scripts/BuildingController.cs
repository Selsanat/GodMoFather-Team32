using UnityEngine;
using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
public class BuildingController : MonoBehaviour
{
    SpriteRenderer TargetSpriteRenderer;
    public List<SpriteRenderer> TargetSpriteRenderers;
    public int FollowerToSpawn = 3;

    void Start()
    {
        if (TargetSpriteRenderers.Count > 0)
        {
            TargetSpriteRenderer = TargetSpriteRenderers[Random.Range(0, TargetSpriteRenderers.Count)];
        }
        foreach (var sr in TargetSpriteRenderers)
        {
            if (sr != TargetSpriteRenderer)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
            }
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
