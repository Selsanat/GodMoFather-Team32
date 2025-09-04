using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
    public  List<GameObject> followers = new  List<GameObject>();

    
    public void SpawnFollower(Vector3 position)
    {

        GameManager.followers++;
        GameObject follower = Instantiate(followerPrefab, position, Quaternion.identity);
        followers.Add(follower);
    }

    private void Update()
    {
        
    }


    [NaughtyAttributes.Button]
    public void Spawn()
    {
        GameObject follower = Instantiate(followerPrefab, transform.position, Quaternion.identity);
        followers.Add(follower);
    }

}
