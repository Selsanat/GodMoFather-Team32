using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverController : MonoBehaviour
{
    // Make it a singleton
    public static GameOverController Instance;
    public bool HasGameEnded = false;
    public CinemachineCamera Camera;
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

    void Update()
    {
        if (HasGameEnded && Input.anyKeyDown)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    public void TriggerGameOver()
    {
        if (HasGameEnded) return;
        // get first canvas group in child, and fade it in
        CanvasGroup canvasGroup = GetComponentInChildren<CanvasGroup>();
        if (canvasGroup != null)
        {

            // Calculer la nouvelle taille orthographique
            float targetSize = 35f; // Taille cible pour le dézoom

            // Tween pour dézoomer la caméra
            DOTween.To(
                () => Camera.Lens.OrthographicSize,
                x => Camera.Lens.OrthographicSize = x,
                targetSize,
                3f
            ).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                canvasGroup.DOFade(1f, 2f);
                HasGameEnded = true;
            });
        }
}
}
