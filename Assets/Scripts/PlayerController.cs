using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject System; //0 = Dead, 1 = Playing, 2 = Completed level
    public float JumpSpeed;
    public float JumpShortSpeed;
    public float MoveSpeed;

    private Rigidbody _body;
    private float _move;
    private bool _isGrounded;
    private float _lastGround;
    private bool _jump;
    private bool _jumpCancel;

    private SystemController _systemController;

    // Use this for initialization
    void Start () {
        _body = GetComponent<Rigidbody>();
        _isGrounded = true;

        _systemController = System.GetComponent<SystemController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_systemController.GameState != SystemController.State.Play) return;
        if (Input.GetButtonDown("Fire2"))
        {
            if (_systemController.TransformMode)
            {
                _systemController.TransformMode = false;
                Time.timeScale = 1;
            }
            else
            {
                _systemController.TransformMode = true;
                transform.parent = null;
                Time.timeScale = 0;
            }
        }

        if ((Input.GetButtonDown("Jump") && _isGrounded) || (Input.GetButtonDown("Jump") && Time.time - _lastGround < 0.1f))
        {
            _jump = true;
        }
        if (Input.GetButtonUp("Jump") && !_isGrounded)
        {
            _jumpCancel = true;
        }

        _move = Input.GetAxis("Horizontal");
    }

	void FixedUpdate ()
	{
	    if (_systemController.GameState != SystemController.State.Play) return;
	    if (_systemController.TransformMode) return;

        _isGrounded = Physics.Raycast(transform.position, transform.up * -1, 0.3f, LayerMask.NameToLayer("Platform")) || Physics.Raycast(transform.position - new Vector3(0.1f, 0), transform.up * -1, 0.3f, LayerMask.NameToLayer("Platform")) || Physics.Raycast(transform.position + new Vector3(0.1f, 0), transform.up * -1, 0.3f, LayerMask.NameToLayer("Platform"));
        if (_isGrounded) _lastGround = Time.time;

        if (_jump)
	    {
	        _body.velocity = new Vector3(_body.velocity.x, JumpSpeed);
	        _jump = false;

	        transform.parent = null;
	    }
        if (_jumpCancel)
        {
            if (_body.velocity.y > JumpShortSpeed) _body.velocity = new Vector2(_body.velocity.x, JumpShortSpeed);
            _jumpCancel = false;
        }
        _body.velocity = new Vector3(_move*MoveSpeed - _systemController.Speed, _body.velocity.y);
	    //if (_isGrounded || _move < 0)
	    //{
     //       _body.velocity = new Vector3(_body.velocity.x - _systemController.Speed, _body.velocity.y);
     //   }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            _systemController.AddPoint();
            Destroy(other.gameObject);
        }
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
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (!_systemController.TransformMode)
            {
                //transform.parent = collision.transform.parent;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
    }
}
