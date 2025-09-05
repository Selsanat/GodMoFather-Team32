using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class PoliceManager : MonoBehaviour
{
    public static PoliceManager Instance;
    public GameObject policeCarPrefab;
    public Transform Roads;
    public float MinDistanceFromPlayer = 20f;
    public int SpawnInterval = 30; // seconds
    private List<GameObject> activePoliceCars = new List<GameObject>();
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

    private void Update()
    {
        // Every SpawnInterval seconds, spawn a police car
        if (Time.frameCount % (SpawnInterval * 60) == 0) // Assuming 60 FPS
        {
            SpawnPoliceCar();
        }
    }
    private void Start()
    {
        SpawnPoliceCar();
    }
    public void SpawnPoliceCar()
    {
        // Spawn police car on a random child of Roads, but at least MinDistanceFromPlayer away from the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        Vector3 playerPosition = player.transform.position;
        Transform[] roadTransforms = Roads.GetComponentsInChildren<Transform>();
        Transform spawnPoint = null;
        int attempts = 0;
        while (spawnPoint == null && attempts < 10)
        {
            Transform potentialSpawn = roadTransforms[Random.Range(1, roadTransforms.Length)];
            if (Vector3.Distance(potentialSpawn.position, playerPosition) >= MinDistanceFromPlayer)
            {
                spawnPoint = potentialSpawn;
            }
            attempts++;
        }
        if (spawnPoint != null)
        {
            activePoliceCars.Add(Instantiate(policeCarPrefab, spawnPoint.position, spawnPoint.rotation));
            
        }
    }

    public void FadeAllPoliceCars(float value)
    {
        print("Fading");
        foreach (GameObject policeCar in activePoliceCars)
        {
            if (policeCar != null)
            {
                SpriteRenderer[] renderers = policeCar.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer renderer in renderers)
                {
                    renderer.DOFade(value, 2f);
                }
            }
        }
    }
}
