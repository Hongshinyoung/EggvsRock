using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    private Rock rock;
    private Egg egg;

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
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 후 콜백 등록
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PopupManager.Instance.HidePopup();
    }

    public void GoTOLobby()
    {
        CurrentState = GameState.Lobby;
        PopupManager.Instance.HidePopup();
        SceneManager.LoadScene("LobbyScene");
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            DataInit();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void DataInit()
    {
        dataManager.LoadDatas();
        egg = FindAnyObjectByType<Egg>();
        if (egg != null)
            egg.InitStat();

        rock = FindAnyObjectByType<Rock>();
        if (rock != null)
            rock.InitStat();
    }

    // 씬이 완전히 로드된 후 Stat 초기화
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DataInit();
        SceneManager.sceneLoaded -= OnSceneLoaded; // 중복 호출 방지
    }
}