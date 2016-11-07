using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    public GameObject System;

    private SystemController _systemController;
    private Text _statusText;

	// Use this for initialization
	void Start ()
	{
	    _systemController = System.GetComponent<SystemController>();
	    _statusText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        string text = "";
        SystemController.State gameState = _systemController.GameState;
        switch (gameState)
        {
            case SystemController.State.Dead:
                text = "Game Over \n Press 'Space' to try again";
                break;
            //case SystemController.State.Ready:
            //    text = "Press 'Enter' to play";
            //    break;
            case SystemController.State.Play:
                text = "";
                break;
            case SystemController.State.Win:
                text = "You Win! \n Press 'Space' to try again";
                break;
        }
	    _statusText.text = text;
	}
}
