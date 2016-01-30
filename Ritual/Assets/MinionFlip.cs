using UnityEngine;
using System.Collections;

public class MinionFlip : MonoBehaviour
{
    private float lastPositionX;
    private bool facingRight;

    void Start()
    {
        facingRight = false;
        lastPositionX = transform.position.x;
    }

    void Update()
    {
        if (lastPositionX > transform.position.x) //Right
        {
            if (!facingRight)
            {
                Flip();
            }
            facingRight = true;
        }
        else //Left
        {
            if (facingRight)
            {
                Flip();
            }
            facingRight = false;
        }
    }

	void LateUpdate ()
    {
        lastPositionX = transform.position.x;
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
