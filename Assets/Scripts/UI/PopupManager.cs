using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject darkenator;
    public GameObject choicePopup;

    public static PopupManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CloseAll();
    }

    public void OpenChoicePopup()
    {
        CameraShake.Instance.StopShake();
        darkenator.SetActive(true);
        choicePopup.SetActive(true);
    }

    public void CloseAll()
    {
        darkenator.SetActive(false);
        choicePopup.SetActive(false);
    }
}
