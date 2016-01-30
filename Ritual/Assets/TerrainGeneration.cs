using UnityEngine;
using System.Collections;

public class TerrainGeneration : MonoBehaviour
{
    public GameObject[] terrainPrefabs;
    public int terrainQuantity;//How many terrain pieces are being updated
    public float spawnMinRadius;
    public float spawnMaxRadius;
    public float despawnRange;
    GameObject[] terrain;
    GameObject player;

	// Use this for initialization
	void Start ()
    {
        //Init terrain
        terrain = new GameObject[terrainQuantity];
        for (int i = 0; i < terrainQuantity; i++)
        {
            float angle = (float)Random.Range(0, 359) * (3.14f / 180.0f);
            float rad = (float)Random.Range(1, (int)spawnMaxRadius);
            terrain[i] = Instantiate(terrainPrefabs[Random.Range(0,terrainPrefabs.Length)], new Vector3(Mathf.Cos(angle) * rad, 0, Mathf.Sin(angle) * rad), Quaternion.identity) as GameObject;
            terrain[i].transform.parent = gameObject.transform;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (!player)
                return;
        }
        
        float distance;
	    //Replace terrain that is too far away from the player
        for (int i = 0; i < terrainQuantity; i++)
        {
            distance = Vector3.Distance(player.transform.position, terrain[i].transform.position);
            if (distance > despawnRange)
            {
                GameObject.Destroy(terrain[i]);

                //Spawn new terrain in the free array slot
                float angle = (float)Random.Range(0, 359) * (3.14f / 180.0f);
                float rad = (float)Random.Range((int)spawnMinRadius, (int)spawnMaxRadius);
                terrain[i] = Instantiate(terrainPrefabs[Random.Range(0, terrainPrefabs.Length)], new Vector3(player.transform.position.x + Mathf.Cos(angle) * rad, 0, player.transform.position.z + Mathf.Sin(angle) * rad), Quaternion.identity) as GameObject;
                terrain[i].transform.parent = gameObject.transform;
                terrain[i].GetComponent<SpriteRenderer>().sortingLayerID = (int)terrain[i].transform.position.z * 10;
            }
        }

	}
}
