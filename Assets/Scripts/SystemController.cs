using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject[] CameraGameObjects;

    private State _gameState;
    public State GameState
    {
        get { return _gameState; }
        set
        {
            _gameState = value;
            SwitchCamera();
        }
    } //0 = Ready, 1 = Playing, -1 = Dead, 2 = Win,

    private bool _transformMode;
    public bool TransformMode {
        get { return _transformMode; }
        set
        {
            _transformMode = value;
            SwitchCamera();
        }
    }

    // Use this for initialization
	void Start ()
	{
	    Time.timeScale = 1;
        GameState = State.Play;;
	}
	
	// Update is called once per frame
	void Update ()
	{
        //if (GameState == State.Ready)
        //{
        //    if (Input.GetButtonDown("Submit"))
        //    {
        //        GameState = State.Play;
        //        Time.timeScale = 1;
        //    }
        //}
	    if (Input.GetButtonDown("Restart"))
	    {
            SceneManager.LoadScene("Level1");
        }
        if (GameState == State.Dead || GameState == State.Win)
	        {
	            if (Input.GetButtonDown("Fire2"))
	            {
	                //Time.timeScale = 0;
                    SceneManager.LoadScene("Level1");
	            }
	        }
	    if (Input.GetButtonDown("Cancel"))
	    {
	        Application.Quit();
	    }
	}

    private void SwitchCamera()
    {
        if (_transformMode)
        {
            CameraGameObjects[0].SetActive(false);
            CameraGameObjects[1].SetActive(true);
            return;
        }
        switch (GameState)
        {
            case State.Play:
                CameraGameObjects[0].SetActive(true);
                CameraGameObjects[1].SetActive(false);
                break;
            case State.Dead:
                CameraGameObjects[0].SetActive(false);
                CameraGameObjects[1].SetActive(true);
                break;
            //case State.Ready:
            //    CameraGameObjects[0].SetActive(false);
            //    CameraGameObjects[1].SetActive(true);
            //    break;
            case State.Win:
                CameraGameObjects[0].SetActive(false);
                CameraGameObjects[1].SetActive(true);
                break;
               
        }
    }

    private void GenerateRow()
    {
        
    }

    public enum State
    {
        Dead = -1,
        Ready,
        Play,
        Win
    };
}
