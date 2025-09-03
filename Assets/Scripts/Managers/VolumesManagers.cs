using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


[RequireComponent(typeof(Volume))]
public class VolumesManagers : MonoBehaviour
{
    [Header("Test Volume Settings")]
    [SerializeField] private Volume _StartVolume;
    [SerializeField] private Volume _TargetVolume;


    VolumesManagers instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
