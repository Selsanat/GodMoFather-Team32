using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowersManager : MonoBehaviour
{

    public static FollowersManager Instance;
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
    }
}
