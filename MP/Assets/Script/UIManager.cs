using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject Logo; // My logo video player
    [SerializeField]
    GameObject Terra_nova; // Terra Nova intro video 
    [SerializeField]
    GameObject Loading_Screen; // loading scene
    [SerializeField]
    InputField Create_Room_Name; 
    [SerializeField]
    InputField Join_Room_Name;
    [SerializeField]
    GameObject Create_Join_Panel; // create/join room panel
    float time = 5f;
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Logo.SetActive(true);
        Terra_nova.SetActive(false);
        Loading_Screen.SetActive(false);
        Create_Join_Panel.SetActive(false);
    }

    public void OnCreate_Room()
    {
        // if create room button is clicked 
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(Create_Room_Name.text,options,TypedLobby.Default);   
    }

    public void OnJoiningRoom()
    {
        // if join room button is clicked 
        PhotonNetwork.JoinRoom(Join_Room_Name.text);// checking for the name of room that we created 
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to master");
    }
    public override void OnCreatedRoom()
    {
        // overriding photon own createatedRoom function 
        Debug.Log("room Created succesfully");
    }
    public override void OnJoinedRoom()  
    {
        // overriding photon own JoineddRoom function 
        Debug.Log("Joined room succesfully");
        SceneManager.LoadScene(1);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // if room craetion failed
        // using photon own functions 
        Debug.Log("Room Creation Failed");
    }
    private void VideoPlayer()
    {
        // loading logo/terra_Nova/loading_screen videos 
        var TerraNova_Video = GameObject.FindWithTag("TerraNovaVP").GetComponent<UnityEngine.Video.VideoPlayer>();
        var Loading_Video = GameObject.FindWithTag("LoadingVP").GetComponent<UnityEngine.Video.VideoPlayer>();
        if (time <= 2)
        {
            Logo.SetActive(false);
            Terra_nova.SetActive(true);
            TerraNova_Video.Play();
        }
        if (time <= -2.5)
        {
            Terra_nova.SetActive(false);
            Loading_Screen.SetActive(true);
            Loading_Video.Play();
        }
        if (time <= -5)
        {
            Loading_Screen.SetActive(false);
            Create_Join_Panel.SetActive(true);
        }
    }

    private void Update()
    {
        time -= 1*Time.deltaTime;
        VideoPlayer();
    }

}
