using UnityEngine;
using System.Collections;

public class StoneScript : MonoBehaviour
{
    public float rotationSpeed = 500.0f;
    public Vector3 speed;

    private float acceleration = 0.0f;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Tree")
        {
            speed = new Vector3(-speed.x, speed.y);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * rotationSpeed));
        transform.position += Vector3.down * Time.deltaTime * acceleration;
        transform.position += speed * Time.deltaTime * ((Random.value * 20.0f) + 10.0f);
        if (transform.position.y < 0.1f)
        {
            Destroy(this);
        }
        acceleration += 42.0f * Time.deltaTime;
	}
}
