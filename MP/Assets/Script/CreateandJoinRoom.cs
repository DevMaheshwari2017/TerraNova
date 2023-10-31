using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class CreateandJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    InputField createroom;
    [SerializeField]
    InputField joinroom;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createroom.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinroom.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed");
    }
}
