using UnityEngine;
using System.Collections;

public class NPCGeneration : MonoBehaviour
{
    GameObject player;
    public GameObject minionPreFab;
    public GameObject hermitPreFab;
    public float spawnInterval;

    private float spawnTimer;

	void Start ()
    {
        player = GameObject.FindWithTag("Player");
	}
	
	void Update () 
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0.0f;
            spawnInterval = 30.0f / Mathf.Sqrt(player.transform.position.x * player.transform.position.x + player.transform.position.z * player.transform.position.z);
            if (spawnInterval > 10.0f)
                spawnInterval = 10.0f;
            else if (spawnInterval < 10.0f)
                spawnInterval = 0.5f;

            float rad = Random.Range(100, 1000) / 10.0f;
            float angle = (float)Random.Range(0, 359) * (float)(3.14f / 180.0f);
            GameObject minion = (GameObject)Instantiate(minionPreFab, new Vector3(Mathf.Cos(angle)*rad, 0.0f, Mathf.Sin(angle)*rad), Quaternion.identity);
        }
    }
}
