using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{
    public SystemController System;
    public GameObject[] Rotators;

	// Use this for initialization
	void Start () {
        GenerateRotators();
    }

        // Update is called once per frame
    void Update () {
        gameObject.transform.Translate(new Vector3(-System.Speed, 0, 0) * Time.deltaTime);
        if (gameObject.transform.position.x <= -12)
        {
            Destroy(GameObject.Find(gameObject.name));
        }
    }

    void GenerateRotators()
    {
        NewRotator(0, 4);
        NewRotator(1, 0);
        NewRotator(2, -4);
    }

    void NewRotator(int r, int posY)
    {
        GameObject rotator = (GameObject)Instantiate(Rotators[r], new Vector3(transform.position.x, posY), Rotators[r].transform.rotation);
        rotator.transform.parent = gameObject.transform;
        RotatorController rc = (RotatorController)rotator.GetComponent("RotatorController");
        rc.System = GameObject.Find("System");
    }
}
