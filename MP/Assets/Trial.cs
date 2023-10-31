using UnityEngine;
using Photon.Pun;

public class Trial : MonoBehaviourPunCallbacks
{
    private Vector3 latestPos;
    private Quaternion latestRot;

    private void Update()
    {
        if (photonView.IsMine)
        {
            // Send local position and rotation data to other players
            this.latestPos = transform.position;
            this.latestRot = transform.rotation;
        }
        else
        {
            // Update local position and rotation with the latest received data
            transform.position = this.latestPos;
            transform.rotation = this.latestRot;
        }
    }           

    [PunRPC]
    private void Internals()
    {
        //Some logic over here 
    }

    void SomeLocalMethod()
    {
        photonView.RPC("MyRemoteProcedure", RpcTarget.All);
    }
}
