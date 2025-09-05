using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup MenuCanvasGroup;
    public CanvasGroup BackgroundCanvasGroup;
    public string NextSceneName = "Scene_Test_GD";
    private bool _AnyKeyPressed = false;

    private void Start()
    {
        // dont destroy this object when loading a new scene
        DontDestroyOnLoad(gameObject);
        if (BackgroundCanvasGroup) DontDestroyOnLoad(BackgroundCanvasGroup);
    }

    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2) && !_AnyKeyPressed)
        {
            _AnyKeyPressed = true;
            if (MenuCanvasGroup != null)
            {
                MenuCanvasGroup.DOFade(0, 2f).OnComplete(() =>
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(NextSceneName);
                    BackgroundCanvasGroup.DOFade(0, 2f).OnComplete(() =>
                    {
                        Destroy(gameObject);
                        if (BackgroundCanvasGroup != null) Destroy(BackgroundCanvasGroup.gameObject);
                    });
                });
            }
        }
    }
}
