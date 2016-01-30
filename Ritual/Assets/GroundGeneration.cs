using UnityEngine;
using System.Collections;

public class GroundGeneration : MonoBehaviour
{
    //Public
    public int width;//Has to be uneven number
    public int height;//Has to be uneven number
    public GameObject plane;
    public GameObject[] planes;

    //Private
    int planeCount;
    GameObject player;
    Vector2 currentPosition;//Current tile of player

	// Use this for initialization
	void Start ()
    {
        planeCount = width * height;
        planes = new GameObject[planeCount];
        for (int w = 0; w < width; w++)
        for (int h = 0; h < height; h++)
        {
            planes[w * width + h] = Instantiate(plane,//Original
                new Vector3((w - Mathf.Floor((float)width / 2.0f)) * 10, 0, (h - Mathf.Floor((float)height / 2.0f)) * 10),//Position
                    Quaternion.identity) as GameObject;//Rotation
            planes[w * width + h].transform.parent = gameObject.transform;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (!player)
                return;
        }

        //Check current tile
        int currentX = (int)Mathf.Round(player.transform.position.x / 10.0f);
        int currentZ = (int)Mathf.Round(player.transform.position.z / 10.0f);

        if (Mathf.Abs(currentX - currentPosition.x) > 0.01f ||
            Mathf.Abs(currentZ - currentPosition.y) > 0.01f)
        {//Moved to another tile
            currentPosition.x = currentX;
            currentPosition.y = currentZ;

            //Reposition tiles
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                {
                    planes[w * width + h].transform.position = new Vector3((currentX + w - Mathf.Floor((float)width / 2.0f)) * 10, 0, (currentZ + h - Mathf.Floor((float)height / 2.0f)) * 10);
                }

            //Recreate tiles
            //for (int w = 0; w < width; w++)
            //    for (int h = 0; h < height; h++)
            //    {
            //        GameObject.Destroy(planes[w * width + h]);
            //        planes[w * width + h] = Instantiate(plane,//Original
            //            new Vector3((currentX + w - Mathf.Floor((float)width / 2.0f)) * 10, 0, (currentZ + h - Mathf.Floor((float)height / 2.0f)) * 10),//Position
            //                Quaternion.identity) as GameObject;//Rotation
            //        planes[w * width + h].transform.parent = gameObject.transform;
            //    }
        }
	}
}

