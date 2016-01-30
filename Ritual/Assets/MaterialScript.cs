using UnityEngine;
using System.Collections;

public class MaterialScript : MonoBehaviour
{
    public float xScale, yScale;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(xScale, yScale);
	}
}
