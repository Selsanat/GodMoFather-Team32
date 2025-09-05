using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;
    public GameObject Tooltip;
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

    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;
    public GameObject imagePrefab;
}
