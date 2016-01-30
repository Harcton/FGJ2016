using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private float scrollSpeed;
    private bool slow = false;
	// Use this for initialization
	void Start ()
    {
        scrollSpeed = 0.1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (scrollSpeed < 0.2f && !slow)
            scrollSpeed += (Time.deltaTime * Time.deltaTime) * 0.45f;
        else
        {
            scrollSpeed -= (Time.deltaTime * Time.deltaTime) * 0.88f;
            slow = true;
        }

        if (scrollSpeed < 0.0f)
            scrollSpeed = 0.0f;
        transform.position += Vector3.forward * Time.deltaTime * scrollSpeed;
	}
}
