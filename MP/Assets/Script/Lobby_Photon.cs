using UnityEngine;
using Photon.Pun;
using UnityEngine.Video;

public class Lobby_Photon : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject Loading_Screen; 
    [SerializeField]
    GameObject Create_Join_Panel; // create/join room panel
    [SerializeField]
    VideoPlayer Loading_Video; // loading video
    void Start()
    {
        Create_Join_Panel.SetActive(false);
        Loading_Video.isLooping = true;
        Loading_Video.Play();
        PhotonNetwork.ConnectUsingSettings();               
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to master");
    }

    public override void OnJoinedLobby()
    {
        Loading_Screen.SetActive(false);       
        Create_Join_Panel.SetActive(true);
    }
}
