using UnityEngine;
using System.Collections;

public class StoneScript : MonoBehaviour
{
    public float rotationSpeed = 500.0f;
    public Vector3 speed;

    private float acceleration = 0.0f;
    private bool active;
    private float destroyTimer;

    void Start()
    {
        active = true;
        destroyTimer = 0.0f;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "TreeCollider")
        {
            speed = new Vector3(-speed.x, speed.y);
            Destroy(gameObject.GetComponent<Collider>());
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (active)
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * rotationSpeed));
            transform.position += Vector3.down * Time.deltaTime * acceleration;
            transform.position += speed * Time.deltaTime * ((Random.value * 20.0f) + 10.0f);
            if (transform.position.y < 0.15f)
            {
                Destroy(gameObject.GetComponent<Collider>());
                active = false;
            }
            acceleration += 42.0f * Time.deltaTime;
        }
        else
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer > 20.0f)
            {
                Destroy(gameObject);
            }
        }
	}
}
