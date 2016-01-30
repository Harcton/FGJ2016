using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //Player Movements & Actions

    public GameObject playerObject;
    public GameObject stonePrefab;
    public float movementSpeed = 200.0f;

    private Animator playerAnimator;
    private bool canMove = true;
    public bool facingRight = false;
    private float idleTime = 0.0f;
    private float throwTimer = 0.0f;
    private bool moved;

    enum AnimationState
    {
        Standing = 0,
        Walking = 1,
        Gangsta = 2,
        Buddha = 3,
        Fire = 4,
        Drinking = 5,
        Water = 6,
        Throw = 7,
    }

	// Use this for initialization
	void Start ()
    {
        playerAnimator = playerObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        throwTimer += Time.deltaTime;
        idleTime += Time.deltaTime;
        moved = false;
        if (playerAnimator.GetInteger("State") == (int)AnimationState.Throw || 
            playerAnimator.GetInteger("State") == (int)AnimationState.Fire || 
            playerAnimator.GetInteger("State") == (int)AnimationState.Water ||
            playerAnimator.GetInteger("State") == (int)AnimationState.Gangsta ||
            throwTimer < 0.3f)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        handlePlayerInput();
	}

    void handlePlayerInput()
    {
        //Movement
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                idleTime = 0.0f;
                playerObject.GetComponent<Transform>().position += Vector3.forward * Time.deltaTime * movementSpeed;
                playerAnimator.SetInteger("State", (int)AnimationState.Walking);
                moved = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                idleTime = 0.0f;
                playerObject.GetComponent<Transform>().position += Vector3.back * Time.deltaTime * movementSpeed;
                playerAnimator.SetInteger("State", (int)AnimationState.Walking);
                moved = true;
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
                moved = true;
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
                moved = true;
            }
        }
        
        //Actions
        if (Input.GetKey(KeyCode.LeftControl) && throwTimer > 1.3f)
        {
            idleTime = 0.0f;
            throwTimer = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Throw);
            GameObject stone = (GameObject)Instantiate(stonePrefab, playerObject.transform.position - new Vector3(0.0f, 0.40f, -1.75f), Quaternion.identity);
            if (facingRight)
            {
                stone.GetComponent<StoneScript>().speed = new Vector3(1.0f, 0.5f, 0.0f);
            }
            else
            {
                stone.GetComponent<StoneScript>().speed = new Vector3(-1.0f, 0.5f, 0.0f);
            }
        }
        else if (Input.GetKey(KeyCode.Tab))
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Fire);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Buddha);
        }
        else if (Input.GetKey(KeyCode.Backspace))
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Water);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Gangsta);
        }
        //Idle
        else if (throwTimer > 0.25f && !moved)
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
