using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public virtual void Init()
    {

    }

    private void Awake()
    {
        Init();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
