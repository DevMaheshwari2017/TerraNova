using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private int gotoLobby = 2;
    [SerializeField]
    private FadingScene scenefader;
    [SerializeField]
    private Animator anim;

    private void Awake()
    {
        anim.SetBool("StopRotAnim",false);
    }
    public void Play()
   {
        scenefader.FadeTo(gotoLobby);
   }

    public void StopRotAnim()
    {
        anim.speed = 0;
    }                                       
                                            
    public void StartrotAnim()              
    {
        anim.speed = 1;
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
