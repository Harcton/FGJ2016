using UnityEngine;
using System.Collections;

public class NPCGeneration : MonoBehaviour
{
    public GameObject minionPreFab;
    public GameObject hermitPreFab;
    public float spawnInterval;

    private float spawnTimer;

	void Start ()
    {
	    
	}
	
	void Update () 
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            spawnTimer = 0.0f;
            GameObject minion = (GameObject)Instantiate(minionPreFab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }
}
