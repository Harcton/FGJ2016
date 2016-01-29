using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerObject;
    public float movementSpeed = 10.0f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        handlePlayerInput();
	}

    void handlePlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerObject.GetComponent<Transform>().position += Vector3.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerObject.GetComponent<Transform>().position += Vector3.back * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerObject.GetComponent<Transform>().position += Vector3.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerObject.GetComponent<Transform>().position += Vector3.left * Time.deltaTime * movementSpeed;
        }
    }
}
