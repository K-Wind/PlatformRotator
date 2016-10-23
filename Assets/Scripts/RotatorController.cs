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
        //Texture2D texture = new Texture2D((int)Renderer.bounds.size.x, (int)Renderer.bounds.size.y);
        //var borderColor = new Color(0, 0, 0, 1);
        //for (var w = 0; w < texture.width; w++)
        //{
        //    for (var h = 0; h < texture.height; w++)
        //    {
        //        if (w < BorderWidth || w > texture.width - 1 - BorderWidth) texture.SetPixel(w, h, borderColor);
        //        else if (h < BorderWidth || h > texture.height - 1 - BorderWidth) texture.SetPixel(w, h, borderColor);
        //    }
        //}
        //texture.Apply();
        //Renderer.material.mainTexture = texture;
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
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, 90);
    }
}
