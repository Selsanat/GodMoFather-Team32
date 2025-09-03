using UnityEngine;
using DG.Tweening;
using TMPro;
public class TitleTextAnimation : MonoBehaviour
{
    public float UpDownMoveDistance = 10f;
    public float UpDownMoveDuration = 1f;
    public float opacityDuration = 1f;
    public float minOpacity = 0.5f;
    private TMPro.TextMeshProUGUI _TextMeshProUGUI;
    void Start()
    {
        _TextMeshProUGUI = GetComponent<TextMeshProUGUI>();

        transform.DOMoveY(transform.position.y + UpDownMoveDistance, UpDownMoveDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        _TextMeshProUGUI.DOBlendableColor(new Color(0, 0, 0, minOpacity), opacityDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    void Update()
    {
        
    }
}
