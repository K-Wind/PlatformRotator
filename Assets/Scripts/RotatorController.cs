using UnityEngine;
using System.Collections;

public class RotatorController : MonoBehaviour
{
    public GameObject System;
    public Renderer Renderer;

    //private int BorderWidth;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update () {
        if (System.GetComponent<SystemController>().TransformMode)
        {
            GetComponent<BoxCollider>().enabled = true;
            transform.Find("Highlight").gameObject.SetActive(true);
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            transform.Find("Highlight").gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (transform.FindChild("Player"))
        {
            transform.FindChild("Player").parent = null;
        }
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, 90);
    }
}
