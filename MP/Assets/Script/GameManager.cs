using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    UIManager UI;
    private int rand;
    private Vector3 pos;
    [SerializeField]
    private GameObject PlPrefab;
    [SerializeField]
    private GameObject Canvas;
    void Start()
    {
        UIManager UI = UIManager.Instance;
        // using singleton design paatern to get a ref of my Canvas 
        if (UI != null)
        {
            Canvas = GameObject.FindWithTag("Canvas");
            Canvas.SetActive(false);         
        }

    }
}
