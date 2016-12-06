using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    private SystemController _systemController;
    private Text _statusText;
    private Text _pointText;

	// Use this for initialization
	void Start ()
	{
	    _systemController = GetComponent<SystemController>();
	    _statusText = GameObject.Find("StatusText").GetComponent<Text>();
        _pointText = GameObject.Find("PointText").GetComponent<Text>();
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
            case SystemController.State.Ready:
                text = "Press 'Enter' to play";
                break;
            case SystemController.State.Play:
                text = "";
                break;
            case SystemController.State.Win:
                text = "You Win! \n Press 'Space' to try again";
                break;
        }
	    _statusText.text = text;

        _pointText.text = "Points: " + _systemController.Points;
	}
}
