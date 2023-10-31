using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private FadingScene scenefader;
    [SerializeField]
    private int mainMenuBuildIndex=1;
    public void Retry()
    {
        scenefader.FadeTo(SceneManager.GetActiveScene().buildIndex); 
    }

    public void MainMenu()
    {
        scenefader.FadeTo(mainMenuBuildIndex);
    }
}
