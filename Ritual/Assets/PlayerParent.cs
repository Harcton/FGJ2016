using UnityEngine;
using System.Collections;

public class PlayerParent : MonoBehaviour
{
    public GameObject player;
	void Update ()
    {
        transform.position = player.transform.position;  	
	}
}
