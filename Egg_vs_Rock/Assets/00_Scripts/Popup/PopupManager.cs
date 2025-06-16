using UnityEngine;

public class PopupManager : MonoBehaviour
{
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
        UIPopup popup = Instantiate(Resources.Load<UIPopup>($"Popup/{popupName}"));
        popup.Show();
    }
    public void HidePopup()
    {
        // Implement popup hide logic here
        Debug.Log("Popup hidden");
    }
}
