using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject PlPrefab;
    [SerializeField]
    private GameObject Canvas;
    [SerializeField]
    private GameObject gameoverpanel;
    public static bool isgameover;
    void Start()
    {
        UIManager UI = UIManager.Instance;
        // using singleton design paatern to get a ref of my Canvas 
        if (UI != null)
        {
            Canvas = GameObject.FindWithTag("Canvas");
            Canvas.SetActive(false);         
        }

        isgameover = false;

    }

    private void Update()
    {
        if (isgameover)
            return;
        if(PlayerStats.lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isgameover = true;
        gameoverpanel.SetActive(true);
    }
}
