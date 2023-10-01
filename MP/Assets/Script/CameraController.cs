using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float CameraSpeed = 30f;
    [SerializeField]
    private float Boarderspace = 10f;
    private bool cameramovement = false;
    private bool ismousedown;
    [SerializeField]
    private float minZoom = 10f;
    [SerializeField]
    private float maxZoom = 50f;
    //[SerializeField]
    //private float minX = 0f;
    //[SerializeField]
    //private float maxX = 25f;
    //[SerializeField]
    //private float minZ = 0f;
    //[SerializeField]
    //private float maxZ = 18f;
    [SerializeField]
    private float zoomspeed = 10f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameramovement = !cameramovement;
        }

        if (!cameramovement)
            return;

        if (Input.GetKey("mouse 2"))
        {
            ismousedown = true;
            //(Input.mousePosition.x - Screen.width * 0.5)/(Screen.width * 0.5)
            transform.Translate(Vector3.left * CameraSpeed * Time.deltaTime * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * CameraSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
        }

        ismousedown = false;

        // zooming in and out using mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
       Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * zoomspeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);
        transform.position = pos;

        if (ismousedown)
            return;
        //Vector3 initialpos = transform.position;
        //initialpos.x = Mathf.Clamp(transform.position.x, minX, maxX);
        //initialpos.z = Mathf.Clamp(transform.position.z, minZ, maxZ);

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - Boarderspace)
        {
            transform.Translate(Vector3.forward * CameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= Boarderspace)
        {
            transform.Translate(Vector3.back * CameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - Boarderspace)
        {
            transform.Translate(Vector3.left * CameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= Boarderspace)
        {
            transform.Translate(Vector3.right * CameraSpeed * Time.deltaTime, Space.World);
        }

        //transform.position = initialpos;
    }
}