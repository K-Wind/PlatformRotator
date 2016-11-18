using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject System; //0 = Dead, 1 = Playing, 2 = Completed level
    public bool TransformMode;
    public float JumpHeight;
    public float MoveSpeed;

    private Rigidbody _body;
    private bool _isGrounded;
    private float _lastClickTime;
    private Vector3 _lastVelocity;

    private SystemController _systemController;

    // Use this for initialization
    void Start () {
        _body = GetComponent<Rigidbody>();
        _systemController = System.GetComponent<SystemController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_systemController.GameState != SystemController.State.Play) return;
        if (!Input.GetButton("Fire2")) return;
        if ((Time.time - _lastClickTime) > 0.05f)
        {
            if (_systemController.TransformMode)
            {
                _systemController.TransformMode = false;
                //_body.velocity = _lastVelocity;
                //_body.useGravity = true;
                Time.timeScale = 1;
            }
            else
            {
                _systemController.TransformMode = true;
                //_lastVelocity = _body.velocity;
                //_body.velocity = new Vector3(0, 0);
                //_body.useGravity = false;
                Time.timeScale = 0.1f;
            }
        }
        _lastClickTime = Time.time;
    }

	void FixedUpdate ()
	{
	    if (_systemController.GameState != SystemController.State.Play) return;
	    if (_systemController.TransformMode) return;
	    float moveVertical = _body.velocity.y;
	    if (Input.GetButton("Jump"))
	    {
	        if (_isGrounded)
	        {
	            moveVertical = JumpHeight;
	            _isGrounded = false;
	        }
	    }
	    float moveHorizontal = Input.GetAxis("Horizontal");
	    _body.velocity = new Vector3(moveHorizontal*MoveSpeed, moveVertical);
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Blocker"))
        {
            _systemController.GameState = SystemController.State.Dead;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            _systemController.GameState = SystemController.State.Win;
            Time.timeScale = 0;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.CompareTag("Platform"))
        {
            Vector3 v = collision.contacts[0].normal;
            _isGrounded = true;
        }
    }
}
