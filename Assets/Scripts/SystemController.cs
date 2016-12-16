using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SystemController : MonoBehaviour
{
    public GameObject PlayerGameObject;
    public GameObject[] CameraGameObjects;
    public GameObject[] Rotators;
    public GameObject Mover;
    public float Speed;
    public int Score;
    public int HighScore;

    private State _gameState;
    public State GameState //0 = Ready, 1 = Playing, -1 = Dead, 2 = Win,
    {
        get { return _gameState; }
        set
        {
            _gameState = value;
            SwitchCamera();
            if (_gameState == State.Dead)
            {
                if (Score > HighScore)
                {
                    HighScore = Score;
                }
            }
        }
    }

    private bool _transformMode;
    public bool TransformMode {
        get { return _transformMode; }
        set
        {
            _transformMode = value;
            SwitchCamera();
        }
    }

    void Awake()
    {
        Rotators = Resources.LoadAll<GameObject>("Rotators");
        GameState = State.Ready;
        Load();
    }

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 0;
        GeneratePlayer();

        System.Random rng = new System.Random();
        var r = Enumerable.Range(0, Rotators.Length).OrderBy(x => rng.Next()).ToArray();
        GenerateRow(new[] { Rotators[r[0]], Rotators[0], Rotators[r[1]] }, new Vector3(4, 0));
        r = Enumerable.Range(0, Rotators.Length).OrderBy(x => rng.Next()).ToArray();
        GenerateRow(new[] { Rotators[r[0]], Rotators[r[1]], Rotators[r[2]] }, new Vector3(8, 0));
        r = Enumerable.Range(0, Rotators.Length).OrderBy(x => rng.Next()).ToArray();
        GenerateRow(new[] { Rotators[r[0]], Rotators[r[1]], Rotators[r[2]] }, new Vector3(12, 0));

        StartCoroutine("WaitAndSpawn");
	}

    // Update is called once per frame
    void Update ()
	{
        if(GameState == State.Ready && Input.anyKey)
	    {
	        GameState = State.Play;
	        Time.timeScale = 1;
	    }
	    if (Input.GetButtonDown("Restart"))
	    {
            Save();
            SceneManager.LoadScene("Level2");
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
            case State.Win:
                CameraGameObjects[0].SetActive(false);
                CameraGameObjects[1].SetActive(true);
                break;
        }
    }

    public void AddPoint()
    {
        Score += 1;
        Speed += 0.1f;
    }

    //Used as a Coroutine
    IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            GenerateRow();
            yield return new WaitForSeconds(4 / Speed);
        }
    }

    private void GeneratePlayer()
    {
        GameObject player = (GameObject)Instantiate(PlayerGameObject, new Vector3(2.5f, 0.5f, 1), Quaternion.identity);
        PlayerController pc = (PlayerController)player.GetComponent("PlayerController");
        pc.System = gameObject;

        ((CameraController) GameObject.Find("Player Camera").GetComponent("CameraController")).Player = player;
    }

    private void GenerateRow()
    {
        GameObject start = (GameObject)Instantiate(Mover, new Vector3(16, 0), Quaternion.identity);
        MoveController controller = (MoveController)start.GetComponent("MoveController");
        controller.System = this;

        System.Random rng = new System.Random();
        var r = Enumerable.Range(0, Rotators.Length-1).OrderBy(x => rng.Next()).ToArray();
        var ra = new[] {Rotators[r[0]], Rotators[r[1]], Rotators[r[2]]};

        foreach (GameObject x in ra)
        {
            x.transform.eulerAngles = new Vector3(0, 0, 90*rng.Next(4));
        }

        controller.Rotators = ra;
    }

    private void GenerateRow(GameObject[] rotators, Vector3 pos)
    {
        GameObject start = (GameObject)Instantiate(Mover, pos, Quaternion.identity);
        MoveController controller = (MoveController)start.GetComponent("MoveController");
        controller.System = this;

        foreach (GameObject x in rotators)
        {
            x.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        controller.Rotators = rotators;
    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.Open);

        PlayerData data = new PlayerData {score = HighScore};

        bf.Serialize(file, data);
        file.Close();
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/highScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/highScore.dat", FileMode.Open);
            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();

            HighScore = data.score;
        }
        else
        {
            File.Create(Application.persistentDataPath + "/highScore.dat");
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


[Serializable]
class PlayerData
{
    public int score;
}
