using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] rotators;

	// Use this for initialization
	void Start () {
        GameObject[] rotators = Resources.LoadAll<GameObject>("Rotators");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
