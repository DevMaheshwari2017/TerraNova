using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;
    [SerializeField]
    private FadingScene sceneFader;
    [SerializeField]
    private int MainMenuBuildIndex =1;

    private void Start()
    {
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void Pause()
    {
        // it flips the true and false so if false then after esc gets true
        pauseUI.SetActive(!pauseUI.activeSelf);

        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        Pause();
        WaveManager.EnemiesAlive = 0;        
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void Menu()
    {
        Pause();
        WaveManager.EnemiesAlive = 0;
        sceneFader.FadeTo(MainMenuBuildIndex);
    }
}
