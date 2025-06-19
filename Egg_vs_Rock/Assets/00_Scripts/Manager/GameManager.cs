using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    public GameState CurrentState { get; private set; } = GameState.Lobby;

    public void EndGame()
    {
        CurrentState = GameState.GameOver;
        PopupManager.Instance.ShowPopup($"{PopupType.PopupResult.ToString()}");
    }

    public void RestartGame()
    {
        CurrentState = GameState.InGame;

        // 게임 재시작 인 게임 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PopupManager.Instance.HidePopup();
    }

    public void GoTOLobby()
    {
        CurrentState = GameState.Lobby;
        // 로비 씬으로 이동
        PopupManager.Instance.HidePopup();
        SceneManager.LoadScene("LobbyScene");
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
