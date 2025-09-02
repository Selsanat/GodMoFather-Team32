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

    private void SetActiveVolume(float duration, Volume targetVolume)
    {
        if (_CurrentVolume != null)
        {
            DOTween.To(() => _CurrentVolume.weight, x => _CurrentVolume.weight = x, 0, duration).OnComplete(() =>
            {
                _CurrentVolume = targetVolume;
                DOTween.To(() => _CurrentVolume.weight, x => _CurrentVolume.weight = x, 1, duration);
            });
        }
        else
        {
            _CurrentVolume = targetVolume;
            DOTween.To(() => _CurrentVolume.weight, x => _CurrentVolume.weight = x, 1, duration);
        }
    }

    [Button("Test Volume Transition")]
    public void TestVolumeTransition()
    {
        SetActiveVolume(1, _TargetVolume);
    }
}
