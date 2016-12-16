using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    private SystemController _systemController;
    private Text _statusText;
    private Text _scoreText;
    private Text _infoText;
    private Text _highScoreText;

    // Use this for initialization
    void Start ()
	{
	    _systemController = GetComponent<SystemController>();
	    _statusText = GameObject.Find("StatusText").GetComponent<Text>();
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        _infoText = GameObject.Find("InfoText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update ()
	{
        SystemController.State gameState = _systemController.GameState;
        switch (gameState)
        {
            case SystemController.State.Dead:
                _statusText.text = "Game Over \n Press 'R' to try again";
                _infoText.text = "";
                _scoreText.text = "Score: " + _systemController.Score;
                break;
            case SystemController.State.Ready:
                _statusText.text = "";
                _infoText.text =  "Earn as many points as possible.\n" +
                        "A point is earned everytime you pass into a new row.\n\n" +
                        "Controls:\n" +
                        "\tUp / W : Jump\n" +
                        "\tLeft- Right / A - D : Move left and right\n" +
                        "\tSpace / Mouse2 : Toggle rotation mode\n"+
                        "\tMouse1 : Rotate clicked square (Rotation mode)\n"+
                        "\tR : Restart\n\n"+
                        "Press any key to start\n(Also, red means dead)";
                _scoreText.text = "";
                break;
            case SystemController.State.Play:
                _statusText.text = "";
                _infoText.text = "";
                _scoreText.text = "Score: " + _systemController.Score;
                break;
            case SystemController.State.Win:
                break;
        }
        _highScoreText.text = "High Score: " + _systemController.HighScore;
	}
}
