using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public GameObject scene2;

    private float scrollSpeed;
    private bool slow = false;
    private bool image = false;
    private float exitTimer;

    private float switchTimer;
	// Use this for initialization
	void Start ()
    {
        scrollSpeed = 0.1f;
        transform.position = new Vector3(0.017f, 0.549f, -1.57f);
        gameObject.GetComponent<Camera>().orthographicSize = 0.34f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!image)
        {
            if (scrollSpeed < 0.2f && !slow)
                scrollSpeed += (Time.deltaTime * Time.deltaTime) * 0.45f;
            else
            {
                scrollSpeed -= (Time.deltaTime * Time.deltaTime) * 1.02f;
                slow = true;
            }
        }
        else
        {
            if (scrollSpeed < 0.2f)
                scrollSpeed -= (Time.deltaTime * Time.deltaTime) * 0.45f;
        }


        if (switchTimer > 5.0f)
        {
            if (!image)
            {
                image = true;
                transform.position = new Vector3(2.781f, 0.549f, 1.485f);
                gameObject.GetComponent<Camera>().orthographicSize = 0.6f;
            }
        }
        else if (scrollSpeed < 0.0f)
        {
            scrollSpeed = 0.0f;
            switchTimer += Time.deltaTime;
        }

        if (transform.position.x > 3.063)
        {
            scrollSpeed = 0.0f;
            exitTimer += Time.deltaTime;
            if (exitTimer > 10.0f)
            {
                Application.LoadLevel("Game");
            }
        }

        if (!image)
        {
            transform.position += Vector3.forward * Time.deltaTime * scrollSpeed;
        }
        else
        {
            transform.position += Vector3.left * Time.deltaTime * scrollSpeed;
        }
	}
}
