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
    public  List<GameObject> followers = new  List<GameObject>();

    
    public void SpawnFollower(Vector3 position)
    {
        AudioManager.instance.PlaySFX("Follower");
        GameManager.followers++;
        GameObject follower = Instantiate(followerPrefab, position, Quaternion.identity);
        followers.Add(follower);

        // Calculer la nouvelle taille orthographique
        float targetSize = Camera.Lens.OrthographicSize + dezoomByFollower;
        WavesManager wavesManager = WavesManager.ins;
        if (wavesManager != null)
        {
            // augmenter la scale de playerWave
            wavesManager.playerWave.transform.parent.transform.DOScale(
                wavesManager.playerWave.transform.parent.transform.localScale + new Vector3(dezoomByFollower / 8, dezoomByFollower / 8, 0),
                1f
            ).SetEase(Ease.OutQuad);
            // add scale to policeWave
            wavesManager.policeWave.transform.parent.transform.DOScale(
                wavesManager.policeWave.transform.parent.transform.localScale + new Vector3(dezoomByFollower / 5, dezoomByFollower / 5, 0),
                1f
            ).SetEase(Ease.OutQuad);
        }

        // Tween pour d�zoomer la cam�ra
        DOTween.To(
            () => Camera.Lens.OrthographicSize,
            x => Camera.Lens.OrthographicSize = x,
            targetSize,
            1f
        ).SetEase(Ease.OutQuad);
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
