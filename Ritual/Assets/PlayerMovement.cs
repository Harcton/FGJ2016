using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerObject;
    public float movementSpeed = 10.0f;

    private Animator playerAnimator;

	// Use this for initialization
	void Start ()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
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
            playerAnimator.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerObject.GetComponent<Transform>().position += Vector3.back * Time.deltaTime * movementSpeed;
            playerAnimator.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerObject.GetComponent<Transform>().position += Vector3.right * Time.deltaTime * movementSpeed;
            playerAnimator.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerObject.GetComponent<Transform>().position += Vector3.left * Time.deltaTime * movementSpeed;
            playerAnimator.SetInteger("State", 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            playerAnimator.SetInteger("State", 2);
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            playerAnimator.SetInteger("State", 0);
        }
    }
}
