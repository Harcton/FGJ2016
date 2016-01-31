using UnityEngine;
using System.Collections;

public enum CharacterType
{
    Hermit,
    Minion,
}
public class MinionPathfinding : MonoBehaviour
{
    public CharacterType charType;
    public float conversion = 100;
    public Element element;

    public enum Element
    {
        Fire = 1,
        Water = 2,
        Earth = 3,
        Air = 4,
    }
   
    //timer randomizer
    float rng;

    //pathfinding towards player
    public float MaxDistanceToPlayer = 0;
    public float CircleRadius = 0;
    public float distanceToPlayer = 0;
    public float distanceToTarget = 0;
    float offset = 0.5f;

    public GameObject master = null;
    GameObject player = null;
    //GameObject enemyMaster = null;
  

    public float movementSpeed = 0.35f;
    float timer = 1;
    public float timerValue;

    //movement variables to move towards player
    Vector3 direction = new Vector3(0, 0, 0);
    Vector3 heading = new Vector3(0, 0, 0);
    Vector3 heading2 = new Vector3(0, 0, 0);
    Vector3 targetPos = new Vector3(0, 3.343f, 0);

    //random point on a circle
    public Vector3 lastRandomPoint = new Vector3(0, 0, 0);
    public Vector3 randomPoint = new Vector3(0, 0, 0);
    float angle; // posX, posY = 0.7f, posZ;

    //combat AI
    int hp = 2;
    GameObject target = null;
    //float timeBetweenAttacks = 1;
    //float nextAttackTime;
    // public float attackRange;
    CharacterType enemyType;
    bool inCombat = false;
    float attackCd;
    GameObject masterTarget;


    //Independent AI
    bool independenceInitialized = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        element = Element.Fire;
    }

    // Update is called once per frame
    void Update()
    {
        if (conversion <= 0)
        {
            master = player;
            UninitIndependence();
        }
        //minion movement
        if(master)
        {

            if (timer <= 0)
            {
                //find new target
                lastRandomPoint = randomPoint;
                randomPoint = Random.insideUnitCircle.normalized;
                randomPoint *= CircleRadius;

                //pelaajan ja edellisen pisteen välinen suunta   
                angle = Mathf.Atan2(lastRandomPoint.x - master.transform.position.x, lastRandomPoint.z - master.transform.position.z);
                angle += Random.Range(Mathf.PI, -Mathf.PI);

                targetPos.x = Mathf.Cos(angle) * CircleRadius + master.transform.position.x;
                targetPos.z = Mathf.Sin(angle) * CircleRadius + master.transform.position.z;

                rng = Random.Range(100, 0) * 0.001f;
                timer = timerValue + rng;
            }
            else
            {
                // rng = Random.Range(200,0) * 0.01f;
                rng = Random.Range(100, 0) * 0.001f;
                timer = timer - (Time.deltaTime + rng);
                if (timer < 0)
                {
                    timer = 0;
                }
            }

            //direction towards target
            heading = new Vector3(
                targetPos.x - transform.position.x,
               0,
               targetPos.z - transform.position.z);

            //distance to target
            distanceToTarget = DistanceToTarget(transform.position, randomPoint);

            //distance to player
            distanceToPlayer = DistanceToTarget(transform.position, master.transform.position);

            //direction towards target calculations end
            direction = heading / distanceToTarget;

            //check whether player has gotten out of range
            if (distanceToPlayer > MaxDistanceToPlayer)
            {
                lastRandomPoint = randomPoint;
                angle = Mathf.Atan2(lastRandomPoint.x - master.transform.position.x, lastRandomPoint.z - master.transform.position.z);
                angle += Random.Range(Mathf.PI, -Mathf.PI);

                targetPos.x = Mathf.Cos(angle) * CircleRadius + master.transform.position.x;
                targetPos.z = Mathf.Sin(angle) * CircleRadius + master.transform.position.z;
                heading = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);

                //direction towards target calculations end
                direction = heading / distanceToTarget;

            }

            //check if the target has been reached
            if (distanceToTarget < offset)
            {
                lastRandomPoint = randomPoint;
                angle = Mathf.Atan2(lastRandomPoint.x - master.transform.position.x, lastRandomPoint.z - master.transform.position.z);
                angle += Random.Range(Mathf.PI, -Mathf.PI);

                targetPos.x = Mathf.Cos(angle) * CircleRadius + master.transform.position.x;
                targetPos.z = Mathf.Sin(angle) * CircleRadius + master.transform.position.z;
                heading = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);

                //direction towards target calculations end
                direction = heading / distanceToTarget;
            }           
            transform.position = Vector3.Lerp(transform.position, targetPos, movementSpeed * Time.deltaTime);
        }
        else
        {
            InitIndependence();
            IndependentMovement();
        }
        //End of Minion movement



            /*
            * HEMIT ACTIONS
            */

        if(charType == CharacterType.Hermit)
        {           
            InitIndependence();
            IndependentMovement();
        }

        /*
        * COMBAT ACTIONS
        */
        attackCd -= Time.deltaTime;
        if(attackCd < 0)
        {
            attackCd = 0;
        }
  }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 3.343f, transform.position.z);
    }

        //independent movement


        //



     


    float DistanceToTarget(Vector3 Pos, Vector3 TargetPos)
    {
        float temp = Vector3.Magnitude(new Vector3(
                  Pos.x - TargetPos.x,
                  0,
                  Pos.z - TargetPos.z));

        return temp;
    }

    void OnDrawGizmos()
    {
        if(target)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(targetPos, new Vector3(targetPos.x, targetPos.y * 30, targetPos.z));
        }

        if(master)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(master.transform.position, new Vector3(master.transform.position.x, master.transform.position.y * 25, master.transform.position.z));
        }
  }




    void assingMaster(GameObject _master)
    {
        master = _master;


        lastRandomPoint = randomPoint;
        randomPoint = Random.insideUnitCircle.normalized;
        randomPoint *= CircleRadius;

        //pelaajan ja edellisen pisteen välinen suunta   
        angle = Mathf.Atan2(master.transform.position.x - lastRandomPoint.x, master.transform.position.z - lastRandomPoint.z);
        angle += Random.Range(Mathf.PI / 2, -Mathf.PI / 2);

        targetPos.x = Mathf.Cos(angle) * CircleRadius + master.transform.position.x;
        targetPos.z = Mathf.Sin(angle) * CircleRadius + master.transform.position.z;

        heading = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);

        //direction towards target calculations end
        direction = heading / distanceToTarget;
    }

    void assingType(CharacterType _charType)
    {
        charType = _charType;
    }

    void IndependentMovement()
    {
        if (timer <= 0)
        {
            //find new target
            lastRandomPoint = randomPoint;
            randomPoint = Random.insideUnitCircle;
            randomPoint *= CircleRadius * 5;

            //pelaajan ja edellisen pisteen välinen suunta   
            angle = Mathf.Atan2(lastRandomPoint.x - player.transform.position.x, lastRandomPoint.z - player.transform.position.z);
            angle += Random.Range(Mathf.PI, -Mathf.PI);

            targetPos.x = Mathf.Cos(angle) * CircleRadius + player.transform.position.x;
            targetPos.z = Mathf.Sin(angle) * CircleRadius + player.transform.position.z;

            rng = Random.Range(100, 0);
            timer = timerValue + rng + 10;
        }
        else
        {
            // rng = Random.Range(200,0) * 0.01f;
            rng = Random.Range(100, 0);
            timer = timer - (Time.deltaTime + rng * 0.01f);
            if (timer < 0)
            {
                timer = 0;
            }
        }

        //direction towards target
        heading = new Vector3(
            targetPos.x - transform.position.x,
           0,
           targetPos.z - transform.position.z);

        //distance to target
        distanceToTarget = DistanceToTarget(transform.position, randomPoint);

        //distance to player
        distanceToPlayer = DistanceToTarget(transform.position, player.transform.position);

        //direction towards target calculations end
        direction = heading / distanceToTarget;

        //check whether player has gotten out of range
        if (distanceToPlayer > MaxDistanceToPlayer)
        {
            lastRandomPoint = randomPoint;
            angle = Mathf.Atan2(lastRandomPoint.x - player.transform.position.x, lastRandomPoint.z - player.transform.position.z);
            angle += Random.Range(Mathf.PI, -Mathf.PI);

            targetPos.x = Mathf.Cos(angle) * CircleRadius + player.transform.position.x;
            targetPos.z = Mathf.Sin(angle) * CircleRadius + player.transform.position.z;
            heading = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);

            //direction towards target calculations end
            direction = heading / distanceToTarget;

        }

        //check if the target has been reached
        if (distanceToTarget < offset)
        {
            lastRandomPoint = randomPoint;
            angle = Mathf.Atan2(lastRandomPoint.x - player.transform.position.x, lastRandomPoint.z - player.transform.position.z);
            angle += Random.Range(Mathf.PI, -Mathf.PI);

            targetPos.x = Mathf.Cos(angle) * CircleRadius + player.transform.position.x;
            targetPos.z = Mathf.Sin(angle) * CircleRadius + player.transform.position.z;
            heading = new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);

            //direction towards target calculations end
            direction = heading / distanceToTarget;
        }
        
        transform.Translate(direction * Time.deltaTime * movementSpeed);
    }

    void InitIndependence()
    {
        if(!independenceInitialized)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            
            movementSpeed /= 4;
            MaxDistanceToPlayer *= 6;
            CircleRadius *= 6;
            independenceInitialized = true;
        }        
    }
    void UninitIndependence()
    {
        if (independenceInitialized)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            
            movementSpeed *= 4;
            MaxDistanceToPlayer /= 6;
            CircleRadius /= 6;
            independenceInitialized = false;
        }
    }

    //combat

    void OnTriggerEnter(Collider other)
    {
        //Hermit decisions
        if(charType == CharacterType.Hermit)
        {
            if(other.gameObject.tag == "Minion")
            {
                if(!other.gameObject.GetComponent<MinionPathfinding>().master)
                {
                   enemyType = other.gameObject.GetComponent<MinionPathfinding>().charType;

                    if(enemyType == CharacterType.Hermit)
                    {
                        //enter combat
                    }
                    else if(enemyType == CharacterType.Minion)
                    {
                        //covert
                    }
                }
                else
                {
                    target = other.gameObject.GetComponent<MinionPathfinding>().master;

                    //enter combat
                }
            }
            if(other.gameObject.tag == "Player")
            {
                target = other.gameObject;

                //enter combat
            }
           
        }

        //Minion actions
         if(charType == CharacterType.Minion)
        {
             if(other.gameObject.tag == "Minion")
             {
                 if(other.gameObject.GetComponent<MinionPathfinding>().master)
                 {
                     //join
                 }

                 else
                 {
                     enemyType = other.gameObject.GetComponent<MinionPathfinding>().charType;

                     if(enemyType == CharacterType.Hermit)
                     {
                         //join
                     }
                     //else do nothing
                 }
             }
           
             else if(other.gameObject.tag == "Player")
            {
               //join
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        
    }

    void OnTriggerExit(Collider other)
    {

    }

    void Attack()
    {
        if (inCombat)
        {
            if(attackCd <= 0)
            {
                if(master.tag == "Player")
                {
                    //masterTarget = master.GetComponent<>
                }
                
            }
        }
    }
}

