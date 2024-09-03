using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Movement : MonoBehaviourPunCallbacks
{
    [SerializeField]
    float speed;
    [SerializeField]
    private TextMeshProUGUI HealthUI;
    [SerializeField]
    private float Health;
    PhotonView View1;

    void Start()
    {
        View1 = GetComponent<PhotonView>();
        HealthUI.text = "Health: " + Health.ToString();
    }

    private void PlayerMovement()
    {
        if (View1.IsMine)
        {
            float Move_x = Input.GetAxis("Horizontal");
            float Move_z = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(Move_x, 0, Move_z) * speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
    public void HealthDamage()
    {
        View1.RPC("RPCHealthDamager", RpcTarget.All, 10);
    }

    [PunRPC]
    private void RPCHealthDamager(int h)
    {
        --Health;
        HealthUI.text = "Health: " + Health.ToString();
        Debug.Log("Parameter passed" + h);
    }

    void Update()
    { 
        // normal player movement
        PlayerMovement();
    }
}
