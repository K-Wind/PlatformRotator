using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private Vector3 cameraOffset;
    private Vector3 lastCameraOffset;
    private Quaternion cameraRotation;
    private Transform lastPlayer;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;

        GetDirection();

        transform.position = player.transform.position + offset + cameraOffset;
        //transform.rotation = Quaternion.Euler(-player.transform.rotation.x + 8, -player.transform.rotation.y + 40 + cameraRotation.y, -player.transform.rotation.z);
        transform.LookAt(player.transform);
        lastPlayer = player.transform;
    }

    void GetDirection()
    {
        if (lastPlayer == null)
        {
            return;
        }
        float dir = player.transform.position.x - lastPlayer.position.x;
        //float dir = player.transform.InverseTransformDirection(player.GetComponent<Rigidbody>().velocity).x;
        if (dir > 0) //Right
        {
            cameraOffset = new Vector3(cameraOffset.x + dir, 0, 0);
            //cameraRotation = Quaternion.Euler(0, cameraRotation.y - Math.Abs(dir*100), 0);
        }else if (dir < 0)
        {
            cameraOffset = new Vector3(dir, 0, 0);
            //cameraRotation = Quaternion.Euler(0, cameraRotation.y + Math.Abs(dir*100), 0);
        }
    }
}
