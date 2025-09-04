using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class VolumesManagers : MonoBehaviour
{
    [Header("Test Volume Settings")]
    [SerializeField] private Volume _TargetVolume;


    VolumesManagers instance;
    Volume _CurrentVolume;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Make sure that the camera use post Process
        Camera.main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
    }

    
}
