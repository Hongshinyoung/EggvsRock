using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupResult : UIPopup
{
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnLobby;

    public override void Init()
    {
        btnRetry.onClick.AddListener(OnClickRetry);
        btnLobby.onClick.AddListener(OnClickLobby);
    }
    private void OnClickRetry()
    {
        GameManager.Instance.RestartGame();
    }

    private void OnClickLobby()
    {
        GameManager.Instance.GoTOLobby();
    }

}
