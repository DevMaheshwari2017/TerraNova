using UnityEngine;
using Photon.Pun;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int toMainMenu; 
    [SerializeField]
    GameObject Logo; // My logo video player
    [SerializeField]
    GameObject Terra_nova; // Terra Nova intro video 
    [SerializeField]
    VideoPlayer TerraNova_Video;
    [SerializeField]
    VideoPlayer logo_Video;

    float time = 5f;

    private void Start()
    {

        Logo.SetActive(true);
        Terra_nova.SetActive(false);
        logo_Video.playOnAwake = true;
        TerraNova_Video.playOnAwake = true;
        logo_Video.isLooping = false;
        logo_Video.loopPointReached += EndReached;
    }

    private void EndReached(VideoPlayer source)
    {
        source.playbackSpeed = source.playbackSpeed/10.0f;
        Debug.Log("Video is finished playing");
    }

    private void VideoPlayer()
    {
        // loading Dev logo/terra_Nova
        if (time <= 2.8f)
        {
            Logo.SetActive(false);
            Terra_nova.SetActive(true);
            TerraNova_Video.Play();
        }
        if (time <= -2)
        {
            Terra_nova.SetActive(false);
            SceneManager.LoadScene(toMainMenu);
        }
    }

    private void Update()
    {
        time -= 1*Time.deltaTime;
        VideoPlayer();
    }

}
