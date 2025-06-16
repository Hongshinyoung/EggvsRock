using UnityEngine;

public class UIPopup : MonoBehaviour
{
    private Transform _popupRoot;

    public virtual void Init()
    {

    }

    private void Awake()
    {
        Init();
    }

    public virtual void Show()
    {
        if (_popupRoot == null)
        {
            _popupRoot = GameObject.Find("PopupRoot").transform;
        }
        transform.SetParent(_popupRoot, false);
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
