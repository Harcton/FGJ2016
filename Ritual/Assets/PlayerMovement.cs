using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerObject;
    public float movementSpeed = 200.0f;

    private Animator playerAnimator;
    private bool facingRight = false;
    private float idleTime = 0.0f;

    enum AnimationState
    {
        Standing = 0,
        Walking = 1,
        Gangsta = 2,
        Buddha = 3,
        Fire = 4,
        Drinking = 5,
    }

	// Use this for initialization
	void Start ()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        idleTime += Time.deltaTime;
        handlePlayerInput();
	}

    void handlePlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            idleTime = 0.0f;
            playerObject.GetComponent<Transform>().position += Vector3.forward * Time.deltaTime * movementSpeed;
            playerAnimator.SetInteger("State", (int)AnimationState.Walking);
        }
        if (Input.GetKey(KeyCode.S))
        {
            idleTime = 0.0f;
            playerObject.GetComponent<Transform>().position += Vector3.back * Time.deltaTime * movementSpeed;
            playerAnimator.SetInteger("State", (int)AnimationState.Walking);
        }
        if (Input.GetKey(KeyCode.D))
        {
            idleTime = 0.0f;
            playerObject.GetComponent<Transform>().position += Vector3.right * Time.deltaTime * movementSpeed;
            if (!facingRight)
            {
                FlipPlayer();
            }
            facingRight = true;
            playerAnimator.SetInteger("State", (int)AnimationState.Walking);
        }
        if (Input.GetKey(KeyCode.A))
        {
            idleTime = 0.0f;
            playerObject.GetComponent<Transform>().position += Vector3.left * Time.deltaTime * movementSpeed;
            if (facingRight)
            {
                FlipPlayer();
            }
            facingRight = false;
            playerAnimator.SetInteger("State", (int)AnimationState.Walking);
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Fire);
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Buddha);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Gangsta);
        }
        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            if (idleTime > 3.0f)
            {
                playerAnimator.SetInteger("State", (int)AnimationState.Drinking);
                if (idleTime > 4.0f)
                {
                    idleTime = 0.0f;
                }
            }
            else
            {
                playerAnimator.SetInteger("State", (int)AnimationState.Standing);
            }
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 scale = playerObject.GetComponent<Transform>().localScale;
        scale.x *= -1;
        playerObject.GetComponent<Transform>().localScale = scale;
    }
}
