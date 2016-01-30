using UnityEngine;
using System.Collections;

public class Terrain : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (Random.Range(1, 100) > 50)
            transform.localScale = new Vector3(-1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
