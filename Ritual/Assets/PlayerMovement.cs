using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //Player Movements & Actions

    public GameObject playerObject;
    public GameObject stonePrefab;

    public int waterAllies;
    public int fireAllies;
    public int earthAllies;
    public int airAllies;

    public bool facingRight = false;

    private float movementSpeed;
    private Animator playerAnimator;
    private bool canMove = true;
    private float idleTime = 0.0f;
    private float throwTimer = 0.0f;

    private bool moved; //Has player moved this frame
    private ActionState actionState;

    private Collider[] minions;
    private float collisionTimer;

    enum ActionState
    {
        NoAction = 0,
        Fire = 1,
        Water = 2,
        Earth = 3,
        Air = 4,
        Throw = 5,

        Gangsta = 6,
    }

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
        collisionTimer = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        throwTimer += Time.deltaTime;
        idleTime += Time.deltaTime;
        moved = false;

        //Check if player can move:
        if (actionState == ActionState.Throw || 
            actionState == ActionState.Fire || 
            actionState == ActionState.Water ||
            actionState == ActionState.Gangsta ||
            throwTimer < 0.3f)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        //Conversions();

        if (actionState != ActionState.Air) //When not flyin'
            movementSpeed = 12.0f;
        else
            movementSpeed = 25.0f;
	}

    void FixedUpdate()
    {
        handlePlayerInput();
        handleStates();
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
        if (Input.GetKey(KeyCode.RightControl) && throwTimer > 0.3f && canMove)
        {
            actionState = ActionState.Throw;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            actionState = ActionState.Fire;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            actionState = ActionState.Air;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            actionState = ActionState.Water;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            actionState = ActionState.Gangsta;
        }
        else if (throwTimer > 0.25f)
        {
            actionState = ActionState.NoAction;
        }
    }

    void handleStates()
    {
        if (actionState == ActionState.Throw && throwTimer > 0.3f && canMove)
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
        else if (actionState == ActionState.Fire)
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Fire);
        }
        else if (actionState == ActionState.Air)
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Buddha);
        }
        else if (actionState == ActionState.Water)
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Water);
        }
        else if (actionState == ActionState.Gangsta)
        {
            idleTime = 0.0f;
            playerAnimator.SetInteger("State", (int)AnimationState.Gangsta);
        }
        //Idle
        else if (actionState == ActionState.NoAction && !moved)
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

    void Conversions()
    {
        collisionTimer += Time.deltaTime;
        if (collisionTimer > 1.0f)
        {
            minions = getMinions();
            collisionTimer = 0.0f;
        }

        MinionPathfinding MS;
        for (int i = 0; i < minions.Length; i++)
        {
            MS = minions[i].gameObject.GetComponent<MinionPathfinding>();
            if (MS.charType == CharacterType.Minion)
            {
                switch (actionState)
                {
                    case ActionState.Fire:
                        {
                            if ((int)MS.element == (int)actionState)
                            {
                                MS.conversion -= (2 * fireAllies + 4) * Time.deltaTime;
                            }
                            else if ((int)MS.element == (int)ActionState.Earth || (int)MS.element == (int)ActionState.Air)
                            {
                                MS.conversion -= (1 * fireAllies + 4) * Time.deltaTime;
                            }
                            break;
                        }
                    case ActionState.Water:
                        {
                            if ((int)MS.element == (int)actionState)
                            {
                                MS.conversion -= (2 * waterAllies + 4) * Time.deltaTime;
                            }
                            else if ((int)MS.element == (int)ActionState.Earth || (int)MS.element == (int)ActionState.Air)
                            {
                                MS.conversion -= (1 * waterAllies + 4) * Time.deltaTime;
                            }
                            break;
                        }
                    case ActionState.Earth:
                        {
                            if ((int)MS.element == (int)actionState)
                            {
                                MS.conversion -= (2 * earthAllies + 4) * Time.deltaTime;
                            }
                            else if ((int)MS.element == (int)ActionState.Fire || (int)MS.element == (int)ActionState.Water)
                            {
                                MS.conversion -= (1 * earthAllies + 4) * Time.deltaTime;
                            }
                            break;
                        }
                    case ActionState.Air:
                        {
                            if ((int)MS.element == (int)actionState)
                            {
                                MS.conversion -= (2 * airAllies + 4) * Time.deltaTime;
                            }
                            else if ((int)MS.element == (int)ActionState.Fire || (int)MS.element == (int)ActionState.Water)
                            {
                                MS.conversion -= (1 * airAllies + 4) * Time.deltaTime;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }

    Collider[] getMinions()
    {
        return Physics.OverlapSphere(transform.position, 8.0f, 1 << 8);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 scale = playerObject.GetComponent<Transform>().localScale;
        scale.x *= -1;
        playerObject.GetComponent<Transform>().localScale = scale;
    }
}
