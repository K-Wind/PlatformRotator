using UnityEngine;
using System.Collections;

public class SystemController : MonoBehaviour
{
    public State GameState; //0 = Ready, 1 = Playing, -1 = Dead, 2 = Win, 
    public GameObject PlayerGameObject;
    public bool TransformMode { get; set; }

    // Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameState == State.Ready)
	    {
	        if (Input.GetButtonDown("Fire2"))
	        {
	            GameState = State.Play;
	        }
	    }
	    if (GameState == State.Dead || GameState == State.Win)
	        {
	            if (Input.GetButtonDown("Fire2"))
	            {
	                GameState = State.Play;
	                GameObject newPlayer = (GameObject)Instantiate(PlayerGameObject, new Vector3(-7.5f, 0.5f), Quaternion.identity);
	                newPlayer.GetComponent<PlayerController>().System = gameObject;
	            }
	        }
	}

    public enum State
    {
        Dead = -1,
        Ready,
        Play,
        Win
    };
}
