using UnityEngine;
using System.Collections;

public class PositionScript : MonoBehaviour
{
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 relativePosition = new Vector3(0.0f, 20.0f, -20.0f);
        transform.position = player.transform.position + relativePosition;
	}
}
