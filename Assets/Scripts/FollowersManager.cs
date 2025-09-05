using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class FollowersManager : MonoBehaviour
{

    public static FollowersManager Instance;
    public CinemachineCamera Camera;
    public float dezoomByFollower = 1f;
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

    public GameObject followerPrefab;
    List<GameObject> followers = new List<GameObject>();

    public void SpawnFollower(Vector3 position)
    {
        GameObject follower = Instantiate(followerPrefab, position, Quaternion.identity);
        followers.Add(follower);

        // Calculer la nouvelle taille orthographique
        float targetSize = Camera.Lens.OrthographicSize + dezoomByFollower;

        // Tween pour dézoomer la caméra
        DOTween.To(
            () => Camera.Lens.OrthographicSize,
            x => Camera.Lens.OrthographicSize = x,
            targetSize,
            1f
        ).SetEase(Ease.OutQuad);
    }
}
