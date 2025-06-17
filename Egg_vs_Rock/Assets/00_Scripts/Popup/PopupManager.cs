using UnityEngine;

public class PopupManager : MonoBehaviour
{
    private UIPopup _currentPopup;
    [SerializeField] private Transform popupRoot;
    public static PopupManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowPopup(string popupName)
    {
        _currentPopup = Instantiate(Resources.Load<UIPopup>($"Popup/{popupName}"));
        if(popupRoot != null)
        {
            _currentPopup.transform.SetParent(popupRoot, false);
        }
        _currentPopup.Show();
    }
    public void HidePopup()
    {
        _currentPopup.gameObject.SetActive(false);
    }
}
