using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject PlPrefab;
    [SerializeField]
    private GameObject gameCompleted_Panel;
    [SerializeField]
    private GameObject gameoverpanel;
    [SerializeField]
    private int MainMenu_Scene;
    public static bool isgameover;
    public bool GameWon;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        isgameover = false;
        GameWon = false;
    }

    private void Update()
    {
        if (isgameover)
            return;
        if (PlayerStats.lives <= 0)
        {
            GameOver();
        }
        if (GameWon)
        {
            GameCompleted();
        }
    }

    private void GameOver()
    {
        isgameover = true;
        gameoverpanel.SetActive(true);
    }

    private void GameCompleted()
    {
        gameCompleted_Panel.SetActive(true);        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(MainMenu_Scene);
    }

}
