using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    private GameObject _player;
    public GameObject Player
    {
        get { return _player;}
        set
        {
            _player = value;
            _offset = transform.position - _player.transform.position;
        }

    }

    private Vector3 _offset;         //Private variable to store the offset distance between the player and camera

    private float cameraOffset;
    private Vector3 lastCameraOffset;
    private Quaternion cameraRotation;
    private Transform lastPlayer;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
    }

    void Update()
    {
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        //Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;
        if (_player != null)
        {
            //GetDirection();

            transform.position = _player.transform.position + _offset;
            //transform.position = new Vector3(transform.position.x + cameraOffset, transform.position.y, transform.position.z);
            //transform.rotation = Quaternion.Euler(-player.transform.rotation.x + 8, -player.transform.rotation.y + 40 + cameraRotation.y, -player.transform.rotation.z);
            transform.LookAt(_player.transform);
            //lastPlayer = _player.transform;
        }
    }

    void GetDirection()
    {
        if (lastPlayer == null)
        {
            return;
        }
        float movement = _player.transform.position.x - lastPlayer.position.x;
        //float dir = player.transform.InverseTransformDirection(player.GetComponent<Rigidbody>().velocity).x;
        cameraOffset = movement*2;
        if (cameraOffset > 0)
        {
            return;
        }
    }
}
